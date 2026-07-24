using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyScript : EntityScript
{
    [Header("AI Settings")]
    [SerializeField] private float reactionRadius;
    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [Header("Obstacle Layer")]
    [SerializeField] private LayerMask obstacleLayer;

    private float destinatonToPlayerX;
    private bool isPlayerRight;

    private void Update()
    {
        destinatonToPlayerX = PlayerScript.Game_player.transform.position.x - transform.position.x;
        if (destinatonToPlayerX > 0)
        {
            isPlayerRight = true;
        }
        else
        {
            isPlayerRight = false;
        } 
    }

    public bool SeePlayer()
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

    public float DestinatonToPlayer()
    {
        return Mathf.Abs(destinatonToPlayerX);
    }

    public bool IsPlayerRight() 
    {
        return isPlayerRight;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float ReactionRadius() {  return reactionRadius; }
}
