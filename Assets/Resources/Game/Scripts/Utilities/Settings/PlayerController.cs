using System;
using UnityEngine;
using DimensionalDeveloper.TankBuilder.Utility;
using Photon.Pun;
using System.Threading.Tasks;
using Unity.VisualScripting;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        info.Sender.TagObject = this.gameObject;
    }
    public void AssignCamera()
    {
        var cam = FindObjectOfType<CameraController>();
        cam.enabled = true;
        cam.Target = transform;
        camera = cam.GetComponentInChildren<Camera>();
        var gameSceneUI = FindObjectOfType<GameSceneUI>();
        gameSceneUI.playerStats = GetComponent<PlayerStats>();
    }
    private Rigidbody2D Rigidbody
    {
        get
        {
            if (_rigidbody) return _rigidbody;

            _rigidbody = GetComponent<Rigidbody2D>();
            ExtensionsLibrary.CheckComponent(_rigidbody, "Rigidbody Component", name);
            return _rigidbody;
        }
    }
    private Rigidbody2D _rigidbody;
    public Transform CannonRotor
    {
        get
        {
            if (cannonRotor) return cannonRotor;

            cannonRotor = transform.Find("Cannon");

            if (!cannonRotor)
                Debug.LogError("Tank Controller: No child named \"Cannon\" has been " +
                                              "found among the first tier children of \"Player Tank\".");

            return cannonRotor;
        }
        set => cannonRotor = value;
    }
    [SerializeField] private Transform cannonRotor;
    public float ForwardSpeed
    {
        get => forwardSpeed;
        set => forwardSpeed = Mathf.Max(0.0f, value);
    }
    [SerializeField] private float forwardSpeed = 10.0f;
    public float BackwardSpeed
    {
        get => backwardSpeed;
        set => backwardSpeed = Mathf.Max(0.0f, value);
    }
    [SerializeField] private float backwardSpeed = 6.0f;
    public float TurnSpeed
    {
        get => turnSpeed;
        set => turnSpeed = Mathf.Max(0.0f, value);
    }
    [SerializeField] private float turnSpeed = 5.0f;
    public float SpeedMultiplier
    {
        get => speedMultiplier;
        set => speedMultiplier = Mathf.Max(value, 1.0f);
    }
    [SerializeField] private float speedMultiplier = 2.0f;
    public float Speed { get; set; }
    public Vector2 velocity
    {
        get => Rigidbody.velocity;
        set => Rigidbody.velocity = value;
    }

    public float currentRotation
    {
        get => _currentRotation;
        set
        {
            _currentRotation = value;
            Rigidbody.MoveRotation(_currentRotation);
        }
    }
    private float _currentRotation = 0;

    public float CurrentSpeed
    {
        get => _currentSpeed;
        set => _currentSpeed = Mathf.Max(0.0f, value);
    }
    private float _currentSpeed;
    public Vector2 MovementDirection
    {
        get => _movementDirection;
        set => _movementDirection = Vector3.ClampMagnitude(value, 1.0f);
    }
    private Vector2 _movementDirection;
    public bool HaltMovement
    {
        get => _haltMovement;
        set
        {
            _haltMovement = value;
            if (_haltMovement) MovementDirection = Vector2.zero;
        }
    }
    private bool _haltMovement;
    public float Acceleration
    {
        get => acceleration;
        set => acceleration = Mathf.Max(0.0f, value);
    }
    [SerializeField] public float acceleration = 50.0f;
    public float Deceleration
    {
        get => deceleration;
        set => deceleration = Mathf.Max(0.0f, value);
    }
    [SerializeField] public float deceleration = 10.0f;
    public float MaxHorizontalSpeed
    {
        get => maxHorizontalSpeed;
        set => maxHorizontalSpeed = Mathf.Max(0.0f, value);
    }
    [SerializeField] private float maxHorizontalSpeed = 100.0f;
    public float MaxUpwardSpeed
    {
        get => maxUpwardSpeed;
        set => maxUpwardSpeed = Mathf.Max(0.0f, value);
    }
    [SerializeField] private float maxUpwardSpeed = 100.0f;
    public float MaxDownwardSpeed
    {
        get => maxDownwardSpeed;
        set => maxDownwardSpeed = Mathf.Max(0.0f, value);
    }
    [SerializeField] private float maxDownwardSpeed = 100.0f;

    public Vector2 Friction
    {
        get => friction;
        set
        {
            friction.x = Mathf.Max(0.0f, value.x);
            friction.y = Mathf.Max(0.0f, value.y);
        }
    }
    [SerializeField] private Vector2 friction = new Vector2(10.0f, 2.0f);

    public enum RotorRotationMethod
    {
        None,
        RotateTowards,
        Lerp,
        Slerp,
        SmoothDamp
    }
    public float RotorSpeed
    {
        get => rotorSpeed;
        set => rotorSpeed = Mathf.Max(0.0f, value);
    }
    [SerializeField] private float rotorSpeed = 5.0f;

    public float RotorSmoothSpeed
    {
        get => rotorSmoothSpeed;
        set => rotorSmoothSpeed = Mathf.Max(0.0f, value);
    }
    [SerializeField] private float rotorSmoothSpeed = 0.2f;
    [HideInInspector] public bool[] hideSection = new bool[6];
    public bool pause = true;
    public RotorRotationMethod rotorRotationMethod = RotorRotationMethod.SmoothDamp;

    public Camera camera;
    public float leftDeadZoneThreshold = 0.2f;
    public float rightDeadZoneThreshold = 0.2f;
    public bool limitMaximumSpeed = true;
    public float cannonAngle;
    public Quaternion cannonVelocity = Quaternion.identity;
    public void HandleInput()
    {
        // Acquire Inputs.
        MovementDirection = InputManager.Instance.Movement;
        cannonAngle = GetCannonInput();

        // Apply constraints to the acquired input.
        var x = Mathf.Abs(MovementDirection.x);
        var y = Mathf.Abs(MovementDirection.y);

        if (x > y) MovementDirection = MovementDirection.WithY(0);
        else if (y > x) MovementDirection = MovementDirection.WithX(0);
        else if (x > 0.69f && y > 0.69f) MovementDirection = MovementDirection.WithY(0) * 1.4285f;
    }
    public void ProcessInput()
    {
        // Update the hulls velocity.
        UpdateVelocity();

        // Update the tanks cannon.
        UpdateCannon();
    }

    private Vector2 InputToVelocity(out float targetSpeed)
    {
        // Acquire the length of the movement direction.
        CurrentSpeed = MovementDirection == Vector2.zero ? 0.0f : MovementDirection.magnitude;

        // Default the target speed to zero.
        targetSpeed = 0;

        // If moving along the x-axis, default to turn speed.
        if (Mathf.Abs(MovementDirection.x) > 0) targetSpeed = turnSpeed * CurrentSpeed;

        // Multiply a speed multiplier when accelerating (only applicable while moving forward or backward).
        if (InputManager.Instance.Accelerate) CurrentSpeed *= speedMultiplier;

        // If moving backwards, set the target speed to backward.
        if (MovementDirection.y < 0.0f) targetSpeed = backwardSpeed * CurrentSpeed;

        // The forward speed should be the quickest so it overwrites the target speed last.
        else if (MovementDirection.y > 0.0f) targetSpeed = forwardSpeed * CurrentSpeed;

        // If necessary, override the target speed.
        OverrideInput(ref targetSpeed);

        // Update the speed displayed in the inspector.
        Speed = targetSpeed;

        // Multiply the direction with speed to acquire the new velocity.
        var newVelocity = MovementDirection * targetSpeed;

        // Transform the newly acquired local velocity's direction from local to world.
        newVelocity = transform.TransformDirection(newVelocity);

        // Remove the y axis from the equation as this will be controlled by gravity or through jumping.
        return newVelocity;
    }
    private void OverrideInput(ref float targetSpeed)
    {
        if (targetSpeed.IsZero()) return;

        // Override speed.
    }

    private void UpdateVelocity()
    {
        var currentVelocity = velocity;
        var deltaTime = Time.deltaTime;

        // Calculate the target velocity.
        var targetVelocity = InputToVelocity(out var targetSpeed);

        // Apply movement to the tank.
        ApplyMovement(ref currentVelocity, targetVelocity, targetSpeed, deltaTime);

        // Apply limits to the velocity.
        LimitVelocity(ref currentVelocity);

        // Finally, apply the new velocity to the rigidbody. 
        if (!currentVelocity.IsNaN()) velocity = currentVelocity;
    }
    private void ApplyMovement(ref Vector2 currentVelocity, Vector2 targetVelocity, float targetSpeed, float deltaTime)
    {
        // Apply torque to the tanks rigidbody for rotation.        
        var torque = -MovementDirection.x * Mathf.Clamp01(1f - Friction.x * deltaTime);
        currentRotation += torque * targetSpeed;

        // if turning, remove all extra velocity.
        if (Mathf.Abs(MovementDirection.x) > 0) targetVelocity = Vector2.zero;

        // If in the previous fixed frame the character was grounded, assign the whole velocity.
        var newVelocity = currentVelocity;
        var targetDirection = targetVelocity / targetSpeed;

        // Calculate the appropriate acceleration and combine the direction.
        var targetAcceleration = targetDirection * (Acceleration * deltaTime);

        // Character is decelerating.
        if (targetAcceleration.IsZero() || newVelocity.IsExceeding(targetSpeed))
        {
            // Find appropriate friction and apply braking friction clamped between zero and one.
            newVelocity *= Mathf.Clamp01(1f - Friction.y * deltaTime);

            // Retrieve the appropriate deceleration value and apply it.
            newVelocity = Vector3.MoveTowards(newVelocity, targetVelocity, Deceleration * deltaTime);
        }

        // Character is accelerating.
        else
        {
            // Find appropriate friction and apply it.
            newVelocity -= (newVelocity - targetDirection * newVelocity.magnitude) * (Friction.x * deltaTime);

            // Apply acceleration while also clamping its length to the target speed.
            newVelocity = Vector3.ClampMagnitude(newVelocity + targetAcceleration, targetSpeed);
        }

        // Update the reference to the character's velocity.
        currentVelocity += newVelocity - currentVelocity;
    }
    private void LimitVelocity(ref Vector2 currentVelocity)
    {
        // If not limiting speed, return.
        if (!limitMaximumSpeed) return;

        // Split the velocity between horizontal and vertical.
        var horizontalVelocity = currentVelocity.x;
        var verticalVelocity = currentVelocity.y;

        // Apply left and right limits.
        if (horizontalVelocity > MaxHorizontalSpeed)
            currentVelocity += Vector2.zero.WithX(MaxHorizontalSpeed - horizontalVelocity);
        else if (horizontalVelocity < -MaxHorizontalSpeed)
            currentVelocity += Vector2.zero.WithX(-MaxHorizontalSpeed - horizontalVelocity);

        // Apply upward and downward limits.
        if (verticalVelocity > MaxUpwardSpeed)
            currentVelocity += Vector2.zero.WithY(MaxUpwardSpeed - verticalVelocity);
        else if (verticalVelocity < -MaxDownwardSpeed)
            currentVelocity += Vector2.zero.WithY(-MaxDownwardSpeed - verticalVelocity);
    }
    private float GetCannonInput()
    {
        // Check for any mouse input.
        var mouseInput = InputManager.Instance.LookUp;

        var screenPos = camera.WorldToScreenPoint(CannonRotor.position);

        var offset = mouseInput - screenPos.xy();

        var result = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;

        return result;
    }

    private void UpdateCannon()
    {

        var newRotation = Quaternion.AngleAxis(cannonAngle, Vector3.back);

        switch (rotorRotationMethod)
        {
            case RotorRotationMethod.None:
                {
                    CannonRotor.rotation = newRotation;
                    break;
                }
            case RotorRotationMethod.RotateTowards:
                {
                    var smoothTime = (RotorSpeed * 100) * Time.deltaTime;
                    CannonRotor.rotation = Quaternion.RotateTowards(CannonRotor.rotation, newRotation, smoothTime);
                    break;
                }
            case RotorRotationMethod.Lerp:
                {
                    var smoothTime = RotorSpeed * Time.deltaTime;
                    CannonRotor.rotation = Quaternion.Lerp(CannonRotor.rotation, newRotation, smoothTime);
                    break;
                }
            case RotorRotationMethod.Slerp:
                {
                    var smoothTime = RotorSpeed * Time.deltaTime;
                    CannonRotor.rotation = Quaternion.Slerp(CannonRotor.rotation, newRotation, smoothTime);
                    break;
                }
            case RotorRotationMethod.SmoothDamp:
                {
                    CannonRotor.rotation = ExtensionsLibrary.SmoothDamp(CannonRotor.rotation, newRotation,
                        ref cannonVelocity, rotorSmoothSpeed);
                    break;
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    
}
