using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Crook : MonoBehaviour
{
    private Collider2D _collider;
    private Bounds _bounds;

    public Bounds LocationBounds => _bounds;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();

        _bounds = _collider.bounds;
    }
}