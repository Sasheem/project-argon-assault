using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;

    // fires upon collision from bullet
    void OnParticleCollision(GameObject other) {
        
        // bring the enemy explosion effect to life and autmatically plays due to setting
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);

        // Point to a parent object we made to hold all objects made at runtime
        vfx.transform.parent = parent;

        // get rid of enemy object
        Destroy(gameObject);
    }
}
