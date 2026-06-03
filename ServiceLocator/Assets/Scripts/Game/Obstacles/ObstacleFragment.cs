using UnityEngine;

public class ObstacleFragment : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _pushForce = 5f;

    public void Push(Vector2 direction)
    {
        _rb.AddForce(direction * _pushForce, ForceMode2D.Impulse);
    }
}