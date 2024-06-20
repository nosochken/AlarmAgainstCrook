using System;
using UnityEngine;

[RequireComponent(typeof(CompositeCollider2D), typeof(Rigidbody2D))]
public class AlarmHouse : MonoBehaviour
{
    private CompositeCollider2D _collider;
    private Rigidbody2D _rigidbody;

    private Crook _crook;
    private bool _isCrookInside;

    public event Action CrookCameIn;
    public event Action CrookCameOut;

    public bool IsCrookInside => _isCrookInside;

    private void Awake()
    {
        _collider = GetComponent<CompositeCollider2D>();
        _collider.isTrigger = true;
        _collider.geometryType = CompositeCollider2D.GeometryType.Polygons;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Crook crook))
        {
            _crook = crook;

            if (_isCrookInside == false)
                DetectCrook();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Crook crook))
        {
            if (_crook == crook)
                LoseSightOfCrook();
        }
    }

    private void DetectCrook()
    {
        _isCrookInside = true;
        CrookCameIn?.Invoke();
    }

    private void LoseSightOfCrook()
    {
        _isCrookInside = false;
        CrookCameOut?.Invoke();
    }
}