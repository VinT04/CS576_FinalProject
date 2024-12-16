using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void loadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void quitScene()
    {
        // works on build only
        Application.Quit();
    }
}
