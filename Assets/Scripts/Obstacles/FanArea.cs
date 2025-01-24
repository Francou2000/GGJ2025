using UnityEngine;

public class FanArea : MonoBehaviour
{
    [Header("Fan Settings")]
    [SerializeField] private Vector2 pushDirection = Vector2.right; 
    [SerializeField] private float pushForce = 10f; 
    [SerializeField] private Vector2 areaSize = new Vector2(5f, 2f); 

    private BoxCollider2D fanCollider;

    private void Awake()
    {
        fanCollider = GetComponent<BoxCollider2D>();
        fanCollider.isTrigger = true; 
        fanCollider.size = areaSize; 
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(pushDirection.normalized * pushForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
