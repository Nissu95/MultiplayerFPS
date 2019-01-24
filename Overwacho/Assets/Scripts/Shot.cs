using UnityEngine;
using UnityEngine.Networking;

public class Shot : NetworkBehaviour {

    [SerializeField] Transform cameraTransform;
    [SerializeField] LayerMask layers;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            CmdFireShot(cameraTransform.position, cameraTransform.forward);
        }
    }

    [Command]
    void CmdFireShot(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layers))
        {
            if (hit.transform.CompareTag("Player"))
                hit.transform.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
