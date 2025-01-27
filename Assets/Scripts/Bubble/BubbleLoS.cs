using UnityEngine;

public class BubbleLoS : MonoBehaviour
{
    private Animator eyesAnimator;

    void Start()
    {
        eyesAnimator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Cactus") || other.CompareTag("Surface"))
        {
            eyesAnimator.SetBool("Scared", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cactus") || other.CompareTag("Surface"))
        {
            eyesAnimator.SetBool("Scared", false);
        }
    }
}
