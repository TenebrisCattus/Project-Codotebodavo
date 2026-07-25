using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CaseScript : MonoBehaviour
{
    private List<Collider2D> inCollision;

    private void Start()
    {
        inCollision = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inCollision.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollision.Remove(collision);
    }

    public List<Collider2D> InTrigger()
    {
        return inCollision;
    }
}
