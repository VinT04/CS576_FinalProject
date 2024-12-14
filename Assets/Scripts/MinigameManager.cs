using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject[] minigames; // Array of minigame prefabs or UI elements

    public void StartMinigame(int roomIndex)
    {
        if (roomIndex < 0 || roomIndex >= minigames.Length)
        {
            Debug.LogError($"Invalid room index {roomIndex}. No minigame available.");
            return;
        }

        // Activate the appropriate minigame
        for (int i = 0; i < minigames.Length; i++)
        {
            minigames[i].SetActive(i == roomIndex); // Activate only the corresponding minigame
        }

        Debug.Log($"Minigame {roomIndex} started.");
    }

    public void OnMinigameComplete()
    {
        // Award a key and deactivate minigames
        int keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
        keysCollected++;
        PlayerPrefs.SetInt("KeysCollected", keysCollected);
        Debug.Log("Minigame completed! Returning to room.");

        foreach (var minigame in minigames)
        {
            minigame.SetActive(false); // Deactivate all minigames
        }

        // Return to the room
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomScene");
    }
}
