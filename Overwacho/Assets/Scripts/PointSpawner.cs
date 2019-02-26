using UnityEngine.Networking;
using UnityEngine;

public class PointSpawner : NetworkBehaviour {

    [SerializeField] GameObject prefab;

    public override void OnStartServer()
    {
        base.OnStartServer();
        GameObject pref = Instantiate(prefab, transform.position, transform.rotation);
        NetworkServer.Spawn(pref);
    }
}
