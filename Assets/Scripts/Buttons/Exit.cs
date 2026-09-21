using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Exit: MonoBehaviour
{
    private Button button;
    private bool state = false;

    void Awake()
    {button = GetComponent<Button>();}

    public Sequence Toggle()
    {
        Sequence seq = DOTween.Sequence();

        if (!state)
            seq.Append(transform.DOLocalMove(new Vector3(-24,0,0), 0.5f));
        else
            seq.Append(transform.DOLocalMove(new Vector3(-24,7,0), 0.5f));

        state = !state;

        seq.OnComplete(() => button.interactable = state);

        return seq;
    }

    public void ExitVisSolve()
    {
    Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
