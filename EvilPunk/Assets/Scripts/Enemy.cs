using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public int maxHP = 100;
    public int damage;
    private int currentHP;

    private void Start()
    {
        currentHP = maxHP;
        FindPlayer();
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player GameObject not found!");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHP -= damage;
            Destroy(other.gameObject);

            if (currentHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
