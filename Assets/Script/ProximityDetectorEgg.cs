using UnityEngine;
using UnityEngine.SceneManagement;

public class ProximityDetectorEgg: MonoBehaviour
{
    public float detectionRadius = 5f;
    private Transform playerTransform;

    [Header("Audio")]
    [SerializeField] private AudioClip[] EggCrack;
    [SerializeField] private AudioClip[] MonsterNoise;
    public float Volume = 1f;
    private bool Played;
    private bool Played1;
    private bool Played2;

    public FadeController fader;

    void Start()
    {
        // Find the player object by its tag
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with 'Player' tag not found!");
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Check if the player is within the detection radius
        if (distanceToPlayer < detectionRadius)
        {
            Debug.Log("Player is near the object!");

            if (!Played2)
            {
                Invoke("EggInteraction", 2);
                Played2 = true;
            }
            // Add your custom logic here (e.g., show an interaction prompt)
        }
        else
        {
            Debug.Log("Player is NOT near the object!");

            // Player is not near
        }
    }



    void EggInteraction()
    {
        if (!Played)
        {
            SoundFXManager.Instance.PlayRandomSoundFXClip(EggCrack, transform, Volume);
            Invoke("CallFade", 1);
            Played = true;

        }
    }

    public void CallFade()
    {
        if (fader != null)
        {
            // Call the public method on the FadeController component
            fader.FadeToBlack();
            Invoke("MainMenu", 4);
            Invoke("MonsterNoiseQue", 1);
        }
        else
        {
            Debug.LogError("FadeController reference is not set!");
        }
    }

    void MonsterNoiseQue()
    {
        SoundFXManager.Instance.PlayRandomSoundFXClip(MonsterNoise, transform, Volume);
    }
    void MainMenu()
    {

        Cursor.lockState = CursorLockMode.None;

        CollectableCount.collectableCount = 0;
        SceneManager.LoadScene("Main Menu");
    }
}