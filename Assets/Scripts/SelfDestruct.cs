using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeTillDestory = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeTillDestory);
    }
}
