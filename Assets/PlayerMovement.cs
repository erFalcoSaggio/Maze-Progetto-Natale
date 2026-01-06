using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    private Rigidbody rb;
    private Vector3 input;

    [Header("Mouse Look")]
    public Transform cameraTransform;
    public float mouseSensitivity = 200f;
    public bool enableMouseLook = false;

    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // mov
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // mov PLAYER
        input = transform.TransformDirection(new Vector3(h, 0, v)).normalized;

        // mouse
        if (enableMouseLook)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //rot ver camera
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            //rot orizz PLAYER
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    void FixedUpdate()
    {
        Vector3 move = input * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
}
