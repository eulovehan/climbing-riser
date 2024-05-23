using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float fallSpeed = 15f;
    public string[] tagsToIgnore = { "Hold", "Pipe" }; // 무시할 태그 배열
    public string tagToPlayer = "Player";

    private void Start()
    {
        // 모든 Collider2D를 찾아서 태그를 검사하고 충돌을 무시하도록 설정
        Collider2D[] collidersToIgnore = FindObjectsOfType<Collider2D>();
        Collider2D obstacleCollider = GetComponent<Collider2D>();

        foreach (Collider2D collider in collidersToIgnore)
        {
            foreach (string tag in tagsToIgnore)
            {
                if (collider.CompareTag(tag))
                {
                    Physics2D.IgnoreCollision(obstacleCollider, collider, true);
                }
            }
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagToPlayer))
        {
            collision.gameObject.GetComponent<index>().obstacleShock();
            remove();
            // GameManager.Instance.GameOver();
        }
    }

    // remove
    void remove() {
        Destroy(gameObject);
    }
}