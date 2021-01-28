using System;
using System.Collections;
using UnityEngine;

public class ClockUpdate : MonoBehaviour
{
    public static event Action<string, int> NextDigit = delegate {};

    private bool        _coroutineAllowed;
    private AudioSource _audioSource;

    [SerializeField] private int _wheelTimeNumber;

    private void Start()
    {
        _coroutineAllowed   = true;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (_coroutineAllowed)
        {
            StartCoroutine("RotateWheelTime");
        }
    }

    private IEnumerator RotateWheelTime()
    {
        _coroutineAllowed = false;

        for (int i = 0; i < 2; i++)
        {
            transform.Rotate(0f, 18f, 0f, Space.Self);
            yield return new WaitForSeconds(0.05f);
        }
        _audioSource.Play(0);
        _coroutineAllowed = true;

        _wheelTimeNumber += 1;

        if (_wheelTimeNumber > 9)
            _wheelTimeNumber = 0;

        NextDigit(name, _wheelTimeNumber);
    }
}
