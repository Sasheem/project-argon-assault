using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
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
        vfx.transform.parent = parentGameObject.transform;
        // adjust hit points
        hitPoints--;
    }

    void KillEnemy() {
        // update scoreboard
        scoreBoard.IncreaseScore(scorePerHit);
        
        // bring the enemy explosion effect to life and autmatically plays due to setting
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);

        // Point to a parent object we made to hold all objects made at runtime
        vfx.transform.parent = parentGameObject.transform;

        // get rid of enemy object
        Destroy(gameObject);
    }
}
