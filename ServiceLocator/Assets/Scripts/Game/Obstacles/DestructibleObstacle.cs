using UnityEngine;
using System.Collections.Generic;

public class DestructibleObstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _wholeSprite;
    [SerializeField] private Collider2D _wholeCollider;
    [SerializeField] private List<ObstacleFragment> _fragments;

    public void Break()
    {
        _wholeSprite.enabled = false;
        _wholeCollider.enabled = false;

        foreach (var fragment in _fragments)
        {
            fragment.gameObject.SetActive(true);
            fragment.Push(Random.insideUnitCircle.normalized);
        }
    }
}