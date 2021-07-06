using System;
using UnityEngine;

public class StatueControl : MonoBehaviour
{
    public static event Action Solved;
    
    [SerializeField] private Animator _secretDoor;
    
    [SerializeField] private int[] _statuePositions;
    [SerializeField] private int[] _correctPositions;

    void Start()
    {
        _statuePositions  = new int[] {1,1,1,1};
        _correctPositions = new int[] {2,3,2,3};

        StatueBook.Pushed += ComparePositions;
    }

    private void ComparePositions(string book, int newPosition)
    {
        switch (book)
        {
            case "BookButton_1":
                _statuePositions[0] = newPosition;
                break;
            case "BookButton_2":
                _statuePositions[1] = newPosition;
                break;
            case "BookButton_3":
                _statuePositions[2] = newPosition;
                break;
            case "BookButton_4":
                _statuePositions[3] = newPosition;
                break;
        }
        
        if (_statuePositions[0] == _correctPositions[0] && 
            _statuePositions[1] == _correctPositions[1] &&
            _statuePositions[2] == _correctPositions[2] &&
            _statuePositions[3] == _correctPositions[3])
        {
            Debug.Log("Statues Match.");

            Solved();

            _secretDoor.SetBool("Opened", true);

        }
    }

    private void OnDestroy()
    {
        StatueBook.Pushed -= ComparePositions;
    }
}
