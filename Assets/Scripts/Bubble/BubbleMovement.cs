using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float moveDelay = 0.1f;
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private float erraticForce = 0.8f;
    [SerializeField] private float enableDelay = 2f;

    private Rigidbody2D rb;
    private float lastMoveTime;
    public bool canMove = true;

    private Vector2 bounceDirection;
    [SerializeField] private float repellentForce = 10f;

    private Vector2 currentBubbleForce;
    [SerializeField] private float resistanceCoefficent = 1.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMoveTime = -moveDelay;
        canMove = true;

        StartCoroutine(ErraticMovement());
    }
    private void FixedUpdate()
    {
        Move();
        AirResistance();
    }

    private void Move()
    {
        if (canMove)
        {
            if (Time.time - lastMoveTime >= moveDelay)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");

                Vector2 direction = new Vector2(horizontal, vertical).normalized;

                rb.AddForce(direction * moveForce, ForceMode2D.Impulse);

                if (rb.linearVelocity.magnitude > maxSpeed)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
                }

                lastMoveTime = Time.time;
            }
        }
    }

    private System.Collections.IEnumerator ErraticMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (canMove)
            {
                Vector2 randomDirection = GetRandomDirection();
                rb.AddForce(randomDirection * erraticForce, ForceMode2D.Impulse);
                
                if (rb.linearVelocity.magnitude > maxSpeed)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
                }
            }
        }
    }

    private Vector2 GetRandomDirection()
    {
        Vector2[] directions = new Vector2[]
        {
            Vector2.right,
            Vector2.left,
            Vector2.up,
            Vector2.down,
            new Vector2(0.3f, 0.3f).normalized,
            new Vector2(-0.3f, 0.3f).normalized,
            new Vector2(0.3f, -0.3f).normalized,
            new Vector2(-0.3f, -0.3f).normalized,
        };

        int randomIndex = Random.Range(0, directions.Length);
        return directions[randomIndex];
    }

    public void bounce(Vector2 collisionPosition)
    {
        canMove = false;
        
        bounceDirection = (transform.position - (Vector3)collisionPosition).normalized;

        rb.linearVelocity *= 0.5f;
        
        rb.AddForce(bounceDirection * repellentForce, ForceMode2D.Impulse);

        Invoke(nameof(EnableMovement), enableDelay);
    }

    private void EnableMovement()
    {
        canMove = true;
    }

    private void AirResistance()
    {
        float actualSpeed = rb.linearVelocity.magnitude;

        if (actualSpeed > 0)
        {
            rb.linearDamping = actualSpeed + resistanceCoefficent;
        }
    }
}