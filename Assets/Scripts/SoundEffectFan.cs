using UnityEngine;

public class SoundEffectFan : MonoBehaviour
{
    [SerializeField] private AudioClip effectClip;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayLoopingSFX(effectClip);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.StopLoopingSFX();
        }
    }
}
