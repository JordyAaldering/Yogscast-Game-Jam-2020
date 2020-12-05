using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float interactRange;

    private Generator hoverObject;
    private bool hasHover;

    private CharacterController characterController;
    private UpgradePanel upgradePanel;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
        upgradePanel.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();

        GetHoverObject();

        if (hasHover && Input.GetButtonDown("Fire1")) {
            hoverObject.HandleClick();
        }

        if (hasHover && Input.GetKey(KeyCode.E)) {
            hoverObject.HandleInteract();
        }
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 moveDir = horizontal * right + vertical * forward;

        characterController.Move(speed * Time.deltaTime * moveDir);
    }

    private void RotateCamera()
	{
        float horizontal = sensitivity * Input.GetAxis("Mouse X");
        float vertical = -sensitivity * Input.GetAxis("Mouse Y");

        transform.rotation *= Quaternion.Euler(0f, horizontal, 0f);
        Camera.main.transform.rotation *= Quaternion.Euler(vertical, 0f, 0f);
    }

    private void GetHoverObject()
	{
        Transform cam = Camera.main.transform;
        if (Physics.Raycast(cam.position, cam.forward, out var hit, interactRange)) {
            hasHover = hit.collider.TryGetComponent(out hoverObject);
        }

        upgradePanel.gameObject.SetActive(hasHover);
        if (hasHover) {
            upgradePanel.SetInfo(hoverObject);
        }
    }
}
