using UnityEngine;
using UnityEngine.Tilemaps;

public enum ObjectType
{
    Obstacle,
    Item
}

public abstract class Object : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D collision);    

    public virtual void DestroyTile(Collider2D collision)
    {
        // 임시 코드
        if (collision.gameObject.name == "Opstacle") return;
        //

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Destroy 실행 준비");
            Tilemap tilemap = gameObject.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                Vector3 hitPoint = collision.ClosestPoint(transform.position);
                Vector3Int cellPosition = tilemap.WorldToCell(hitPoint);
                if (tilemap.HasTile(cellPosition))
                    tilemap.SetTile(cellPosition, null);
            }
            Debug.Log("Destroy 실행 완료");
        }
    }
}