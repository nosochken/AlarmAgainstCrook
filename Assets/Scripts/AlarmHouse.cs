using System;
using UnityEngine;

[RequireComponent(typeof(CompositeCollider2D), typeof(Rigidbody2D))]
public class AlarmHouse : MonoBehaviour
{
    [SerializeField] private Witness _witness;

    private CompositeCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private Bounds _bounds;

    private Crook _crook;

    private bool _isCrookOnriggerEnter;
    private bool _isCrookOnriggerExit;
    private bool _isCrookInside;

    public event Action CrookCameIn;
    public event Action CrookCameOut;

    public Bounds LocationBounds => _bounds;

    public bool IsCrookInside => _isCrookInside;

    private void Awake()
    {
        _collider = GetComponent<CompositeCollider2D>();
        _collider.isTrigger = true;
        _bounds = _collider.bounds;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void OnEnable()
    {
        _witness.CrookSpottedInHouse += DetectCrook;
    }

    private void OnDisable()
    {
        _witness.CrookSpottedInHouse -= DetectCrook;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Crook crook))
        {
            _crook = crook;
            _isCrookOnriggerEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Crook crook))
        {
            if (_crook == crook)
                _isCrookOnriggerExit = true;
        }

        if (_isCrookOnriggerEnter && _isCrookOnriggerExit == true)
            RespondToActionOfCrook();

        ResetTriggerCrossingData();
    }

    private void RespondToActionOfCrook()
    {
        if (_isCrookInside == false)
            DetectCrook();
        else
            LoseSightOfCrook();
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

    private void ResetTriggerCrossingData()
    {
        _isCrookOnriggerEnter = false;
        _isCrookOnriggerExit = false;
    }
}