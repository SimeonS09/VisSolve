using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TriangleTransformations : MonoBehaviour
{
    public GameObject end1;
    public GameObject endbg1;
    public GameObject midPart1;
    public GameObject midPartbg1;
    public GameObject end2;
    public GameObject endbg2;
    public GameObject midPart2;
    public GameObject midPartbg2;
    public GameObject end3;
    public GameObject endbg3;
    public GameObject midPart3;
    public GameObject midPartbg3;

    public Canvas angleLineCanvas;
    public Canvas anglebgCanvas;

    public Renderer end1R;
    public Renderer endbg1R;
    public Renderer midPart1R;
    public Renderer midPartbg1R;
    public Renderer end2R;
    public Renderer endbg2R;
    public Renderer midPart2R;
    public Renderer midPartbg2R;
    public Renderer end3R;
    public Renderer endbg3R;
    public Renderer midPart3R;
    public Renderer midPartbg3R;
    public Image angleLineI;
    public Image anglebgI;

    public float length = 0f;
    public float angle = 0f;
    public float triangleangle = 0f;

    public Color color = Color.white;
    public Color aColor = Color.white;

    void Awake()
    {
        end1R = end1.GetComponent<Renderer>();
        endbg1R = endbg1.GetComponent<Renderer>();
        midPart1R = midPart1.GetComponent<Renderer>();
        midPartbg1R = midPartbg1.GetComponent<Renderer>();
        end2R = end2.GetComponent<Renderer>();
        endbg2R = endbg2.GetComponent<Renderer>();
        midPart2R = midPart2.GetComponent<Renderer>();
        midPartbg2R = midPartbg2.GetComponent<Renderer>();
        end3R = end3.GetComponent<Renderer>();
        endbg3R = endbg3.GetComponent<Renderer>();
        midPart3R = midPart3.GetComponent<Renderer>();
        midPartbg3R = midPartbg3.GetComponent<Renderer>();
    }

    void Start()
    {
        transform.eulerAngles = new Vector3 (0, 0, angle);
        UpdateColors();
    }
    void LateUpdate()
    {
        end1R.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;
        endbg1R.sortingOrder = -(int)Mathf.Abs(7.5f*length) -1 - 10;
        midPart1R.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;
        midPartbg1R.sortingOrder = -(int)Mathf.Abs(7.5f*length) -1 - 10;
        end2R.sortingOrder = -(int)Mathf.Abs(7.5f*length*Mathf.Tan(triangleangle*Mathf.Deg2Rad)) - 10;
        endbg2R.sortingOrder = -(int)Mathf.Abs(7.5f*length*Mathf.Tan(triangleangle*Mathf.Deg2Rad)) -1 - 10;
        midPart2R.sortingOrder = -(int)Mathf.Abs(7.5f*length*Mathf.Tan(triangleangle*Mathf.Deg2Rad)) - 10;
        midPartbg2R.sortingOrder = -(int)Mathf.Abs(7.5f*length*Mathf.Tan(triangleangle*Mathf.Deg2Rad)) -1 - 10;
        end3R.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;
        endbg3R.sortingOrder = -(int)Mathf.Abs(7.5f*length) -1 - 10;
        midPart3R.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 10;
        midPartbg3R.sortingOrder = -(int)Mathf.Abs(7.5f*length) -1 - 10;

        angleLineCanvas.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 2 - 10;
        anglebgCanvas.sortingOrder = -(int)Mathf.Abs(7.5f*length) - 3 - 10;


        end1.transform.localScale = new Vector3 (0.65f * Generaldata.gridScale, 0.65f * Generaldata.gridScale, 1);
        end1.transform.localPosition = new Vector3 (0, 0, 0);

        endbg1.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
        endbg1.transform.localPosition = new Vector3 (0, 0, 0);

        midPart1.transform.localScale = new Vector3 (length * 5f, 0.65f * Generaldata.gridScale, 1);
        midPart1.transform.localPosition = new Vector3 (length * 2.5f, 0, 0);

        midPartbg1.transform.localScale = new Vector3 (length * 5f, Generaldata.gridScale, 1);
        midPartbg1.transform.localPosition = new Vector3 (length * 2.5f, 0, 0);

        end2.transform.localScale = new Vector3 (0.65f * Generaldata.gridScale, 0.65f * Generaldata.gridScale, 1);
        end2.transform.localPosition = new Vector3 (length * 5f, 0, 0);

        endbg2.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
        endbg2.transform.localPosition = new Vector3 (length * 5f, 0, 0);

        midPart2.transform.localScale = new Vector3 (length * 5f * Mathf.Tan(triangleangle * Mathf.Deg2Rad), 0.65f * Generaldata.gridScale, 1);
        midPart2.transform.localPosition = new Vector3 (length * 5f, length * 2.5f*Mathf.Tan(triangleangle * Mathf.Deg2Rad), 0);
        midPart2.transform.localEulerAngles = new Vector3 (0, 0, 90);

        midPartbg2.transform.localScale = new Vector3 (length * 5f * Mathf.Tan(triangleangle * Mathf.Deg2Rad), Generaldata.gridScale, 1);
        midPartbg2.transform.localPosition = new Vector3 (length * 5f, length * 2.5f*Mathf.Tan(triangleangle * Mathf.Deg2Rad), 0);
        midPartbg2.transform.localEulerAngles = new Vector3 (0, 0, 90);

        end3.transform.localScale = new Vector3 (0.65f * Generaldata.gridScale, 0.65f * Generaldata.gridScale, 1);
        end3.transform.localPosition = new Vector3 (length * 5f, length * 5f * Mathf.Tan(triangleangle * Mathf.Deg2Rad), 0);

        endbg3.transform.localScale = new Vector3 (Generaldata.gridScale, Generaldata.gridScale, 1);
        endbg3.transform.localPosition = new Vector3 (length * 5f, length * 5f * Mathf.Tan(triangleangle * Mathf.Deg2Rad), 0);

        midPart3.transform.localScale = new Vector3 (5f * length / Mathf.Cos(triangleangle * Mathf.Deg2Rad), 0.65f * Generaldata.gridScale, 1);
        midPart3.transform.localPosition = new Vector3 (length * 2.5f, length * 2.5f*Mathf.Tan(triangleangle * Mathf.Deg2Rad));
        midPart3.transform.localEulerAngles = new Vector3 (0, 0, triangleangle);

        midPartbg3.transform.localScale = new Vector3 (5f * length / Mathf.Cos(triangleangle * Mathf.Deg2Rad), Generaldata.gridScale, 1);
        midPartbg3.transform.localPosition = new Vector3 (length * 2.5f, length * 2.5f*Mathf.Tan(triangleangle * Mathf.Deg2Rad));
        midPartbg3.transform.localEulerAngles = new Vector3 (0, 0, triangleangle);

        angleLineCanvas.transform.localScale = new Vector3 (length * 3f, length * 3f, 1);
        angleLineCanvas.transform.localPosition = new Vector3 (0, 0, 0);
        angleLineI.fillAmount = triangleangle/360f;

        anglebgCanvas.transform.localScale = new Vector3 (length * 3f + Generaldata.gridScale * 0.3f, length * 3f + Generaldata.gridScale * 0.3f, 1);
        anglebgCanvas.transform.localPosition = new Vector3 (0, 0, 0);
        anglebgI.fillAmount = triangleangle/360f;
    }

    public void UpdateColors()
    {
        end1R.material.color = new Color(color.r, color.g, color.b, color.a);
        endbg1R.material.color = new Color(0, 0, 0, color.a);
        midPart1R.material.color = new Color(color.r, color.g, color.b, color.a);
        midPartbg1R.material.color = new Color(0, 0, 0, color.a);
        end2R.material.color = new Color(color.r, color.g, color.b, color.a);
        endbg2R.material.color = new Color(0, 0, 0, color.a);
        midPart2R.material.color = new Color(color.r, color.g, color.b, color.a);
        midPartbg2R.material.color = new Color(0, 0, 0, color.a);
        end3R.material.color = new Color(color.r, color.g, color.b, color.a);
        endbg3R.material.color = new Color(0, 0, 0, color.a);
        midPart3R.material.color = new Color(color.r, color.g, color.b, color.a);
        midPartbg3R.material.color = new Color(0, 0, 0, color.a);

        angleLineI.color = new Color(aColor.r, aColor.g, aColor.b, color.a);
        anglebgI.color = new Color(0, 0, 0, color.a);
    }

    public Tween ChangeLength(float expLength)
    {
        return DOTween.To(() => length,
                x => length = x,
                expLength,
                3)
                .SetEase(Ease.InOutSine).OnUpdate(UpdateColors);
    }

    public Tween ChangeAngle(float expAngle)
    {
        return transform.DOLocalRotate(new Vector3(0, 0, expAngle), 3)
            .SetEase(Ease.InOutSine);
    }

    public Tween ChangeTriangleAngle(float expAngle)
    {
        return DOTween.To(() => triangleangle,
                x => triangleangle = x,
                expAngle,
                3)
                .SetEase(Ease.InOutSine);
    }

    public Tween ChangePosition(Vector3 vector3)
    {
        return transform.DOLocalMove(vector3, 3)
            .SetEase(Ease.InOutSine);
    }

    public Tween ChangeAngleColor(Color newColor)
    {
        return DOTween.To(() => 
            aColor,
            x => aColor = x,
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
