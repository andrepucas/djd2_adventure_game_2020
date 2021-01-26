using System;
using System.Collections;
using UnityEngine;

public class StatueBook : MonoBehaviour
{
    public static event Action<string, int> Pushed = delegate {};

    private Interactive _book;
    private bool        _coroutineAllowed;
    private int         _statuePosition;

    private void Start()
    {
        _book               = GetComponent<Interactive>();
        _coroutineAllowed   = true;
        _statuePosition     = 1;
    }

    private void OnMouseDown()
    {
        if (_coroutineAllowed && _book.isActive)
        {
            StartCoroutine("PushBook");
        }
    }

    private IEnumerator PushBook()
    {
        _coroutineAllowed = false;

        _book.Interact();
        yield return new WaitForSeconds(2f);

        _coroutineAllowed = true;

        _statuePosition += 1;

        if (_statuePosition > 4)
            _statuePosition = 1;

        Pushed(name, _statuePosition);
    }
}
