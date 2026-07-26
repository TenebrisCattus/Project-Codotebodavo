using UnityEngine;

public class LocationScript : MonoBehaviour
{
    [SerializeField] private string currentLocationName;
    [SerializeField] private string nextLocationName;
    [SerializeField] private int timer;
    void Start()
    {
        PlayerScript.Game_player.SetTimer(timer);
        PlayerScript.Game_player.SetLocs(currentLocationName, nextLocationName);
    }
}
