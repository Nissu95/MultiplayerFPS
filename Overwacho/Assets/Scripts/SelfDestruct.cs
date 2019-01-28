using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    private void Start()
    {
        Invoke("Destruction", 5);
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}
