using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public static Spawner singleton;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (singleton != null)
            Destroy(gameObject);
        else
            singleton = this;
    }


    List<Transform> redTeamSpawnPoints = new List<Transform>();
    List<Transform> blueTeamSpawnPoints = new List<Transform>();

    public Transform GetSpawnPoint(Team team)
    {
        switch (team)
        {
            case Team.Red:
                return redTeamSpawnPoints[Random.Range(0, redTeamSpawnPoints.Count)];
            case Team.Blue:
                return blueTeamSpawnPoints[Random.Range(0, blueTeamSpawnPoints.Count)];
        }

        return null;
    }

    public void SetSpawnPoint(Team team, Transform spawnPoint)
    {
        switch (team)
        {
            case Team.Red:
                redTeamSpawnPoints.Add(spawnPoint);
                break;
            case Team.Blue:
                blueTeamSpawnPoints.Add(spawnPoint);
                break;
        }
    }
}
