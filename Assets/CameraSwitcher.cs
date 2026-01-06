using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Transform topDownTarget;
    public Transform firstPersonTarget;

    public GameObject messageUI;
    public PlayerMovement playerMovement;

    private bool isFirstPerson = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;

            // Attiva/disattiva mouse look
            playerMovement.enableMouseLook = isFirstPerson;

            // Nascondi messaggio
            if (isFirstPerson && messageUI != null)
                messageUI.SetActive(false);
        }

        // Sposta la camera
        if (isFirstPerson)
        {
            transform.position = firstPersonTarget.position;
            transform.rotation = firstPersonTarget.rotation;
        }
        else
        {
            transform.position = topDownTarget.position;
            transform.rotation = topDownTarget.rotation;
        }
    }
}
