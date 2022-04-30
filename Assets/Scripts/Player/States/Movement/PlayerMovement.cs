using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private bool lockCursor = true;
    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField] Transform playerCamera = null;

    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runBuildUpSpeed = 5f;
    [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    [SerializeField] private float slopeForce = 3f;
    [SerializeField] private float slopeRayLength = 1.5f;

    [Header("Jump Settings")]
    [SerializeField] private float gravity = -13.0f;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode runKey;

    private bool isJumping;
    private float movementSpeed = 6f;
    private float cameraPitch = 0.0f;
    private float velocityY = 0.0f;
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;
    public CharacterController controller = null;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update() {}

    public bool AnyInput() 
    {
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool moveInput = moveVector == Vector2.zero ? false : true;
        bool jumpInput = Input.GetKeyDown(jumpKey);

        return true ? moveInput || jumpInput : false;
    }

    public void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    public void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if(controller.isGrounded) 
        {
            velocityY = 0.0f;
        }
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (Vector3.ClampMagnitude(transform.forward * currentDir.y + transform.right * currentDir.x, 1.0f)) * movementSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        // If moving and on slope, apply extra gravity
        if(targetDir != Vector2.zero && OnSlope()) 
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
        }

        SetMovementSpeed();
        JumpInput();
    }

    private bool OnSlope() 
    {
        if(isJumping)
        {
            return false;
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeRayLength))
        {
            if(hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    private void SetMovementSpeed()
    {
        float speed = Input.GetKey(runKey) ? runSpeed : walkSpeed;
        movementSpeed = Mathf.Lerp(movementSpeed, speed, Time.deltaTime * runBuildUpSpeed);

    }

    private void JumpInput()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    IEnumerator JumpEvent()
    {
        controller.slopeLimit = 90f;
        float timeInAir = 0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);
        controller.slopeLimit = 45f;
        isJumping = false;

    }

}
