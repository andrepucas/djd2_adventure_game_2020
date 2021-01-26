using System;
using System.Collections;
using UnityEngine;

public class SafeLockRotate : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate {};

    private bool     _coroutineAllowed;
    private int      _wheelLockNumber;

    private void Start()
    {
        _coroutineAllowed   = true;
        _wheelLockNumber    = 0;
    }

    private void OnMouseDown()
    {
        if (_coroutineAllowed)
        {
            StartCoroutine("RotateWheelLock");
        }
    }

    private IEnumerator RotateWheelLock()
    {
        _coroutineAllowed = false;

        for (int i = 0; i < 12; i++)
        {
            transform.Rotate(-3f, 0f, 0f, Space.World);
            yield return new WaitForSeconds(0.005f);
        }

        _coroutineAllowed = true;

        _wheelLockNumber += 1;

        if (_wheelLockNumber > 9)
            _wheelLockNumber = 0;

        Rotated(name, _wheelLockNumber);
    }
}
