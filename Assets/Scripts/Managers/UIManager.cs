using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private List<GameObject> bouncesSprites;
    [SerializeField] private GameObject bouncesContainer, canvas;
    [Header("Prefabs")]
    [SerializeField] private GameObject bounceSpritePrefab, pausePanelPrefab, easyPanelPrefab;
    private GameObject pausePanelInstance;
    private GameObject easyPanelInstance;
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

    public void RemoveBounds()
    {
        if (bouncesSprites.Count > 0)
        {
            GameObject lastImage = bouncesSprites[bouncesSprites.Count - 1];
            Destroy(lastImage);
            bouncesSprites.RemoveAt(bouncesSprites.Count - 1);
        }
    }

    public void AddBounds()
    {
        GameObject newBound = Instantiate(bounceSpritePrefab, bouncesContainer.transform);
        bouncesSprites.Add(newBound);
    }

    public void RestartBounds(int lifes)
    {
        foreach (GameObject bound in bouncesSprites) { Destroy(bound); }
        bouncesSprites.Clear();

        for (int i = 0; i < lifes; i++)
        {
            GameObject newBound = Instantiate(bounceSpritePrefab, bouncesContainer.transform);
            bouncesSprites.Add(newBound);
        }
    }

    public void LoadLevelByName(string name) { LoadScenesUtils.LoadSceneByName(name); }
}
