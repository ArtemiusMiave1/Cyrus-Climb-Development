// original by asteins
// adapted by @torahhorse
// http://wiki.unity3d.com/index.php/SmoothMouseLook

// Instructions:
// There should be one MouseLook script on the Player itself, and another on the camera
// player's MouseLook should use MouseX, camera's MouseLook should use MouseY

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour
{
 
	public enum RotationAxes { MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseX;
	public bool invertY = false;
	
	public float sensitivityX = 10F;
	public float sensitivityY = 9F;
 
	public float minimumX = -360F;
	public float maximumX = 360F;
 
	public float minimumY = -85F;
	public float maximumY = 85F;
 
	float rotationX = 0F;
	float rotationY = 0F;
 
	private List<float> rotArrayX = new List<float>();
	float rotAverageX = 0F;	
 
	private List<float> rotArrayY = new List<float>();
	float rotAverageY = 0F;
 
	public float framesOfSmoothing = 5;
 
	Quaternion originalRotation;


    // 玩家移动旋转相关变量
    public float rotationSmoothness = 5f;
    private float targetPlayerRotationY = 0f;
    private float currentPlayerRotationY = 0f;

    // 摄像头引用
    public Transform mainCamera;
    //private Quaternion initialWorldRotation;
    //private Vector3 initialLocalPosition;

    Quaternion originalCameraRotation;


    void Start ()
	{			
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		
		originalRotation = transform.localRotation;

        // 获取或设置主摄像头
        if (mainCamera == null)
        {
            mainCamera = transform.Find("Main Camera (1)");
            if (mainCamera == null)
            {
                Camera cam = GetComponentInChildren<Camera>();
                if (cam != null) mainCamera = cam.transform;
            }
        }

        if (mainCamera != null)
        {
            originalCameraRotation = mainCamera.localRotation;
            /*// 保存摄像头的初始世界旋转和本地位置
            initialWorldRotation = mainCamera.rotation;
            initialLocalPosition = mainCamera.localPosition;*/
        }
    }
 
	void Update ()
	{
        HandleMovementRotation();
        ApplyFinalRotation();
        KeepCameraUpright();
        //UpdateCameraRotation();

        if (axes == RotationAxes.MouseX)
		{			
			rotAverageX = 0f;

            //rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.timeScale;
            float mouseXInput = Input.GetAxis("Mouse X") * sensitivityX * Time.timeScale;

            // 检查是否已达到限制，只有在未达到限制时才累积输入
            if (!IsAtXLimit(mouseXInput))
            {
                rotationX += mouseXInput;
            }

            rotArrayX.Add(rotationX);
 
			if (rotArrayX.Count >= framesOfSmoothing)
			{
				rotArrayX.RemoveAt(0);
			}
			for(int i = 0; i < rotArrayX.Count; i++)
			{
				rotAverageX += rotArrayX[i];
			}
			rotAverageX /= rotArrayX.Count;
			rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
 
			/*Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;*/			
		}
		else
		{			
			rotAverageY = 0f;
 
 			float invertFlag = 1f;
 			if( invertY )
 			{
 				invertFlag = -1f;
 			}
            //rotationY += Input.GetAxis("Mouse Y") * sensitivityY * invertFlag * Time.timeScale;
            float mouseYInput = Input.GetAxis("Mouse Y") * sensitivityY * invertFlag * Time.timeScale;

            // 检查是否已达到限制，只有在未达到限制时才累积输入
            if (!IsAtYLimit(mouseYInput))
            {
                rotationY += mouseYInput;
            }

            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
 	
			rotArrayY.Add(rotationY);
 
			if (rotArrayY.Count >= framesOfSmoothing)
			{
				rotArrayY.RemoveAt(0);
			}
			for(int j = 0; j < rotArrayY.Count; j++)
			{
				rotAverageY += rotArrayY[j];
			}
			rotAverageY /= rotArrayY.Count;
 
			/*Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
			transform.localRotation = originalRotation * yQuaternion;*/
		}
	}
	
	public void SetSensitivity(float s)
	{
		sensitivityX = s;
		sensitivityY = s;
	}
 
	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}

    // 检查X轴是否已达到限制
    private bool IsAtXLimit(float newInput)
    {
        float projectedRotation = rotationX + newInput;
        float clampedRotation = ClampAngle(projectedRotation, minimumX, maximumX);

        // 如果投影的旋转值被限制，说明已达到边界
        return Mathf.Abs(projectedRotation - clampedRotation) > 0.001f;
    }

    // 检查Y轴是否已达到限制
    private bool IsAtYLimit(float newInput)
    {
        float projectedRotation = rotationY + newInput;
        float clampedRotation = Mathf.Clamp(projectedRotation, minimumY, maximumY);

        // 如果投影的旋转值被限制，说明已达到边界
        return Mathf.Abs(projectedRotation - clampedRotation) > 0.001f;
    }


    /*void HandleMovementRotation()
    {
        // 获取WASD输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 根据WASD输入计算目标旋转角度
        if (vertical > 0) // W键 - 向前
        {
            targetPlayerRotationY = 0f;
        }
        else if (vertical < 0) // S键 - 向后
        {
            targetPlayerRotationY = 180f;
        }

        if (horizontal > 0) // D键 - 向右
        {
            if (vertical > 0) // W + D = 45度
                targetPlayerRotationY = 45f;
            else if (vertical < 0) // S + D = 135度
                targetPlayerRotationY = 135f;
            else // 仅D键 = 90度
                targetPlayerRotationY = 90f;
        }
        else if (horizontal < 0) // A键 - 向左
        {
            if (vertical > 0) // W + A = -45度
                targetPlayerRotationY = -45f;
            else if (vertical < 0) // S + A = -135度
                targetPlayerRotationY = -135f;
            else // 仅A键 = -90度
                targetPlayerRotationY = -90f;
        }

        // 平滑过渡到目标旋转
        currentPlayerRotationY = Mathf.LerpAngle(currentPlayerRotationY, targetPlayerRotationY, rotationSmoothness * Time.deltaTime);
    }

    void ApplyFinalRotation()
    {
        // 应用鼠标视角旋转（X和Y轴）
        Quaternion mouseRotation = Quaternion.identity;

        if (axes == RotationAxes.MouseX)
        {
            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
            mouseRotation = originalRotation * xQuaternion;
        }
        else
        {
            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
            mouseRotation = originalRotation * yQuaternion;
        }

        // 应用玩家Y轴旋转（来自WASD）
        Quaternion playerYRotation = Quaternion.Euler(0f, currentPlayerRotationY, 0f);

        // 组合旋转：先应用玩家朝向，再应用视角旋转
        transform.localRotation = playerYRotation * mouseRotation;
    }*/

    void HandleMovementRotation()
    {
        // 获取WASD输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 根据WASD输入计算目标旋转角度
        if (vertical > 0) // W键 - 向前
        {
            if (horizontal > 0) // W + D = 45度
                targetPlayerRotationY = 45f;
            else if (horizontal < 0) // W + A = -45度
                targetPlayerRotationY = -45f;
            else // 仅W键 = 0度
                targetPlayerRotationY = 0f;
        }
        else if (vertical < 0) // S键 - 向后
        {
            if (horizontal > 0) // S + D = 135度
                targetPlayerRotationY = 135f;
            else if (horizontal < 0) // S + A = -135度
                targetPlayerRotationY = -135f;
            else // 仅S键 = 180度
                targetPlayerRotationY = 180f;
        }
        else // 没有前后输入
        {
            if (horizontal > 0) // 仅D键 = 90度
                targetPlayerRotationY = 90f;
            else if (horizontal < 0) // 仅A键 = -90度
                targetPlayerRotationY = -90f;
            // 没有输入时保持当前旋转
        }

        // 平滑过渡到目标旋转
        currentPlayerRotationY = Mathf.LerpAngle(currentPlayerRotationY, targetPlayerRotationY, rotationSmoothness * Time.deltaTime);
    }

    void ApplyFinalRotation()
    {
        // 只旋转玩家本体（Y轴）
        transform.localRotation = Quaternion.Euler(0f, currentPlayerRotationY, 0f);
    }

    void KeepCameraUpright()
    {
        if (mainCamera != null)
        {
            // 保持摄像头的本地旋转为初始值（保持0旋转）
            mainCamera.localRotation = originalCameraRotation;

            // 应用鼠标视角旋转到摄像头上
            if (axes == RotationAxes.MouseX)
            {
                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                mainCamera.localRotation *= xQuaternion;
            }
            else
            {
                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                mainCamera.localRotation *= yQuaternion;
            }
        }
    }
}