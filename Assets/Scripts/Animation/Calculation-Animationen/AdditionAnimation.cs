using UnityEngine;
using DG.Tweening;

public class AdditionAnimation : MonoBehaviour
{
    public LineTransformations linePrefab;


    public LineTransformations line1 = null;
    public LineTransformations line2 = null;
    public LineTransformations line3 = null;

    public AnimationControl animationControl;
    public AnzeigeAnimation anzeigeAnimation;
    public Gridzooming gridzooming;
    
    public Sequence seq;

    public LineTransformations StartAnimation(decimal term1, Color color1, LineTransformations currentVisLine1, decimal term2, Color color2, LineTransformations currentVisLine2, Color color3)
    {
        print("addition");

        seq = DOTween.Sequence();

        float term1f = (float)term1;
        float term2f = (float)term2;

        if (term1 * term2 >= 0)
        {
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
                line2.transform.localPosition = new Vector3(20 + 5*term1f, 20, -1);
                line2.color = color2;
                line2.color.a = 0f;
                line2.UpdateColors();
            }
            else 
            {
                line2 = currentVisLine2;
                if (line2.transform.localPosition != new Vector3(20 + 5*term1f, 20, -1) || line1.angle != 0)
                {
                    seq.Append(line2.ChangePosition(new Vector3(20 + 5*term1f, 20, -1)));
                    seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f + term2f, 4));
                    seq.Join(line2.ChangeAngle(0));
                }
            }

            line3 = Instantiate(linePrefab, transform);
            line3.transform.localPosition = new Vector3(20, 20 + 0.3f*(Mathf.Abs(term1f)+Mathf.Abs(term2f)), -1);
            line3.color = color3;
            line3.color.a = 0f;
            line3.UpdateColors();
            
            
            if (currentVisLine2 == null) seq.Append(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4));
            if (currentVisLine1 == null)
            { 
                seq.Append(line1.FadeIn());
                seq.Join(line1.ChangeLength(term1f));
            }
            
            if (currentVisLine2 == null) 
            {
                seq.Append(line2.FadeIn());
                seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f + term2f, 4));
                seq.Join(line2.ChangeLength(term2f));
            }
            
            seq.Append(line3.FadeIn());
            seq.Append(line3.ChangeLength(term1f + term2f));
            seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f + term2f, 4 + 0.06f*(Mathf.Abs(term1f)+Mathf.Abs(term2f))));
            seq.Append(line1.FadeOut());
            seq.Join(line2.FadeOut());
            seq.Join(animationControl.FinishCalculation("+"));
            seq.AppendCallback(() =>
            {
                Destroy(line1.gameObject);
                Destroy(line2.gameObject);
            });

            seq.OnComplete(() => animationControl.StartCalculation());
        }

        else if (Mathf.Abs(term1f) > Mathf.Abs(term2f))
        {
            if (currentVisLine1 == null)
            {
                line1 = Instantiate(linePrefab, transform);
                line1.transform.localPosition = new Vector3(20, 20, -1);
                line1.color = color1;
                line1.color.a = 0f;
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
                line2.transform.localPosition = new Vector3(20 + 5*term1f, 20 + 0.3f*Mathf.Abs(term1f), -1);
                line2.color = color2;
                line2.color.a = 0f;
            }
            else 
            {
                line2 = currentVisLine2;
                if (line2.transform.localPosition != new Vector3(20 + 5*term1f, 20 + 0.3f*Mathf.Abs(term1f), -1) || line1.angle != 0)
                {
                    seq.Append(line2.ChangePosition(new Vector3(20 + 5*term1f, 20 + 0.3f*Mathf.Abs(term1f), -1)));
                    seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4 + 0.06f*Mathf.Abs(term1f)));
                    seq.Join(line2.ChangeAngle(0));
                }
            }

            line3 = Instantiate(linePrefab, transform);
            line3.transform.localPosition = new Vector3(20, 20 + 0.3f*Mathf.Abs(term1f), -1);
            line3.color = color3;
            line3.color.a = 0f;

            if (currentVisLine2 == null) seq.Append(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4));
            if (currentVisLine1 == null)
            { 
                seq.Append(line1.FadeIn());
                seq.Join(line1.ChangeLength(term1f));
            }
            
            if (currentVisLine2 == null) 
            {
                seq.Append(line2.FadeIn());
                seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4 + 0.06f*Mathf.Abs(term1f)));
                seq.Join(line2.ChangeLength(term2f));
            }
            seq.Append(line3.FadeIn());


            seq.Append(line3.ChangeLength(term1f + term2f));
            seq.Join(gridzooming.UppdateGrid(4, 4 + 0.06f*Mathf.Abs(term1f), 4 + term1f + term2f,  4 + 0.06f*Mathf.Abs(term1f)));   
            seq.Append(line1.FadeOut());
            seq.Join(line2.FadeOut());
            seq.Join(animationControl.FinishCalculation("+"));

            seq.AppendCallback(() =>
            {
                Destroy(line1.gameObject);
                Destroy(line2.gameObject);
            });

            seq.OnComplete(() => animationControl.StartCalculation());
        }

        else if (Mathf.Abs(term1f) <= Mathf.Abs(term2f)) 
        {
            if (currentVisLine1 == null)
            {
                line1 = Instantiate(linePrefab, transform);
                line1.transform.localPosition = new Vector3(20, 20, -1);
                line1.color = color1;
                line1.color.a = 0f;
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
                line2.transform.localPosition = new Vector3(20 + 5*term1f, 20 + 0.3f*Mathf.Abs(term2f), -1);
                line2.color = color2;
                line2.color.a = 0f;
            }
            else 
            {
                line2 = currentVisLine2;
                if (line2.transform.localPosition != new Vector3(20 + 5*term1f, 20 + 0.3f*Mathf.Abs(term2f), -1) || line1.angle != 0)
                {
                    seq.Append(line2.ChangePosition(new Vector3(20 + 5*term1f, 20 + 0.3f*Mathf.Abs(term2f), -1)));
                    seq.Join(gridzooming.UppdateGrid(4 + term1f + term2f, 4, 4 + term1f, 4 + 0.06f*Mathf.Abs(term2f)));
                    seq.Join(line2.ChangeAngle(0));
                }
            }

            line3 = Instantiate(linePrefab, transform);
            line3.transform.localPosition = new Vector3(20, 20, -1);
            line3.color = color3;
            line3.color.a = 0f;

            if (currentVisLine2 == null) seq.Append(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4));
            if (currentVisLine1 == null)
            { 
                seq.Append(line1.FadeIn());
                seq.Join(line1.ChangeLength(term1f));
            }
            
            
            if (currentVisLine2 == null) 
            {
                seq.Append(line2.FadeIn());
                seq.Join(gridzooming.UppdateGrid(4 + term1f + term2f, 4, 4 + term1f, 4 + 0.06f*Mathf.Abs(term2f)));
                seq.Join(line2.ChangeLength(term2f));
            }
            seq.Append(line3.FadeIn());

            seq.Join(line3.ChangeLength(term1f + term2f));
            seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f + term2f, 4));
            seq.Append(line1.FadeOut());
            seq.Join(line2.FadeOut());
            seq.Join(animationControl.FinishCalculation("+"));
            seq.AppendCallback(() =>
            {
                Destroy(line1.gameObject);
                Destroy(line2.gameObject);
            });

            seq.OnComplete(() => animationControl.StartCalculation());
        }
        seq.Play();
        return line3;
    }
}