using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] GameObject deatchVFX;
    [SerializeField] GameObject parent;
    void OnParticleCollision(GameObject other) {
        var vfx = Instantiate(deatchVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(this.gameObject);
    }
}
