using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System;

public class PowerAnimation : MonoBehaviour
{
    public LineTransformations linePrefab;
    public ArtificialLineTransformations artificialLinePrefab;
    public ArtificialLineTransformationWithNumber artificialLineWithNumberPrefab;

    public LineTransformations line1 = null;
    public LineTransformations line2 = null;
    public List<LineTransformations> lines = new List<LineTransformations>();
    public ArtificialLineTransformationWithNumber artLineWN1 = null;
    public ArtificialLineTransformationWithNumber artLineWN2 = null;
    public ArtificialLineTransformations artLine1 = null;
    public ArtificialLineTransformations artLine2 = null;
    private List<Color> colorfractions = new List<Color>();
    

    public AnimationControl animationControl;
    public AnzeigeAnimation anzeigeAnimation;
    public Gridzooming gridzooming;

    public Sequence seq;

    public LineTransformations StartAnimation(decimal term1, Color color1, LineTransformations currentVisLine, decimal term2, Color color2, Color color3)
    {
        print("power");
        lines.Clear();
        List<Color> colorfractions = new List<Color>();
        seq = DOTween.Sequence();

        float term1f = (float)term1;
        float term2f = (float)term2;
        
        float lowestX = (float)Math.Ceiling(1.4f*Mathf.Abs(term1f));

        for (int i=1; i < term2f; i++)
        {
            colorfractions.Add(new Color(
            color1.r + i*(color3.r - color1.r)/(term2f-1),
            color1.g + i*(color3.g - color1.g)/(term2f-1),
            color1.b + i*(color3.b - color1.b)/(term2f-1),
            1));
        }

        if (currentVisLine == null)
        {
            line1 = Instantiate(linePrefab, transform);
            line1.transform.localPosition = new Vector3(20, 20 + 5*(lowestX+term2f), -1);
            line1.color = color1;
            line1.color.a = 0;
            line1.UpdateColors();
        }
        else 
        {
            line1 = currentVisLine;
            if (line1.transform.localPosition != new Vector3(20, 20 + 5*(lowestX+term2f), -1) || line1.angle != 0)
            {
                seq.Append(line1.ChangePosition (new Vector3(20, 20 + 5*(lowestX+term2f), -1)));
                seq.Join(gridzooming.UppdateGrid(4, 4 + lowestX+term2f, 4 + term1f, 4 + lowestX+term2f));
                seq.Join(line1.ChangeAngle(0));
            }
        }

        line2 = Instantiate(linePrefab, transform);
        line2.transform.localPosition = new Vector3(20, 20 + 5*(lowestX+term2f), -1);
        line2.angle = -90;
        line2.color = color2;
        line2.color.a = 0f;
        line2.UpdateColors();
        
        artLineWN1 = Instantiate(artificialLineWithNumberPrefab, transform);
        artLineWN1.transform.localPosition = new Vector3(20, 20 + 5*(lowestX+term2f), -1);
        artLineWN1.length = 1;
        artLineWN1.angle = 90;
        artLineWN1.color.a = 0;
        artLineWN1.UpdateColors();

        artLine1 = Instantiate(artificialLinePrefab, transform);
        artLine1.transform.localPosition = new Vector3(20 + 5*term1f, 20 + 5*(lowestX+term2f), -1);
        if(term1f > 0) artLine1.angle = 180 - Mathf.Atan(1/term1f) * Mathf.Rad2Deg;
        else  artLine1.angle = -Mathf.Atan(1/term1f) * Mathf.Rad2Deg;
        artLine1.color.a = 0;
        artLine1.UpdateColors();

        artLineWN2 = Instantiate(artificialLineWithNumberPrefab, transform);
        artLineWN2.transform.localPosition = new Vector3(20, 20, -1);
        artLineWN2.length = 1;
        artLineWN2.angle = 90;
        artLineWN2.color.a = 0;
        artLineWN2.UpdateColors();

        artLine2 = Instantiate(artificialLinePrefab, transform);
        artLine2.transform.localPosition = new Vector3(20 + 5*term1f, 20, -1);
        artLine2.angle = 180 - Mathf.Atan(1/term1f) * Mathf.Rad2Deg;
        artLine2.color.a = 0;
        artLine2.UpdateColors();

        if (currentVisLine == null)
        {
            seq.Append(gridzooming.UppdateGrid(4, 4 + lowestX+term2f, 4 + term1f, 4 + lowestX+term2f));
            seq.Join(line1.FadeIn());
            seq.Join(line1.ChangeLength(term1f));
        }
        
        seq.Append(artLineWN1.FadeIn());
        seq.Join(gridzooming.UppdateGrid(4, 4 + lowestX+term2f, 4 + term1f, 4 + lowestX+term2f + 1));

        seq.Append(artLine1.FadeIn());
        seq.Join(artLine1.ChangeLength(Mathf.Sqrt((term1f*term1f)+1)));

        seq.Append(artLineWN1.FadeOut());
        seq.AppendCallback(() =>{Destroy(artLineWN1.gameObject);});

        seq.Append(gridzooming.UppdateGrid(4, 4 + lowestX, 4 + term1f, 4 + lowestX+term2f + 1));
        seq.Join(line2.FadeIn());
        seq.Join(line2.ChangeLength(term2f));
        
        lines.Add(line1);
        LineTransformations currentLine1;

        for (int i=1; i < term2f; i++)
        {
            currentLine1 = Instantiate(linePrefab, transform);
            currentLine1.transform.localPosition = new Vector3(20, 20 + 5*(lowestX+term2f) - 5*i, -1);
            currentLine1.length = term1f;
            currentLine1.color = color1;
            currentLine1.color.a = 0;
            currentLine1.UpdateColors();
            lines.Add(currentLine1);
        }
        int j = 0;
        for (int i=0; i < term2f; i++)
        {
            seq.AppendCallback(() => {
                lines[j].color.a = 1;
                lines[j].UpdateColors();
                j++;
            });
            seq.Append(artLine1.ChangePosition(new Vector3(20 + 5*term1f, 20 + 5*(lowestX+term2f) -5*(i+1), -1)));
            seq.Join(lines[i].ChangePosition(new Vector3(20, 20 + 5*(lowestX+term2f) -5*(i+1), -1)));
        }
        seq.Append(artLine1.FadeOut());
        seq.Join(line2.FadeOut());
        seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f, 4 + lowestX+term2f));

        seq.AppendCallback(() =>{
            Destroy(artLine1.gameObject);
            Destroy(line2.gameObject);
        });
        
        seq.Append(lines[^1].ChangePosition(new Vector3(20, 20, -1)));
        if (term1f <= 0) seq.Join(gridzooming.UppdateGrid(4, 4 + term1f, 4 + term1f, 4 + lowestX+term2f-1));

        for (int i=0; i < term2f-1; i++)
        {seq.Join(lines[^(i+2)].ChangePosition(new Vector3(20, 20 + 5*lowestX + 5*i, -1)));}
        
        seq.Append(artLineWN2.FadeIn());
        seq.Append(artLine2.FadeIn());
        if (term1f > 0) seq.Join(artLine2.ChangeLength(Mathf.Sqrt((term1f*term1f)+1)));
        else seq.Join(artLine2.ChangeLength(-Mathf.Sqrt((term1f*term1f)+1)));

        seq.Append(lines[^2].ChangePosition(new Vector3(20, 20, -1)));
        seq.Join(lines[^2].ChangeAngle(90));

        if (term1f <= 0) seq.Join(gridzooming.UppdateGrid(4, 4 + term1f, 4 + term1f, 4 + lowestX+term2f-2));

        for (int i=0; i < term2f-2; i++)
        {seq.Join(lines[^(i+3)].ChangePosition(new Vector3(20, 20 + 5*lowestX + 5*i, -1)));}        
        
        if (term1f > 0) seq.Append(artLine2.ChangeLength(term1f*Mathf.Sqrt((term1f*term1f)+1)));
        else seq.Append(artLine2.ChangeLength(-term1f*Mathf.Sqrt((term1f*term1f)+1)));
        seq.Join(artLine2.ChangePosition(new Vector3(20 + term1f*term1f*5, 20, -1)));

        seq.Join(lines[^1].ChangeLength(term1f*term1f));
        seq.Join(lines[^1].ChangeColor(colorfractions[0]));
        if (term1f > 0) seq.Join(gridzooming.UppdateGrid(4, 4, 4 + term1f*term1f, 4 + lowestX+term2f-2));
        else seq.Join(gridzooming.UppdateGrid(4, 4 + term1f, 4 + term1f*term1f, 4 + lowestX+term2f-2));

        seq.Append(lines[^2].FadeOut());
        seq.AppendCallback(() => Destroy(lines[^2].gameObject));
        
        seq.Append(artLine2.ChangeAngle(180 - Mathf.Atan(1/(term1f*term1f)) * Mathf.Rad2Deg));
        seq.Join(artLine2.ChangeLength(Mathf.Sqrt(Mathf.Pow(term1f, 4)+1)));

        for (int i=0; i < term2f-2; i++)
        {
            if (i != 0)
            {
                seq.Append(artLine2.ChangeAngle(180 - Mathf.Atan(1/Mathf.Pow(term1f, i+2)) * Mathf.Rad2Deg));
                if (term1f > 0) seq.Join(artLine2.ChangeLength(Mathf.Sqrt(Mathf.Pow(term1f, 2*i+4)+1)));
                else if (i%2 == 0) seq.Join(artLine2.ChangeLength(Mathf.Sqrt(Mathf.Pow(term1f, 2*i+4)+1)));
                else seq.Join(artLine2.ChangeLength(-Mathf.Sqrt(Mathf.Pow(term1f, 2*i+4)+1)));
            }

            seq.Append(lines[^(i+3)].ChangeAngle(90));
            seq.Join(lines[^(i+3)].ChangePosition(new Vector3(20, 20, -1)));
            for (int k=i+4; k <= term2f; k++)
            {seq.Join(lines[^k].ChangePosition(new Vector3(20, 20 + 5*lowestX + 5*(k-(i+4)), -1)));}
            
            if (term1f <= 0 && i%2 != 0) seq.Append(artLine2.ChangeLength(-term1f*Mathf.Sqrt(Mathf.Pow(term1f, 2*i+4)+1)));
            else seq.Append(artLine2.ChangeLength(term1f*Mathf.Sqrt(Mathf.Pow(term1f, 2*i+4)+1)));
            seq.Join(artLine2.ChangePosition(new Vector3(20 + Mathf.Pow(term1f, i+3)*5, 20, -1)));
            seq.Join(lines[^1].ChangeLength(Mathf.Pow(term1f, i+3)));
            seq.Join(lines[^1].ChangeColor(colorfractions[i+1]));
            if (term1f > 0) seq.Join(gridzooming.UppdateGrid(4, 4, 4 + Mathf.Pow(term1f, i+3), 4 + lowestX+term2f-(i+4)));
            else seq.Join(gridzooming.UppdateGrid(4, 4 + term1f, 4 + Mathf.Pow(term1f, i+3), 4 + lowestX+term2f-(i+4)));

            seq.Append(lines[^(i+3)].FadeOut());
        }
        seq.Append(artLine2.FadeOut());
        seq.Join(artLineWN2.FadeOut());
        seq.Join(gridzooming.UppdateGrid(4, 4, 4 + Mathf.Pow(term1f, term2f), 4));

        seq.Join(animationControl.FinishCalculation("^"));

        seq.AppendCallback(() => {
            foreach(LineTransformations l in lines) if (l != null && l != lines[^1]) Destroy(l.gameObject);
            Destroy(artLine2.gameObject);
            Destroy(artLineWN2.gameObject);
        }); 

        seq.OnComplete(() => animationControl.StartCalculation());
        seq.Play();
        return lines[^1];
    }
}