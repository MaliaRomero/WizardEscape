using UnityEngine.SceneManagement;
using UnityEngine;

public class Back3Levels : MonoBehaviour

{
    public int gameStartScene;

    public void ToggleScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

}

