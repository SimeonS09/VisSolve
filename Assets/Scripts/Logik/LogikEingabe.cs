using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class LogikEingabe : MonoBehaviour
{
    public AnzeigeEingabe anzeigeEingabe;
    public AnimationControl animationControl;

    public List<object> calculation;
    public List<object> PostFixCalculation;

    public void StartConversion(string calculaitonStr)
    {
        calculation = TurnIntoInfix(calculaitonStr);
        PostFixCalculation = TurnIntoPostfix(calculation);
        string output = "";
        foreach (var item in PostFixCalculation) output += item + " ";
        Debug.Log("Postfixcal: " + output);

        if (!anzeigeEingabe.CheckForErrors(PostFixCalculation))
        {
            //Blockiert Alle Tasten
            anzeigeEingabe.UpdateButtons(true);
            animationControl.StartAnimation();
        }
        else print("error");
    }

    private List<object> TurnIntoInfix(string calculation)
    {
        List<object> newCal = new List<object>();
        decimal currentNumber = 0;

        bool isBuildingNumber = false;
        bool isNegative = false;
        bool hasPoint = false;

        decimal digitfactor = 0.1m;

        for(int i = 0; i < calculation.Length; i++)
        {
            if (calculation[i] == '(') 
            {
                newCal.Add("(");
            }
            else if (calculation[i] == ')') 
            {
                if (isBuildingNumber)
                {
                    newCal.Add(currentNumber);
                    currentNumber = 0;
                    isBuildingNumber = false;
                    isNegative = false;
                    hasPoint = false;
                    digitfactor = 0.1m;
                }
                newCal.Add(")");
            }
            else if (calculation[i] == ' ')
            {
                if (isBuildingNumber)
                {
                    newCal.Add(currentNumber);
                    currentNumber = 0;
                    isBuildingNumber = false;
                    isNegative = false;
                    hasPoint = false;
                    digitfactor = 0.1m;
                }
                newCal.Add(calculation[i+1].ToString());
                i += 2;
            }
            else if (calculation[i] == '.') hasPoint = true;
            else if (calculation[i] == '-') 
            {
                isNegative = true;
                isBuildingNumber = true;
            }
            else if (calculation[i] == '<' && calculation[i+1] == 'v') //Potenz
            {
                if (isBuildingNumber)
                {
                    newCal.Add(currentNumber);
                    currentNumber = 0;
                    isBuildingNumber = false;
                    isNegative = false;
                    hasPoint = false;
                    digitfactor = 0.1m;
                }
                newCal.Add("^");
                newCal.Add(decimal.Parse(calculation[i+25].ToString()));
                i += 42;
            }
            else if (calculation[i] == '√')
            {
                newCal.Add((decimal)2);
                newCal.Add("%");
            }
            
            else if (calculation[i] == '<' && calculation[i+1] == 's') //nte-Wurzel
            {
                newCal.Add(decimal.Parse(calculation[i+5].ToString()));
                newCal.Add("%");
                i += 23;
            }
            else
            {
                if (isBuildingNumber)
                {
                    if (!hasPoint)
                    {
                        if (!isNegative)
                        {
                            currentNumber = currentNumber * 10m + decimal.Parse(calculation[i].ToString());
                        }
                        else
                        {
                            currentNumber = currentNumber * 10m - decimal.Parse(calculation[i].ToString());
                        }
                    }
                    else
                    {
                        if (!isNegative)
                        {
                            currentNumber = currentNumber + digitfactor * decimal.Parse(calculation[i].ToString());
                        }
                        else
                        {
                            currentNumber = currentNumber - digitfactor * decimal.Parse(calculation[i].ToString());
                        }
                        digitfactor *= 0.1m;
                    }
                }
                else
                {
                    currentNumber = decimal.Parse(calculation[i].ToString());
                    isBuildingNumber = true;
                }
            }
        }
        if (isBuildingNumber) newCal.Add(currentNumber);
        return newCal;
    }

    private List<object> TurnIntoPostfix(List<object> calculation)
    {
        List<object> Output = new List<object>();
        List<object> OperatorStack = new List<object>();

        List<int> ips = new List<int>();
        List<int> indexOutput = new List<int>();
        List<int> indexOperatorStack = new List<int>();

        int j = 0;
        int i = 0;

        foreach (object obj in calculation)
        {
            ips.Add(j);
            j++;
        }
        foreach (object element in calculation)
        {
            
            if (element is decimal)
            {
                Output.Add(element);
                indexOutput.Add(ips[i]);
            }
            else if (element is string str1 && str1 == "(")
            {
                OperatorStack.Add(element);
                indexOperatorStack.Add(ips[i]);
            }
            else if (element is string str2 && str2 ==  ")")
            {
                if (OperatorStack.Count > 0)
                {
                    while ((string)OperatorStack[^1] != "(")
                    {
                        Output.Add(OperatorStack[^1]);
                        indexOutput.Add(indexOperatorStack[^1]);
                        OperatorStack.RemoveAt(OperatorStack.Count -1);
                        indexOperatorStack.RemoveAt(indexOperatorStack.Count -1);

                        if (OperatorStack.Count == 0)
                        {
                            return new List<object>();
                        }
                    }
                    OperatorStack.RemoveAt(OperatorStack.Count -1);
                    indexOperatorStack.RemoveAt(indexOperatorStack.Count -1);
                }
                else
                { 
                    return new List<object>();
                }

            }
            else if (element is string str3 && (str3 == "+" || str3 ==  "-" ))
            {
                if (OperatorStack.Count > 0)
                {
                    while ((string)OperatorStack[^1] != "(" && ((string)OperatorStack[^1] == "+" || (string)OperatorStack[^1] == "-" || 
                    (string)OperatorStack[^1] == "*" || (string)OperatorStack[^1] == "/" || (string)OperatorStack[^1] == "%" || 
                    (string)OperatorStack[^1] == "^"))
                    {
                        Output.Add(OperatorStack[^1]);
                        indexOutput.Add(indexOperatorStack[^1]);
                        OperatorStack.RemoveAt(OperatorStack.Count -1);
                        indexOperatorStack.RemoveAt(indexOperatorStack.Count -1);
                        if (OperatorStack.Count == 0) break;
                    }
                }
                OperatorStack.Add(element);
                indexOperatorStack.Add(ips[i]);
            }
            else if (element is string str4 && (str4 == "*" || str4 ==  "/"))
            {
                if (OperatorStack.Count > 0)
                {
                    while ((string)OperatorStack[^1] != "(" && ((string)OperatorStack[^1] == "*" || (string)OperatorStack[^1] == "/" || 
                    (string)OperatorStack[^1] == "%" || (string)OperatorStack[^1] == "^"))
                    {
                        Output.Add(OperatorStack[^1]);
                        indexOutput.Add(indexOperatorStack[^1]);
                        OperatorStack.RemoveAt(OperatorStack.Count -1);
                        indexOperatorStack.RemoveAt(indexOperatorStack.Count -1);
                        if (OperatorStack.Count == 0) break;
                    }
                }
                OperatorStack.Add(element);
                indexOperatorStack.Add(ips[i]);
            }
            else if (element is string str5 && (str5 == "^"))
            {
                if (OperatorStack.Count > 0)
                {
                    while ((string)OperatorStack[^1] != "(" && (string)OperatorStack[^1] == "^")
                    {
                        Output.Add(OperatorStack[^1]);
                        indexOutput.Add(indexOperatorStack[^1]);
                        OperatorStack.RemoveAt(OperatorStack.Count -1);
                        indexOperatorStack.RemoveAt(indexOperatorStack.Count -1);
                        if (OperatorStack.Count == 0) break;
                    }
                }
                OperatorStack.Add(element);
                indexOperatorStack.Add(ips[i]);
            }
            else if (element is string str6 && str6 == "%")
            {
                if (OperatorStack.Count > 0)
                {
                    while ((string)OperatorStack[^1] != "(" && ((string)OperatorStack[^1] == "%" || (string)OperatorStack[^1] == "^"))
                    {
                        Output.Add(OperatorStack[^1]);
                        indexOutput.Add(indexOperatorStack[^1]);
                        OperatorStack.RemoveAt(OperatorStack.Count -1);
                        indexOperatorStack.RemoveAt(indexOperatorStack.Count -1);
                        if (OperatorStack.Count == 0) break;
                    }
                }
                OperatorStack.Add(element);
                indexOperatorStack.Add(ips[i]);
            }
            i += 1;
        }

        while (OperatorStack.Count > 0)
        {   
            Output.Add(OperatorStack[^1]);
            indexOutput.Add(indexOperatorStack[^1]);
            OperatorStack.RemoveAt(OperatorStack.Count -1);
            indexOperatorStack.RemoveAt(indexOperatorStack.Count -1);
        }
        animationControl.notationTranslation = indexOutput;
        return Output;
    }
}