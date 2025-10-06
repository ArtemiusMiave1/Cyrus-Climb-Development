using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
public GameObject AudioMenu;
private bool menuActivated;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && menuActivated)
        {
            Time.timeScale = 1;
            AudioMenu.SetActive(false);
            menuActivated = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        else if (Input.GetKeyDown(KeyCode.J) && !menuActivated)
        {
            Time.timeScale = 0;
            AudioMenu.SetActive(true);
            menuActivated = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    
}
