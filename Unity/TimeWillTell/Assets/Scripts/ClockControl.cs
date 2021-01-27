using System;
using System.Collections;
using UnityEngine;

public class ClockControl : MonoBehaviour
{
    public static event Action Solved;
    
    [SerializeField] private int[] _correctTime;

    private Interactive _interactive;
    private int[]       _playerTime;

    private void Start()
    {
        _interactive = GetComponent<Interactive>();

        _playerTime  = new int[] {0,5,3,8};
        _correctTime = new int[] {1,7,2,0};

        ClockUpdate.NextDigit += CompareTime;
    }

    private void CompareTime(string row, int number)
    {
        switch (row)
        {
            case "WheelTime_1":
                _playerTime[0] = number;
                break;
            case "WheelTime_2":
                _playerTime[1] = number;
                break;
            case "WheelTime_3":
                _playerTime[2] = number;
                break;
            case "WheelTime_4":
                _playerTime[3] = number;
                break;
        }

        if (_playerTime[0] == _correctTime[0] && 
            _playerTime[1] == _correctTime[1] &&
            _playerTime[2] == _correctTime[2] &&
            _playerTime[3] == _correctTime[3])
        {
            Debug.Log("Clock Opened");
            Solved();
        }
    }
}
