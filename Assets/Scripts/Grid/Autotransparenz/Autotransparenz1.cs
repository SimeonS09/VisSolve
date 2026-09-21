using UnityEngine;
using DG.Tweening;

public class Autotransparenz1 : MonoBehaviour
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
        AdjustAlpha1();
         
    }
    void AdjustAlpha1()
    {
        gridScale = Generaldata.gridScale;

        if(gridScale < 0.02)
        {
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, 0);

            transform.localScale = new Vector3 (transform.localScale.x, 0.001f, 1);
        }
        if(gridScale >= 0.02 && gridScale < 0.2)
        {
            alpha = (gridScale - 0.02f) / 0.18f;
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, alpha);

            with = gridScale / 20;
            transform.localScale = new Vector3 (transform.localScale.x, with, 1);;
        }
        if(gridScale >= 0.2 && gridScale < 2)
        {
            alpha = 1 - ((gridScale - 0.2f) / 1.8f);
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, alpha);

            transform.localScale = new Vector3 (transform.localScale.x, 0.01f, 1);
        }
        if(gridScale >= 2)
        {
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, 0);

            transform.localScale = new Vector3 (transform.localScale.x, 0.01f, 1);
        }
    }
}

