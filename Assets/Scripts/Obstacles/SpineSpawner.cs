using UnityEngine;

public class SpineSpawner : MonoBehaviour
{
    [Header("Spine Settings")]
    [SerializeField] private GameObject spinePrefab; // Prefab for the spine projectile
    [SerializeField] private Transform spawnPoint; // Point from where spines are spawned
    [SerializeField] private float spawnFrequency = 1f; // Time interval between spine spawns
    [SerializeField] private float spineSpeed = 5f; // Speed of the spines
    [SerializeField] private float initialOffset = 0f; // Initial time offset

    private float timer; // Internal timer to track spawn intervals

    void Start()
    {
        // Initialize the timer with the offset
        timer = initialOffset;
    }

    void Update()
    {
        // Increment the timer by the time elapsed since the last frame
        timer += Time.deltaTime;

        // Check if it's time to spawn a spine
        if (timer >= spawnFrequency)
        {
            SpawnSpine();
            timer -= spawnFrequency; // Reset timer for the next spawn
        }
    }

    private void SpawnSpine()
    {
        // Instantiate the spine at the spawn point
        GameObject spine = Instantiate(spinePrefab, spawnPoint.position, Quaternion.identity);

        // Add velocity to the spine to make it move
        Rigidbody2D rb = spine.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.down * spineSpeed; // Move downwards
        }

        // Destroy the spine after a certain time to prevent clutter
        Destroy(spine, 5f);
    }
}
