using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool> { }

public class Player : NetworkBehaviour {

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform cameraTransform;
    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;
    [SerializeField] Renderer remoteCharacter;
    [SerializeField] Material redTeamMaterial;
    [SerializeField] Material blueTeamMaterial;
    [SerializeField] Image teamColorImage;
    [SerializeField] GameObject ragdollPrefab;
    
    [SyncVar(hook = "OnColorChanged")]
    Color playerColor;
    Team team;

    CharacterController characterController;
    Animator animator;

    void Start()
    {
        if (isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        EnablePlayer();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        OnColorChanged(playerColor);
    }

    public void ChangeColor(Color color)
    {
        playerColor = color;
    }

    public Team GetTeam()
    {
        return team;
    }
	
	void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        animator.SetFloat("Walk", moveVertical);
        animator.SetFloat("Strafe", moveHorizontal);

        Vector3 movement = Vector3.zero;

        movement += (transform.forward * moveVertical);
        movement += (transform.right * moveHorizontal);

        if (!characterController.isGrounded)
            movement += Vector3.down;

        movement.Normalize();

        characterController.Move(movement * speed * Time.deltaTime);

        float xRotation = Input.GetAxis("Mouse X") * Time.deltaTime;
        float yRotation = Input.GetAxis("Mouse Y") * Time.deltaTime;

        transform.Rotate(0, xRotation * rotationSpeed, 0);
        cameraTransform.Rotate(-yRotation * rotationSpeed, 0, 0);
    }

    public void DisablePlayer()
    {
        onToggleShared.Invoke(false);

        if (isLocalPlayer)
            onToggleLocal.Invoke(false);
        else
            onToggleRemote.Invoke(false);
    }

    public void EnablePlayer()
    {
        onToggleShared.Invoke(true);

        if (isLocalPlayer)
            onToggleLocal.Invoke(true);
        else
            onToggleRemote.Invoke(true);

        Transform spawn = Spawner.singleton.GetSpawnPoint(team);

        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
    }

    void OnColorChanged(Color color)
    {
        teamColorImage.color = color;

        if (color == Color.red)
        {
            remoteCharacter.material = redTeamMaterial;
            team = Team.Red;
        }
        else if (color == Color.blue)
        {
            remoteCharacter.material = blueTeamMaterial;
            team = Team.Blue;
        }
    }

    public void CreateRagdoll()
    {
        GameObject ragdoll = Instantiate<GameObject>(ragdollPrefab, transform.position, transform.rotation);

        Renderer renderer = ragdoll.GetComponentInChildren<Renderer>();

        if (team == Team.Red)
            renderer.material = redTeamMaterial;
        else if (team == Team.Blue)
            renderer.material = blueTeamMaterial;
    }
}
