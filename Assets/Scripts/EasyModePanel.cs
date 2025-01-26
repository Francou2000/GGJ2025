using UnityEngine;

public class EasyModePanel : MonoBehaviour
{
    public void TurnOffEasyPannel() { LoadScenesUtils.ReLoadLevel(); }

    public void ChangeToEasyGame(bool decision) 
    {
        SaveSystem.SaveLevel(decision);
        LoadScenesUtils.LoadSceneByName("MainMenu");
    }

    public void LoadLevelByName(string name) { LoadScenesUtils.LoadSceneByName(name); }
}
