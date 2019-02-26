using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    [SerializeField] float addPercentNumber = 2;

    PointPercentageBar bar;
    bool isBlue = false;
    bool isRed = false;

    private void Awake()
    {
        bar = FindObjectOfType<PointPercentageBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = null;

        if (other.CompareTag("Player"))
            player = other.GetComponent<Player>();

        if (player && player.GetTeam() == Team.Blue)
            isBlue = true;

        if (player && player.GetTeam() == Team.Red)
            isRed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = null;

        if (other.CompareTag("Player"))
            player = other.GetComponent<Player>();

        if (player && player.GetTeam() == Team.Blue)
            isBlue = false;

        if (player && player.GetTeam() == Team.Red)
            isRed = false;
    }

    private void FixedUpdate()
    {
        if (isBlue && !isRed)
            bar.ChangePercentBlue(addPercentNumber);
        else if (!isBlue && isRed)
            bar.ChangePercentRed(addPercentNumber);
    }

}
