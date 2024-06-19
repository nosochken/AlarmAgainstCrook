using System;
using UnityEngine;

public class Witness : MonoBehaviour
{
    [SerializeField] private Crook _crook;
    [SerializeField] private AlarmHouse _alarmHouse;

    public event Action CrookSpottedInHouse;

    private void Start()
    {
        if (_alarmHouse.LocationBounds.Contains(_crook.LocationBounds.min) &&
        _alarmHouse.LocationBounds.Contains(_crook.LocationBounds.max))
        {
            CrookSpottedInHouse?.Invoke();
        }
    }
}