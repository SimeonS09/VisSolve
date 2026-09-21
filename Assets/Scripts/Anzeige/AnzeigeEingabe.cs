using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;
using DG.Tweening;
using System.Linq;

public class CalculationStore
{
    public string Calculation;
    public bool IsBuildingNumber;
    public bool IsNegative;
    public bool HasPoint;
    public bool HasNummer;
    public int Power;
    public int RootIndex;
    public WurzelBalken RootBar;
    public int RootCount;
    public int BracketInRootCount;
    public bool EndState;
    public int BracketCount;

    public CalculationStore(string calculation, bool isBuildingNumber, bool isNegative, bool hasPoint, bool hasNummer, int power, int rootIndex, WurzelBalken rootBar, int rootCount, int bracketInRootCount, bool endState, int bracketCount)
    {
        Calculation = calculation;
        IsBuildingNumber = isBuildingNumber;
        IsNegative = isNegative;
        HasPoint = hasPoint;
        HasNummer = hasNummer;
        Power = power;
        RootIndex = rootIndex;
        RootBar = rootBar;
        RootCount = rootCount;
        BracketInRootCount = bracketInRootCount;
        EndState = endState;
        BracketCount = bracketCount;
    }
}
public class AnzeigeEingabe : MonoBehaviour
{
    public LogikEingabe logikEingabe;
    public Stopp stopp;
    public Pause pause;
    public TMP_Text ausgabeText;
    public TMP_Text errorText;

    public Button button9;
    public Button button8;
    public Button button7;
    public Button button6;
    public Button button5;
    public Button button4;
    public Button button3;
    public Button button2;
    public Button button1;
    public Button button0;

    public Button buttonPoint;

    public Button buttonMinusNum;

    public Button buttonPlus;
    public Button buttonMinus;
    public Button buttonMult;
    public Button buttonDiv;

    public Button buttonPow;
    public Button buttonRoot;

    public Button buttonOpenBracket;
    public Button buttonCloseBracket;

    public Button buttonDel;
    public Button buttonClear;
    public Button buttonEnter;
    
    public List<CalculationStore> calculationStore = new List<CalculationStore>();
    private List<(object obj, int ip, string color)> calculationInf = new List<(object, int, string)>();
    private List<char> memory = new List<char>();
    private string calculationOrigin;
    public string Calculation = "";
    private bool isBuildingNumber = false;
    private bool isNegative = false;
    private bool hasPoint = false;
    private bool hasNummer = false;
    public int power = 0;
    public int rootIndex = 0;
    public WurzelBalken rootBarPrefab;
    public WurzelBalken rootBar;
    public int rootCount;
    public int bracketInRootCount = 0;

    private decimal nummerCount;
    private decimal testNummer;
    private int bracketCount = 0;

    private decimal maxValue = 4000m;
    private decimal minValue = 0.01m;
    private decimal maxDigit = 4;

    private bool endState = false;

    private Tween t = null;

    public CalculationStore enterStore;


    void Start()
    {
        button9.onClick.AddListener(() => 
        {if (!Generaldata.keyBlock) AddNummer("9");});
        button8.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("8");});
        button7.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("7");});
        button6.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("6");});
        button5.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("5");});
        button4.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("4");});
        button3.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("3");});
        button2.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("2");});
        button1.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("1");});
        button0.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddNummer("0");});

        buttonPoint.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddPoint();});
        buttonMinusNum.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddMinusNum();});

        buttonPlus.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddOperation("+");});
        buttonMinus.onClick.AddListener(() => 
        {if (!Generaldata.keyBlock) AddOperation("-");});
        buttonMult.onClick.AddListener(() => 
        {if (!Generaldata.keyBlock) AddOperation("*");});
        buttonDiv.onClick.AddListener(() => 
        {if (!Generaldata.keyBlock) AddOperation("/");});

        buttonPow.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) IncreasePower();});
        buttonRoot.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddRoot();});

        buttonOpenBracket.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddOpenBracket();});
        buttonCloseBracket.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) AddCloseBracket();});

        buttonDel.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) Delete();});
        buttonClear.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) Clear();});
        buttonEnter.onClick.AddListener(() =>
        {if (!Generaldata.keyBlock) Enter();});

        errorText.color = new Color(1,1,1,0);
        UpdateButtons(false);
    }

    void Update()
    {
        if (!Generaldata.keyBlock)
        {
            // Tastatur
            foreach (char c in Input.inputString)
            {
                if (c == '^')
                {
                    foreach (char cp in Input.inputString)
                    {
                        if (cp == '2') AddPower(2);
                        else if (cp == '3') AddPower(3);
                        else if (cp == '4') AddPower(4);
                        else if (cp == '5') AddPower(5);
                        else if (cp == '6') AddPower(6);
                        else if (cp == '7') AddPower(7);
                        else if (cp == '8') AddPower(8);
                        else if (cp == '9') AddPower(9);
                    }
                }
                else if (c == '0') AddNummer("0");
                else if (c == '1') AddNummer("1");
                else if (c == '2') AddNummer("2");
                else if (c == '3') AddNummer("3");
                else if (c == '4') AddNummer("4");
                else if (c == '5') AddNummer("5");
                else if (c == '6') AddNummer("6");
                else if (c == '7') AddNummer("7");
                else if (c == '8') AddNummer("8");
                else if (c == '9') AddNummer("9");
                else if (c == '+') AddOperation("+");
                else if (c == '-') AddOperation("-");
                else if (c == '*') AddOperation("*");
                else if (c == '/') AddOperation("/");
                else if (c == '\'') IncreasePower();
                else if (c == '%') AddRoot();
                else if (c == '(' || c == '[') AddOpenBracket();
                else if (c == ')' || c == ']') AddCloseBracket();
                else if (c == '.' || c == ',') AddPoint();
                else if (c == '_') AddMinusNum();
                else if (c == '=') Enter();
            }
            if (Input.GetKeyDown(KeyCode.Backspace)) Delete();
            if (Input.GetKeyDown(KeyCode.Delete)) Clear();
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) 
            && Input.GetKeyDown(KeyCode.Backspace)) Clear();
            if (Input.GetKeyDown(KeyCode.Return)) Enter();

            // Keypad
            if (Input.GetKeyDown(KeyCode.Keypad0)) AddNummer("0");
            if (Input.GetKeyDown(KeyCode.Keypad1)) AddNummer("1");
            if (Input.GetKeyDown(KeyCode.Keypad2)) AddNummer("2");
            if (Input.GetKeyDown(KeyCode.Keypad3)) AddNummer("3");
            if (Input.GetKeyDown(KeyCode.Keypad4)) AddNummer("4");
            if (Input.GetKeyDown(KeyCode.Keypad5)) AddNummer("5");
            if (Input.GetKeyDown(KeyCode.Keypad6)) AddNummer("6");
            if (Input.GetKeyDown(KeyCode.Keypad7)) AddNummer("7");
            if (Input.GetKeyDown(KeyCode.Keypad8)) AddNummer("8");
            if (Input.GetKeyDown(KeyCode.Keypad9)) AddNummer("9");
            if (Input.GetKeyDown(KeyCode.KeypadPlus)) AddOperation("+");
            if (Input.GetKeyDown(KeyCode.KeypadMinus)) AddOperation("-");
            if (Input.GetKeyDown(KeyCode.KeypadMultiply)) AddOperation("*");
            if (Input.GetKeyDown(KeyCode.KeypadDivide)) AddOperation("/");
            if (Input.GetKeyDown(KeyCode.KeypadPeriod)) AddPoint();
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) Enter();
        }

        if (t != null) t.timeScale = 1f / Generaldata.calculationSpeed;

        UpdateAusgabe();  
    }

    void UpdateAusgabe()
    {
        ausgabeText.text = Calculation;
        ausgabeText.enableAutoSizing = !Generaldata.calculationIsRunning;
        Canvas.ForceUpdateCanvases();
        ausgabeText.ForceMeshUpdate();
    }

    void AddNummer(string nummer)
    {
        if (ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning)
        {
            print("number: " + nummer);
            if (endState) Clear();

            if(power == 0)
            {
                if(Calculation.Length > 0)
                {
                    if (Calculation[Calculation.Length-1] == ')')
                        {
                            // )3 -> ) * 3
                            AddOperation("*");
                        }
                }

                calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));

                if(Calculation.Length > 0)
                {
                    if (Calculation[Calculation.Length-1] == '0' && !hasPoint && !hasNummer)
                    {
                        // 01 -> 1
                        Calculation = Calculation.Remove(Calculation.Length - 1 , 1);
                        calculationStore.RemoveAt(calculationStore.Count -1);
                    }
                }
                Calculation += nummer;
                isBuildingNumber = true;
                rootIndex = 0;
                
                if(nummer != "0")
                {
                    hasNummer = true;
                }

                CheckForLimits();

                endState = false;
            }
        }
    }

    void AddPoint()
    {
        if (ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning)
        {
            if (endState) Clear();

            if (!hasPoint && power == 0)
            {
                if (Calculation.Length > 0)
                {   
                    if (isBuildingNumber && Calculation[Calculation.Length - 1] != '-')
                    {
                        calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));
                        Calculation += ".";
                    }
                    if (Calculation[Calculation.Length - 1] == '-')
                    {
                        // - . -> - 0.
                        AddNummer("0");
                        calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));
                        Calculation += ".";
                    }
                }
                if (!isBuildingNumber)
                {
                    // - . -> - 0.
                    AddNummer("0");
                    calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));
                    Calculation += ".";
                }
                hasPoint = true;
                rootIndex = 0;

                endState = false;
            }
        }
    }

    void AddMinusNum()
    {
        if (ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning)
        {
            if (endState) Clear();

            if (!isBuildingNumber && power == 0)
            {
                if (Calculation.Length > 0)
                {
                    if (Calculation[Calculation.Length-1] == ')')
                    {
                        // )- -> ) * - 
                        AddOperation("*");
                    }
                }
                calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));
                Calculation += "-";
                isNegative = true;
                isBuildingNumber = true;
                rootIndex = 0;

                endState = false;
            }
        }
    }
    
    void AddOperation(string operation)
    {
        if (ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning)
        {
            if(Calculation.Length > 0)
            {
                if (((isBuildingNumber && Calculation[Calculation.Length - 1] != '-') || Calculation[Calculation.Length - 1] == ')' || power > 0) && rootIndex == 0)
                {
                    calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));
                    while (Calculation[^1] == '0' && hasPoint)
                    {
                        // 1.500 -> 1.5
                        Calculation = Calculation.Remove(Calculation.Length - 1 , 1);
                        calculationStore.RemoveAt(calculationStore.Count -1);
                    }
                    if (Calculation[Calculation.Length - 1] == '.')
                    {
                        // 1. -> 1
                        Calculation = Calculation.Remove(Calculation.Length -1, 1);
                        calculationStore.RemoveAt(calculationStore.Count -1);
                    }
                    if (Calculation.Length > 1)
                    {
                        if (Calculation[^1] == '0' && Calculation[^2] == '-')
                        {
                            // -0 -> 0
                            Calculation = Calculation.Remove(Calculation.Length -2, 1);
                            calculationStore.RemoveAt(calculationStore.Count -2);
                            calculationStore[^1].Calculation = calculationStore[^1].Calculation.Remove(Calculation.Length - 1 , 1);
                        }
                    }
                    Calculation += " " + operation + " ";
                    isNegative = false;
                    isBuildingNumber = false;
                    hasPoint = false;
                    hasNummer = false;
                    power = 0;

                    if (bracketInRootCount < 2) bracketInRootCount = 0; 
                }
                endState = false;
            }
        }
    }

    void IncreasePower()
    {
        if ((ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning) && Calculation.Length > 0)
        {
            if ((isBuildingNumber || Calculation[Calculation.Length - 1] == ')' || power > 0) && rootIndex == 0)
            {
                //-1^2 -> (-1)^2
                if (isBuildingNumber && isNegative)
                {
                    if (bracketCount > 5) return;     

                    while (Calculation[^1] != '-')
                    {
                        memory.Add(Calculation[^1]);
                        Delete();
                    }
                    Delete();
                    AddOpenBracket();
                    AddMinusNum();

                    for(int i = memory.Count -1; i >= 0; i--)
                    {
                        print(memory[i].ToString());
                        if (memory[i] == '.') AddPoint();
                        else AddNummer(memory[i].ToString());
                    }
                    AddCloseBracket();
                    memory.Clear();
                }

                calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));
                if (power == 0)
                {
                    Calculation += "<voffset=0.4em><size=50%>2</size></voffset>";
                    power = 2;
                }

                else if (power == 9)
                {
                    Calculation = Calculation.Remove(Calculation.Length -43, 43);
                    calculationStore.RemoveAt(calculationStore.Count -1);
                    Calculation += "<voffset=0.4em><size=50%>2</size></voffset>";
                    power = 2;
                }

                else
                {
                    Calculation = Calculation.Remove(Calculation.Length -43, 43);
                    calculationStore.RemoveAt(calculationStore.Count -1);
                    
                    power += 1;
                    Calculation += $"<voffset=0.4em><size=50%>{power}</size></voffset>";
                    
                    isNegative = false;
                    isBuildingNumber = false;
                    hasPoint = false;
                    hasNummer = false;

                    endState = false;
                }
            }
        }
    }

    void AddPower(int goPower)
    {
        if ((ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning) && Calculation.Length > 0)
        {
            if ((isBuildingNumber || Calculation[Calculation.Length - 1] == ')' || power > 0) && rootIndex == 0)
            {
                //-1^4 -> (-1)^4
                if (isBuildingNumber && isNegative)
                {
                    if (bracketCount > 5) return;     

                    while (Calculation[^1] != '-')
                    {
                        memory.Add(Calculation[^1]);
                        Delete();
                    }
                    Delete();
                    AddOpenBracket();
                    AddMinusNum();

                    for(int i = memory.Count -1; i >= 0; i--)
                    {
                        print(memory[i].ToString());
                        if (memory[i] == '.') AddPoint();
                        else AddNummer(memory[i].ToString());
                    }
                    AddCloseBracket();
                    memory.Clear();
                }

                calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));
                if (power > 0)
                {
                    Calculation = Calculation.Remove(Calculation.Length -43, 43);
                    calculationStore.RemoveAt(calculationStore.Count -1);
                }
                Calculation += $"<voffset=0.4em><size=50%>{goPower}</size></voffset>";
                power = goPower;

                isNegative = false;
                isBuildingNumber = false;
                hasPoint = false;
                hasNummer = false;

                endState = false;
            }
        }
    }

    void AddRoot()
    {
        if ((ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning) && bracketInRootCount < 2)
        {
            if (Calculation.Length > 0)
            {
                if (Calculation[Calculation.Length - 1] == '-')
                {
                    // -√... -> -1 * √...
                    AddNummer("1");
                    AddOperation("*");
                }
                else if (isBuildingNumber || power > 0 || Calculation[Calculation.Length - 1] == ')')
                {
                    //3√... -> 3 * √...  ||   )√... -> ) * √...  
                    AddOperation("*");
                }
            }
            calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));

            bracketInRootCount = 1;

            if (rootIndex == 0)
            {
                rootCount += 1;
                Calculation += "√";
                rootIndex = 2;

                UpdateAusgabe();
                ausgabeText.ForceMeshUpdate();

                rootBar = Instantiate(rootBarPrefab);
                rootBar.ausgabe = (TextMeshProUGUI)ausgabeText;
                rootBar.rootCount = rootCount;
            }

            else if (rootIndex == 9)
            {
                Calculation = Calculation.Remove(Calculation.Length -24, 24);
                calculationStore.RemoveAt(calculationStore.Count -1);

                UpdateAusgabe();
                ausgabeText.ForceMeshUpdate();
                rootBar.CheckForRemoval();

                Calculation += "√";
                rootIndex = 2;
                
                UpdateAusgabe();
                ausgabeText.ForceMeshUpdate();

                rootBar = Instantiate(rootBarPrefab);
                rootBar.ausgabe = (TextMeshProUGUI)ausgabeText;
                rootBar.rootCount = rootCount;
            }

            else
            {
                if (rootIndex == 2) Calculation = Calculation.Remove(Calculation.Length -1, 1);
                else Calculation = Calculation.Remove(Calculation.Length -24, 24);
                calculationStore.RemoveAt(calculationStore.Count -1);
                
                rootIndex += 1;
                Calculation += $"<sup>{rootIndex}<space=-10></sup>√";

                isNegative = false;
                isBuildingNumber = false;
                hasPoint = false;
                hasNummer = false;

                endState = false;
            }
        }
    }

    void AddOpenBracket()
    {
        if ((ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning) && bracketCount < 6)
        {
            if(Calculation.Length > 0)
            {
                if (Calculation[Calculation.Length - 1] == '-')
                {
                    // -(... -> -1 * (...
                    AddNummer("1");
                    AddOperation("*");
                }
                else if (isBuildingNumber || power > 0 || Calculation[Calculation.Length - 1] == ')')
                {
                    //3(... -> 3 * (...  ||   )(... -> ) * (...  
                    AddOperation("*");
                }
            }
            calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));

            Calculation += "(";
            isNegative = false;
            isBuildingNumber = false;
            hasPoint = false;
            hasNummer = false;
            power = 0;
            rootIndex = 0;
            bracketCount += 1;
            if(bracketInRootCount > 0) bracketInRootCount ++;

            endState = false;
        }
    }

    void AddCloseBracket()
    {
        if ((ausgabeText.preferredWidth < 2000 || Generaldata.animationIsRunning) && bracketCount > 0)
        {
            if(Calculation.Length > 0)
            {
                if (((isBuildingNumber && Calculation[Calculation.Length - 1] != '-') || Calculation[Calculation.Length - 1] == ')' || power > 0) && rootIndex == 0)
                {
                    calculationStore.Add(new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount));

                    //Das selbe wie bei AddOperation
                    while (Calculation[Calculation.Length - 1] == '0' && hasPoint)
                        {
                            // 1.500 -> 1.5
                            Calculation = Calculation.Remove(Calculation.Length - 1 , 1);
                            calculationStore.RemoveAt(calculationStore.Count -1);
                        }
                        if (Calculation[Calculation.Length - 1] == '.')
                        {
                            // 1. -> 1
                            Calculation = Calculation.Remove(Calculation.Length - 1 , 1);
                            calculationStore.RemoveAt(calculationStore.Count -1);
                        }
                        if (Calculation.Length > 1)
                        {
                            if (Calculation[Calculation.Length - 1] == '0' && Calculation[^2] == '-')
                            {
                                // -0 -> 0
                                Calculation = Calculation.Remove(Calculation.Length -2, 1);
                                calculationStore.RemoveAt(calculationStore.Count -2);
                                calculationStore[^1].Calculation = calculationStore[^1].Calculation.Remove(Calculation.Length -1, 1);
                            }
                        }
                    Calculation += ")";
                    isNegative = false;
                    isBuildingNumber = false;
                    hasPoint = false;
                    hasNummer = false;
                    power = 0;
                    bracketCount -= 1;

                    if (bracketInRootCount > 2) bracketInRootCount --;
                    else if (bracketInRootCount == 2) bracketInRootCount = 0;

                    endState = false;
                }
            }
        }
    }

    void Clear()
    {
        

        // Setzt Alles auf Anfang
        Calculation = "";
        isBuildingNumber = false;
        isNegative = false;
        hasPoint = false;
        hasNummer = false;
        power = 0;
        rootIndex = 0;
        UpdateAusgabe();
        if (rootBar != null) rootBar.CheckForRemoval();
        rootBar = null;
        rootCount = 0;
        bracketInRootCount = 0;
        bracketCount = 0;        

        calculationStore.Clear();

        endState = false;
    }
    
    public void Delete()
    {
        if (Calculation.Length > 0)
        {
            //Ladet den letzten Stand
            Calculation = calculationStore[^1].Calculation;
            isBuildingNumber = calculationStore[^1].IsBuildingNumber;
            isNegative = calculationStore[^1].IsNegative;
            hasPoint = calculationStore[^1].HasPoint;
            hasNummer = calculationStore[^1].HasNummer;
            power = calculationStore[^1].Power;
            rootIndex = calculationStore[^1].RootIndex;
            rootBar = calculationStore[^1].RootBar;
            rootCount = calculationStore[^1].RootCount;
            bracketInRootCount = calculationStore[^1].BracketInRootCount;
            endState = calculationStore[^1].EndState;
            bracketCount = calculationStore[^1].BracketCount;

            calculationStore.RemoveAt(calculationStore.Count -1);
        }
    }

    void Enter()
    {
        if (Calculation != "" && Calculation[^1] != '(' && Calculation[^1] != '-' && !Generaldata.animationIsRunning && rootIndex == 0)
        {
            endState = false;
            Generaldata.animationIsRunning = true;
            CorrectCalculation();
            enterStore = new CalculationStore(Calculation, isBuildingNumber, isNegative, hasPoint, hasNummer, power, rootIndex, rootBar, rootCount, bracketInRootCount, endState, bracketCount);
            logikEingabe.StartConversion(Calculation);
        }
    }

    void CheckForLimits()
    {
        int numberCounter = 0;
        int finalDigitCount = 0;

        for (int i = 1; i <= Calculation.Length; i++)
        {
            if (Calculation[^i] == '.') finalDigitCount = i -1;
            if (Calculation[^i] != ' ' && Calculation[^i] != '(' && Calculation[^i] != '√') numberCounter++;
            else break;
        }

        string numberStr = Calculation.Substring(Calculation.Length - numberCounter, numberCounter);
        decimal number = decimal.Parse(numberStr);

        if (number > maxValue || number < -maxValue 
            || (number < minValue && number > -minValue && number != 0)
            || finalDigitCount > maxDigit) Delete();
    }

    public void UpdateButtons(bool keyBlock)
    {
        Generaldata.keyBlock = keyBlock;

        button9.interactable = !keyBlock;
        button8.interactable = !keyBlock;
        button7.interactable = !keyBlock;
        button6.interactable = !keyBlock;
        button5.interactable = !keyBlock;
        button4.interactable = !keyBlock;
        button3.interactable = !keyBlock;
        button2.interactable = !keyBlock;
        button1.interactable = !keyBlock;
        button0.interactable = !keyBlock;

        buttonPoint.interactable = !keyBlock;

        buttonMinusNum.interactable = !keyBlock;

        buttonPlus.interactable = !keyBlock;
        buttonMinus.interactable = !keyBlock;
        buttonMult.interactable = !keyBlock;
        buttonDiv.interactable = !keyBlock;

        buttonPow.interactable = !keyBlock;
        buttonRoot.interactable = !keyBlock;

        buttonOpenBracket.interactable = !keyBlock;
        buttonCloseBracket.interactable = !keyBlock;

        buttonDel.interactable = !keyBlock;
        buttonClear.interactable = !keyBlock;

        buttonEnter.interactable = !keyBlock; 
    }

    public void CorrectCalculation()
    {
        // 1 + 3 + ((  -> 1 + 3
        while (Calculation[^1] == '(' || Calculation[^1] == ' ' || Calculation[^1] == '-')
        {Delete();}
        
        //Das selbe wie bei AddOperation
        while (Calculation[^1] == '0' && hasPoint)
        {
            // 1.500 -> 1.5
            Calculation = Calculation.Remove(Calculation.Length -1, 1);
            calculationStore.RemoveAt(calculationStore.Count -1);
        }
        if (Calculation[^1] == '.')
        {
            // 1. -> 1
            Calculation = Calculation.Remove(Calculation.Length -1, 1);
            calculationStore.RemoveAt(calculationStore.Count -1);
            hasPoint = false;
        }
        if (Calculation.Length > 1)
        {
            if (Calculation[^1] == '0' && Calculation[^2] == '-')
            {
                // -0 -> 0
                Calculation = Calculation.Remove(Calculation.Length -2, 1);
                calculationStore.RemoveAt(calculationStore.Count - 2);
                calculationStore[^1].Calculation = calculationStore[^1].Calculation.Remove(Calculation.Length -1, 1);
            }
        }

        // ((3 + 1) -> ((3 + 1))
        while (bracketCount > 0)
        {AddCloseBracket();}

        UpdateAusgabe();
    }

    public bool CheckForErrors(List<object> PFCal)
    {
        List<object> CalculationTest = new List<object>();
        foreach (var item in PFCal) CalculationTest.Add(item);
        
        int i = 0;
        while (i < CalculationTest.Count) 
        {
            if (CalculationTest[i] is string)
            {
                if ((string)CalculationTest[i] == "+")
                {
                    decimal a = (decimal)CalculationTest[i-2];
                    decimal b = (decimal)CalculationTest[i-1];
                    CalculationTest[i] = a + b;
                    CalculationTest.RemoveAt(i - 1);
                    CalculationTest.RemoveAt(i - 2);
                    i -= 2;
                }
                else if ((string)CalculationTest[i] == "-")
                {
                    decimal a = (decimal)CalculationTest[i-2];
                    decimal b = (decimal)CalculationTest[i-1];
                    CalculationTest[i] = a - b;
                    CalculationTest.RemoveAt(i - 1);
                    CalculationTest.RemoveAt(i - 2);
                    i -= 2;
                }
                else if ((string)CalculationTest[i] == "*")
                {
                    decimal a = (decimal)CalculationTest[i-2];
                    decimal b = (decimal)CalculationTest[i-1];
                    CalculationTest[i] = a * b;
                    CalculationTest.RemoveAt(i - 1);
                    CalculationTest.RemoveAt(i - 2);
                    i -= 2;
                }
                else if ((string)CalculationTest[i] == "/")
                {
                    decimal a = (decimal)CalculationTest[i-2];
                    decimal b = (decimal)CalculationTest[i-1];
                    if (b == 0)
                    {
                        Error("Division by 0");
                        return true;
                    }
                    CalculationTest[i] = a / b;
                    CalculationTest.RemoveAt(i - 1);
                    CalculationTest.RemoveAt(i - 2);
                    i -= 2;
                }
                else if ((string)CalculationTest[i] == "^")
                {
                    decimal a = (decimal)CalculationTest[i-2];
                    decimal b = (decimal)CalculationTest[i-1];
                    CalculationTest[i] = (decimal)Mathf.Pow((float)a, (float)b);
                    CalculationTest.RemoveAt(i - 1);
                    CalculationTest.RemoveAt(i - 2);
                    i -= 2;
                }
                else if ((string)CalculationTest[i] == "%")
                {
                    decimal a = (decimal)CalculationTest[i-2];
                    decimal b = (decimal)CalculationTest[i-1];
                    if (b < 0 && a%2 == 0)
                    {
                        Error("Even root of a negative number");
                        return true;
                    }
                    if (b >= 0) CalculationTest[i] = (decimal)Mathf.Pow((float)b, 1f/(float)a);
                    else CalculationTest[i] = (decimal)-Mathf.Pow((float)-b, 1f/(float)a);
                    CalculationTest.RemoveAt(i - 1);
                    CalculationTest.RemoveAt(i - 2);
                    i -= 2;
                }
            }
            if ((decimal)CalculationTest[i] > 4000m || (decimal)CalculationTest[i] < -4000m)
            {
                Error("Overflow, no intermediate step can contain a nummer over 4000 or under -4000");
                return true;
            }
            if ((decimal)CalculationTest[i] < 0.01m && (decimal)CalculationTest[i] > -0.01m && (decimal)CalculationTest[i] != 0)
            {
                Error("Overflow, no intermediate step can contain a nummer smaller than 0.01 or -0.01");
                return true;
            }
            i++;
        }
        return false;
    }

    public void Error(string errorType)
    {
        errorText.text = "Error: " + errorType;
        errorText.color = new Color(255/255, 116/255, 116/255, 0);
        Generaldata.keyBlock = true;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(ausgabeText.DOFade(0,0.5f));
        sequence.Append(errorText.DOFade(1,0.5f));
        sequence.AppendInterval(3);    
        sequence.Append(errorText.DOFade(0,0.5f));
        sequence.Append(ausgabeText.DOFade(1,0.5f));
        sequence.Play();
        sequence.OnComplete(() => 
        {
            Generaldata.keyBlock = false;
            Generaldata.animationIsRunning = false;
        });

        
        //Ladet den letzten Stand
        Calculation = enterStore.Calculation;
        isBuildingNumber = enterStore.IsBuildingNumber;
        isNegative = enterStore.IsNegative;
        hasPoint = enterStore.HasPoint;
        hasNummer = enterStore.HasNummer;
        power = enterStore.Power;
        rootIndex = enterStore.RootIndex;
        rootBar = enterStore.RootBar;
        rootCount = enterStore.RootCount;
        bracketInRootCount = enterStore.BracketInRootCount;
        endState = enterStore.EndState;
        bracketCount = enterStore.BracketCount;
    }
    
    public void ResetCalculation(decimal result)
    {
        UpdateButtons(false);
        Generaldata.animationIsRunning = false;
        Generaldata.calculationIsRunning =  false;
        endState = true;
        if (pause.pause)
        {
            pause.pauseSeq.Kill();
            pause.animationIsRunning = false;
            pause.PauseAnimation();
        }
        
        calculationStore.Clear();
        calculationStore.Add(new CalculationStore("", false, false, false, false, 0, 0, null, 0, 0, false, 0));
        print("result:" + result);
        decimal d = Math.Round(result, 4, MidpointRounding.AwayFromZero);
        print("rounded:" + d);
        Calculation = d.ToString("0.####");
        isBuildingNumber = true;
        isNegative = false;
        hasPoint = false;
        hasNummer = false;
        power = 0;
        rootIndex = 0;
        rootBar = null;
        rootCount = 0;
        bracketInRootCount = 0;
        bracketCount = 0;
    }

    public void RestartCalculation()
    {
        //Ladet den letzten Stand
        Calculation = enterStore.Calculation;
        isBuildingNumber = enterStore.IsBuildingNumber;
        isNegative = enterStore.IsNegative;
        hasPoint = enterStore.HasPoint;
        hasNummer = enterStore.HasNummer;
        power = enterStore.Power;
        rootIndex = enterStore.RootIndex;
        rootBar = enterStore.RootBar;
        rootCount = enterStore.RootCount;
        bracketInRootCount = enterStore.BracketInRootCount;
        endState = enterStore.EndState;
        bracketCount = enterStore.BracketCount;
    
        if (pause.pause)
        {
            pause.pauseSeq.Kill();
            pause.animationIsRunning = false;
            pause.PauseAnimation();
        }

        UpdateButtons(false);
        Generaldata.animationIsRunning = false;
        Generaldata.calculationIsRunning = false;
        pause.pause = false;
        UpdateAusgabe();
    }
}