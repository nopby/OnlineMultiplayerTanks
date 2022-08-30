using System.Collections;
using UnityEngine;
using DimensionalDeveloper.TankBuilder.Utility;
using DimensionalDeveloper.TankBuilder.Controllers;
using Photon.Pun;
public class CameraController : MonoBehaviour
{
    public Camera Camera
    {
        get
        {
            if (_camera != null) return _camera;

            _camera = GetComponentInChildren<Camera>();
            ExtensionsLibrary.CheckComponent(_camera, "Camera Component", name);
            return _camera;
        }
    }
    private Camera _camera;
    public Transform Target
    {
        get => cameraTarget;
        set => cameraTarget = value;
    }
    public Transform cameraTarget;
    private Quaternion Rotation
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }
    public Transform CameraTransform
    {
        get
        {
            // Check if exists.
            if (_cameraTransform) return _cameraTransform;

            // If not, attempt to find camera within children.
            _cameraTransform = Camera.transform;

            // Check if exists.
            if (_cameraTransform) return _cameraTransform;

            Debug.LogError("Camera Controller: A camera can not be found among the children of this gameObject.");
            return null;
        }
    }
    private Transform _cameraTransform;
    public KeyCode UnlockCursorKey
    {
        get => unlockCursorKey;
        set => unlockCursorKey = value;
    }
    [SerializeField] private KeyCode unlockCursorKey = KeyCode.Escape;

    public enum FollowMethod
    {
        Lerp,
        SmoothDamp,
    }
    public FollowMethod followMethod = FollowMethod.SmoothDamp;

    private Vector2 ScreenOffset => farLook ? FarLookOffset : transform.TransformDirection(PositionOffset).xy();
    public Vector2 PositionOffset
    {
        get => positionOffset;
        set
        {
            positionOffset.x = Mathf.Clamp(value.x, -10.0f, 10.0f);
            positionOffset.y = Mathf.Clamp(value.y, -10.0f, 10.0f);
        }
    }
    [SerializeField] private Vector2 positionOffset = Vector2.zero;

    public Vector2 FarLookOffset
    {
        get => farLookOffset;
        set
        {
            farLookOffset.x = Mathf.Clamp(value.x, -15, 15);
            farLookOffset.y = Mathf.Clamp(value.y, -15, 15);
        }
    }
    [SerializeField] private Vector2 farLookOffset = Vector2.zero;

    public float CameraDistance
    {
        get => cameraDistance;
        set
        {
            cameraDistance = value;
            Camera.orthographicSize = cameraDistance;
        }
    }
    [SerializeField] private float cameraDistance = 30.0f;

    public bool[] hideSection = new bool[3];



    public float deadZoneThreshold = 0.2f;

    public bool farLook;
    public string farLookInput = "Far Look";
    public float farLookSpeed = 10.0f;
    public float farLookDistance = 15.0f;




    public TankController tankController;

    public bool rotateWithTarget;

    public bool smoothRotation = true;

    public float smoothRotationFactor = 2.5f;

    public bool lockCursor;

    private Vector2 _velocity;

    public bool smoothMovement = true;

    public float lerpMovementSpeed = 25f;

    public float smoothMovementTime = 0.25f;

    private Vector2 _currentPosition;

    private Vector2 _mousePosition;


    private void HandleInput()
    {
        if (tankController != null && (tankController.pause || tankController.HaltMovement)) return;

        _mousePosition = InputManager.Instance.LookUp;
        farLook = InputManager.Instance.FarLook;

        if (!farLook) return;

        var lookOffset = GetFarLookInput() * farLookDistance;
        FarLookOffset = Vector2.Lerp(FarLookOffset, lookOffset, Time.deltaTime * farLookSpeed);
    }

    private Vector2 GetFarLookInput()
    {
        var mousePosition = InputManager.Instance.LookUp - _mousePosition;

        mousePosition = (Camera.ScreenToWorldPoint((_mousePosition + mousePosition)).xy() - Target.position.xy()).normalized;

        return mousePosition;
    }

    private void HandleRotation()
    {
        var targetRotation = rotateWithTarget ? Target.rotation : Quaternion.identity;

        if (smoothRotation)
        {
            var smoothTime = smoothRotationFactor * Time.deltaTime;
            Rotation = Quaternion.Slerp(Rotation, targetRotation, smoothTime);
        }

        else Rotation = targetRotation;
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        Cursor.lockState = !value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = !value;
    }
    private void FollowTarget()
    {
        if (Target == null) return;

        var targetPosition = Target.position.xy() + ScreenOffset;

        if (smoothMovement)
        {
            _currentPosition = followMethod == FollowMethod.Lerp
                ? Vector2.Lerp(_currentPosition, targetPosition, Time.deltaTime * lerpMovementSpeed)
                : Vector2.SmoothDamp(_currentPosition, targetPosition, ref _velocity, smoothMovementTime);
        }

        else _currentPosition = targetPosition;

        transform.position = _currentPosition;
    }

    public void ResetTarget()
    {
        if (tankController == null)
        {
            Debug.LogError("Camera Controller: A tank controller component was not found, " +
                                                  "please add one manually.");

            return;
        }

        Target = tankController.transform;
    }

    public void Reset()
    {
        if (Target == null) return;

        _currentPosition = Target.position.xy() + PositionOffset;
        if (rotateWithTarget) transform.rotation = Target.rotation;
    }

    private void Start()
    {
        Reset();
    }
    private void Update()
    {
        HandleInput();
    }
    private void FixedUpdate()
    {
        HandleRotation();
    }

    private void LateFixedUpdate()
    {
        FollowTarget();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(RunLateFixedUpdate));
        Reset();
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(RunLateFixedUpdate));
    }
    private IEnumerator RunLateFixedUpdate()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            LateFixedUpdate();
        }
    }


}