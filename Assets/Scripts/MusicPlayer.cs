using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    void Awake() {
        int numOfInstances = FindObjectsOfType<MusicPlayer>().Length;
        if (numOfInstances > 1) {
            Destroy(gameObject);
        } 
        else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
