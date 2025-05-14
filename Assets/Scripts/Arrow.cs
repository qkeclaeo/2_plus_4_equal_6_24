using UnityEngine;

public class Arrow : MonoBehaviour
{
    Player player;
    const float arrowSpeed = 5.0f;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.Log("Player not found!");
            return;
        }
    }

    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 13)
        {
            transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
        }
    }
}
