using UnityEngine;
using TMPro;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI chickenModeData;
    private void Start() 
    {
        LevelData levelData = SaveSystem.LoadLevel();
        scoreText.text = "Your Score: " + GameManager.Instance.GetElapsedTime();
        if (levelData != null && levelData.chickenMode) chickenModeData.text = "Chicken Mode is: " + levelData.chickenMode.ToString();
        else chickenModeData.text = "Chicken Mode is: false";
    }
    public void LoadLevelByName(string name) { LoadScenesUtils.LoadSceneByName(name); }
}
