using UnityEngine;
using DG.Tweening;
using TMPro;

public class Grössentext10 : MonoBehaviour
{
    private float gridScale;
    private float length;
    private TMP_Text text;
    private RectTransform rectTransform;


    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        AdjustText10();
    }

    void AdjustText10()
    {
        gridScale = Generaldata.gridScale;

        if (gridScale < 1)
        {
            text.alpha = 0;
        }
        if (gridScale < 10 && gridScale >= 1)
        {
            text.alpha = 1;
            length = 1/gridScale*25;
            transform.localPosition = new Vector3 (length, 1.2f, 0);
        }
        if (gridScale >= 10)
        {
            text.alpha = 0;
        }
    }
}