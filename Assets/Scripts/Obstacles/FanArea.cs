using UnityEngine;

public class FanArea : MonoBehaviour
{
    [Header("Fan Settings")]
    [SerializeField] private Vector2 pushDirection = Vector2.right; 
    [SerializeField] private float pushForce = 10f; 
    [SerializeField] private Vector2 areaSize = new Vector2(5f, 2f);
    [SerializeField] private Vector2 areaOffset = new Vector2(2.5f, 0f);
    [SerializeField] private CircleCollider2D soundArea;

    private BoxCollider2D fanCollider;

    private void Awake()
    {
        fanCollider = GetComponent<BoxCollider2D>();
        fanCollider.isTrigger = true; 
        fanCollider.size = areaSize;
        fanCollider.offset = areaOffset;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(pushDirection.normalized * pushForce);
            }
        }
    }
}
