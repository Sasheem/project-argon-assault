using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;

    ScoreBoard scoreBoard;

    void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // fires upon collision from bullet
    void OnParticleCollision(GameObject other) {
        ProcessHit();
        KillEnemy();
        
    }

    void ProcessHit() {
        // update scoreboard
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void KillEnemy() {
        // bring the enemy explosion effect to life and autmatically plays due to setting
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);

        // Point to a parent object we made to hold all objects made at runtime
        vfx.transform.parent = parent;

        // get rid of enemy object
        Destroy(gameObject);
    }
}
