using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript: MonoBehaviour
{
    // Call this method to restart the current scene
    public void RestartGame()
    {
        // Get the currently active scene and reload it
        SceneManager.LoadScene("CyrusClimbMainScene 1");
    }

    // Optional: Automatically restart when pressing R
    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}