using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    void Update()
    {if (Input.GetKeyDown(KeyCode.Escape)) Exit();}

    public void Exit()
    {
        if (Generaldata.informationsOpen)
        {
            SceneManager.UnloadSceneAsync("Informations");
            Time.timeScale = 1f;
            Generaldata.informationsOpen = false;

            Scene scene = SceneManager.GetSceneByName("AfA-Taschenrechner");
            foreach (GameObject obj in scene.GetRootGameObjects())
            {if (obj.name == "Main-Screen") {obj.SetActive(true);}}
        }
    }
}
