using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake() {
        // Singleton pattern
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;

        // destorys or don't destroy this current instance of the class
        if (numMusicPlayers > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
