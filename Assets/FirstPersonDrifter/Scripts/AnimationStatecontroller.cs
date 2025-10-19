using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatecontroller : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 1f;
    public float deceleration = 3f;
    int VelocityHash;

    public float running;
    public float climbing;
    public float jump;

    void Start()
    {
        animator = GetComponent<Animator>();

        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {

        if (animator.GetBool("isRunning") == true && velocity < running)
        {
            velocity += Time.deltaTime * acceleration;
        }

        if (animator.GetBool("isClimbing") == true && velocity < climbing)
        {
            velocity += Time.deltaTime * (acceleration * 10); 
        }

        if (animator.GetBool("isClimbing") == false && velocity > running + .05)
        {
            velocity -= Time.deltaTime * (deceleration * 10);
        }
        

        if (animator.GetBool("isRunning") == false && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }

        if (animator.GetBool("isRunning") == false && velocity <0.0f)
        {
            velocity = 0.0f;
        }




        animator.SetFloat(VelocityHash, velocity);
    }
}
