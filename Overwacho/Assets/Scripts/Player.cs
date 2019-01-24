using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool> { }

public class Player : NetworkBehaviour {
    
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform cameraTransform;
    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;

    CharacterController characterController;
    
    void Start ()
    {
        if (isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        EnablePlayer();
        characterController = GetComponent<CharacterController>();
    }
	
	void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.zero;

        movement += (transform.forward * moveVertical);
        movement += (transform.right * moveHorizontal);

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

    void EnablePlayer()
    {
        onToggleShared.Invoke(true);

        if (isLocalPlayer)
            onToggleLocal.Invoke(true);
        else
            onToggleRemote.Invoke(true);
    }
}
