using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private float playerLifes;
    private float elapsedTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() { for (int i = 0; i < playerLifes; i++) { UIManager.Instance.AddBounds(); } }

    private void Update() { elapsedTime += Time.deltaTime;}

    public string GetElapsedTime() { return string.Format("{0:00}:{1:00}", Mathf.FloorToInt(elapsedTime / 60), Mathf.FloorToInt(elapsedTime % 60)); }

    public void AddDeath(int quantity) 
    {
        playerLifes -= quantity; 
        UIManager.Instance.RemoveBounds();
        if (playerLifes <= 0)
        {
            //Activar modo facil
        }
    }


}
