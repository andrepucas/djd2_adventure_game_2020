using UnityEngine;

public class MemoryTravel : MonoBehaviour
{
    private GameObject _player;
    private CharacterController _controller;
    private Vector3 _distance;
    private bool _inMemoryTravel;

    private GameObject _cube;


    void Start()
    {
        _player = GameObject.Find("Player"); 
        _controller = GetComponent<CharacterController>();
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
                _distance = new Vector3(-15, 0, 0);
            }
            else
            {
                _distance = new Vector3(15, 0, 0);
            }

            Teleport();

            Debug.Log($"Distance: {_distance}");
        }
    }

    private void Teleport()
    {
        _controller.enabled = false;
        
        _player.transform.Translate(_distance, Space.World);

        _controller.enabled = true;

        _inMemoryTravel = !_inMemoryTravel;


        Debug.Log($"MemoryTravel: {_inMemoryTravel}");

    }
}
