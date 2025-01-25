using UnityEngine;
using UnityEngine.Rendering;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float moveDelay = 0.5f;
    private Rigidbody2D rb;
    private float lastMoveTime;

    public bool canMove = true;

    void Start() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        lastMoveTime = -moveDelay; 
        canMove = true;
    }
    
    private void FixedUpdate() { Move(); }

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

    public void bounce()
    {

    }
}