using UnityEngine;
using System;
using System.IO;
public class InitGame : MonoBehaviour
{
    void Start()
    {
    }

    public void InitializePlayerPrefs()
    {
        string filename1 = Application.dataPath + "/scores.txt";
        string filename2 = Application.dataPath + "/visited.txt";
        if (new FileInfo(filename1).Length == 0 || new FileInfo(filename2).Length == 0)
        {
            PlayerPrefs.SetInt("RoomIndex", 0); // Room index is set to 0 by default, means spawn will be in main room
            PlayerPrefs.SetInt("KeysCollected", 0);
            PlayerPrefs.SetInt("GameWon", 0);

            // Save PlayerPrefs to ensure persistence
            PlayerPrefs.Save();
        }
    }
}
