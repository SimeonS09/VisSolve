using UnityEngine;
using DG.Tweening;

public class MultiplicationAnimation : MonoBehaviour
{
    public LineTransformations linePrefab;
    public ArtificialLineTransformations artificialLinePrefab;
    public ArtificialLineTransformationWithNumber artificialLinePrefabWithNumber;

    public LineTransformations line1 = null;
    public LineTransformations line2 = null;
    public ArtificialLineTransformationWithNumber artLine1 = null;
    public ArtificialLineTransformations artLine2 = null;

    public AnimationControl animationControl;
    public AnzeigeAnimation anzeigeAnimation;
    public Gridzooming gridzooming;

    public Sequence seq;

    public LineTransformations StartAnimation(decimal term1, Color color1, LineTransformations currentVisLine1, decimal term2, Color color2, LineTransformations currentVisLine2, Color color3)
    {
        print("multiplication");

        seq = DOTween.Sequence();

        float term1f;
        float term2f;

        if (Mathf.Abs((float)term1) >= Mathf.Abs((float)term2))
        {
            term1f = (float)term1;
            term2f = (float)term2;
        }
        else
        {
            term1f = (float)term2;
            term2f = (float)term1;
            (color1, color2) = (color2, color1);
            (currentVisLine1, currentVisLine2) = (currentVisLine2, currentVisLine1);
        }
        if (currentVisLine1 == null)
        {
            line1 = Instantiate(linePrefab, transform);
            line1.transform.localPosition = new Vector3(20, 20, -1);
            line1.color = color1;
            line1.color.a = 0;
            line1.UpdateColors();
        }
        else 
        {
            line1 = currentVisLine1;
            if (line1.transform.localPosition != new Vector3(20, 20, -1) || line1.angle != 0)
            {
                seq.Append(line1.ChangePosition(new Vector3(20, 20, -1)));
                seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4));
                seq.Join(line1.ChangeAngle(0));
            }
        }
        if (currentVisLine2 == null)
        {
            line2 = Instantiate(linePrefab, transform);
            line2.transform.localPosition = new Vector3(20, 20, -1);
            line2.angle = 90;
            line2.color = color2;
            line2.color.a = 0f;
            line2.UpdateColors();
        }
        else 
        {
            line2 = currentVisLine2;
            if (line2.transform.localPosition != new Vector3(20, 20, -1) || line2.angle != 90)
            {
                seq.Append(line2.ChangePosition(new Vector3(20, 20, -1)));
                seq.Join(gridzooming.UppdateGrid(4, 4, 4, 4 + term2f));
                seq.Join(line2.ChangeAngle(90));
            }
        }

        artLine1 = Instantiate(artificialLinePrefabWithNumber, transform);
        artLine1.transform.localPosition = new Vector3(20, 20, -1);
        artLine1.length = 1;
        artLine1.angle = 90;
        artLine1.color.a = 0;
        artLine1.UpdateColors();

        artLine2 = Instantiate(artificialLinePrefab, transform);
        artLine2.transform.localPosition = new Vector3(20 + 5*term1f, 20, -1);
        if(term1f > 0) artLine2.angle = 180 - Mathf.Atan(1/term1f) * Mathf.Rad2Deg;
        else  artLine2.angle = -Mathf.Atan(1/term1f) * Mathf.Rad2Deg;
        artLine2.color.a = 0;
        artLine2.UpdateColors();

        if (currentVisLine2 == null) seq.Append(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4));
        else seq.Append(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4 + term2f));
        if (currentVisLine1 == null)
        { 
            seq.Append(line1.FadeIn());
            seq.Join(line1.ChangeLength(term1f));
        }
        
        seq.Append(artLine1.FadeIn());
        seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f, 5));

        seq.Append(artLine2.FadeIn());
        seq.Join(artLine2.ChangeLength(Mathf.Sqrt(1+(term1f*term1f))));

        if (currentVisLine2 == null) 
        {
            seq.Append(line2.FadeIn());
            seq.Join(line2.ChangeLength(term2f));
        }
        if (term2f >= 1) seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4 + term2f));
        else if (term2f <= -1) seq.Join(gridzooming.UppdateGrid(4, 5, 4 + term1f, 4 + term2f));

        if(term2f < 0) seq.Append(gridzooming.UppdateGrid(4 + term1f, 4, 4 + term1f*term2f, 4 + term2f));
        else seq.Append(gridzooming.UppdateGrid(4, 4, 4 + term1f*term2f, 4 + term2f)); 
        seq.Append(artLine2.ChangePosition(new Vector3(20 + 5*(term1f*term2f), 20, -1)));
        seq.Join(artLine2.ChangeLength(Mathf.Sqrt(1+(term1f*term1f))*term2f));
        seq.Join(line1.ChangeLength(term1f*term2f));
        seq.Join(line1.ChangeColor(color3));
        seq.Join(artLine1.FadeOut());
        seq.AppendCallback(() => {Destroy(artLine1.gameObject);});

        seq.Append(line2.FadeOut());
        seq.Join(artLine2.FadeOut());
        seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f*term2f, 4));
        seq.Join(animationControl.FinishCalculation("*"));

        seq.AppendCallback(() =>
        {
            Destroy(line2.gameObject);
            Destroy(artLine2.gameObject);
        });

        seq.OnComplete(() => animationControl.StartCalculation());
        
        seq.Play();
        return line1;
    }
}