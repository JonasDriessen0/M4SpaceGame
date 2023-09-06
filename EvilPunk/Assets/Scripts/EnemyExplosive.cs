using UnityEngine;

public class EnemyExplosive : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public int maxHP = 100;
    public int damage;
    public float damageEffectDuration = 0.5f;
    public float attackDistance = 7f;
    public float explosionDuration = 1f;

    private int currentHP;
    private bool isTakingDamage = false;
    private float damageEffectTimer;
    private SpriteRenderer[] spriteRenderers;
    private Color[] originalColors;
    private Animator animator;
    private SphereCollider explosionRadius;
    private bool isExplosionActive = false;

    private void Start()
    {
        currentHP = maxHP;
        FindPlayer();

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        originalColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
        }

        animator = GetComponentInChildren<Animator>();
        explosionRadius = GetComponentInChildren<SphereCollider>();
        explosionRadius.enabled = false;
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

            if (direction.x > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (direction.x < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            bool shouldAttack = distanceToPlayer <= attackDistance;
            animator.SetBool("Attack", shouldAttack);

            if (shouldAttack)
            {
                if (!isExplosionActive)
                {
                    Invoke("ActivateExplosionRadius", 1f);
                    isExplosionActive = true;
                }
            }
        }

        if (isTakingDamage)
        {
            damageEffectTimer -= Time.deltaTime;
            if (damageEffectTimer <= 0f)
            {
                StopDamageEffect();
            }
            else
            {
                float t = Mathf.PingPong((damageEffectTimer / damageEffectDuration) * 2f, 1f);

                for (int i = 0; i < spriteRenderers.Length; i++)
                {
                    Color targetColor = Color.Lerp(originalColors[i], Color.red, t);
                    spriteRenderers[i].color = targetColor;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bullet bullet = other.GetComponent<bullet>();

        if (bullet != null)
        {
            currentHP -= bullet.damage;
            Destroy(other.gameObject);

            if (currentHP <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                StartDamageEffect();
            }
        }
    }

    private void StartDamageEffect()
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true;
            damageEffectTimer = damageEffectDuration;
        }
    }

    private void StopDamageEffect()
    {
        isTakingDamage = false;
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
    }

    private void ActivateExplosionRadius()
    {
        if (!explosionRadius.enabled)
        {
            explosionRadius.enabled = true;
            Invoke("DisableExplosionRadius", explosionDuration);
            Destroy(gameObject, 1f);
        }
    }

    private void DisableExplosionRadius()
    {
        explosionRadius.enabled = false;
        isExplosionActive = false;
    }
}
