using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject canvas;
    [Header("Prefabs")]
    [SerializeField] private GameObject pausePanelPrefab, easyPanelPrefab, victoryPanelPrefab;
    private GameObject pausePanelInstance;
    private GameObject easyPanelInstance;
    private GameObject victoryPanelInstance;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Update() 
    { 
        timerText.text = GameManager.Instance.GetElapsedTime();
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) { PauseMenu(); }
    }

    public void PauseMenu()
    {
        if (pausePanelInstance == null)
        {
            pausePanelInstance = Instantiate(pausePanelPrefab, canvas.transform);
            Time.timeScale = 0f;
        }
        else
        {
            Destroy(pausePanelInstance);
            pausePanelInstance = null;
            Time.timeScale = 1f;
        }
    }

    public void EasyPanel()
    {
        if (easyPanelInstance == null)
        {
            easyPanelInstance = Instantiate(easyPanelPrefab, canvas.transform);
            Time.timeScale = 0f;
        }
        else
        {
            Destroy(easyPanelInstance);
            easyPanelInstance = null;
            Time.timeScale = 1f;
        }
    }

    public void VictoryPanel()
    {
        if (victoryPanelInstance == null)
        {
            victoryPanelInstance = Instantiate(victoryPanelPrefab, canvas.transform);
            Time.timeScale = 0f;
        }
        else
        {
            Destroy(victoryPanelInstance);
            victoryPanelInstance = null;
            Time.timeScale = 1f;
        }
    }

    public void LoadLevelByName(string name) { LoadScenesUtils.LoadSceneByName(name); }
}
