using UnityEngine;
using TMPro;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private void Start() { scoreText.text = "Your Score: " + GameManager.Instance.GetElapsedTime(); }
    public void LoadLevelByName(string name) { LoadScenesUtils.LoadSceneByName(name); }
}
