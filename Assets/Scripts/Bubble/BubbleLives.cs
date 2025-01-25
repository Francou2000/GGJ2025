using UnityEngine;

public class BubbleLives : MonoBehaviour
{
    [Header("Life Settings")]
    [SerializeField] private int maxLives = 3; 
    [SerializeField] private float firstRegenTime = 4f; 
    [SerializeField] private float regenSpeedup = 2f;

    [SerializeField] private int currentLives;
    private float regenTimer;
    private bool isRegenerating;

    private BubbleMovement bubbleMovement;

    void Start()
    {
        currentLives = maxLives;
        bubbleMovement = GetComponent<BubbleMovement>();
    }

    void Update()
    {
        if (isRegenerating)
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0f && currentLives < maxLives)
            {
                RegenerateLife();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cactus") || collision.CompareTag("Surface"))
        {
            LoseLife();
            Debug.Log("Colisiono");
        }
    }

    private void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;

            bubbleMovement.bounce();

            StartRegeneration();

            if (currentLives <= 0)
            {
                GameManager.Instance.AddDeath(1);

                GameManager.Instance.OnPlayerDie();
            }
        }
    }

    private void StartRegeneration()
    {
        isRegenerating = true;
        regenTimer = firstRegenTime; 
    }

    private void StopRegeneration()
    {
        isRegenerating = false;
    }

    private void RegenerateLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;

            regenTimer = Mathf.Max(0.5f, regenTimer - regenSpeedup);
        }

        if (currentLives == maxLives)
        {
            StopRegeneration();
        }
    }
}
