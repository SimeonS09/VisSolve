using UnityEngine;
using System;
using TMPro;

public class ArtificialNumberPosition : MonoBehaviour
{
    public TMP_Text numberText;
    public GameObject line;
    public ArtificialLineTransformationWithNumber lineTransformations;
    public GameObject midPart;

    public float distance = 1;
    public Color color = Color.white;
    private decimal d = 0;

    void Update()
    {
        float angle = line.transform.eulerAngles.z * Mathf.Deg2Rad;

        float x = midPart.transform.position.x - Mathf.Sin(angle) * numberText.preferredWidth * distance;
        float y = midPart.transform.position.y + Mathf.Cos(angle) * numberText.preferredHeight * distance;

        transform.position = new Vector3(x, y, -0.1f);
        transform.eulerAngles = new Vector3(0,0,0);
        transform.localScale = new Vector3(Generaldata.gridScale, Generaldata.gridScale, 1);

        d = Math.Round((decimal)lineTransformations.length, 4, MidpointRounding.AwayFromZero);
    }
    public void UpdateColors()
    {numberText.text =  $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>" + d.ToString("0.####") + "</color>";}
}