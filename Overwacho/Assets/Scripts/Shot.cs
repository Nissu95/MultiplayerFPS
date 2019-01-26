using UnityEngine;
using UnityEngine.Networking;

public class Shot : NetworkBehaviour {

    [SerializeField] GameObject[] muzzleFlash;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform shotPosition;
    [SerializeField] LayerMask layers;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;
    [SerializeField] AudioClip shotSound;

    Animator animator;
    AudioSource audioSource;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            CmdFireShot(cameraTransform.position, cameraTransform.forward);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("reload");
        }
    }

    [Command]
    void CmdFireShot(Vector3 origin, Vector3 direction)
    {
        RpcShotEffects();

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layers))
        {
            if (hit.transform.CompareTag("Player"))
                hit.transform.GetComponent<Health>().TakeDamage(damage);
        }
    }

    [ClientRpc]
    void RpcShotEffects()
    {
        Instantiate<GameObject>(muzzleFlash[Random.Range(0, muzzleFlash.Length)], shotPosition.position, shotPosition.rotation);
        audioSource.PlayOneShot(shotSound);
    }
}
