using UnityEngine;
using System.Collections;

public class PlayerMemoryTravel : MonoBehaviour
{
    private Transform           _playerTransf, _cameraTransf;
    private CharacterController _controller;
    private Vector3             _distance, _height;
    private bool                _inMemoryTravel;
    private bool                _memTravelReady;

    private void Start()
    {
        _playerTransf   = GameObject.Find("Player").transform;
        _cameraTransf   = GetComponentInChildren<Camera>().transform;
        _controller     = GetComponent<CharacterController>();
        _inMemoryTravel = false;
        _memTravelReady = true;
    }

    private void Update()
    {
        CheckForMemoryTravel();
    }

    private void CheckForMemoryTravel()
    {
        if (Input.GetButtonDown("Memory Travel") && !Cursor.visible)
        {
            if (_inMemoryTravel)
            {
                _distance   = new Vector3(-35f, 0f, 0f);
                _height     = new Vector3(0f, 0.7f, 0f);
            }

            else
            {
                _distance   = new Vector3(35f, 0f, 0f);
                _height     = new Vector3(0f, 0.3f, 0f);
            }

            if (_memTravelReady == true)
                Teleport();
        } 
    }

    private void Teleport()
    {
        _controller.enabled = false;
        
        _playerTransf.Translate(_distance, Space.World);
        _cameraTransf.localPosition = _height;

        _controller.enabled = true;

        _inMemoryTravel = !_inMemoryTravel;
        _memTravelReady = false;

        if (!_memTravelReady)
        {
            StartCoroutine(CooldownCourotine());
        }
        else _memTravelReady = true;
    }

    private IEnumerator CooldownCourotine()
    {
        yield return new WaitForSeconds(.5f);
        _memTravelReady = true;
    }
}
