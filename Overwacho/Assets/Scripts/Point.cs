using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    [SerializeField] float addPercentNumber = 2;

    List<Player> players = new List<Player>();
    PointPercentageBar bar;
    bool isBlue = false;
    bool isRed = false;

    private void Awake()
    {
        bar = GetComponentInChildren<PointPercentageBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            players.Add(other.GetComponent<Player>());

        foreach (Player player in players)
        {
            if (player.GetTeam() == Team.Blue)
                isBlue = true;
            if (player.GetTeam() == Team.Red)
                isRed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player thisPlayer = null;

        if (other.CompareTag("Player"))
            thisPlayer = other.GetComponent<Player>();

        foreach (Player player in players)
        {
            if (thisPlayer == player)
            {
                if (player.GetTeam() == Team.Blue)
                    isBlue = false;
                if (player.GetTeam() == Team.Red)
                    isRed = false;
                players.Remove(player);
            }
        }

    }

    private void FixedUpdate()
    {
        foreach (Player player in players)
        {
            if (!player.GetComponent<Health>().isAlive)
            {
                if (player.GetTeam() == Team.Blue)
                    isBlue = false;
                if (player.GetTeam() == Team.Red)
                    isRed = false;
                players.Remove(player);
            }
        }

        if (isBlue && !isRed)
            bar.ChangePercentBlue(addPercentNumber);
        else if (!isBlue && isRed)
            bar.ChangePercentRed(addPercentNumber);
    }

}
