using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;

    private CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();

        if (Input.GetButton("Fire1")) {
            HandleClick();
		}
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 moveDir = horizontal * right + vertical * forward;

        cc.Move(speed * Time.deltaTime * moveDir);
    }

    private void RotateCamera()
	{
        float horizontal = sensitivity * Input.GetAxis("Mouse X");
        float vertical = -sensitivity * Input.GetAxis("Mouse Y");

        transform.rotation *= Quaternion.Euler(0f, horizontal, 0f);
        Camera.main.transform.rotation *= Quaternion.Euler(vertical, 0f, 0f);
    }

    private void HandleClick()
	{
        Transform cam = Camera.main.transform;
        if (Physics.Raycast(cam.position, cam.forward, out var hit, 10f)) {
            hit.collider.GetComponent<IClickable>()?.HandleClick();
        }
	}
}
