using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class Stopp : MonoBehaviour
{
    public AnimationControl animationControl;
    public AnzeigeAnimation anzeigeAnimation;
    public AnzeigeEingabe anzeigeEingabe;

    public AdditionAnimation addition;
    public SubstractionAnimation substraction;
    public MultiplicationAnimation multiplication;
    public DivisionAnimation division;
    public PowerAnimation power;
    public RootAnimation root;

    private Button button;

    private bool state = false;
    public bool animationIsRunning = false;

    public Sequence stoppSeq;


    void Awake()
    {
        button = GetComponent<Button>();
        stoppSeq = DOTween.Sequence();
    }

    void Update()
    {
        if (Input.inputString == "x") StoppAnimation();
        stoppSeq.timeScale = 1/Generaldata.calculationSpeed;
    }

    public Sequence Toggle()
    {
        Sequence seq = DOTween.Sequence();

        if (!state)
            seq.Append(transform.DOLocalMove(new Vector3(-12,0,0), 0.5f));
        else
            seq.Append(transform.DOLocalMove(new Vector3(-12,7,0), 0.5f));

        state = !state;

        seq.OnComplete(() => button.interactable = state);

        return seq;
    }

    public void StoppAnimation()
    {
        if (Generaldata.calculationIsRunning && !animationIsRunning)
        {
            addition.seq.Kill();
            substraction.seq.Kill();
            multiplication.seq.Kill();
            division.seq.Kill();
            power.seq.Kill();
            root.seq.Kill();

            anzeigeAnimation.autoBrackSeq.Kill();
            animationControl.t.Kill();
            print("seq kill");

            animationIsRunning = true;

            stoppSeq = DOTween.Sequence();

            stoppSeq.Append(transform.DOScale(1.2f, 0.1f));
            stoppSeq.Append(transform.DOScale(1f, 0.1f));

            anzeigeAnimation.tokenList.Clear();
            anzeigeAnimation.UpdateAnzeige();

            if (addition.line1 != null) Destroy(addition.line1.gameObject);
            if (addition.line2 != null) Destroy(addition.line2.gameObject);
            if (addition.line3 != null) Destroy(addition.line3.gameObject);

            if (substraction.line1 != null) Destroy(substraction.line1.gameObject);
            if (substraction.line2 != null) Destroy(substraction.line2.gameObject);
            if (substraction.line3 != null) Destroy(substraction.line3.gameObject);

            if (multiplication.line1 != null) Destroy(multiplication.line1.gameObject);
            if (multiplication.line2 != null) Destroy(multiplication.line2.gameObject);
            if (multiplication.artLine1 != null) Destroy(multiplication.artLine1.gameObject);
            if (multiplication.artLine2 != null) Destroy(multiplication.artLine2.gameObject);

            if (division.line1 != null) Destroy(division.line1.gameObject);
            if (division.line2 != null) Destroy(division.line2.gameObject);
            if (division.artLine1 != null) Destroy(division.artLine1.gameObject);
            if (division.artLine2 != null) Destroy(division.artLine2.gameObject);

            if (power.line1 != null) Destroy(power.line1.gameObject);
            if (power.line2 != null) Destroy(power.line2.gameObject);
            foreach (LineTransformations l in power.lines) if (l != null) Destroy(l.gameObject);
            if (power.artLineWN1 != null) Destroy(power.artLineWN1.gameObject);
            if (power.artLineWN2 != null) Destroy(power.artLineWN2.gameObject);
            if (power.artLine1 != null) Destroy(power.artLine1.gameObject);
            if (power.artLine2 != null) Destroy(power.artLine2.gameObject);
    
            if (root.line1 != null) Destroy(root.line1.gameObject);
            if (root.line2 != null) Destroy(root.line2.gameObject);
            if (root.line3 != null) Destroy(root.line3.gameObject);
            if (root.triangle != null) Destroy(root.triangle.gameObject);
            foreach (TriangleTransformations t in root.triangles) if (t != null) Destroy(t.gameObject);
            
            stoppSeq.OnComplete(() =>
            {animationIsRunning = false;});

            anzeigeEingabe.RestartCalculation();
        }
    }
}