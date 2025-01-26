using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button loadGameButton;
    [SerializeField] Button easyGameButton;
    private void Start() 
    {
        if (SaveSystem.LoadLevel() != null && SaveSystem.LoadLevel().chickenMode)
        {
            easyGameButton.gameObject.SetActive(true);
            if (SaveSystem.LoadGame() != null) loadGameButton.gameObject.SetActive(true);
            else loadGameButton.gameObject.SetActive(false);
        }
        else
        {
            easyGameButton.gameObject.SetActive(false);
            loadGameButton.gameObject.SetActive(false);
        }
    }
    public void PlayNewGame(string name) 
    {
        SaveSystem.DeleteAllData();
        LoadScenesUtils.LoadSceneByName(name);
    }
    public void PlayLoadGame(string name) { LoadScenesUtils.LoadSceneByName(name); }

    public void ExitGame() { Application.Quit(); }
}
