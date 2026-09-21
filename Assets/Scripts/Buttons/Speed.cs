using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;

public class Speed : MonoBehaviour
{
    private Slider slider;
    private bool state = false;
    
    public TMP_Text calSpeedAnzeige;

    void Awake()
    {slider = GetComponent<Slider>();}

    void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slider.value = -1f;
            ChangeSpeed();
        }
        else if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            slider.value = 1f;
            ChangeSpeed();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slider.value -= 0.1f;
            ChangeSpeed();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slider.value += 0.1f;
            ChangeSpeed();
        }
    }

    public Sequence Toggle()
    {
        Sequence seq = DOTween.Sequence();

        if (!state)
            seq.Append(transform.DOLocalMove(new Vector3(-38,0,0), 0.5f));
        else
            seq.Append(transform.DOLocalMove(new Vector3(-38,7,0), 0.5f));

        state = !state;

        seq.OnComplete(() => slider.interactable = state);
        return seq;
    }

    public void ChangeSpeed()
    {
        if (slider.value > 1) slider.value = 1;
        else if (slider.value < -1) {slider.value = -1;}

        Generaldata.calculationSpeed = Mathf.Pow(3f, slider.value);
        
        decimal d = Math.Round((decimal)Generaldata.calculationSpeed, 2, MidpointRounding.AwayFromZero);
        calSpeedAnzeige.text = d.ToString("0.##") + "x";
    }
}