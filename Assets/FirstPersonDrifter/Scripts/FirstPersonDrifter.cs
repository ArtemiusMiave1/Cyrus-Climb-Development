// original by Eric Haines (Eric5h5)
// adapted by @torahhorse
// http://wiki.unity3d.com/index.php/FPSWalkerEnhanced

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class FirstPersonDrifter: MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
 
    // If true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
    private bool limitDiagonalSpeed = true;
 
    public bool enableRunning = true;
 
    public float jumpSpeed = 4.0f;
    public float gravity = 10.0f;
 
    // Units that player can fall before a falling damage function is run. To disable, type "infinity" in the inspector
    private float fallingDamageThreshold = 10.0f;
 
    // If the player ends up on a slope which is at least the Slope Limit as set on the character controller, then he will slide down
    public bool slideWhenOverSlopeLimit = false;
 
    // If checked and the player is on an object tagged "Slide", he will slide down it regardless of the slope limit
    public bool slideOnTaggedObjects = false;
 
    public float slideSpeed = 5.0f;
 
    // If checked, then the player can change direction while in the air
    public bool airControl = true;
 
    // Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
    public float antiBumpFactor = .75f;
 
    // Player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
    public int antiBunnyHopFactor = 1;
 
    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private CharacterController controller;
    private Transform myTransform;
    private float speed;
    private RaycastHit hit;
    private float fallStartLevel;
    private bool falling;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private bool playerControl = false;
    private int jumpTimer;

    public float climbSpeed = 3.0f;     //攀爬速度
    private bool isClimbing = false;    //攀爬状态
    private Collider climbingSurface;   //当前攀爬的表面

    [Header("Wall Backward Jump")]
    public float backStepDistance = 1.5f;  // 后退距离
    public float backStepDuration = 0.3f;  // 后退时间
    public float wallJumpUpForce = 5f;      // 向上跳的力量
    public float wallJumpDuration = 0.3f;   // 力量作用时间

    private bool isWallJumping = false;
    private float wallJumpTimer = 0f;
    private Vector3 wallJumpDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myTransform = transform;
        speed = walkSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        slideLimit = controller.slopeLimit - .1f;
        jumpTimer = antiBunnyHopFactor;
    }

    void Update()
    {
        // 检测是否按下攀爬键（例如空格键）来退出攀爬模式
        if (isClimbing && Input.GetButtonDown("Jump"))
        {
            ManualExitClimbingMode();
        }
    }

    void FixedUpdate() 
    {
        if (isClimbing)
        {
            /*// 在攀爬表面上移动
            Vector3 move = new Vector3(inputX, inputY, 0) * climbSpeed;

            // 将移动方向转换到攀爬表面的局部空间
            Vector3 surfaceRight = climbingSurface.transform.right;
            Vector3 surfaceUp = climbingSurface.transform.up;

            // 计算实际移动方向
            Vector3 worldMove = surfaceRight * move.x + surfaceUp * move.y;

            // 应用移动
            controller.Move(worldMove * Time.deltaTime);

            // 在攀爬时禁用重力
            moveDirection.y = 0;*/

            // 1. 直接使用墙面坐标系
            float climbX = Input.GetAxis("Horizontal");
            float climbY = Input.GetAxis("Vertical");

            // 2. 计算墙面坐标系移动
            Vector3 move = climbingSurface.transform.right * climbX
                         + climbingSurface.transform.up * climbY;

            // 2. 标准化并应用速度
            if (move.magnitude > 1) move.Normalize();
            move *= climbSpeed;

            // 3. 禁用所有物理影响
            moveDirection = Vector3.zero;
            controller.Move(move * Time.deltaTime);

            return;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;

        if (grounded) {
            bool sliding = false;
            // See if surface immediately below should be slid down. We use this normally rather than a ControllerColliderHit point,
            // because that interferes with step climbing amongst other annoyances
            if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance)) {
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }
            // However, just raycasting straight down from the center can fail when on steep slopes
            // So if the above raycast didn't catch anything, raycast down from the stored ControllerColliderHit point instead
            else {
                Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }
 
            // If we were falling, and we fell a vertical distance greater than the threshold, run a falling damage routine
            if (falling) {
                falling = false;
                if (myTransform.position.y < fallStartLevel - fallingDamageThreshold)
                    FallingDamageAlert (fallStartLevel - myTransform.position.y);
            }
 
            if( enableRunning )
            {
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
                {
                    speed = runSpeed;  // Set to run speed
                }
                else
                {
                    speed = walkSpeed;  // Set to walk speed if not running
                }
            }
 
            // If sliding (and it's allowed), or if we're on an object tagged "Slide", get a vector pointing down the slope we're on
            if ( (sliding && slideWhenOverSlopeLimit) || (slideOnTaggedObjects && hit.collider.tag == "Slide") ) {
                Vector3 hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize (ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
                playerControl = false;
            }
            // Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
            else {
                moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
                moveDirection = myTransform.TransformDirection(moveDirection) * speed;
                playerControl = true;
            }
 
            // Jump! But only if the jump button has been released and player has been grounded for a given number of frames
            if (!Input.GetButton("Jump"))
                jumpTimer++;
            else if (jumpTimer >= antiBunnyHopFactor) {
                moveDirection.y = jumpSpeed;
                jumpTimer = 0;
            }
        }
        else {
            // If we stepped over a cliff or something, set the height at which we started falling
            if (!falling) {
                falling = true;
                fallStartLevel = myTransform.position.y;
            }

            // If air control is allowed, check movement but don't touch the y component
            if (airControl && playerControl)
            {
                moveDirection.x = inputX * speed * inputModifyFactor;
                moveDirection.z = inputY * speed * inputModifyFactor;
                moveDirection = myTransform.TransformDirection(moveDirection);
            }
        }
 
        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
 
        // Move the controller, and set grounded true or false depending on whether we're standing on something
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
    }
 
    // Store point that we're in contact with for use in FixedUpdate if needed
    void OnControllerColliderHit (ControllerColliderHit hit) {
        contactPoint = hit.point;
    }
 
    // If falling damage occured, this is the place to do something about it. You can make the player
    // have hitpoints and remove some of them based on the distance fallen, add sound effects, etc.
    void FallingDamageAlert (float fallDistance)
    {
        //print ("Ouch! Fell " + fallDistance + " units!");   
    }

    void OnTriggerEnter(Collider other)
    {
        // 当进入触发器时进入攀爬模式
        if (!isClimbing && other.isTrigger && !other.CompareTag("Collectable"))
        {
            EnterClimbingMode(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 当离开触发器时退出攀爬模式
        if (isClimbing && other == climbingSurface)
        {
            ExitClimbingMode();
        }
    }

    private void EnterClimbingMode(Collider surface)
    {
        isClimbing = true;
        climbingSurface = surface;

        // 可以在这里添加其他进入攀爬模式的效果
        Debug.Log("Entered climbing mode");
    }

    private void ExitClimbingMode()
    {
        isClimbing = false;
        climbingSurface = null;

        // 可以在这里添加其他退出攀爬模式的效果
        Debug.Log("Exited climbing mode");
    }

    /*private void ManualExitClimbingMode()
    {
        *//*if (climbingSurface == null) return;

        // 先保存墙面法线
        Vector3 wallNormal = climbingSurface.transform.forward;

        isClimbing = false;
        grounded = false;

        // 施加反向力（使用墙面法线方向）
        moveDirection = -wallNormal * climbExitForce;

        *//*// 2. 施加明确的向后退力（Z轴负方向）
        moveDirection = transform.TransformDirection(Vector3.back) * climbExitForce;
        moveDirection.y = 2f;  // 添加少许向上的力*//*

        // 3. 立即应用一次移动（关键步骤！）
        controller.Move(moveDirection * Time.deltaTime);

        // 4. 清除引用
        climbingSurface = null;*//*

        if (climbingSurface == null) return;

        // 1. 计算蹬跳方向（向后+向上）
        Vector3 backDirection = -climbingSurface.transform.forward;
        wallJumpDirection = (backDirection + Vector3.up).normalized;

        *//*// 2. 分开计算水平和垂直力（避免归一化削弱后退力）
        moveDirection.x = backDirection.x * wallJumpBackForce;
        moveDirection.z = backDirection.z * wallJumpBackForce;
        moveDirection.y = wallJumpUpForce;*//*

        // 3. 设置跳跃状态
        isWallJumping = true;
        wallJumpTimer = wallJumpDuration;
        isClimbing = false;
        grounded = false;

        // 4. 清除引用
        climbingSurface = null;

        // 可以在这里添加其他退出攀爬模式的效果
        Debug.Log("Manually exited climbing mode");
    }*/

    private void ManualExitClimbingMode()
    {
        if (climbingSurface == null) return;

        // 1. 计算后退目标位置（世界空间Z轴负方向）
        Vector3 backStep = transform.position + Vector3.back * backStepDistance;

        // 2. 施加瞬时向上力
        moveDirection.y = wallJumpUpForce;
        grounded = false;

        // 3. 启动后退协程
        StartCoroutine(PerformBackStep(backStep));

        // 4. 清除状态
        isClimbing = false;
        climbingSurface = null;
    }

    private IEnumerator PerformBackStep(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        // 记录初始Y速度（保留物理效果）
        float initialYVelocity = moveDirection.y;

        while (elapsed < backStepDuration)
        {
            // 计算水平插值
            float t = elapsed / backStepDuration;
            Vector3 newPos = Vector3.Lerp(startPos, targetPos, t);

            // 保持物理计算的Y轴位置
            newPos.y = transform.position.y;

            // 使用CharacterController移动（关键修改！）
            controller.Move(newPos - transform.position);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 最终位置同步
        Vector3 finalPos = targetPos;
        finalPos.y = transform.position.y;
        controller.Move(finalPos - transform.position);
    }
}