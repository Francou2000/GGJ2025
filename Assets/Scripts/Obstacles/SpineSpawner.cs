using UnityEngine;

public class SpineSpawner : MonoBehaviour
{
    [Header("Spine Settings")]
    [SerializeField] private GameObject spinePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnFrequency = 1f;
    [SerializeField] private float spineSpeed = 5f; 
    [SerializeField] private float initialOffset = 0f;
    [SerializeField] private AudioClip effectClip;

    private float timer; 

    void Start()
    {
        timer = initialOffset;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnFrequency)
        {
            SpawnSpine();
            timer -= spawnFrequency; 
        }
    }

    private void SpawnSpine()
    {
        GameObject spine = Instantiate(spinePrefab, spawnPoint.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX(effectClip);

        Rigidbody2D rb = spine.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.down * spineSpeed; 
        }

        Destroy(spine, 2f);
    }
}
