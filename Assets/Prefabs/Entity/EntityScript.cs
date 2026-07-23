using UnityEngine;

public class EntityScript : MonoBehaviour
{
    private float HP = 1.0f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public float GetHP() 
    { 
        return HP;
    }

    public void SetHP(float HP)
    {
        this.HP = HP;
    }
}
