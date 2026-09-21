using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Informations : MonoBehaviour
{
    public GameObject mainScreen;
    private Button button;
    private bool state = false;

    void Awake()
    {button = GetComponent<Button>();}

    void Update()
    {if (Input.GetKeyDown(KeyCode.Escape)) EnterInformations();}

    public Sequence Toggle()
    {
        Sequence seq = DOTween.Sequence();

        if (!state)
            seq.Append(transform.DOLocalMove(new Vector3(-18,0,0), 0.5f));
        else
            seq.Append(transform.DOLocalMove(new Vector3(-18,7,0), 0.5f));

        state = !state;

        seq.OnComplete(() => button.interactable = state);

        return seq;
    }

    public void EnterInformations()
    {
        if (!Generaldata.informationsOpen)
        {   
            SceneManager.LoadScene("Informations", LoadSceneMode.Additive);
            Time.timeScale = 0f;
            mainScreen.SetActive(false);
            Generaldata.informationsOpen = true;
        }
    }
}
