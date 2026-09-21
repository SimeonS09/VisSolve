using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
public class RootAnimation : MonoBehaviour
{
    public LineTransformations linePrefab;
    public TriangleTransformations trianglePrefab;

    public LineTransformations line1 = null;
    public LineTransformations line2 = null;
    public LineTransformations line3 = null;
    public TriangleTransformations triangle = null;
    public List<TriangleTransformations> triangles = new List<TriangleTransformations>();

    private bool zoomingIn = true;
    public AnimationControl animationControl;
    public AnzeigeAnimation anzeigeAnimation;
    public Gridzooming gridzooming;

    public Sequence seq;

    public LineTransformations StartAnimation(decimal term1, Color color1, decimal term2, Color color2, LineTransformations currentVisLine, Color color3)
    {
        print("root");
        
        triangles.Clear();
        seq = DOTween.Sequence();

        float term1f = (float)term2;
        float term2f = (float)term1;
        (color1, color2) = (color2, color1);

        if (term1f == 0)
        {
            seq.Append(gridzooming.UppdateGrid(4, 4, 4, 4));

            if (currentVisLine == null)
            {
                line1 = Instantiate(linePrefab, transform);
                line1.transform.localPosition = new Vector3(20, 20, -1);
                line1.color = color1;
                line1.color.a = 0;
                line1.UpdateColors();
                seq.Join(line1.FadeIn());
            }
            else 
            {
                line1 = currentVisLine;
                if (line1.transform.localPosition != new Vector3(20, 20, -1) || line1.angle != 0)
                {
                    seq.Join(line1.ChangePosition(new Vector3(20, 20, -1)));
                    seq.Join(line1.ChangeAngle(0));
                }
            }
            seq.AppendInterval(1f);

            line3 = Instantiate(linePrefab, transform);
            line3.transform.localPosition = new Vector3(20, 20, -1);
            line3.length = 0;
            line3.angle = 0;
            line3.color = color3;
            line3.color.a = 0;
            line3.UpdateColors();
            seq.Append(line3.FadeIn());
        }


        else
        {
            //positiv
            if (term1f > 0)
            {
                float fakeAngle;
                if (term1f >= 1) fakeAngle = 30;
                else fakeAngle = 50;
                //unterscheided zwischen zwei Fällen für die Animation
                if (term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f) < 1) zoomingIn = false;
                else zoomingIn = true;

                if (currentVisLine == null)
                {
                    line1 = Instantiate(linePrefab, transform);
                    line1.transform.localPosition = new Vector3(20 - 5f*term1f, 20, -1);
                    line1.color = color1;
                    line1.color.a = 0;
                    line1.UpdateColors();
                }
                else 
                {
                    line1 = currentVisLine;
                    if (line1.transform.localPosition != new Vector3(20 - 5f*term1f, 20, -1) || line1.angle != 0)
                    {
                        seq.Append(line1.ChangePosition(new Vector3(20 - 5f*term1f, 20, -1)));
                        seq.Join(gridzooming.UppdateGrid(4 - term1f, 4, 4, 4));
                        seq.Join(line1.ChangeAngle(0));
                    }
                }

                triangle = Instantiate(trianglePrefab, transform);
                triangle.transform.localPosition = new Vector3(20 - 5f*term1f, 20, -1);
                triangle.length = term1f;
                triangle.triangleangle = fakeAngle;
                triangle.aColor = color2;
                triangle.color.a = 0;
                triangle.UpdateColors();

                if (currentVisLine == null)
                { 
                    seq.Append(gridzooming.UppdateGrid(4 - term1f, 4, 4, 4));
                    seq.Join(line1.FadeIn());
                    seq.Join(line1.ChangeLength(term1f));
                }
                
                seq.Append(triangle.FadeIn());
                seq.Join(gridzooming.UppdateGrid(4 - term1f, 4 + term1f * Mathf.Tan(fakeAngle*Mathf.Deg2Rad), 4, 4));

                triangles.Add(triangle);

                for (int i=0; i<term2f-1; i++)
                {
                    triangle = Instantiate(trianglePrefab, transform);
                    if (i %4 == 0) triangle.transform.localPosition = new Vector3(20 - 5f*term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 20, -1);
                    else if (i %4 == 1) triangle.transform.localPosition = new Vector3(20, 20 + 5f*term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), -1);
                    else if (i %4 == 2) triangle.transform.localPosition = new Vector3(20 + 5f*term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 20, -1);
                    else triangle.transform.localPosition = new Vector3(20, 20 - 5f*term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), -1);
                    triangle.length = term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i);
                    triangle.triangleangle = fakeAngle;
                    float doAngle = i*-90;
                    while (doAngle < 0) doAngle += 360;
                    triangle.angle = doAngle%360;
                    triangle.aColor = color2;
                    triangle.color.a = 0;
                    triangle.UpdateColors();

                    triangles.Add(triangle);
                }
                seq.Append(triangles[0].FadeIn());
                int j = 1;
                for (int i=1; i < term2f; i++)
                {
                    seq.AppendCallback(() => {
                    triangles[j].color.a = 1;
                    triangles[j].UpdateColors();
                    j++;});

                    if (i %4 == 1) 
                    {
                        seq.Append(triangles[i].ChangePosition(new Vector3(20, 20 + 5f*term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), -1)));
                        seq.Join(gridzooming.UppdateGrid(4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i + 1), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i-1), 4));
                    }
                    else if (i %4 == 2)
                    {
                        seq.Append(triangles[i].ChangePosition(new Vector3(20 + 5f*term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 20, -1)));
                        seq.Join(gridzooming.UppdateGrid(4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i + 1), 4, 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i-1)));
                    } 
                    else if (i %4 == 3)
                    {
                        seq.Append(triangles[i].ChangePosition(new Vector3(20, 20 - 5f*term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), -1)));
                        seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i + 1), 4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i-1), 4));
                    }
                    else
                    {
                        seq.Append(triangles[i].ChangePosition(new Vector3(20 - 5f*term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 20, -1)));
                        seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i + 1), 4, 4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i-1)));
                    } 
                    seq.Join(triangles[i].ChangeLength(term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i)));
                    seq.Join(triangles[i].ChangeAngle(i*-90));
                }
                
                line2 = Instantiate(linePrefab, transform);
                line2.transform.localPosition = new Vector3(20, 20, -1);
                line2.length = term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f);
                float angle = 180 - term2f*90;
                while (angle < 0) angle += 360;
                line2.angle =  angle%360;
                line2.color = color2;
                line2.color.a = 0;
                line2.UpdateColors();

                seq.Append(line2.FadeIn());
                seq.AppendInterval(1f);

                float newAngle = Mathf.Atan(1/Mathf.Pow(term1f, 1/term2f))*Mathf.Rad2Deg;

                if (zoomingIn)
                {
                    print("in");
                    if (term1f >= 1) seq.Append(gridzooming.UppdateGrid(4 - term1f, 4 - term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), 3), 4 + term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), 2), 4 + term1f*Mathf.Tan(fakeAngle*Mathf.Deg2Rad)));
                    else
                    {
                        if (term2f %4 == 0)      seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-0), 4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-1), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-2), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-3)));
                        else if (term2f %4 == 1) seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-1), 4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-2), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-3), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-0)));
                        else if (term2f %4 == 2) seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-2), 4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-3), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-0), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-1)));
                        else                     seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-3), 4 - term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-0), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-1), 4 + term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-2)));
                    }
                }
                else
                {
                    print("out");
                    if (term1f >= 1) seq.Append(gridzooming.UppdateGrid(4 - term1f, 4 - term1f*Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), 3), 4 + term1f*Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), 2), 4 + term1f*Mathf.Tan(newAngle*Mathf.Deg2Rad)));
                    else
                    {
                        if (term2f %4 == 0)      seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-0), 4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-1), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-2), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-3)));
                        else if (term2f %4 == 1) seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-1), 4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-2), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-3), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-0)));
                        else if (term2f %4 == 2) seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-2), 4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-3), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-0), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-1)));
                        else                     seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-3), 4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-0), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-1), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-2)));
                    }
                }

                seq.AppendInterval(1f);

                seq.Join(triangles[0].ChangeTriangleAngle(newAngle).OnUpdate(() =>
                    {
                        for (int j=0; j < term2f; j++)
                        {
                            if (j %4 == 1) triangles[j].transform.localPosition = new Vector3(20, 20 + 5f*term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j), -1);
                            else if (j %4 == 2) triangles[j].transform.localPosition = new Vector3(20 + 5f*term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j), 20, -1);
                            else if (j %4 == 3) triangles[j].transform.localPosition = new Vector3(20, 20 - 5f*term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j), -1);
                            else triangles[j].transform.localPosition = new Vector3(20 - 5f*term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j), 20, -1);

                            triangles[j].length = term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j);
                            line2.length = term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), term2f);
                        }
                        line2.UpdateColors();
                    }));
                for (int i=1; i < term2f; i++) seq.Join(triangles[i].ChangeTriangleAngle(newAngle));

                seq.AppendCallback(() =>
                    {   
                        line2.length = 1;
                        line2.UpdateColors();
                    });
                
                line3 = Instantiate(linePrefab, transform);
                line3.transform.localPosition = new Vector3(20, 20, -1);
                line3.length = Mathf.Pow(term1f, 1/term2f);
                line3.angle = (angle + 90)%360;
                line3.color = color3;
                line3.color.a = 0;
                line3.UpdateColors();

                seq.Append(line3.FadeIn());
                seq.Append(line1.FadeOut());
                seq.Join(triangles[0].FadeOut());
                seq.AppendInterval(1f);

                for (int i=1; i < term2f-1; i++)
                {
                    seq.Append(triangles[i].FadeOut());
                    if (i %4 == 0) seq.Join(gridzooming.UppdateGrid(4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+2), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+1), 4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i), 4));
                    else if (i %4 == 1) seq.Join(gridzooming.UppdateGrid(4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+1), 4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+2), 4, 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i)));
                    else if (i %4 == 2) seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+1), 4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+2), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i), 4));
                    else seq.Join(gridzooming.UppdateGrid(4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+1), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+2), 4, 4 - term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i)));
                }

                seq.Append(triangles[(int)term2f-1].FadeOut());            

                seq.Append(line2.FadeOut());
                if (term2f %4 == 0) seq.Join(gridzooming.UppdateGrid(4,4 - Mathf.Pow(term1f, 1/term2f),4,4));
                else if (term2f %4 == 1) seq.Join(gridzooming.UppdateGrid(4 - Mathf.Pow(term1f, 1/term2f),4,4,4));
                else if (term2f %4 == 2) seq.Join(gridzooming.UppdateGrid(4,4 + Mathf.Pow(term1f, 1/term2f),4,4));
                else seq.Join(gridzooming.UppdateGrid(4 + Mathf.Pow(term1f, 1/term2f),4,4,4));
            }



            //negativ

            else
            {
                float fakeAngle;
                if (-term1f >= 1) fakeAngle = 30;
                else fakeAngle = 50;
                //unterscheided zwischen zwei Fällen für die Animation
                if (-term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f) < 1) zoomingIn = false;
                else zoomingIn = true;


                if (currentVisLine == null)
                {
                    line1 = Instantiate(linePrefab, transform);
                    line1.transform.localPosition = new Vector3(20, 20, -1);
                    line1.color = color1;
                    line1.color.a = 0;
                    line1.UpdateColors();
                }
                else 
                {
                    line1 = currentVisLine;
                    if (line1.transform.localPosition != new Vector3(20, 20, -1) || line1.angle != 0)
                    {
                        seq.Append(line1.ChangePosition(new Vector3(20, 20, -1)));
                        seq.Join(gridzooming.UppdateGrid(4 - -term1f, 4, 4, 4));
                        seq.Join(line1.ChangeAngle(0));
                    }
                }

                triangle = Instantiate(trianglePrefab, transform);
                triangle.transform.localPosition = new Vector3(20 - 5f*-term1f, 20, -1);
                triangle.length = -term1f;
                triangle.triangleangle = fakeAngle;
                triangle.aColor = color2;
                triangle.color.a = 0;
                triangle.UpdateColors();

                if (currentVisLine == null)
                { 
                    seq.Append(gridzooming.UppdateGrid(4 - -term1f, 4, 4, 4));
                    seq.Join(line1.FadeIn());
                    seq.Join(line1.ChangeLength(term1f));
                }
                
                seq.Append(triangle.FadeIn());
                seq.Join(gridzooming.UppdateGrid(4 - -term1f, 4 + -term1f * Mathf.Tan(fakeAngle*Mathf.Deg2Rad), 4, 4));

                triangles.Add(triangle);

                for (int i=0; i<term2f-1; i++)
                {
                    triangle = Instantiate(trianglePrefab, transform);
                    if (i %4 == 0) triangle.transform.localPosition = new Vector3(20 - 5f*-term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 20, -1);
                    else if (i %4 == 1) triangle.transform.localPosition = new Vector3(20, 20 + 5f*-term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), -1);
                    else if (i %4 == 2) triangle.transform.localPosition = new Vector3(20 + 5f*-term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 20, -1);
                    else triangle.transform.localPosition = new Vector3(20, 20 - 5f*-term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), -1);
                    triangle.length = -term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i);
                    triangle.triangleangle = fakeAngle;
                    float doAngle = i*-90;
                    while (doAngle < 0) doAngle += 360;
                    triangle.angle = doAngle%360;
                    triangle.aColor = color2;
                    triangle.color.a = 0;
                    triangle.UpdateColors();

                    triangles.Add(triangle);
                }
                seq.Append(triangles[0].FadeIn());
                int j = 1;
                for (int i=1; i < term2f; i++)
                {
                    seq.AppendCallback(() => {
                    triangles[j].color.a = 1;
                    triangles[j].UpdateColors();
                    j++;});

                    if (i %4 == 1) 
                    {
                        seq.Append(triangles[i].ChangePosition(new Vector3(20, 20 + 5f*-term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), -1)));
                        seq.Join(gridzooming.UppdateGrid(4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i + 1), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i-1), 4));
                    }
                    else if (i %4 == 2)
                    {
                        seq.Append(triangles[i].ChangePosition(new Vector3(20 + 5f*-term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 20, -1)));
                        seq.Join(gridzooming.UppdateGrid(4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i + 1), 4, 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i-1)));
                    } 
                    else if (i %4 == 3)
                    {
                        seq.Append(triangles[i].ChangePosition(new Vector3(20, 20 - 5f*-term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), -1)));
                        seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i + 1), 4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i-1), 4));
                    }
                    else
                    {
                        seq.Append(triangles[i].ChangePosition(new Vector3(20 - 5f*-term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 20, -1)));
                        seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i + 1), 4, 4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i-1)));
                    } 
                    seq.Join(triangles[i].ChangeLength(-term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), i)));
                    seq.Join(triangles[i].ChangeAngle(i*-90));
                }
                
                line2 = Instantiate(linePrefab, transform);
                line2.transform.localPosition = new Vector3(20, 20, -1);
                line2.length = -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f);
                float angle = 180 -term2f*90;
                while (angle < 0) angle += 360;
                line2.angle =  angle%360;
                line2.color = color2;
                line2.color.a = 0;
                line2.UpdateColors();

                seq.Append(line2.FadeIn());
                seq.AppendInterval(1f);

                float newAngle = Mathf.Atan(1/Mathf.Pow(-term1f, 1/term2f))*Mathf.Rad2Deg;

                print(zoomingIn);
                if (zoomingIn)
                {
                    if (-term1f >= 1) seq.Append(gridzooming.UppdateGrid(4 - -term1f, 4 - -term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), 3), 4 + -term1f*Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), 2), 4 + -term1f*Mathf.Tan(fakeAngle*Mathf.Deg2Rad)));
                    else
                    {
                        if (term2f %4 == 0)      seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-0), 4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-1), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-2), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-3)));
                        else if (term2f %4 == 1) seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-1), 4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-2), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-3), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-0)));
                        else if (term2f %4 == 2) seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-2), 4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-3), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-0), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-1)));
                        else                     seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-3), 4 - -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-0), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-1), 4 + -term1f * Mathf.Pow(Mathf.Tan(fakeAngle*Mathf.Deg2Rad), term2f-2)));
                    }
                }
                else
                {
                    if (-term1f >= 1) seq.Append(gridzooming.UppdateGrid(4 - -term1f, 4 - -term1f*Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), 3), 4 + -term1f*Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), 2), 4 + -term1f*Mathf.Tan(newAngle*Mathf.Deg2Rad)));
                    else
                    {
                        if (term2f %4 == 0)      seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-0), 4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-1), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-2), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-3)));
                        else if (term2f %4 == 1) seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-1), 4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-2), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-3), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-0)));
                        else if (term2f %4 == 2) seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-2), 4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-3), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-0), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-1)));
                        else                     seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-3), 4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-0), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-1), 4 + term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), term2f-2)));
                    }
                }

                seq.AppendInterval(1f);

                
                seq.Join(triangles[0].ChangeTriangleAngle(newAngle).OnUpdate(() =>
                    {
                        for (int j=0; j < term2f; j++)
                        {
                            if (j %4 == 1) triangles[j].transform.localPosition = new Vector3(20, 20 + 5f*-term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j), -1);
                            else if (j %4 == 2) triangles[j].transform.localPosition = new Vector3(20 + 5f*-term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j), 20, -1);
                            else if (j %4 == 3) triangles[j].transform.localPosition = new Vector3(20, 20 - 5f*-term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j), -1);
                            else triangles[j].transform.localPosition = new Vector3(20 - 5f*-term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j), 20, -1);

                            triangles[j].length = -term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), j);
                            line2.length = -term1f * Mathf.Pow(Mathf.Tan(triangles[0].triangleangle*Mathf.Deg2Rad), term2f);
                        }
                        line2.UpdateColors();
                    }));
                for (int i=1; i < term2f; i++) seq.Join(triangles[i].ChangeTriangleAngle(newAngle));

                seq.AppendCallback(() =>
                    {   
                        line2.length = 1;
                        line2.UpdateColors();
                    });
                
                line3 = Instantiate(linePrefab, transform);
                line3.transform.localPosition = new Vector3(20, 20, -1);
                line3.length = -Mathf.Pow(-term1f, 1/term2f);
                line3.angle = (angle - 90)%360;
                line3.color = color3;
                line3.color.a = 0;
                line3.UpdateColors();

                seq.Append(line3.FadeIn());
                seq.Append(line1.FadeOut());
                seq.Join(triangles[0].FadeOut());
                seq.AppendInterval(1f);

                for (int i=1; i < term2f-1; i++)
                {
                    seq.Append(triangles[i].FadeOut());
                    if (i %4 == 0) seq.Join(gridzooming.UppdateGrid(4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+2), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+1), 4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i), 4));
                    else if (i %4 == 1) seq.Join(gridzooming.UppdateGrid(4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+1), 4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+2), 4, 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i)));
                    else if (i %4 == 2) seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+1), 4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+2), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i), 4));
                    else seq.Join(gridzooming.UppdateGrid(4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+1), 4 + -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i+2), 4, 4 - -term1f * Mathf.Pow(Mathf.Tan(newAngle*Mathf.Deg2Rad), i)));
                }

                seq.Append(triangles[(int)term2f-1].FadeOut());

                seq.Append(line2.FadeOut());
                if (term2f %4 == 0) seq.Join(gridzooming.UppdateGrid(4, 4 - Mathf.Pow(-term1f, 1/term2f),4,4));
                else if (term2f %4 == 1) seq.Join(gridzooming.UppdateGrid(4 - Mathf.Pow(-term1f, 1/term2f),4,4,4));
                else if (term2f %4 == 2) seq.Join(gridzooming.UppdateGrid(4,4 + Mathf.Pow(-term1f, 1/term2f),4,4));
                else seq.Join(gridzooming.UppdateGrid(4 + Mathf.Pow(-term1f, 1/term2f),4,4,4));
            }
        }

        seq.Join(animationControl.FinishCalculation("%"));
        seq.AppendCallback(() => 
        {
            if (line1 != null) Destroy(line1.gameObject);
            if (line2 != null) Destroy(line2.gameObject);
            foreach(TriangleTransformations t in triangles) if (t != null) Destroy(t.gameObject);
        });
        
        seq.OnComplete(() => animationControl.StartCalculation());
        seq.Play();
    
        return line3;
    }
}