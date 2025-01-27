using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 screenSize = new Vector2(16, 9);
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector2 screenOffset;

    private Vector2 currentScreenCenter;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.position;
            if (!IsPlayerWithinCurrentScreen(playerPosition))
            {
                UpdateCurrentScreen(playerPosition);
            }

            Vector3 targetPosition = new Vector3(currentScreenCenter.x, currentScreenCenter.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }

    private void UpdateCurrentScreen(Vector2 playerPosition)
    {
        float halfWidth = screenSize.x / 2;
        float halfHeight = screenSize.y / 2;

        if (playerPosition.x > currentScreenCenter.x + halfWidth)
        {
            currentScreenCenter.x += screenSize.x;
        }
        else if (playerPosition.x < currentScreenCenter.x - halfWidth)
        {
            currentScreenCenter.x -= screenSize.x;
        }

        if (playerPosition.y > currentScreenCenter.y + halfHeight)
        {
            currentScreenCenter.y += screenSize.y;
        }
        else if (playerPosition.y < currentScreenCenter.y - halfHeight)
        {
            currentScreenCenter.y -= screenSize.y;
        }

        currentScreenCenter += screenOffset;
    }

    private bool IsPlayerWithinCurrentScreen(Vector2 playerPosition)
    {
        float halfWidth = screenSize.x / 2;
        float halfHeight = screenSize.y / 2;

        return playerPosition.x >= currentScreenCenter.x - halfWidth &&
               playerPosition.x <= currentScreenCenter.x + halfWidth &&
               playerPosition.y >= currentScreenCenter.y - halfHeight &&
               playerPosition.y <= currentScreenCenter.y + halfHeight;
    }
}
