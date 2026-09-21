using UnityEngine;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;

public class Token
{
    public object obj;
    public int ip;
    public Color color;
    public float alpha;
    public float size;
}
public class AnzeigeAnimation : MonoBehaviour
{
    public List<Token> tokenList;
    public AnzeigeEingabe anzeigeEingabe;
    public AnimationControl animationControl;

    public Sequence autoBrackSeq;

    public void CreateTokens(List<object> calculation)
    {
        tokenList = new List<Token>();
        for(int i = 0; i < calculation.Count; i++)
        {
            if (calculation[i] is decimal|| calculation[i] is string s &&(s == "(" || s == ")"))
            {
                if (i + 1 < calculation.Count) 
                {
                    if (calculation[i+1] is string sw && sw == "%")
                    {
                        if ((decimal)calculation[i] == 2)
                        {
                            tokenList.Add(new Token
                            {
                                obj = "",
                                ip = i,
                                color = Color.white,
                                alpha = 1,
                                size = animationControl.fontSize
                            });
                        }
                        else
                        {
                            tokenList.Add(new Token
                            {
                                obj = $"<sup>{calculation[i]}<space=-10></sup>",
                                ip = i,
                                color = Color.white,
                                alpha = 1,
                                size = animationControl.fontSize
                            });
                        }
                    }
                    else
                    {
                        tokenList.Add(new Token
                        {
                            obj = calculation[i],
                            ip = i,
                            color = Color.white,
                            alpha = 1,
                            size = animationControl.fontSize
                        });
                    }
                }
                else
                {
                    tokenList.Add(new Token
                    {
                        obj = calculation[i],
                        ip = i,
                        color = Color.white,
                        alpha = 1,
                        size = animationControl.fontSize
                    });
                }
                
            }
            else if (calculation[i] is string str)
            {
                if (str == "%")
                {
                    tokenList.Add(new Token
                    {
                        obj = "√",
                        ip = i,
                        color = Color.white,
                        alpha = 1,
                        size = animationControl.fontSize
                    });
                }
                else if (str == "^")
                {
                    tokenList.Add(new Token
                    {
                        obj = "",
                        ip = i,
                        color = Color.white,
                        alpha = 1,
                        size = animationControl.fontSize
                    });
                    i++;
                    tokenList.Add(new Token
                    {
                        obj = $"<voffset=0.4em><size=50%>{calculation[i]}</size></voffset>",
                        ip = i,
                        color = Color.white,
                        alpha = 1,
                        size = animationControl.fontSize
                    });
                }
                else
                {
                    tokenList.Add(new Token
                    {
                        obj = " " + str + " ",
                        ip = i,
                        color = Color.white,
                        alpha = 1,
                        size = animationControl.fontSize
                    });
                }
            }
        }
    }

    public Tween ColorToken(int ip, Color endColor)
    {
        
        for(int i = 0; i < tokenList.Count; i++)
        {
            if(ip == tokenList[i].ip)
            {
                Token t = tokenList[i];

                return DOTween.To(
                    () => t.color,
                    x => t.color = x,
                    endColor,
                    0.4f
                ).OnUpdate(UpdateAnzeige);  
            }
        }
        return null;
        
    }

    public Sequence RemoveToken(int ip)
    {
        for(int i = 0; i < tokenList.Count; i++)
        {
            if(ip == tokenList[i].ip)
            {
                int index = i;
                Sequence seq = DOTween.Sequence();
                seq.Append(DOTween.To(
                () => tokenList[index].alpha,
                x => tokenList[index].alpha = x,
                0,
                0.4f)
                .OnUpdate(UpdateAnzeige));

                seq.Append(DOTween.To(
                () => tokenList[index].size,
                x => tokenList[index].size = x,
                0,
                0.4f)
                .OnUpdate(UpdateAnzeige)
                .SetEase(Ease.InOutSine));

                seq.AppendInterval(0.5f);
                seq.AppendCallback(() => {
                    DeleteToken(ip);
                });
                return seq;
            }
        }
        return null;
    }

    public void DeleteToken(int ip)
    {
        for(int i = 0; i < tokenList.Count; i++)
        {
            if(ip == tokenList[i].ip)
            {
                tokenList.RemoveAt(i);
                UpdateAnzeige();
                return;
            }
        }
    }

    public Sequence ReplaceToken(int ip, Token newToken)
    {
        
        for(int i = 0; i < tokenList.Count; i++)
        {
            if(ip == tokenList[i].ip)
            {
                Sequence seq = DOTween.Sequence();

                seq.Append(DOTween.To(
                () => tokenList[i].alpha,
                x => tokenList[i].alpha = x,
                0,
                0.4f)
                .OnUpdate(UpdateAnzeige)
                .OnComplete(() => {
                tokenList[i] = newToken;
                tokenList[i].alpha = 0;}));

                seq.Append(DOTween.To(
                () => tokenList[i].alpha,
                x => tokenList[i].alpha = x,
                1,
                0.4f)
                .OnUpdate(UpdateAnzeige));

                return seq;
            }
        }
        return null;
    }

    public void AutoBracketDelete()
    {
        for (int i = 1; i < tokenList.Count - 1; i++)
        {
            Token left = tokenList[i - 1];
            Token middle = tokenList[i];
            Token right = tokenList[i + 1];
            Token doubleRight = null;
            if (i < tokenList.Count - 2) doubleRight = tokenList[i+2];

            if (left.obj.ToString() == "(" && right.obj.ToString() == ")" && ((doubleRight != null && doubleRight.obj.ToString() != "") || (middle.obj is decimal d && d >= 0)))
            {
                autoBrackSeq = DOTween.Sequence();
                autoBrackSeq.Append(RemoveToken(right.ip));
                autoBrackSeq.Join(RemoveToken(left.ip));
                autoBrackSeq.Play().OnComplete(() => {
                    // Sobald diese Klammern gelöscht sind, wieder weitermachen
                    AutoBracketDelete();
                });
                return; // stoppe die Schleife hier, warte auf Animation
            }
        }
        animationControl.NextCalculation();        
    }

    public void UpdateAnzeige()
    {
        var sb = new StringBuilder();
        foreach (var t in tokenList) 
        {
            Color c = t.color;
            c.a = t.alpha;
            sb.Append($"<size={t.size}>");
            sb.Append($"<color=#{ColorUtility.ToHtmlStringRGBA(c)}>");
            if (t.obj is decimal d) sb.Append(d.ToString("0.####")); 
            else sb.Append(t.obj.ToString()); 
            sb.Append("</color>");
            sb.Append("</size>");
        }
        if (Generaldata.calculationIsRunning)
        {anzeigeEingabe.Calculation = sb.ToString();}
    }
}
