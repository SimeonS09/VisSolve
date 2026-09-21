using UnityEngine;
using DG.Tweening;

public class AutoTransparenz100 : MonoBehaviour
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
        AdjustAlpha100();
         
    }
    void AdjustAlpha100()
    {
        gridScale = Generaldata.gridScale;

        if(gridScale < 2)
        {
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, 0);

            transform.localScale = new Vector3 (transform.localScale.x, 0.001f, 1);
        }
        if(gridScale >= 2 && gridScale < 20)
        {
            alpha = (gridScale - 2f) / 18f;
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, alpha);

            with = gridScale / 2000 ;
            transform.localScale = new Vector3 (transform.localScale.x, with, 1);
        }
        if(gridScale >= 20)
        {
            spriteRenderer.material.color = new Color(212f/255f, 239f/255f, 250f/255f, 1);
            transform.localScale = new Vector3 (transform.localScale.x, 0.01f, 1);
        }
    }
}
