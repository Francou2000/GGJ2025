using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //FALTA TODA LA PARTE DE CONTROL DE SLIDERS DE VOLUMEN QUE VA POR ACA
    public void TurnOffPause() { UIManager.Instance.PauseMenu(); }
    public void LoadLevelByName(string name) { LoadScenesUtils.LoadSceneByName(name); }
}
