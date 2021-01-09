using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float FORWARD_ACCELERATION    = 10.0f;
    private const float BACKWARD_ACCELERATION   = 10.0f;
    private const float STRAFE_ACCELERATION     = 20.0f;
    private const float GRAVITY_ACCELERATION    = 10.0f;
    private const float MAX_FORWARD_VELOCITY    = 3.5f;
    private const float MAX_BACKWARD_VELOCITY   = 2.5f;
    private const float MAX_STRAFE_VELOCITY     = 3.0f;
    private const float ANGULAR_VELOCITY_FACTOR = 2.0f;
    private const float UPPER_HEAD_TILT         = 290.0f;
    private const float LOWER_HEAD_TILT         = 70.0f;
    private const float INTRO_LEFT_PEEK         = 20.0f;
    private const float INTRO_RIGHT_PEEK        = 340f;

    private CharacterController _controller;
    private Transform           _cameraTransform;
    private Vector3             _acceleration;
    private Vector3             _velocity;
    private Animator            _mainDoorAnim;
    private bool                _inIntroZone;

    #region Singleton
    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one player movement.");
            return;
        }

        instance = this;
    }
    #endregion

    private void Start()
    {
        _controller         = GetComponent<CharacterController>();
        _cameraTransform    = GetComponentInChildren<Camera>().transform;
        _acceleration       = Vector3.zero;
        _velocity           = Vector3.zero;
        _mainDoorAnim       = GameObject.Find("MainDoor").GetComponent<Animator>();
        _inIntroZone        = true;

        HideCursor();
    }

    private void HideCursor()
    {
        Cursor.visible      = false;
        Cursor.lockState    = CursorLockMode.Locked;
    }

    private void Update()
    {
        UpdateHeadTilt();
        UpdateRotation();
    }

    private void UpdateHeadTilt()
    {
        Vector3 cameraRotation = _cameraTransform.localEulerAngles;

        cameraRotation.x -= Input.GetAxis("Mouse Y") * ANGULAR_VELOCITY_FACTOR;

        if (cameraRotation.x < 180f)
            cameraRotation.x = Mathf.Min(cameraRotation.x, LOWER_HEAD_TILT);

        else
            cameraRotation.x = Mathf.Max(cameraRotation.x, UPPER_HEAD_TILT);

        _cameraTransform.localEulerAngles = cameraRotation;
    }
    
    private void UpdateRotation()
    {
        if (_inIntroZone)
        {
            Vector3 cameraRotation = _cameraTransform.localEulerAngles;
        
            cameraRotation.y += Input.GetAxis("Mouse X") * ANGULAR_VELOCITY_FACTOR;

            if (cameraRotation.y < 180f)
                cameraRotation.y = Mathf.Min(cameraRotation.y, INTRO_LEFT_PEEK);

            else
                cameraRotation.y = Mathf.Max(cameraRotation.y, INTRO_RIGHT_PEEK);

            _cameraTransform.localEulerAngles = cameraRotation;
        }
        else
        {
            float rotation = Input.GetAxis("Mouse X") * ANGULAR_VELOCITY_FACTOR;

            transform.Rotate(0, rotation, 0);
        }
    }

    private void FixedUpdate()
    {
        UpdateAcceleration();
        UpdateVelocity();
        UpdatePosition();
    }

    private void UpdateAcceleration()
    {
        _acceleration.z = Input.GetAxis("Forward");
        _acceleration.z *= (_acceleration.z >=0) ? 
            FORWARD_ACCELERATION : BACKWARD_ACCELERATION;

        _acceleration.x = Input.GetAxis("Strafe") * STRAFE_ACCELERATION;

        _acceleration.y = -GRAVITY_ACCELERATION;
    }

    private void UpdateVelocity()
    {
        _velocity += _acceleration * Time.fixedDeltaTime;
        
        _velocity.z = (_acceleration.z == 0f || _velocity.z * _acceleration.z < 0) ? 0 : 
            Mathf.Clamp(_velocity.z, -MAX_BACKWARD_VELOCITY, MAX_FORWARD_VELOCITY);

        _velocity.x = (_acceleration.x == 0f || _velocity.x * _acceleration.x < 0) ? 0 : 
            Mathf.Clamp(_velocity.x, -MAX_STRAFE_VELOCITY, MAX_STRAFE_VELOCITY);
    }

    private void UpdatePosition()
    {
        Vector3 motion = _velocity * Time.fixedDeltaTime;

        _controller.Move(transform.TransformVector(motion));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "IntroZone" && _inIntroZone)
        {
            _inIntroZone = false;

            _mainDoorAnim.SetBool("Close", true);

            Debug.Log("Inside House.");
        }
    }
}
