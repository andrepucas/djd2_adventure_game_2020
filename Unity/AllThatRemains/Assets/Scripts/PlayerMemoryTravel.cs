using UnityEngine;
using System.Collections;

public class PlayerMemoryTravel : MonoBehaviour
{
    private Transform           _playerTransf, _cameraTransf;
    private CharacterController _controller;
    private Vector3             _distance, _height;
    private bool                _inMemoryTravel;
    private bool                _memTravelReady;

    #region Singleton
    public static PlayerMemoryTravel instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one player memory travel.");
            return;
        }

        instance = this;
    }
    #endregion

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
        
        if (Input.GetButtonDown("Memory Travel"))
        {
            if (_inMemoryTravel)
            {
                _distance   = new Vector3(-40f, 0f, 0f);
                _height     = new Vector3(0f, 0.7f, 0f);
            }

            else
            {
                _distance   = new Vector3(40f, 0f, 0f);
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

        if (!_inMemoryTravel)
            StartCoroutine(CooldownCourotine());
        else
            _memTravelReady = true;
    }

    private IEnumerator CooldownCourotine()
    {
        yield return new WaitForSeconds(3);
        _memTravelReady = true;
    }
}
