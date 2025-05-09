using UnityEngine;

public enum ObjectType
{
    Obstacle,
    Item
}

public abstract class Object : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D collision);
}