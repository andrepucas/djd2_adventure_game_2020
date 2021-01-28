using System;
using System.Collections;
using UnityEngine;

public class SafeLockRotate : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate {};

    private SpriteRenderer[]    _sprites;
    private AudioSource         _audioSource;
    
    private bool     _coroutineAllowed;
    private int      _wheelLockNumber;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _sprites     = GetComponentsInChildren<SpriteRenderer>();
        
        _coroutineAllowed = true;
        _wheelLockNumber  = 0;

        SafeLockControl.Solved += SolvedColor;
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

        for (int i = 0; i < 6; i++)
        {
            transform.Rotate(-6f, 0f, 0f, Space.World);
            
            yield return new WaitForSeconds(0.005f);
        }
        _audioSource.Play(0);
        _coroutineAllowed = true;

        _wheelLockNumber += 1;

        if (_wheelLockNumber > 9)
            _wheelLockNumber = 0;

        Rotated(name, _wheelLockNumber);
    }

    private void SolvedColor()
    {
        foreach (SpriteRenderer sprite in _sprites)
        {
            sprite.color = Color.green;
        }
    }
}
