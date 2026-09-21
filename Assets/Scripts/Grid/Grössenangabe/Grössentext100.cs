using UnityEngine;
using DG.Tweening;
using TMPro;

public class Grössentext100 : MonoBehaviour
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
        AdjustText100();
    }

    void AdjustText100()
    {
        gridScale = Generaldata.gridScale;

        if (gridScale < 10)
        {
            text.alpha = 0;
        }
        if (
            gridScale >= 10)
        {
            text.alpha = 1;
            length = 1/gridScale*250;
            transform.localPosition = new Vector3 (length, 1.2f, 0);

        }
    }
}