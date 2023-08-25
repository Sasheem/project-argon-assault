using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;

    ScoreBoard scoreBoard;

    void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddRigidbody();
    }

    // allows for children colliders to register collisions
    void AddRigidbody() {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // fires upon collision from bullet
    void OnParticleCollision(GameObject other) {
        ProcessHit();
        if (hitPoints < 1) {
            KillEnemy();
        }
    }

    void ProcessHit() {
        // play hitVFX
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        // adjust hit points
        hitPoints--;
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
