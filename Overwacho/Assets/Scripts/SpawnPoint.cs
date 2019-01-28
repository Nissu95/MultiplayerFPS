using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField] Team team;

	void Start () {
        Spawner.singleton.SetSpawnPoint(team, this.transform);
	}
}
