using System;
using UnityEngine;

public class SafeLockControl : MonoBehaviour
{
    public static event Action Solved;
    
    [SerializeField] private int[] _correctCombo;

    private AudioSource _audioSource;
    private Interactive _interactive;
    private int[]       _playerCombo;

    private void Start()
    {
        _interactive = GetComponent<Interactive>();
        _audioSource = GetComponent<AudioSource>();

        _playerCombo  = new int[6];
        _correctCombo = new int[ ] {9,6,2,5,1,7};

        SafeLockRotate.Rotated += CompareCombos;
    }

    private void CompareCombos(string wheelLock, int number)
    {
        switch (wheelLock)
        {
            case "WheelLock_1":
                _playerCombo[0] = number;
                break;
            case "WheelLock_2":
                _playerCombo[1] = number;
                break;
            case "WheelLock_3":
                _playerCombo[2] = number;
                break;
            case "WheelLock_4":
                _playerCombo[3] = number;
                break;
            case "WheelLock_5":
                _playerCombo[4] = number;
                break;
            case "WheelLock_6":
                _playerCombo[5] = number;
                break;
        }

        if (_playerCombo[0] == _correctCombo[0] && 
            _playerCombo[1] == _correctCombo[1] &&
            _playerCombo[2] == _correctCombo[2] &&
            _playerCombo[3] == _correctCombo[3] &&
            _playerCombo[4] == _correctCombo[4] &&
            _playerCombo[5] == _correctCombo[5])
        {
            Debug.Log("Safe Opened");
            _audioSource.Play(0);
            Solved();
        }
    }

    private void OnDestroy()
    {
        SafeLockRotate.Rotated -= CompareCombos;
    }
}
