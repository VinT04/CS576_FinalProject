using UnityEngine;

using UnityEngine.SceneManagement;



public class LoadSceneOnCollision : MonoBehaviour

{

    public string sceneToLoad; // Name of the scene to load on collision

    private GameObject canvas;

    void Start()
    {
        sceneToLoad = "Intro_maze";
        canvas = GameObject.FindGameObjectWithTag("Canvas-Minimap");
    }

    private void OnTriggerEnter(Collider _)

    {
        Debug.Log("Collided!");
        DontDestroyOnLoad(canvas);
        SceneManager.LoadScene(sceneToLoad);
    }

}
