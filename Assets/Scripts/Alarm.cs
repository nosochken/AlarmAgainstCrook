using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AlarmHouse _alarmHouse;

    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _maxDelta = 0.3f;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.volume = _minVolume;
    }

    private void OnEnable()
    {
        _audioSource.Play();

        _alarmHouse.CrookCameIn += IncreaseVolume;
        _alarmHouse.CrookCameOut += ReduceVolume;
    }

    private void OnDisable()
    {
        _audioSource.Stop();

        _alarmHouse.CrookCameIn -= IncreaseVolume;
        _alarmHouse.CrookCameOut -= ReduceVolume;
    }

    private void IncreaseVolume()
    {
        StartCoroutine(SmoothlyChangeVolume(_maxVolume));
    }

    private void ReduceVolume()
    {
        StartCoroutine(SmoothlyChangeVolume(_minVolume));
    }

    private IEnumerator SmoothlyChangeVolume(float target)
    {
        bool isCrookInside = _alarmHouse.IsCrookInside;

        while (_audioSource.volume != target && isCrookInside == _alarmHouse.IsCrookInside)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _maxDelta * Time.deltaTime);

            yield return null;
        }
    }
}