using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Instance.GetPlayer())
        {
            SaveSystem.SaveGame(GameManager.Instance.GetPlayerLifes(), GameManager.Instance.GetTime(), GameManager.Instance.GetPlayer().transform.position);
            Destroy(gameObject);
        }
    }
}
