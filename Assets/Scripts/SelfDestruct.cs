/*
    Destroy the gameobject when it's particlesystem stops playing.
*/

using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    void Update() {
        if (!GetComponent<ParticleSystem>().isPlaying) {
            Destroy(this.gameObject);
        }
    }
}
