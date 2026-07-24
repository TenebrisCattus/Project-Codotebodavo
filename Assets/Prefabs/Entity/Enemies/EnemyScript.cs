using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyScript : EntityScript
{
    private LayerMask obstacleLayer;

    private void Start()
    {
        obstacleLayer = LayerMask.NameToLayer("Default");
    }
    void Update()
    {
        Debug.Log(HasLineOfSight());
    }

    bool HasLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, PlayerScript.Game_player.transform.position, obstacleLayer);
        if (hit.collider == null)
        {
            return true; // Прямая видимость есть
        }
        else
        {
            return false; // На пути есть препятствие
        }
    }
}
