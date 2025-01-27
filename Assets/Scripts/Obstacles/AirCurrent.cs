using System.Collections;
using UnityEngine;

public class AirCurrent : MonoBehaviour
{
    [Header("Path Settings")]
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float transportSpeed = 5f;

    [Header("Suction Settings")]
    [SerializeField] private float suctionForce = 10f;
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform exitPoint;

    private bool isPlayerInCurrent = false;
    private int currentPathIndex = 0;
    [SerializeField] private AudioClip effectClip;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null && !isPlayerInCurrent)
            {
                Vector2 directionToEntry = (entryPoint.position - other.transform.position).normalized;
                playerRb.linearVelocity = directionToEntry * suctionForce;
                AudioManager.Instance.PlayLoopingSFX(effectClip);

                if (Vector2.Distance(other.transform.position, entryPoint.position) < 0.5f)
                {
                    isPlayerInCurrent = true;
                    playerRb.linearVelocity = Vector2.zero;
                    other.GetComponent<BubbleMovement>().canMove = false;
                    StartCoroutine(TransportPlayer(other.transform, playerRb));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<BubbleMovement>().canMove = true;
            isPlayerInCurrent = false;
            currentPathIndex = 0;
            AudioManager.Instance.StopLoopingSFX();
        }
    }

    private IEnumerator TransportPlayer(Transform player, Rigidbody2D playerRb)
    {
        while (isPlayerInCurrent && currentPathIndex < pathPoints.Length)
        {

            Vector2 targetPosition = pathPoints[currentPathIndex].position;
            Vector2 direction = (targetPosition - (Vector2)player.position).normalized;

            playerRb.linearVelocity = direction * transportSpeed;

            if (Vector2.Distance(player.position, targetPosition) < 0.2f)
            {
                currentPathIndex++;
            }

            yield return null;
        }

        if (currentPathIndex >= pathPoints.Length)
        {
            Vector2 directionToExit = (exitPoint.position - player.position).normalized;
            playerRb.linearVelocity = directionToExit * suctionForce;
        }

        isPlayerInCurrent = false;
    }
}
