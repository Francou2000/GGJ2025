using UnityEngine;
using System.Collections;

public class BubbleLives : MonoBehaviour
{
    [Header("Life Settings")]
    [SerializeField] private int maxLives = 3; 
    [SerializeField] private float firstRegenTime = 4f; 
    [SerializeField] private float regenSpeedup = 2f;
    [SerializeField] private AudioClip effectClip;
    [SerializeField] private GameObject eyesHurt;
    [SerializeField] private GameObject eyesIdle;
    [SerializeField] private GameObject eyesScared;

    [SerializeField] private int currentLives;
    private float regenTimer;
    private bool isRegenerating;

    private BubbleMovement bubbleMovement;
    private Animator bubbleAnimator;

    private Vector2 cactusCollisionPosition;
    private Vector2 surfaceCollisionPosition;

    [SerializeField] private LayerMask cactusAndSurfaceMask; 
    [SerializeField] private float losRange = 10f;

    private enum EyeState { Idle, Scared, Hurt }
    private EyeState currentEyeState = EyeState.Idle;

    private bool isScared = false;

    void Start() 
    { 
        bubbleMovement = GetComponent<BubbleMovement>(); 
        bubbleAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isRegenerating)
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0f && currentLives < maxLives) { RegenerateLife(); }
            UpdateEyes(EyeState.Hurt);
        }
        CheckLineOfSight();
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
            UpdateEyes(EyeState.Hurt); 

            StartRegeneration();

            if (currentLives <= 0) { StartCoroutine(Death()); }
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

    public void AddLife(int quanity) { currentLives += quanity; }

    private IEnumerator Death()
    {
        AudioManager.Instance.PlaySFX(effectClip);

        bubbleAnimator.SetTrigger("Death");

        yield return new WaitForSeconds(bubbleAnimator.GetCurrentAnimatorStateInfo(0).length);

        GameManager.Instance.OnPlayerDie();
    }

    private void CheckLineOfSight()
    {
        int rayCount = 360; 
        float angleStep = 360f / rayCount;
        bool isInSight = false;


        if (currentLives < maxLives)
        {
            UpdateEyes(EyeState.Hurt);
            return; 
        }


        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, losRange, cactusAndSurfaceMask);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Cactus") || hit.collider.CompareTag("Surface"))
                {
                    UpdateEyes(EyeState.Scared); 
                    isScared = true;
                    isInSight = true;
                    Debug.Log($"LoS with {hit.collider.name}");
                    break; 
                }
            }
        }

        if (!isInSight)
        {
            if (currentLives == maxLives) 
            {
                if (isScared) 
                {
                    UpdateEyes(EyeState.Idle);
                    isScared = false; 
                }
            }
            else if (!isScared) 
            {
                UpdateEyes(EyeState.Hurt);
            }
        }
    }

    private void UpdateEyes(EyeState newState)
    {
        if (currentEyeState == newState) return;

        currentEyeState = newState;

        switch (currentEyeState)
        {
            case EyeState.Hurt:
                eyesHurt.SetActive(true);
                eyesIdle.SetActive(false);
                eyesScared.SetActive(false);
                break;

            case EyeState.Scared:
                eyesScared.SetActive(true);
                eyesIdle.SetActive(false);
                eyesHurt.SetActive(false);
                break;

            case EyeState.Idle:
            default:
                eyesIdle.SetActive(true);
                eyesHurt.SetActive(false);
                eyesScared.SetActive(false);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        int rayCount = 360; 
        float angleStep = 360f / rayCount;
        Gizmos.color = isScared ? Color.red : Color.green;


        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * losRange);
        }
       
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}
