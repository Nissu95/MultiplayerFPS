using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        Player player = null;

        if (other.CompareTag("Player"))
            player = other.GetComponent<Player>();

        if (player && player.GetTeam() == Team.Blue)
            GameManager.instance.porcent++;

        if (player && player.GetTeam() == Team.Red)
            GameManager.instance.porcent--;
    }

}
