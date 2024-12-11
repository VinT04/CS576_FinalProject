using System.Collections; // Required for IEnumerator
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class EntranceTrigger : MonoBehaviour
{
    public string sceneToLoad = "Intro_outdoor"; // Set the name of your new scene in the Inspector
    public Vector3 spawnLocation = new Vector3(842.3f, 0.153762f, 783.6736f); // Position from the image

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering new scene...");
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is loaded
        }

        // Teleport the player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            BlakeMovement blakeMovement = player.GetComponent<BlakeMovement>();
            if (blakeMovement != null)
            {
                blakeMovement.teleport(new Vector3(842.3f, 0.153762f, 783.6736f));
            }
            else
            {
                Debug.LogError("BlakeMovement script not found on the player!");
            }
        }
        else
        {
            Debug.LogError("Player not found in the new scene!");
        }
    }

}
