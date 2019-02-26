using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    static Transform mainCamera;

    void Update()
    {
        if (Camera.main)
        {
            mainCamera = Camera.main.transform;
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.position);
        }
    }
}