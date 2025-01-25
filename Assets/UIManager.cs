using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private List<GameObject> bouncesSprites;
    [SerializeField] private GameObject bounceSpritePrefab, bouncesContainer;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Update() { timerText.text = GameManager.Instance.GetElapsedTime(); }

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
}
