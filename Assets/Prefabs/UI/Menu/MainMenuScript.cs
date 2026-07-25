using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]public GameObject SettingsPanel;
    [SerializeField]public GameObject MainMenuPanel;
    public void PlayGame()
    {
        // Надо поменять семпл сцен на сцену с катсценой или чё там со старта
        SceneManager.LoadScene("SampleScene");
    }
    public void ContinueGame()
    {
        // Продолжить игру, тут ничего нету

    }
    public void Settings()
    {
        SettingsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);

    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
