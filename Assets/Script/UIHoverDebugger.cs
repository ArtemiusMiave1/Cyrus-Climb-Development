using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverDebugger : MonoBehaviour
{
    void Update()
    {
        // Check what the mouse is currently over
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Get the object under the mouse
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                Debug.Log("Hovering over: " + result.gameObject.name);
            }
        }

        // Left mouse click check
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on a UI element!");
            }
            else
            {
                Debug.Log("Clicked in the world (not UI).");
            }
        }
    }
}