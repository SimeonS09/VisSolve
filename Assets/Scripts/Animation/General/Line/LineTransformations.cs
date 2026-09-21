using UnityEngine;
using DG.Tweening;

public class LineTransformations : MonoBehaviour
{
    public GameObject end1;
    public GameObject negEnd1;
    public GameObject midPart;
    public GameObject negMidPart;
    public GameObject end2;
    public GameObject negEnd2;

    private Renderer end1R;
    private Renderer negEnd1R;
    private Renderer midPartR;
    private Renderer negMidPartR;
    private Renderer end2R;
    private Renderer negEnd2R;

    public GameObject line;
    public GameObject number;
    public AdjustNumberBackground numberbg;
    public NumberPosition numberPosition;

    public float length = 0f;
    public float angle = 0f;

    public Color color = Color.white;

    void Awake()
    {
        end1R = end1.GetComponent<Renderer>();
        negEnd1R = negEnd1.GetComponent<Renderer>();
        midPartR = midPart.GetComponent<Renderer>();
        negMidPartR = negMidPart.GetComponent<Renderer>();
        end2R = end2.GetComponent<Renderer>();
        negEnd2R = negEnd2.GetComponent<Renderer>();
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

        negEnd1R.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;
        negMidPartR.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;
        negEnd2R.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;

        end1.transform.localScale = new Vector3 (0.8f * Generaldata.gridScale, 0.8f * Generaldata.gridScale, 1);
        end1.transform.localPosition = new Vector3 (0, 0, 0);

        negEnd1.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
        negEnd1.transform.localPosition = new Vector3 (0, 0, 0.1f);

        midPart.transform.localScale = new Vector3 (length * 5, 0.8f * Generaldata.gridScale, 1);
        midPart.transform.localPosition = new Vector3 (length * 2.5f, 0, 0);

        negMidPart.transform.localScale = new Vector3 (length * 5, 1 * Generaldata.gridScale, 1);
        negMidPart.transform.localPosition = new Vector3 (length * 2.5f, 0, 0.1f);

        end2.transform.localScale = new Vector3 (0.8f * Generaldata.gridScale, 0.8f * Generaldata.gridScale, 1);
        end2.transform.localPosition = new Vector3 (length * 5, 0, 0);

        negEnd2.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
        negEnd2.transform.localPosition = new Vector3 (length * 5, 0, 0.1f);
        
        number.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
    }

    public void UpdateColors()
    {
        if (numberbg != null)
        numberbg.UpdateColors();
        numberPosition.color = color;
        numberPosition.UpdateColors();

        if(midPart.transform.localScale.x < 0)
        {
            end1R.material.color = new Color(10f/255f, 10f/255f, 26f/255f, Mathf.CeilToInt(color.a));
            midPartR.material.color = new Color(10f/255f, 10f/255f, 26f/255f, Mathf.CeilToInt(color.a));
            end2R.material.color = new Color(10f/255f, 10f/255f, 26f/255f, Mathf.CeilToInt(color.a));

            negEnd1R.material.color = new Color(color.r, color.g, color.b, color.a);
            negMidPartR.material.color = new Color(color.r, color.g, color.b, color.a);
            negEnd2R.material.color = new Color(color.r, color.g, color.b, color.a);
        }
        else
        {
            end1R.material.color = new Color(color.r, color.g, color.b, color.a);
            midPartR.material.color = new Color(color.r, color.g, color.b, color.a);
            end2R.material.color = new Color(color.r, color.g, color.b, color.a);

            negEnd1R.material.color = new Color(color.r, color.g, color.b, 0f);
            negMidPartR.material.color = new Color(color.r, color.g, color.b, 0f);
            negEnd2R.material.color = new Color(color.r, color.g, color.b, 0f);
        }
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

    public Tween ChangeColor(Color newColor)
    {
        return DOTween.To(() => 
            color,
            x => color = x,
            newColor,
            3).OnUpdate(UpdateColors);
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
