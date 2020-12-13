using UnityEngine;
using System.Collections;

public class MemoryTravel : MonoBehaviour
{
    private GameObject          _player;
    private CharacterController _controller;
    private Vector3             _distance;
    private bool                _inMemoryTravel;
    private bool                _memTravelReady;

    private void Start()
    {
        _player         = GameObject.Find("Player"); 
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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_inMemoryTravel)
            {
                _distance = new Vector3(-15, 0, 0);
            }
            else
            {
                _distance = new Vector3(15, 0, 0);
            }

            if (_memTravelReady == true)
                Teleport();
                
        } 
    }

    private void Teleport()
    {
        _controller.enabled = false;
        
        _player.transform.Translate(_distance, Space.World);

        _controller.enabled = true;

        _inMemoryTravel = !_inMemoryTravel;
        _memTravelReady = false;

        StartCoroutine(CooldownCourotine());
    }

    private IEnumerator CooldownCourotine()
    {
        yield return new WaitForSeconds(5);
        _memTravelReady = true;
    }
}
