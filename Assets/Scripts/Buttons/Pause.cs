using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pause : MonoBehaviour
{
    public AnimationControl animationControl;
    public AnzeigeAnimation anzeigeAnimation;

    public AdditionAnimation addition;
    public SubstractionAnimation substraction;
    public MultiplicationAnimation multiplication;
    public DivisionAnimation division;
    public PowerAnimation powerAnimation;
    public RootAnimation rootAnimation;

    public Sprite pauseSprite;
    public Sprite playSprite;

    private Image image;
    private Button button;

    private bool state = false;
    public bool pause = false;
    public bool animationIsRunning = false;

    public Sequence pauseSeq;

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        pauseSeq = DOTween.Sequence();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) PauseAnimation();
        pauseSeq.timeScale = 1/Generaldata.calculationSpeed;
    }

    public Sequence Toggle()
    {
        Sequence seq = DOTween.Sequence();

        if (!state)
            seq.Append(transform.DOLocalMove(new Vector3(-6,0,0), 0.5f));
        else
            seq.Append(transform.DOLocalMove(new Vector3(-6,7,0), 0.5f));

        state = !state;

        seq.OnComplete(() => button.interactable = state);

        return seq;
    }

    public void PauseAnimation()
    {
        if (Generaldata.calculationIsRunning && !animationIsRunning)
        {
            animationIsRunning = true;

            pauseSeq = DOTween.Sequence();

            pauseSeq.Append(transform.DOScale(1.2f, 0.1f));

            if (!pause)
            {
                pauseSeq.AppendCallback(() => image.sprite = playSprite);
                addition.seq.Pause();
                substraction.seq.Pause();
                multiplication.seq.Pause();
                division.seq.Pause();
                powerAnimation.seq.Pause();
                rootAnimation.seq.Pause();

                anzeigeAnimation.autoBrackSeq.Pause();
                animationControl.t.Pause();
            }
            else
            {
                pauseSeq.AppendCallback(() => image.sprite = pauseSprite);
                addition.seq.Play();
                substraction.seq.Play();
                multiplication.seq.Play();
                division.seq.Play();
                powerAnimation.seq.Play();
                rootAnimation.seq.Play();

                anzeigeAnimation.autoBrackSeq.Play();
                animationControl.t.Play();
            }

            pauseSeq.Append(transform.DOScale(1f, 0.1f));

            pauseSeq.OnComplete(() => 
            {animationIsRunning = false;});

            pause = !pause;
            pauseSeq.Play();
        }
    }
}