using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Instance.GetPlayer())
        {
            UIManager.Instance.VictoryPanel();
            Destroy(gameObject);
        }
    }
}
