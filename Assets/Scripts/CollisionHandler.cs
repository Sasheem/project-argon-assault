using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    void OnTriggerEnter(Collider other) {
        StartCrashSequence();
    }

    void StartCrashSequence() {
        crashVFX.Play();
        DisableChildrenMesh();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void DisableChildrenMesh() {
        foreach (MeshRenderer meshInChild in GetComponentsInChildren<MeshRenderer>())
            meshInChild.enabled = false;
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
