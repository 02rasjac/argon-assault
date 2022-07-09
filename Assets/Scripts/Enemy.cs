using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Tooltip("Ammount of points player gets when killing this enemy")]
    [SerializeField] int killPoints = 1;

    [Header("VFX")]
    [SerializeField] GameObject deatchVFX;
    [SerializeField] GameObject parent;

    bool exploding = false;
    ScoreBoard scoreBoard;

    void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();    
    }

    void OnParticleCollision(GameObject other) {
        if (!exploding) {
            exploding = true;
            var vfx = Instantiate(deatchVFX, transform.position, Quaternion.identity);
            vfx.transform.parent = parent.transform;

            scoreBoard.IncreaseScore(killPoints);
            Destroy(this.gameObject);
        }
    }
}
