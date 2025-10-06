using UnityEngine;
using UnityEngine.UI;  // For UI Button

public class ItemsToggleVisibility : MonoBehaviour
{
    // Assign these in the Inspector
    public GameObject[] objectToShow;
    public GameObject[] objectsToHide;

    public Button ItemUI;

    void Start()
    {
        ItemUI.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Objects to hide
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }

        // Objects to show
        foreach (GameObject obj in objectToShow)
        {
            obj.SetActive(true);
        }
    }
}
