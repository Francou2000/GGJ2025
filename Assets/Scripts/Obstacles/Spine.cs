using System;
using UnityEngine;

public class Spine : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] float minTime;
    private float lifeTime = 0f;

    private void Update()
    {
        lifeTime += Time.deltaTime;
    }

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
        else if (collision.CompareTag("Surface") && lifeTime > minTime)
        {
            Destroy(gameObject);
        }
    }
}

