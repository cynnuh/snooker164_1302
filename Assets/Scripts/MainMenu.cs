using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Startgame()
    {
        SceneManager.LoadScene("Scene01");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
