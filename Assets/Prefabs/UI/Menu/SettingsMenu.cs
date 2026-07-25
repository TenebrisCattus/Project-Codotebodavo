using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] public GameObject SettingsPanel;
    [SerializeField] public GameObject MainMenuPanel;

    public void BackToMenu()
    {
        SettingsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);

    }
}
