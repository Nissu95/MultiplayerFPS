using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSlider : MonoBehaviour {

    [SerializeField] GameObject slider;

    private void Start()
    {
        slider.SetActive(true);
    }

}
