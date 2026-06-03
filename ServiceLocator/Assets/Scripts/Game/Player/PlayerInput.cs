using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _autoAimCheckRadius = 5f;
    [SerializeField] private LayerMask _obstacleLayer;

    private Bullet.Pool _bulletPool;
    private ISoundPlayer _soundPlayer;
    private Camera _camera;
    private InputAction _shootAction;

    [Inject]
    public void Construct(Bullet.Pool bulletPool, ISoundPlayer soundPlayer)
    {
        _bulletPool = bulletPool;
        _soundPlayer = soundPlayer;
        _camera = Camera.main;
    }

    void Awake()
    {
        _shootAction = new InputAction("Shoot", binding: "<Mouse>/leftButton");
        _shootAction.performed += OnShoot;
    }

    void OnEnable() => _shootAction.Enable();
    void OnDisable() => _shootAction.Disable();
    void OnDestroy() => _shootAction.performed -= OnShoot;

    void OnShoot(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePos - (Vector2)_firePoint.position).normalized;

        bool autoAim = Physics2D.OverlapCircle(_firePoint.position, _autoAimCheckRadius, _obstacleLayer) == null;

        _soundPlayer?.PlayShootSound();

        Bullet bullet = _bulletPool.Spawn();
        bullet.Initialize(_firePoint.position, direction, autoAim);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_firePoint.position, _autoAimCheckRadius);
    }
}