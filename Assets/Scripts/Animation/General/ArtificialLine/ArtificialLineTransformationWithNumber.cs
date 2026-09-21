using UnityEngine;
using DG.Tweening;

public class ArtificialLineTransformationWithNumber : MonoBehaviour
{
    public GameObject end1;
    public GameObject midPart;
    public GameObject end2;
    public GameObject end1bg;
    public GameObject midPartbg;
    public GameObject end2bg;
    

    private Renderer end1R;
    private Renderer midPartR;
    private Renderer end2R;
    private Renderer end1bgR;
    private Renderer midPartbgR;
    private Renderer end2bgR;

    public GameObject line;
    public GameObject number;
    public ArtificialAdjustNumberBackground numberbg;
    public ArtificialNumberPosition numberPosition;


    public float length = 0f;
    public float angle = 0f;

    public Color color = Color.white;

    void Awake()
    {
        end1R = end1.GetComponent<Renderer>();
        midPartR = midPart.GetComponent<Renderer>();
        end2R = end2.GetComponent<Renderer>();

        end1bgR = end1bg.GetComponent<Renderer>();
        midPartbgR = midPartbg.GetComponent<Renderer>();
        end2bgR = end2bg.GetComponent<Renderer>();
    }
    void Start()
    {
        line.transform.eulerAngles = new Vector3 (0, 0, angle);
        UpdateColors();
    }
      
    void LateUpdate()
    {
        end1R.sortingOrder = -(int)Mathf.Abs(7.5f*length) + 1 - 10;
        midPartR.sortingOrder = -(int)Mathf.Abs(7.5f*length) + 1 - 10;
        end2R.sortingOrder = -(int)Mathf.Abs(7.5f*length) + 1 - 10;

        end1bgR.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;
        midPartbgR.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;
        end2bgR.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;

        end1.transform.localScale = new Vector3 (0.65f * Generaldata.gridScale, 0.65f * Generaldata.gridScale, 1);
        end1.transform.localPosition = new Vector3 (0, 0, 0);

        end1bg.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
        end1bg.transform.localPosition = new Vector3 (0, 0, 0);

        midPart.transform.localScale = new Vector3 (length * 5f, 0.65f * Generaldata.gridScale, 1);
        midPart.transform.localPosition = new Vector3 (length * 2.5f, 0, 0);

        midPartbg.transform.localScale = new Vector3 (length * 5f, Generaldata.gridScale, 1);
        midPartbg.transform.localPosition = new Vector3 (length * 2.5f, 0, 0);

        end2.transform.localScale = new Vector3 (0.65f * Generaldata.gridScale, 0.65f * Generaldata.gridScale, 1);
        end2.transform.localPosition = new Vector3 (length * 5f, 0, 0);

        end2bg.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
        end2bg.transform.localPosition = new Vector3 (length * 5f, 0, 0);

        number.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
    }

    public void UpdateColors()
    {
        numberbg.UpdateColors();
        numberPosition.color = color;
        numberPosition.UpdateColors();

        end1R.material.color = new Color(color.r, color.g, color.b, color.a);
        midPartR.material.color = new Color(color.r, color.g, color.b, color.a);
        end2R.material.color = new Color(color.r, color.g, color.b, color.a);

        end1bgR.material.color = new Color(0, 0, 0, color.a);
        midPartbgR.material.color = new Color(0, 0, 0, color.a);
        end2bgR.material.color = new Color(0, 0, 0, color.a);
    }

    public Tween ChangeLength(float expLength)
    {
        return DOTween.To(() => 
            length,
            x => length = x,
            expLength,
            3)
            .SetEase(Ease.InOutSine).OnUpdate(UpdateColors);
    }
    
    public Tween ChangeAngle(float expAngle)
    {
        if (expAngle < 0) expAngle += 360;
        return DOTween.To(
            () => line.transform.localEulerAngles.z,
            x => line.transform.localEulerAngles = new Vector3(0, 0, x),
            expAngle,
            3f
        );
    }

    public Tween ChangePosition(Vector3 vector3)
    {
        return transform.DOLocalMove(vector3, 3)
            .SetEase(Ease.InOutSine);
    }
    public Tween FadeIn()
    {
        return DOTween.To(() => 
            color.a,
            x => color.a = x,
            1,
            1).OnUpdate(UpdateColors);
    }
    public Tween FadeOut()
    {
        return DOTween.To(() => 
            color.a,
            x => color.a = x,
            0,
            1).OnUpdate(UpdateColors);
    }
}
