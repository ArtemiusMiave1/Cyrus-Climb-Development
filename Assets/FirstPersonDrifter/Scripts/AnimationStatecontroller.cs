using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatecontroller : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 1f;
    public float deceleration = 10f;
    int VelocityHash;
    void Start()
    {
        animator = GetComponent<Animator>();

        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal"); // A/D or left stick X
        //float vertical = Input.GetAxis("Vertical");     // W/S or left stick Y
        //if (animator.GetBool("isClimbing") == true)
        //{
        //    // up
        //    if (horizontal > 0 && velocity > 0)
        //    {
        //        velocity += Time.deltaTime * acceleration;
        //        animator.speed = 1.0f;
        //    }
        //    if (horizontal < 0 && velocity > 0)
        //    {
        //        velocity += Time.deltaTime * acceleration;
        //        animator.speed = -2.0f;
        //    }
        //    if (vertical > 0 && velocity < 0.5f)
        //    {
        //        velocity += Time.deltaTime * acceleration;
        //        animator.speed = 1.0f;
        //    }
        //    if (vertical < 0 && velocity < 0.5f)
        //    {
        //        velocity += Time.deltaTime * acceleration;
        //        animator.speed = -2.0f;
        //    }
        //}

            // down


            // right


            // left



            //if (animator.GetBool("isRunning") == true && velocity < 0.15f)
            //{
            //    velocity += Time.deltaTime * acceleration;
            //}

            //if (animator.GetBool("isClimbing") == true && velocity < 1f)
            //{
            //    velocity += Time.deltaTime * (acceleration * 10); 
            //}

            //if (animator.GetBool("isClimbing") == false && velocity > 0.155f)
            //{
            //    velocity -= Time.deltaTime * (deceleration * 10);
            //}
            //if (animator.GetBool("jump") == true && velocity >= 0)
            //{

            //}

            //if (animator.GetBool("isRunning") == false && velocity > 0.0f)
            //{
            //    velocity -= Time.deltaTime * deceleration;
            //}

            //if (animator.GetBool("isRunning") == false && velocity <0.0f)
            //{
            //    velocity = 0.0f;
            //}






            animator.SetFloat(VelocityHash, velocity);
    }
}
