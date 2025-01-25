using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button loadGameButton;
    private void Start() { if (SaveSystem.LoadGame() == null) loadGameButton.interactable = false; }
    public void PlayNewGame(string name) 
    {
        SaveSystem.DeleteAllData();
        LoadScenesUtils.LoadSceneByName(name);
    }
    public void PlayLoadGame(string name) { LoadScenesUtils.LoadSceneByName(name); }

    public void ExitGame() { Application.Quit(); }
}
