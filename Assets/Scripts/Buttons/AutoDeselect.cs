using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoDeselect: MonoBehaviour
{
    void Update()
    {EventSystem.current.SetSelectedGameObject(null);    }
}

public class AutoDeselectButton: Button
{
    void Update()
    {
        if (Input.touchCount > 0 &&
        Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            InstantClearState();
        }
    }
}
