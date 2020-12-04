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
    }

    private void MovePlayer()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        float horizontal = speed * Input.GetAxis("Horizontal");
        float vertical = speed * Input.GetAxis("Vertical");
        Vector3 moveDir = vertical * forward + horizontal * right;

        cc.Move(moveDir * Time.deltaTime);
    }

    private void RotateCamera()
	{
        float horizontal = sensitivity * Input.GetAxis("Mouse X");
        transform.rotation *= Quaternion.Euler(0f, horizontal, 0f);

        float vertical = -sensitivity * Input.GetAxis("Mouse Y");
        Camera.main.transform.rotation *= Quaternion.Euler(vertical, 0f, 0f);
    }
}
