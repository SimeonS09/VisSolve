using UnityEngine;
using DG.Tweening;

public class Grössenbalken10 : MonoBehaviour
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
        AdjustBar10();
    }

    void AdjustBar10()
    {
        gridScale = Generaldata.gridScale;
        
        if (gridScale < 1)
        {
            spriteRenderer.material.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        }
        if (gridScale >= 1 && gridScale < 10)
        {
            spriteRenderer.material.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            length = 1/gridScale*50;
            transform.localScale = new Vector3 (length, 0.2f, 1);
            transform.localPosition = new Vector3 (length/2, 0, 0);

        }
        if (gridScale >= 10)
        {
            spriteRenderer.material.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        }
    }
}