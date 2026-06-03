using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Rigidbody2D _rb;
    private InputAction _moveAction;
    private Vector2 _input;

    void Awake()
    {
        _rb = GetComponent < Rigidbody2D > ();

        _moveAction = new InputAction("Move", type: InputActionType.Value);
        _moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        _moveAction.performed += ctx => _input = ctx.ReadValue<Vector2>();
        _moveAction.canceled += ctx => _input = Vector2.zero;
    }

    void OnEnable() => _moveAction.Enable();
    void OnDisable() => _moveAction.Disable();

    void FixedUpdate()
    {
        _rb.linearVelocity = _input.normalized * _speed;
    }
}