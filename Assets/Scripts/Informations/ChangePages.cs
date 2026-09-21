using UnityEngine;

public class ChangePages : MonoBehaviour
{
    public GameObject toRem1 = null;
    public GameObject toRem2 = null;
    public GameObject toRem3 = null;
    public GameObject toRem4 = null;

    public GameObject toShow1 = null;
    public GameObject toShow2 = null;
    public GameObject toShow3 = null;
    public GameObject toShow4 = null;
    
    public void Switch()
    {
        if (toRem1 != null) toRem1.SetActive(false);
        if (toRem2 != null) toRem2.SetActive(false);
        if (toRem3 != null) toRem3.SetActive(false);
        if (toRem4 != null) toRem4.SetActive(false);

        if (toShow1 != null) toShow1.SetActive(true);
        if (toShow2 != null) toShow2.SetActive(true);
        if (toShow3 != null) toShow3.SetActive(true);
        if (toShow4 != null) toShow4.SetActive(true);
    }
}
