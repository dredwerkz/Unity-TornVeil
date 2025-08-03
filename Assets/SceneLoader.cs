using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainGame()
    {
        Debug.Log("Doing a thing..");
        SceneManager.LoadScene("MainGame");
    }
}
