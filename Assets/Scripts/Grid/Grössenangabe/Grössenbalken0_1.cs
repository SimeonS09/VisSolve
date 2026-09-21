using UnityEngine;
using DG.Tweening;

public class Grössenbalken0_1 : MonoBehaviour
{
    private float gridScale;
    private float length;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AdjustBar0_1();
    }

    void AdjustBar0_1()
    {
        gridScale = Generaldata.gridScale;

        if (gridScale < 0.1)
        {
            spriteRenderer.material.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            length = 1/gridScale*0.5f;
            transform.localScale = new Vector3 (length, 0.2f, 1);
            transform.localPosition = new Vector3 (length/2, 0, 0);

        }
        if (gridScale >= 0.1)
        {
            spriteRenderer.material.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);

        }
    }
}