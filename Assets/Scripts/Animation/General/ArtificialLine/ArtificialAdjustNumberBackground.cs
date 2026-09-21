using UnityEngine;
using TMPro;

public class ArtificialAdjustNumberBackground : MonoBehaviour
{
    public TMP_Text numberText;
    public ArtificialLineTransformationWithNumber lineTransformations;
    private Renderer r;
    void Awake()
    {
        r = GetComponent<Renderer>();
    }
    void Update()
    {transform.localScale = new Vector3(numberText.preferredWidth + 0.4f, 2, 1);}

    public void UpdateColors()
    {r.material.color = new Color(10f/255f, 10f/255f, 26f/255f, lineTransformations.color.a);}
}