using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalDoorScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(PlayerScript.Game_player.nextLocName);
    }
}
