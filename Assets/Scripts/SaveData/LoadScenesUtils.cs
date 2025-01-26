using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadScenesUtils
{
    public static void LoadSceneByName(string name) { SceneManager.LoadScene(name); }
    public static void LoadSceneByIndex(int index) { SceneManager.LoadScene(index); }
    public static void LoadNextScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    public static void ReLoadLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
}
