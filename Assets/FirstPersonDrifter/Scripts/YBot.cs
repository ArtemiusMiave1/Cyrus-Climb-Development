using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBot : MonoBehaviour
{
    [Header("Components")]
    Animator animator;
    public AudioSource primaryAudioSource;
    public AudioSource footstepAudioSource;
    public ParticleSystem footstepFX;
    public ParticleSystem climbingFX;
    public ParticleSystem jumpingFX;
    public FirstPersonDrifter FirstPersonDrifterRefrence;

    [Header("Settings")]
    public AudioClip[] footstepSounds;
    public AudioClip[] climbingSounds;
    public AudioClip[] landingSounds;
    public AudioClip[] jumpingSounds;
    readonly string animParam_Walking = "walking";
    public bool visualize = false;
    public bool previouslyGrounded;

   

    private void Awake()
    {
        animator = GetComponent<Animator>();
        primaryAudioSource = GetComponent<AudioSource>();
        if (FirstPersonDrifterRefrence.grounded)
        {
            Debug.Log("myBoolean in ScriptA is true!");
        }
    }


    public void Update()
    {
        if (!previouslyGrounded && FirstPersonDrifterRefrence.grounded == true)
        {
            PlayLandingSound();

            previouslyGrounded = true;
        }

        if (previouslyGrounded && FirstPersonDrifterRefrence.grounded == false && animator.GetBool("isClimbing") == false || Input.GetButtonDown("Jump") && animator.GetBool("isClimbing") == true)
        {
            PlayJumpingSound();
            previouslyGrounded = false;
        }
    }

    public void PlayLandingSound()
    {

        int random = Random.Range(0, landingSounds.Length);
        var clip = landingSounds[random];
        primaryAudioSource.PlayOneShot(clip);

        jumpingFX.Emit(10);

    }

    public void PlayJumpingSound()
    {

        int random = Random.Range(0, jumpingSounds.Length);
        var clip = jumpingSounds[random];
        primaryAudioSource.PlayOneShot(clip);

        jumpingFX.Emit(5);

    }

    public void Footstep()
    {
        if (FirstPersonDrifterRefrence.grounded == true)
            {
            if (animator.GetBool("isRunning") == true && animator.GetBool("isClimbing") == false)
            {
                int random = Random.Range(0, footstepSounds.Length);
                var clip = footstepSounds[random];
                footstepAudioSource.PlayOneShot(clip);
            }
        }

        if (visualize)
        {
        }
    }

    public void Climbing()
    {
        if (animator.GetBool("isRunning") == true && animator.GetBool("isClimbing") == true)
        {
            int random = Random.Range(0, climbingSounds.Length);
            var clip = climbingSounds[random];
            primaryAudioSource.PlayOneShot(clip);
        }

        
    }

    public void Particle()
    {
        if (animator.GetBool("isClimbing") == false && FirstPersonDrifterRefrence.grounded == true)
        {
            footstepFX.Emit(1);
        }
        if (animator.GetBool("isClimbing") == true && FirstPersonDrifterRefrence.grounded == false)
        {
            climbingFX.Emit(2);
        }
    }
}
