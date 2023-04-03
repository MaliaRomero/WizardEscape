using UnityEngine.SceneManagement;
using UnityEngine;

public class Back2Levels : MonoBehaviour
{
    public int gameStartScene;

    public void ToggleScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

}
