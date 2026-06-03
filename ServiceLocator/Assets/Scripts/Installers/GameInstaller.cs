using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _hitSound;

    [Header("Bullet")]
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletLifetime = 3f;

    [Header("Player")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Target")]
    [SerializeField] private Target _target;

    public override void InstallBindings()
    {
        Container.BindInstance(_audioSource).AsSingle();
        Container.BindInstance(_playerInput).AsSingle();
        Container.Bind<ITargetData>().To<Target>().FromInstance(_target).AsSingle();

        if (_shootSound != null)
            Container.BindInstance(_shootSound).WithId("ShootSound");
        if (_hitSound != null)
            Container.BindInstance(_hitSound).WithId("HitSound");

        Container.Bind<ISoundPlayer>().To < SoundPlayer > ().AsSingle().NonLazy();
        Container.Bind<float>().WithId("BulletLifetime").FromInstance(_bulletLifetime).AsSingle();

        Container.BindMemoryPool<Bullet, Bullet.Pool>()
            .WithInitialSize(50)
            .FromComponentInNewPrefab(_bulletPrefab)
            .UnderTransformGroup("Bullets");
    }
}