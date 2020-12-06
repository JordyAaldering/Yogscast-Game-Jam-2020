using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float sensitivity;
    public float interactRange;

    private Generator hoverObject;
    private RewardSled rewardObject;
    private bool hasHover, hasReward;

    private CharacterController characterController;
    private UpgradePanel upgradePanel;
    private Animator animator;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
        animator = GetComponentInChildren<Animator>();

        upgradePanel.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (SettingsPanel.IsOpen) {
            return;
		}

        MovePlayer();
        RotateCamera();

        GetHoverObject();

        if (Input.GetButtonDown("Fire1")) {
            animator.SetTrigger("doPunch");
            if (hasHover) {
                hoverObject.HandleClick();
            } else {
                Transform cam = Camera.main.transform;
                if (Physics.Raycast(cam.position, cam.forward, out var hit, interactRange * 2f)) {
                    if (hit.collider.TryGetComponent(out Secret secret)) {
                        secret.Claim();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (hasHover) {
                hoverObject.HandleInteract();
            } else if (hasReward) {
                rewardObject.ClaimReward();
            }
        }
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 moveDir = horizontal * right + vertical * forward;
        moveDir *= Input.GetButton("Fire3") ? sprintSpeed : speed;

        animator.SetBool("isMoving", Vector3.Magnitude(moveDir) > float.Epsilon);

        if (!characterController.isGrounded) {
            moveDir.y -= 9.81f;
        }

        characterController.Move(moveDir * Time.deltaTime);
    }

    private void RotateCamera()
	{
        float horizontal = sensitivity * Input.GetAxis("Mouse X");
        float vertical = -sensitivity * Input.GetAxis("Mouse Y");

        transform.rotation *= Quaternion.Euler(0f, horizontal, 0f);

        Transform cam = Camera.main.transform;
        cam.rotation *= Quaternion.Euler(vertical, 0f, 0f);
        float angle = cam.rotation.eulerAngles.x;
        angle = (angle > 60f && angle < 150f) ? 60f : ((angle < 280f && angle > 150f) ? 280f : angle);
        cam.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }

    private void GetHoverObject()
	{
        hasHover = false;
        hasReward = false;

        Transform cam = Camera.main.transform;
        if (Physics.Raycast(cam.position, cam.forward, out var hit, interactRange)) {
            if (!(hasHover = hit.collider.TryGetComponent(out hoverObject))) {
                hasReward = hit.collider.TryGetComponent(out rewardObject);
            }
        }

        upgradePanel.gameObject.SetActive(hasHover || hasReward);
        if (hasHover) {
            upgradePanel.SetInfo(hoverObject);
        } else if (hasReward) {
            upgradePanel.SetReward(rewardObject);
		}
    }
}
