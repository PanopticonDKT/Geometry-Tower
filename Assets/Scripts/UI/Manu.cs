using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public string sceneName;

    public void ExitGame()
    {
        Debug.Log("Exit pressed");   
        Application.Quit();
    }
    public void Scene()
    {
        SceneManager.LoadScene(sceneName);   // имя игровой сцены
    }
}
