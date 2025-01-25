using UnityEngine;
using UnityEngine.Rendering;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float moveDelay = 0.5f;
    [SerializeField] private float interval = 0.2f;
    [SerializeField] private float erraticForce = 3f;

    private Rigidbody2D rb;
    private float lastMoveTime;
    public bool canMove = true;

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

    public void bounce()
    {
        
    }
}