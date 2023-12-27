using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//---------------- Скрипт главного меню ----------------//

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
