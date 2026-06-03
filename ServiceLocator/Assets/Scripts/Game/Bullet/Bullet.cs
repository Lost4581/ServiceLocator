using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _obstacleLayer;

    private ITargetData _targetData;
    private ISoundPlayer _soundPlayer;
    private float _maxLifetime;
    private float _timer;
    private bool _autoAim;
    private IMemoryPool _pool;

    [Inject]
    public void Construct(ITargetData targetData, ISoundPlayer soundPlayer, [Inject(Id = "BulletLifetime")] float maxLifetime)
    {
        _targetData = targetData;
        _soundPlayer = soundPlayer;
        _maxLifetime = maxLifetime;
    }

    public void SetPool(IMemoryPool pool) => _pool = pool;

    public void Initialize(Vector2 position, Vector2 direction, bool autoAim)
    {
        gameObject.SetActive(true);
        transform.position = position;
        _autoAim = autoAim;
        _timer = 0f;
        _rb.linearVelocity = direction * _speed;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        _rb.linearVelocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer >= _maxLifetime)
        {
            ReturnToPool();
            return;
        }

        if (_autoAim && _targetData != null)
        {
            Collider2D obstacle = Physics2D.OverlapCircle(_rb.position, _checkRadius, _obstacleLayer);
            if (obstacle == null)
            {
                Vector2 toTarget = ((Vector2)_targetData.Position - _rb.position).normalized;
                _rb.linearVelocity = toTarget * _speed;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out DestructibleObstacle obstacle))
        {
            obstacle.Break();
            _soundPlayer?.PlayHitSound();
        }
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_pool != null)
            _pool.Despawn(this);
        else
            Deactivate();
    }

    public class Pool : MemoryPool<Bullet>
    {
        protected override void OnCreated(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        protected override void OnSpawned(Bullet bullet)
        {
            bullet.SetPool(this);
        }

        protected override void OnDespawned(Bullet bullet)
        {
            bullet.Deactivate();
            bullet.SetPool(null);
        }
    }
}