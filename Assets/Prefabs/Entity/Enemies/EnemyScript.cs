using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyScript : EntityScript
{
    public Transform target; // Объект, до которого проверяем видимость
    public LayerMask obstacleLayer; // Слой, на котором находятся стены и препятствия

    void Update()
    {
        Debug.Log(HasLineOfSight());
    }

    bool HasLineOfSight()
    {
        // Выпускаем луч от текущего объекта до цели
        RaycastHit2D hit = Physics2D.Linecast(transform.position, target.position, obstacleLayer);

        // Если луч ничего не встретил на своем пути, значит, преград нет
        if (hit.collider == null)
        {
            Debug.DrawLine(transform.position, target.position, Color.green);
            return true; // Прямая видимость есть
        }
        else
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            return false; // На пути есть препятствие
        }
    }
}
