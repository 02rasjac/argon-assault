using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Tooltip("Ammount of points player gets when killing this enemy")]
    [SerializeField] int pointsPerHit = 1;
    [Tooltip("How many hits the enemy can take before dieing")]
    [SerializeField] int health = 3;

    [Header("VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject parent;

    bool exploding = false;
    ScoreBoard scoreBoard;

    void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();    
    }

    void OnParticleCollision(GameObject other) {
        health--;
        if (!exploding) { 
            scoreBoard.IncreaseScore(pointsPerHit);
            var hitVFXInst = Instantiate(hitVFX, transform.position, Quaternion.identity);
            hitVFXInst.transform.parent = parent.transform;
            
            if (health <= 0) {
                exploding = true;
                var deathVFXInst = Instantiate(deathVFX, transform.position, Quaternion.identity);
                deathVFXInst.transform.parent = parent.transform;

                Destroy(this.gameObject);
            }
        }
    }
}
