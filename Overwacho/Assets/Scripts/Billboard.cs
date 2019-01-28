using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    static Transform mainCamera;

    void Update()
    {
        mainCamera = Camera.main.transform;

        if (mainCamera)
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.position);
    }
}