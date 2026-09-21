using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationControll : MonoBehaviour
{
    public GameObject keyBinds;
    public GameObject addition;
    public GameObject subtraktion;
    public GameObject multiplikation;
    public GameObject division;
    public GameObject power;
    public GameObject root;
    public GameObject wI;

    public TMP_Text keyBindsB;
    public TMP_Text additionB;
    public TMP_Text subtraktionB;
    public TMP_Text multiplikationB;
    public TMP_Text divisionB;
    public TMP_Text powerB;
    public TMP_Text rootB;
    public TMP_Text wIB;
    
    public void KeyBinds()
    {
        keyBinds.SetActive(true);
        addition.SetActive(false);
        subtraktion.SetActive(false);
        multiplikation.SetActive(false);
        division.SetActive(false);
        power.SetActive(false);
        root.SetActive(false);
        wI.SetActive(false);

        keyBindsB.color = new Color(70f/255f, 170f/255f, 240f/255f, 1);
        additionB.color = new Color(1, 1, 1, 1);
        subtraktionB.color = new Color(1, 1, 1, 1);
        multiplikationB.color = new Color(1, 1, 1, 1);
        divisionB.color = new Color(1, 1, 1, 1);
        powerB.color = new Color(1, 1, 1, 1);
        rootB.color = new Color(1, 1, 1, 1);
        wIB.color = new Color(1, 1, 1, 1);
    }

    public void Addition()
    {
        keyBinds.SetActive(false);
        addition.SetActive(true);
        subtraktion.SetActive(false);
        multiplikation.SetActive(false);
        division.SetActive(false);
        power.SetActive(false);
        root.SetActive(false);        
        wI.SetActive(false);

        keyBindsB.color = new Color(1, 1, 1, 1);
        additionB.color = new Color(70f/255f, 170f/255f, 240f/255f, 1);
        subtraktionB.color = new Color(1, 1, 1, 1);
        multiplikationB.color = new Color(1, 1, 1, 1);
        divisionB.color = new Color(1, 1, 1, 1);
        powerB.color = new Color(1, 1, 1, 1);
        rootB.color = new Color(1, 1, 1, 1);
        wIB.color = new Color(1, 1, 1, 1);
    }

    public void Subtraktion()
    {
        keyBinds.SetActive(false);
        addition.SetActive(false);
        subtraktion.SetActive(true);
        multiplikation.SetActive(false);
        division.SetActive(false);
        power.SetActive(false);
        root.SetActive(false);
        wI.SetActive(false);

        keyBindsB.color = new Color(1, 1, 1, 1);
        additionB.color = new Color(1, 1, 1, 1);
        subtraktionB.color = new Color(70f/255f, 170f/255f, 240f/255f, 1);
        multiplikationB.color = new Color(1, 1, 1, 1);
        divisionB.color = new Color(1, 1, 1, 1);
        powerB.color = new Color(1, 1, 1, 1);
        rootB.color = new Color(1, 1, 1, 1);
        wIB.color = new Color(1, 1, 1, 1);        
    }

    public void Multiplikation()
    {
        keyBinds.SetActive(false);
        addition.SetActive(false);
        subtraktion.SetActive(false);
        multiplikation.SetActive(true);
        division.SetActive(false);
        power.SetActive(false);
        root.SetActive(false);
        wI.SetActive(false);

        keyBindsB.color = new Color(1, 1, 1, 1);
        additionB.color = new Color(1, 1, 1, 1);
        subtraktionB.color = new Color(1, 1, 1, 1);
        multiplikationB.color = new Color(70f/255f, 170f/255f, 240f/255f, 1);
        divisionB.color = new Color(1, 1, 1, 1);
        powerB.color = new Color(1, 1, 1, 1);
        rootB.color = new Color(1, 1, 1, 1);
        wIB.color = new Color(1, 1, 1, 1);
    }

    public void Division()
    {
        keyBinds.SetActive(false);
        addition.SetActive(false);
        subtraktion.SetActive(false);
        multiplikation.SetActive(false);
        division.SetActive(true);
        power.SetActive(false);
        root.SetActive(false);
        wI.SetActive(false);

        keyBindsB.color = new Color(1, 1, 1, 1);
        additionB.color = new Color(1, 1, 1, 1);
        subtraktionB.color = new Color(1, 1, 1, 1);
        multiplikationB.color = new Color(1, 1, 1, 1);
        divisionB.color = new Color(70f/255f, 170f/255f, 240f/255f, 1);
        powerB.color = new Color(1, 1, 1, 1);
        rootB.color = new Color(1, 1, 1, 1);
        wIB.color = new Color(1, 1, 1, 1);
    }

    public void Power()
    {
        keyBinds.SetActive(false);
        addition.SetActive(false);
        subtraktion.SetActive(false);
        multiplikation.SetActive(false);
        division.SetActive(false);
        power.SetActive(true);
        root.SetActive(false);
        wI.SetActive(false);

        keyBindsB.color = new Color(1, 1, 1, 1);
        additionB.color = new Color(1, 1, 1, 1);
        subtraktionB.color = new Color(1, 1, 1, 1);
        multiplikationB.color = new Color(1, 1, 1, 1);
        divisionB.color = new Color(1, 1, 1, 1);
        powerB.color = new Color(70f/255f, 170f/255f, 240f/255f, 1);
        rootB.color = new Color(1, 1, 1, 1);
        wIB.color = new Color(1, 1, 1, 1);
    }

    public void Root()
    {
        keyBinds.SetActive(false);
        addition.SetActive(false);
        subtraktion.SetActive(false);
        multiplikation.SetActive(false);
        division.SetActive(false);
        power.SetActive(false);
        root.SetActive(true);
        wI.SetActive(false);

        keyBindsB.color = new Color(1, 1, 1, 1);
        additionB.color = new Color(1, 1, 1, 1);
        subtraktionB.color = new Color(1, 1, 1, 1);
        multiplikationB.color = new Color(1, 1, 1, 1);
        divisionB.color = new Color(1, 1, 1, 1);
        powerB.color = new Color(1, 1, 1, 1);
        rootB.color = new Color(70f/255f, 170f/255f, 240f/255f, 1);
        wIB.color = new Color(1, 1, 1, 1);
    }

    public void WI()
    {
        keyBinds.SetActive(false);
        addition.SetActive(false);
        subtraktion.SetActive(false);
        multiplikation.SetActive(false);
        division.SetActive(false);
        power.SetActive(false);
        root.SetActive(false);
        wI.SetActive(true);

        keyBindsB.color = new Color(1, 1, 1, 1);
        additionB.color = new Color(1, 1, 1, 1);
        subtraktionB.color = new Color(1, 1, 1, 1);
        multiplikationB.color = new Color(1, 1, 1, 1);
        divisionB.color = new Color(1, 1, 1, 1);
        powerB.color = new Color(1, 1, 1, 1);
        rootB.color = new Color(1, 1, 1, 1);
        wIB.color = new Color(70f/255f, 170f/255f, 240f/255f, 1);
    }
}
