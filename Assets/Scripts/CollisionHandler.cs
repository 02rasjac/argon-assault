using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [SerializeField] float loadDelay = 1f;
    [SerializeField] GameObject explosion;

    bool initiated = false;

    void OnTriggerEnter(Collider other) {
        Debug.Log($"{this.name} **triggered by** {other.gameObject.name}");
        if (!initiated) {
            initiated = true;
            InitiateCrash();
        }
    }

    void InitiateCrash() {
        // Activate explosion VFX and disable controls
        explosion.GetComponent<ParticleSystem>().Play();
        GetComponent<PlayerController>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadScene", loadDelay);
    }

    void ReloadScene() {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
