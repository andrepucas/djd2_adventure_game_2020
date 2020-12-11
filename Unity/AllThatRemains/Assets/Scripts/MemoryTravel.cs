using UnityEngine;

public class MemoryTravel : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3             _distance;
    private bool                _inMemoryTravel;

    void Start()
    {
        _controller     = GetComponent<CharacterController>();
        _inMemoryTravel = false;
    }

    void Update()
    {
        CheckForMemoryTravel();
    }

    private void CheckForMemoryTravel()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_inMemoryTravel)
            {
                _distance = new Vector3(-15,0,0);
            }
            else
            {
                _distance = new Vector3(15,0,0);
            }

            Teleport();
        }
    }

    private void Teleport()
    {
            _controller.detectCollisions = false;
            _controller.Move(transform.TransformVector(_distance));
            _controller.detectCollisions = true;
            
            _inMemoryTravel = !_inMemoryTravel;

            Debug.Log("Memory Travelled.");
            Debug.Log($"MemoryTravel: {_inMemoryTravel}");
    }
}
