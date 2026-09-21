using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

public class AnimationControl : MonoBehaviour
{
    public AdditionAnimation additionAnimation;
    public SubstractionAnimation substractionAnimation;
    public MultiplicationAnimation multiplicationAnimation;
    public DivisionAnimation divisionAnimation;
    public PowerAnimation powerAnimation;
    public RootAnimation rootAnimation;
    public LogikEingabe logikEingabe;
    public AnzeigeEingabe anzeigeEingabe;
    public AnzeigeAnimation anzeigeAnimation;
    public List<object> PostFixCalculation; 
    public List<int> notationTranslation;
    public List<Color> colorList;
    public int colorListIndex;
    private LineTransformations currentVisableLine = null;
    private int currentVisableLinePosition;
    private int currentIndex;
    public float fontSize;
    public Tween t;

    void Update()
    {
        if (Generaldata.calculationIsRunning) DOTween.timeScale = Generaldata.calculationSpeed;
        else DOTween.timeScale = 1;
    }

    public void StartAnimation()
    {
        Generaldata.calculationIsRunning = true;
        PostFixCalculation = logikEingabe.PostFixCalculation;

        fontSize = anzeigeEingabe.ausgabeText.fontSize;

        colorList = PickColors(PostFixCalculation.Count);
        colorListIndex = 0;

        currentVisableLine = null;
        currentVisableLinePosition = 1;

        anzeigeAnimation.CreateTokens(logikEingabe.calculation);
        StartCalculation();
    }

    public void StartCalculation()
    {
        for (int i = 0; i < PostFixCalculation.Count; i++)
        {
            if (PostFixCalculation[i] is string s0 && 
            (s0 == "+" || s0 == "-" || s0 == "*" || s0 == "/" || s0 == "^" || s0 == "%") &&
            (i - 1 == currentIndex - 2))
            {
                currentVisableLinePosition = 2;
                anzeigeAnimation.AutoBracketDelete();
                return;
            }
            else if (PostFixCalculation[i] is string s1 && 
            (s1 == "+" || s1 == "-" || s1 == "*" || s1 == "/" || s1 == "^" || s1 == "%") &&
            (i - 2 == currentIndex - 2))
            {
                currentVisableLinePosition = 1;
                anzeigeAnimation.AutoBracketDelete();
                return;
            }
        }
        if (currentVisableLine != null)
        {
            t = currentVisableLine.FadeOut().OnComplete(() => {
            if (currentVisableLine != null)
            Destroy(currentVisableLine.gameObject);
            currentVisableLine = null;
            anzeigeAnimation.AutoBracketDelete();
            });
            t.Play();
        }
        else anzeigeAnimation.AutoBracketDelete();
    }

    public void NextCalculation()
    {
        if (PostFixCalculation.Count == 1)
        {
            Generaldata.calculationIsRunning = false;
            anzeigeAnimation.ColorToken(notationTranslation[0], Color.white);
            anzeigeEingabe.ResetCalculation((decimal)PostFixCalculation[0]);
            return;
        }
        for (int i = 0; i < PostFixCalculation.Count; i++)
        {
            if (PostFixCalculation[i] is string s0 && s0 == "+")
            {
                Color c1;
                Color c2;
                (c1, c2) = UpdateColors(i);
                
                currentIndex = i;
                if (currentVisableLinePosition == 1)
                {
                    currentVisableLine = additionAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1,
                    currentVisableLine, (decimal)PostFixCalculation[i-1], c2, null, colorList[colorListIndex]);
                }
                else
                {
                    currentVisableLine = additionAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1, 
                    null, (decimal)PostFixCalculation[i-1], c2, currentVisableLine, colorList[colorListIndex]);
                }
                colorListIndex++;
                
                return;
            }
            else if (PostFixCalculation[i] is string s1 && s1 == "-")
            {
                Color c1;
                Color c2;
                (c1, c2) = UpdateColors(i);
                
                currentIndex = i;
                if (currentVisableLinePosition == 1)
                {
                    currentVisableLine = substractionAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1,
                    currentVisableLine, (decimal)PostFixCalculation[i-1], c2, null, colorList[colorListIndex]);
                }
                else
                {
                    currentVisableLine = substractionAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1, 
                    null, (decimal)PostFixCalculation[i-1], c2, currentVisableLine, colorList[colorListIndex]);
                }
                colorListIndex++;
                
                return;
            }
            else if (PostFixCalculation[i] is string s2 && s2 == "*")
            {
                Color c1;
                Color c2;
                (c1, c2) = UpdateColors(i);
                
                currentIndex = i;
                if (currentVisableLinePosition == 1)
                {
                    currentVisableLine = multiplicationAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1,
                    currentVisableLine, (decimal)PostFixCalculation[i-1], c2, null, colorList[colorListIndex]);
                }
                else
                {
                    currentVisableLine = multiplicationAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1, 
                    null, (decimal)PostFixCalculation[i-1], c2, currentVisableLine, colorList[colorListIndex]);
                }
                colorListIndex++;
                
                return;
            }
            else if (PostFixCalculation[i] is string s3 && s3 == "/")
            {
                Color c1;
                Color c2;
                (c1, c2) = UpdateColors(i);
                
                currentIndex = i;
                if (currentVisableLinePosition == 1)
                {
                    currentVisableLine = divisionAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1,
                    currentVisableLine, (decimal)PostFixCalculation[i-1], c2, null, colorList[colorListIndex]);
                }
                else
                {
                    currentVisableLine = divisionAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1, 
                    null, (decimal)PostFixCalculation[i-1], c2, currentVisableLine, colorList[colorListIndex]);
                }
                colorListIndex++;
                
                return;
            }
            else if (PostFixCalculation[i] is string s4 && s4 == "^")
            {
                Color c1;
                Color c2;
                (c1, c2) = UpdateColors(i);
                
                currentIndex = i;

                currentVisableLine = powerAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1,
                currentVisableLine, (decimal)PostFixCalculation[i-1], c2, colorList[colorListIndex]);
                colorListIndex++;
                
                return;
            }
            else if (PostFixCalculation[i] is string s5 && s5 == "%")
            {
                Color c1;
                Color c2;
                (c1, c2) = UpdateColors(i);
                
                currentIndex = i;

                currentVisableLine = rootAnimation.StartAnimation((decimal)PostFixCalculation[i-2], c1,
                (decimal)PostFixCalculation[i-1], c2, currentVisableLine, colorList[colorListIndex]);
                colorListIndex++;
                
                return;
            }
            
        }
        return;   
    } 

    public Sequence FinishCalculation(string operation)
    {
        Sequence seq = DOTween.Sequence();
        
        decimal a = (decimal)PostFixCalculation[currentIndex - 2];
        decimal b = (decimal)PostFixCalculation[currentIndex - 1];

        decimal result = 0;

        if (operation == "+") result = a + b;
        else if (operation == "-") result = a - b;
        else if (operation == "*") result = a * b;
        else if (operation == "/") result = a / b;
        else if (operation == "^") result = (decimal)Mathf.Pow((float)a, (float)b);
        else if (operation == "%" && b >= 0) result = (decimal)Mathf.Pow((float)b, 1f/(float)a);
        else if (operation == "%" && b < 0) result = (decimal)-Mathf.Pow((float)-b, 1f/(float)a);
        
        PostFixCalculation[currentIndex - 2] = result;
        PostFixCalculation.RemoveAt(currentIndex);
        PostFixCalculation.RemoveAt(currentIndex-1);

        seq.Append(anzeigeAnimation.RemoveToken(notationTranslation[currentIndex]));
        seq.Join(anzeigeAnimation.RemoveToken(notationTranslation[currentIndex-1]));

        Token resultToken = new Token
        {
            obj = Math.Round((decimal)PostFixCalculation[currentIndex-2], 4, MidpointRounding.AwayFromZero),
            ip = notationTranslation[currentIndex-2],
            color = colorList[colorListIndex],
            alpha = 1,
            size = fontSize
        };

        seq.Join(anzeigeAnimation.ReplaceToken(notationTranslation[currentIndex-2], resultToken));

        seq.AppendCallback(() => {
            notationTranslation.RemoveAt(currentIndex);
            notationTranslation.RemoveAt(currentIndex-1);
        });

        return seq;
    }

    private (Color, Color) UpdateColors(int i)
    {
        Color c1 = Color.white;
        Color c2 = Color.white;
        for (int j = 0; j < anzeigeAnimation.tokenList.Count; j++)
        {
            var token = anzeigeAnimation.tokenList[j];
            if (token.ip == notationTranslation[i-2])
            {
                if (token.color == Color.white)
                {
                    anzeigeAnimation.ColorToken(notationTranslation[i-2], colorList[colorListIndex]);
                    c1 = colorList[colorListIndex];
                    colorListIndex++;
                }
                else c1 = token.color;
            }
        }
        for (int j = 0; j < anzeigeAnimation.tokenList.Count; j++)
        {
            var token = anzeigeAnimation.tokenList[j];
            if (token.ip == notationTranslation[i-1])
            {
                if (token.color == Color.white)
                {
                    anzeigeAnimation.ColorToken(notationTranslation[i-1], colorList[colorListIndex]);
                    c2 = colorList[colorListIndex];
                    colorListIndex++;
                }
                else c2 = token.color;
            }
        }
        return (c1, c2);
    }
    private List<Color> PickColors(int objCount)
    {
        //Wählt die Farben aus
        List<Color> colors = new List<Color>();
        int startingColor = Random.Range(1, 1531);
        colors.Add(TurnIntoColor(startingColor));
        for (int i = 1; i < objCount; i++)
        {
            int nextColor = (startingColor + (i * (1530/objCount))) % 1530;
            colors.Add(TurnIntoColor(nextColor));
        }

        //Mischt die Farben durch
        for (int i = objCount-1; i > 0; i--)
        {
            int j = Random.Range(0, i+1);
            (colors[i], colors[j]) = (colors[j], colors[i]);
        }
        return colors;
    }
   private Color TurnIntoColor(int colorNumber)
{
    float r = 0, g = 0, b = 0;

    if (colorNumber <= 255)
    {
        r = 255;
        g = colorNumber;
        b = 0;
    }
    else if (colorNumber <= 510)
    {
        r = 510 - colorNumber;
        g = 255;
        b = 0;
    }
    else if (colorNumber <= 765)
    {
        r = 0;
        g = 255;
        b = colorNumber - 510;
    }
    else if (colorNumber <= 1020)
    {
        r = 0;
        g = 1020 - colorNumber;
        b = 255;
    }
    else if (colorNumber <= 1275)
    {
        r = colorNumber - 1020;
        g = 0;
        b = 255;
    }
    else
    {
        r = 255;
        g = 0;
        b = 1530 - colorNumber;
    }

    return new Color(r / 255f, g / 255f, b / 255f);
    }
}
