using System.Security.Cryptography;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(string name) { LoadScenesUtils.LoadSceneByName(name); }

    public void ExitGame() { Application.Quit(); }
}
