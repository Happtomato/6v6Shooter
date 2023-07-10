using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMovementController : MonoBehaviour
{
    
    //---------------------------------------------------------------------------------
    //---------------------  keybindings  ---------------------------------------------
    
    //----------- movement -----------
    public KeyCode forwards = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode strafeLeft = KeyCode.A;
    public KeyCode strafeRight = KeyCode.D;
    public KeyCode jump = KeyCode.Space;

    //----------- advanced movement -----------
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode tacticalSprint;
    public KeyCode crouch = KeyCode.LeftControl;
    public KeyCode lie = KeyCode.LeftAlt;
    
    //----------- Combat -----------
    public MouseButton shoot = MouseButton.LeftMouse;
    public MouseButton ads = MouseButton.RightMouse;
    public KeyCode reload = KeyCode.R;
    public KeyCode changeMag;
    public KeyCode switchToMeele = KeyCode.V;
    public KeyCode switchToSecondary;
    public KeyCode switchToPrimary;
    public KeyCode granadeOne;
    public KeyCode granadeTwo;
    
    
    //----------- abbilities general -----------
    public KeyCode gadgetOne;
    public KeyCode gadgetTwo;
    public KeyCode gadgetThree;
    
    //----------- Attacker Abilities -----------
    
    //----------- Defender Abilities -----------
    
    //----------- interactions -----------
    public KeyCode use;
    public KeyCode emote;
    
    //----------- User-Interface -----------
    
    
    //---------------------------------------------------------------------------------
    
    //-------------------------------------------------------------------------------
    //---------------------  Variables  ---------------------------------------------
    
    //----------- Movement -----------
    
    private bool isSprinnting;
    public float walkSpeed = 3.0f;
    public float sprintSpeed = 6.0f;
    public float tacticalSprintSpeed = 8.0f;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    public float jetpackFuel = 100f;
    public float jumpForce = 250.0f;
    bool _grounded;
    public LayerMask groundedMask;
    private bool doubleJumpUsed;
    RaycastHit _groundRaycastHit;
    
    //----------- Camera Movement -----------
    
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;
    bool cursorVisible;
    
    Transform cameraT;
    float verticalLookRotation;
    
    //----------- Objects -----------
    
    Rigidbody rigidbodyR;
    
    //-------------------------------------------------------------------------------
    
    

    
    void Start () {
        if (Camera.main != null) cameraT = Camera.main.transform;
        rigidbodyR = GetComponent<Rigidbody> ();
        LockMouse ();
    }
	
    
    void Update () {
       
        // rotation
        
        Rotate();

        // movement
        
        Move();
        
        // sprint
        
        //Sprinting
		
        if (Input.GetKey(sprint)) {
            walkSpeed = sprintSpeed;
        }
        else {
            walkSpeed = walkSpeed;
        }
        if (Input.GetKeyUp(sprint)){
            walkSpeed = 4f;
        }
        
        
        
        // jump & double Jump
        if (Input.GetButtonDown ("Jump")) {
            if (_grounded) {
                rigidbodyR.AddForce (transform.up * jumpForce);
            }
            else if (!doubleJumpUsed)
            {
                rigidbodyR.AddForce (transform.up * jumpForce);
                doubleJumpUsed = true;
            }
        }

        Ray ray = new Ray (transform.position, -transform.up);
        

        if (Physics.Raycast(ray, out _groundRaycastHit, 1 + .1f, groundedMask)) {
            _grounded = true;
        }
        else {
            _grounded = false;
        }

        /* Lock/unlock mouse on click */
        if (Input.GetMouseButtonUp (0)) {
            if (!cursorVisible) {
                UnlockMouse ();
            } else {
                LockMouse ();
            }
        }
    }
    
    //crouch and slide

    /*
    void crouch()
    {
        if (isSprinnting)
        {
            Slide();
        }
        else
        {
            //code for crouching
        }
    }
    */

    void FixedUpdate() {
        rigidbodyR.MovePosition (rigidbodyR.position + transform.TransformDirection (moveAmount) * Time.fixedDeltaTime);
    }

    void UnlockMouse() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorVisible = true;
    }

    void LockMouse() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorVisible = false;
    }

    void Slide()
    {
        
    }

    void Rotate()
    {
        transform.Rotate (Vector3.up * (Input.GetAxis ("Mouse X") * mouseSensitivityX));
        verticalLookRotation += Input.GetAxis ("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp (verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
    {
        if (Input.GetKey(strafeRight))
        {
            transform.position += new Vector3(walkSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(strafeLeft))
        {
            transform.position -= new Vector3(walkSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(forwards))
        {
            transform.position += new Vector3(0f, walkSpeed * Time.deltaTime, 0f);
        }

        if (Input.GetKey(backwards))
        {
            transform.position -= new Vector3(0f, walkSpeed * Time.deltaTime, 0f);
        }
    }
}
