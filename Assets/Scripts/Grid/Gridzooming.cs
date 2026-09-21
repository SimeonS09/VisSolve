using UnityEngine;
using DG.Tweening;

public class Gridzooming : MonoBehaviour
{
    public GameObject raster_100;
    public GameObject raster_10;
    public GameObject raster_1;
    public GameObject raster_01;
    public GameObject raster;


    private float zoomingDuration;
    private float alpha;

    private float currentMinX;
     
    private float currentMinY;
    
    private float currentMaxX;
    
    private float currentMaxY;


    void Update()
    {
        currentMinX = (64 - (raster.transform.localPosition.x + 64) - 64 * Generaldata.gridScale) / 5;
        currentMinY = (44 - (raster.transform.localPosition.y + 44) - 44 * Generaldata.gridScale) / 5;
        currentMaxX = currentMinX + Generaldata.gridScale * 128 / 5;
        currentMaxY = currentMinY + Generaldata.gridScale * 88 / 5;

        Generaldata.gridScale = 1 / transform.localScale.x;

        raster_100.transform.localPosition = new Vector3 (500 * Mathf.FloorToInt(currentMinX / 100), 500 * Mathf.FloorToInt(currentMinY / 100), 0);
        raster_10.transform.localPosition = new Vector3 (50 * Mathf.FloorToInt(currentMinX / 10), 50 * Mathf.FloorToInt(currentMinY / 10), 0);
        raster_1.transform.localPosition = new Vector3 (5 * Mathf.FloorToInt(currentMinX), 5 * Mathf.FloorToInt(currentMinY), 0);
        raster_01.transform.localPosition = new Vector3 (0.5f * Mathf.FloorToInt(currentMinX / 0.1f), 0.5f * Mathf.FloorToInt(currentMinY / 0.1f), 0);
    }

    public Sequence UppdateGrid(float expMinXOrg, float expMinYOrg, float expMaxXOrg, float expMaxYOrg)
    {
        
        Sequence seq = DOTween.Sequence();
        float expMinX;
        float expMaxX;
        float expMinY;
        float expMaxY;

        if (expMaxXOrg < expMinXOrg) 
        {
            expMinX = expMaxXOrg;
            expMaxX = expMinXOrg;
        }
        else 
        {
            expMinX = expMinXOrg;
            expMaxX = expMaxXOrg;
        }

        if (expMaxYOrg < expMinYOrg) 
        {
            expMinY = expMaxYOrg;
            expMaxY = expMinYOrg;
        }
        else 
        {
            expMinY = expMinYOrg;
            expMaxY = expMaxYOrg;
        }
        
        if (expMinX != expMaxX || expMinY != expMaxY)
        {
            if (expMaxX - expMinX > 3600) (expMinX, expMaxX) = ((expMaxX + expMinX)/2 - 1800, (expMaxX + expMinX)/2 + 1800);
            if (expMaxY - expMinY > 2700) (expMinY, expMaxY) = ((expMaxY + expMinY)/2 - 1350, (expMaxY + expMinY)/2 + 1350);
            if (expMaxX - expMinX < 0.3f) (expMinX, expMaxX) = ((expMaxX + expMinX)/2 - 0.15f, (expMaxX + expMinX)/2 + 0.15f);
            if (expMaxY - expMinY < 0.2f) (expMinY, expMaxY) = ((expMaxY + expMinY)/2 - 0.1f, (expMaxY + expMinY)/2 + 0.1f);
            
            float expGridScale;

            if ((expMaxX - expMinX)/16 > (expMaxY - expMinY)/11) expGridScale = (expMaxX - expMinX)/25.6f * 1.25f; 
            else expGridScale = (expMaxY - expMinY)/17.6f * 1.25f;

            seq.Append(AdjustScale(expGridScale));
        }
        seq.Join(AdjustPosition((expMaxX + expMinX)/2,(expMaxY + expMinY)/2));
        return seq;
    }

    private Sequence AdjustScale(float expGridScale)
    {

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleX(1/expGridScale, 2/Generaldata.calculationSpeed).SetEase(Ease.InOutSine));
        seq.Join(transform.DOScaleY(1/expGridScale, 2/Generaldata.calculationSpeed).SetEase(Ease.InOutSine));
        return seq;

    }
    private Tween AdjustPosition(float expX, float expY)
    {
        Vector3 v = new Vector3(expX * -5, expY * -5, 0);
        return raster.transform.DOLocalMove(v, 2/Generaldata.calculationSpeed).SetEase(Ease.InOutSine);
    }
}
