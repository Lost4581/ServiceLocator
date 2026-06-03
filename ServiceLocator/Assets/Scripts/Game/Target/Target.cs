using UnityEngine;

public class Target : MonoBehaviour, ITargetData
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;

    public Vector3 Position => transform.position;

    void Update()
    {
        if (_player != null)
            transform.position = _player.position + _offset;
    }
}