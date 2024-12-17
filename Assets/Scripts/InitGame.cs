using UnityEngine;

public class InitGame : MonoBehaviour
{
    void Start()
    {
        InitializePlayerPrefs();
    }

    private void InitializePlayerPrefs()
    {
        PlayerPrefs.SetInt("RoomIndex",0); // Room index is set to 0 by default, means spawn will be in main room
        PlayerPrefs.SetInt("KeysCollected",0);
        PlayerPrefs.SetInt("GameWon",0);

        // Save PlayerPrefs to ensure persistence
        PlayerPrefs.Save();
    }
}
