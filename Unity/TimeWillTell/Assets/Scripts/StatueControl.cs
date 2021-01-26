using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueControl : MonoBehaviour
{
    [SerializeField] private int[] _statuePositions;
    [SerializeField] private int[] _correctPositions;

    void Start()
    {
        _statuePositions = new int[] {1,1,1,1};
        
        _correctPositions = new int[] {2,4,3,3};

        StatueBook.Pushed += ComparePositions;
    }

    private void ComparePositions(string book, int newPosition)
    {
        switch (book)
        {
            case "BookSecretButton_1":
                _statuePositions[0] = newPosition;
                break;
            case "BookSecretButton_2":
                _statuePositions[1] = newPosition;
                break;
            case "BookSecretButton_3":
                _statuePositions[2] = newPosition;
                break;
            case "BookSecretButton_4":
                _statuePositions[3] = newPosition;
                break;
        }
        
        if (_statuePositions[0] == _correctPositions[0] && 
            _statuePositions[1] == _correctPositions[1] &&
            _statuePositions[2] == _correctPositions[2] &&
            _statuePositions[3] == _correctPositions[3])

        Debug.Log("Statues Match.");
    }
}
