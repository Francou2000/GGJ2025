using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private Vector2 screenSize = new Vector2(16, 9); 
    [SerializeField] private float smoothSpeed = 0.125f; 
    [SerializeField] private Vector2 screenOffset; 

    private Vector2 currentScreenCenter; 

    void Start()
    {
        UpdateCurrentScreen();
    }

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

    private void UpdateCurrentScreen(Vector2? newPlayerPosition = null)
    {
        Vector2 position = newPlayerPosition ?? player.position;

        float screenX = Mathf.Floor(position.x / screenSize.x) * screenSize.x;
        float screenY = Mathf.Floor(position.y / screenSize.y) * screenSize.y;
        currentScreenCenter = new Vector2(screenX, screenY) + screenSize / 2 + screenOffset;
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
