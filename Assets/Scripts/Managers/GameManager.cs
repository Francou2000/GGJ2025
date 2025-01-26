using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int playerLifes;
    [SerializeField] private GameObject player;
    private float elapsedTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() 
    {
        Time.timeScale = 1f;
        if (SaveSystem.LoadGame() != null)
        {
            SaveData loadedData = SaveSystem.LoadGame();
            if (loadedData != null)
            {
                playerLifes = loadedData.lives;
                elapsedTime = loadedData.time;
                player.transform.position = loadedData.GetPosition();
                UIManager.Instance.RestartBounds(playerLifes);
                player.GetComponent<BubbleLives>().AddLife(loadedData.lives);
            }
        }
        else 
        { 
            for (int i = 0; i < playerLifes; i++) { UIManager.Instance.AddBounds(); }
            player.GetComponent<BubbleLives>().AddLife(playerLifes);
        }
    }

    private void Update() { elapsedTime += Time.deltaTime; }

    public GameObject GetPlayer() { return player; }

    public int GetPlayerLifes() { return playerLifes; }

    public string GetElapsedTime() { return string.Format("{0:00}:{1:00}", Mathf.FloorToInt(elapsedTime / 60), Mathf.FloorToInt(elapsedTime % 60)); }

    public float GetTime() { return elapsedTime; }

    public void SetTime(float time) { elapsedTime = time; }

    public void OnPlayerDie() 
    {
        SaveData loadedData = SaveSystem.LoadGame();
        if (loadedData != null)
        {
            playerLifes = loadedData.lives;
            elapsedTime = loadedData.time;
            player.transform.position = loadedData.GetPosition();
            UIManager.Instance.RestartBounds(playerLifes);
            player.GetComponent<BubbleLives>().AddLife(loadedData.lives);
        }
        else { UIManager.Instance.EasyPanel(); }
    }

    public void AddDeath(int quantity) 
    {
        playerLifes -= quantity; 
        UIManager.Instance.RemoveBounds();
    }
}
