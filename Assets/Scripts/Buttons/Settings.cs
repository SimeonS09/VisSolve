using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Settings : MonoBehaviour
{
    public Pause pause;
    public Stopp stopp;
    public Informations informations;
    public Exit exit;
    public Speed speed;

    private bool state = false;
    private bool animationIsRunning = false;
    private Image image;
    private Sequence seq;

    void Awake()
    {
        image = GetComponent<Image>();
        seq = DOTween.Sequence();
    }
    
    void Update()
    {
        if (Generaldata.animationIsRunning) seq.timeScale = 1/Generaldata.calculationSpeed;
        else  seq.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Tab))
        {Toggle();}
    }
    public void Toggle()
    {
        seq = DOTween.Sequence();

        if (!animationIsRunning)
        {
            seq.Append(pause.Toggle());
            seq.Join(stopp.Toggle());
            seq.Join(informations.Toggle());
            seq.Join(exit.Toggle());
            seq.Join(speed.Toggle());

            animationIsRunning = true;
            if (!state)
            {
                seq.Join(transform.DOLocalRotate(new Vector3 (0,0,90), 0.5f));
                seq.Join(image.DOColor(new Color(52f/255f, 184f/255f, 255f/255f, 1), 0.5f));
            }
            else
            {
                seq.Join(transform.DOLocalRotate(new Vector3 (0,0,0), 0.5f));
                seq.Join(image.DOColor(new Color(255f/225f, 255f/255f, 255f/255f, 1), 0.5f));
            }
            state = !state;
            seq.OnComplete(() => {animationIsRunning = false;});
            seq.Play();
        }
    }
}
