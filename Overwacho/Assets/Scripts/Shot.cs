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
    Player playerScript;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerScript = GetComponent<Player>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            CmdFireShot(cameraTransform.position, cameraTransform.forward, playerScript.GetTeam());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("reload");
        }
    }

    [Command]
    void CmdFireShot(Vector3 origin, Vector3 direction, Team team)
    {
        RpcShotEffects();

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layers))
        {
            if (hit.transform.CompareTag("Player") && team != hit.transform.GetComponent<Player>().GetTeam())
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

public enum Team { Red, Blue };
