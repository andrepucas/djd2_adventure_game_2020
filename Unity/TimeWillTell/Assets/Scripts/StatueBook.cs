using System;
using System.Collections;
using UnityEngine;

public class StatueBook : MonoBehaviour
{
    public static event Action<string, int> Pushed = delegate {};

    private Interactive _book;
    private Animator    _anim;
    private bool        _coroutineAllowed;
    private int         _statuePosition;

    private void Start()
    {
        _book = GetComponent<Interactive>();
        _anim = GetComponent<Animator>();

        _coroutineAllowed = true;
        _statuePosition   = 1;

        StatueControl.Solved += LockBook;
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
        _book.isActive = false;

        yield return new WaitForSeconds(2);

        _coroutineAllowed = true;
        _book.isActive = true;

        _statuePosition += 1;

        if (_statuePosition > 4)
            _statuePosition = 1;

        Pushed(name, _statuePosition);
    }

    private void LockBook()
    {
        _book.isActive = false;
        _anim.SetBool("Solved", true);
    }

    private void OnDestroy()
    {
        StatueControl.Solved -= LockBook;
    }
}
