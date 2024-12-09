using UnityEngine;

public class CollisionDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"{gameObject.name} initialized with CollisionDebugger");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected between {gameObject.name} and {collision.gameObject.name}");
    }
}
