using UnityEngine;

public class Spine : MonoBehaviour
{
    [SerializeField] private int damage = 1; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Instance.GetPlayer())
        {
            GameManager.Instance.AddDeath(damage);

            if (GameManager.Instance.GetPlayerLifes() <= 0)
            {
                GameManager.Instance.OnPlayerDie();
            }
        }
    }
}
