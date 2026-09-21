using UnityEngine;
using DG.Tweening;

public class Autotransparenz0_1 : MonoBehaviour
{
    
    private float alpha;
    private float with;
    private float gridScale = Generaldata.gridScale; 
    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        AdjustAlpha0_1();
         
    }
    void AdjustAlpha0_1()
    {
        gridScale = Generaldata.gridScale;

        if(gridScale < 0.02)
        {
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, 1);

            transform.localScale = new Vector3 (transform.localScale.x, 0.01f, 1);
        }
        if(gridScale >= 0.02 && gridScale < 0.2)
        {
            alpha = 1 - ((gridScale - 0.02f) / 0.18f);
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, alpha);

            transform.localScale = new Vector3 (transform.localScale.x, 0.01f, 1);
        }
        if(gridScale >= 0.2)
        {
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, 0);

            transform.localScale = new Vector3 (transform.localScale.x, 0.01f, 1);
        }
    }
}
