using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.Tilemaps;

public enum ObjectType
{
    Coin,
    SpeedUp,
    SpeedDown,
    Heal,
    NormalObstacle,
    Arrow,
    EndPoint
}

public class Object : MonoBehaviour
{
    [SerializeField] private ObjectType _objectType;
    public ObjectType ObjectType { get => _objectType; }
    private string _objectName;
    public string ObjectName { get => _objectName; }

    Player player;
    Tilemap tilemap;
    Dictionary<Vector3Int, TileBase> removedTiles = new Dictionary<Vector3Int, TileBase>();

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        _objectName = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        player = collider.GetComponent<Player>();
        switch (_objectType)
        {
            case ObjectType.Coin:
                {
                    player.Coin();
                    AudioManager.Instance.PlaySFX(AudioManager.Sfx.item_coin);
                }
                break;
            case ObjectType.SpeedUp:
                {
                    player.ChangeSpeed(10.0f);
                    AudioManager.Instance.PlaySFX(AudioManager.Sfx.item_fast);
                }
                break;
            case ObjectType.SpeedDown:
                {
                    player.ChangeSpeed(-10.0f);
                    AudioManager.Instance.PlaySFX(AudioManager.Sfx.item_slow);
                }
                break;
            case ObjectType.Heal:
                {
                    player.ChangeHp(player.MaxHp * 0.2f);
                }
                break;
            case ObjectType.NormalObstacle:
                {
                    player.ChangeHp(-10.0f);
                }
                break;
            case ObjectType.Arrow:
                {
                    player.ChangeHp(-10.0f);
                }
                break;
            case ObjectType.EndPoint:
                {
                    player.EndPoint();
                }
                break;
            default:
                {
                    Debug.Log("Item");
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (tilemap != null)
        {
            Vector3 hitPoint = collision.ClosestPoint(collision.transform.position);
            Vector3Int cellPosition = tilemap.WorldToCell(hitPoint);
            TileBase tile = tilemap.GetTile(cellPosition); //여기서 타일베이스로 정하고
            if (tile != null)
            {
                removedTiles[cellPosition] = tile;
                tilemap.SetTile(cellPosition, null);
            }
        }
    }

    public void RestoreTiles()
    {
        foreach (var rmt in removedTiles)
        {
            Debug.LogError($"복구된 타일{rmt.Key}, {rmt.Value}");
            tilemap.SetTile(rmt.Key, rmt.Value);
        }
    }

    private void MoveArrow(GameObject gameObject)
    {
        const float arrowSpeed = 5.0f;
        gameObject.transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
    }
}