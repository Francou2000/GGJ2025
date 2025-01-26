using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Nombre de la escena del menu")
        {
            AudioManager.Instance.PlayMusic(menuMusic);
        }
        else if (currentScene == "Nombre de la escena del gameplay")
        {
            AudioManager.Instance.PlayMusic(gameplayMusic);
        }
    }
}
