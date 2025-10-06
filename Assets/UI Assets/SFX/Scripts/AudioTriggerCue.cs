using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerCue : MonoBehaviour
{
    [SerializeField] private AudioClip[] AudioClips;
    public float Volume = 1f;
    private bool Played;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Played == false)
        {
            //Play sound FX
           // SoundFXManager.Instance.PlaySoundFXClip(NarrativeFound, transform, 1f);
            SoundFXManager.Instance.PlayRandomSoundFXClip(AudioClips, transform, Volume);


            Debug.Log("Player entered an Audio Cue");
            Debug.Log("Player collided with----" + gameObject.name);
            Played = true;
           
            
            
        }
    }
}
