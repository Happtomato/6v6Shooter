using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    
    //keybindings 
    
    //movement
    public KeyCode forwards;
    public KeyCode backwards;
    public KeyCode strafeLef;
    public KeyCode strafeRight;
    public KeyCode jump;

    //advanced movement
    public KeyCode sprint;
    public KeyCode tacticalSprint;
    public KeyCode crouch;
    public KeyCode lie;
    
    //Combat
    public KeyCode shoot;
    public KeyCode ads;
    public KeyCode reload;
    public KeyCode changeMag;
    public KeyCode meele;
    public KeyCode switchToMeele;
    public KeyCode switchToSecondary;
    public KeyCode switchToPrimary;
    public KeyCode granadeOne;
    public KeyCode granadeTwo;
    
    
    //abbilities general
    public KeyCode gadgetOne;
    public KeyCode gadgetTwo;
    public KeyCode gadgetThree;
    
    //Attacker Abilities
    
    //Defender Abilities
    
    //interactions
    public KeyCode use;
    public KeyCode emote;
    
    
   
    //Variables
    
    
    private bool doubleJumpUsed;
    
    
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    private bool isSprinnting;
    
    public float walkSpeed = 3.0f;
    public float sprintSpeed = 6.0f;
    public float tacticalSprintSpeed = 8.0f;
    
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    
    Transform cameraT;
    float verticalLookRotation;
    
    Rigidbody rigidbodyR;
    
    public float jumpForce = 250.0f;
    bool _grounded;
    public LayerMask groundedMask;

    bool cursorVisible;

    // Use this for initialization
    void Start () {
        cameraT = Camera.main.transform;
        rigidbodyR = GetComponent<Rigidbody> ();
        LockMouse ();
    }
	
    // Update is called once per frame
    void Update () {
        // rotation
        transform.Rotate (Vector3.up * (Input.GetAxis ("Mouse X") * mouseSensitivityX));
        verticalLookRotation += Input.GetAxis ("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp (verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        // movement
        Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        // sprint
        /*
        while(Input.GetKeyDown("sprint"){
            walkSpeed = sprintSpeed;
        }
        */
        // jump & double Jump
        if (Input.GetButtonDown ("Jump")) {
            if (_grounded) {
                rigidbodyR.AddForce (transform.up * jumpForce);
            }
        }

        Ray ray = new Ray (transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
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
}
