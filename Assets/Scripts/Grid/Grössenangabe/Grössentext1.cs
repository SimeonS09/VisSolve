using UnityEngine;
using DG.Tweening;
using TMPro;

public class Grössentext1 : MonoBehaviour
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
        AdjustText1();
    }

    void AdjustText1()
    {
        gridScale = Generaldata.gridScale;

        if (gridScale < 0.1)
        {
            text.alpha = 0;
        }
        if (gridScale < 1 && gridScale >= 0.1)
        {
            text.alpha = 1;
            length = 1/gridScale*2.5f;
            transform.localPosition = new Vector3 (length, 1.2f, 0);
        }
        if (gridScale >= 1)
        {
            text.alpha = 0;
        }
    }
}