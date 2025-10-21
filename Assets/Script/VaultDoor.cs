using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VaultDoor : MonoBehaviour
{

    [Header("Animation")]
    public Animator Amulet;
    public int delayTime;
    public Animator LeftDoor;
    public Animator RightDoor;
    public bool Door_Active;
    public bool InTheZone;
    public bool Triggered;
    public static bool InZone;
    public int collectableTotal;
    public int currentColelctables;

    public static int collectableCount;

    [Header("Camera Shake")]
    public GameObject Camera;
    public bool shakeStart = false;
    public AnimationCurve shakeCurve;
    public float shakeDuration = 1f;

    [Header("Audio")]
    [SerializeField] private AudioClip[] AudioClips;
    public float Volume = 1f;
    private bool Played;


    [Header("UI")]
    public GameObject Enough;
    public GameObject notEnough;
    private bool menuActivated;
    // Start is called before the first frame update



    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Triggered == false)
        {
            if (InTheZone && currentColelctables == collectableTotal)
            {
                Enough.SetActive(true);
                notEnough.SetActive(false);
            }

            if (!InTheZone && currentColelctables == collectableTotal)
            {
                Enough.SetActive(false);
                notEnough.SetActive(false);
            }

            if (InTheZone && currentColelctables != collectableTotal)
            {
                notEnough.SetActive(true);
            }
            if (!InTheZone && currentColelctables != collectableTotal)
            {
                notEnough.SetActive(false);
            }
        }



            if (shakeStart)
        {
            shakeStart = false;
            StartCoroutine(Shaking());
        }

        currentColelctables = collectableCount;
        InTheZone = InZone;


        if (collectableCount  == collectableTotal)
        {
            Door_Active = true;
        }





        if (Door_Active == true && InZone == true && Triggered == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Amulet.SetBool("Door_Activated", true);
                Invoke("DelayedAnimation", delayTime);
                Triggered = true;

            }
        }
    }

    void DelayedAnimation()
    {

        LeftDoor.SetBool("Door_Activated", true);
        RightDoor.SetBool("Door_Activated", true);

        StartCoroutine(Shaking());
        if (Played == false)
        {
            SoundFXManager.Instance.PlayRandomSoundFXClip(AudioClips, transform, Volume);
            Played = true;
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = Camera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            startPosition = Camera.transform.position;
            elapsedTime += Time.deltaTime;
            float strength = shakeCurve.Evaluate(elapsedTime/shakeDuration);  
            Camera.transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        Camera.transform.position = startPosition;
    }
}
