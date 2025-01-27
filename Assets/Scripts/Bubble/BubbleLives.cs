using UnityEngine;
using System.Collections;

public class BubbleLives : MonoBehaviour
{
    [Header("Life Settings")]
    [SerializeField] private int maxLives = 3; 
    [SerializeField] private float firstRegenTime = 4f; 
    [SerializeField] private float regenSpeedup = 2f;
    [SerializeField] private AudioClip effectClip;

    [SerializeField] private int currentLives;
    private float regenTimer;
    private bool isRegenerating;

    private BubbleMovement bubbleMovement;
    private Animator bubbleAnimator;
    private Animator eyesAnimator;

    private Vector2 cactusCollisionPosition;
    private Vector2 surfaceCollisionPosition;

    void Start() 
    { 
        bubbleMovement = GetComponent<BubbleMovement>(); 
        bubbleAnimator = GetComponent<Animator>();
        eyesAnimator = GetComponentInChildren<Animator>();
    }

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

            eyesAnimator.SetBool("Hurt", true);

            GameManager.Instance.AddDeath(1);

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
        eyesAnimator.SetBool("Hurt", false);
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

}
