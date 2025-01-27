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

    private Vector2 cactusCollisionPosition;
    private Vector2 surfaceCollisionPosition;

    void Start() { bubbleMovement = GetComponent<BubbleMovement>(); }

    void Update()
    {
        if (isRegenerating)
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0f && currentLives < maxLives) { RegenerateLife(); }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cactus")
        {
            Debug.Log("ColisionCactus");
            LoseLife();
            cactusCollisionPosition = collision.transform.position;
            bubbleMovement.bounce(cactusCollisionPosition);
        }

        if (collision.gameObject.tag == "Surface")
        {
            Debug.Log("ColisionSurface");
            LoseLife();
            surfaceCollisionPosition = collision.ClosestPoint(transform.position);
            bubbleMovement.bounce(surfaceCollisionPosition);
        }
    }

    private void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;

            GameManager.Instance.AddDeath(1);

            StartRegeneration();

            if (currentLives <= 0) { GameManager.Instance.OnPlayerDie(); }
        }
    }

    private void StartRegeneration()
    {
        isRegenerating = true;
        regenTimer = firstRegenTime; 
    }

    private void StopRegeneration() { isRegenerating = false; }

    private void RegenerateLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            regenTimer = Mathf.Max(0.5f, regenTimer - regenSpeedup);
        }
        if (currentLives == maxLives) { StopRegeneration(); }
    }

    public void AddLife(int quanity) { currentLives += quanity; }
}
