using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    private const float INTERACT_RADIUS = 1.5f;
    
    private UserInterface       _ui;
    private PlayerDirectory     _directory;
    private PlayerMovement      _movement;
    private PlayerMemoryTravel  _memoryTravel;
    private Transform           _camera;
    private Transform           _originalCamera;
    private Interactive         _currentInteractive;
    private Interactive         _comboInteractive;
    private bool                _hasRequiredInteractive;
    private bool                _inCombinationMode;
    private bool                _isInspecting;

    private void Start()
    {
        _ui                     = UserInterface.instance;
        _directory              = PlayerDirectory.instance;
        _movement               = PlayerMovement.instance;
        _memoryTravel           = PlayerMemoryTravel.instance;
        _camera                 = GetComponentInChildren<Camera>().transform;
        _originalCamera         = GetComponentInChildren<Camera>().transform;
        _hasRequiredInteractive = false;
        _inCombinationMode      = false;
        _isInspecting           = false;

        SafeLockControl.Solved += CombinationSolved;
    }

    private void Update()
    {
        LookForInteractive();
        LookForAction();
        LookForQuitCombination();
        LookForInspectMode();
    }

    private void LookForInteractive()
    {
        if (Physics.Raycast(_camera.position, _camera.forward, 
            out RaycastHit hit, INTERACT_RADIUS))
        {
            Interactive interactive = hit.transform.GetComponent<Interactive>();

            Debug.DrawRay(_camera.position, _camera.forward, Color.red, .1f);

            if (interactive == null) 
                ClearInteractive();

            else if (interactive != _currentInteractive)
                NewInteractive(interactive);
        }
        else
            ClearInteractive();
    }

    private void ClearInteractive()
    {
        _currentInteractive = null;
        _ui.HideInteractionMsg();
    }

    private void NewInteractive(Interactive interactive)
    {
        if (interactive.isActive)
        {
            _currentInteractive = interactive;

            if (PlayerHasRequiredInteractive())
            {
                _hasRequiredInteractive = true;
                _ui.ShowInteractionMsg(interactive.GetInteractionMsg());
            }
            else
            {
                _hasRequiredInteractive = false;
                _ui.ShowInteractionMsg(interactive.GetRequirementMsg());
            }
        }
    }

    private bool PlayerHasRequiredInteractive()
    {
        if (_currentInteractive.requirements == null) 
            return true;

        for (int i = 0; i < _currentInteractive.requirements.Length; ++i)
        {
            if (!_directory.Contains(_currentInteractive.requirements[i]))
                return false;
        }
        return true;
    }

    private void LookForAction()
    {
        if (Input.GetMouseButtonDown(0) && _currentInteractive != null)
        {
            if (_currentInteractive.type == InteractiveType.PICKABLE)
                PickUp();

            else if (_currentInteractive.type == InteractiveType.COMBINATION)
                Combination();

            else if (_hasRequiredInteractive)
                Interaction();
        }
    }

    private void PickUp()
    {
        _currentInteractive.gameObject.SetActive(false);

        _directory.Add(_currentInteractive);

        _isInspecting = true;
    }

    private void Combination()
    {
        _inCombinationMode = true;
        
        _comboInteractive = _currentInteractive;
        
        _currentInteractive.GetComponent<BoxCollider>().enabled = false;
        
        _movement.enabled       = false;
        _memoryTravel.enabled   = false;

        MoveCameraTo(_currentInteractive.viewPoint);
        _ui.ShowCursor();
    }

    private void MoveCameraTo(GameObject viewPoint)
    {
        _originalCamera.position = _camera.position;

        _camera.position = viewPoint.transform.position;
        _camera.rotation = viewPoint.transform.rotation;
    }

    private void Interaction()
    {
        for (int i = 0; i < _currentInteractive.requirements.Length; ++i)
        {
            Interactive currentRequirement = _currentInteractive.requirements[i];

            currentRequirement.gameObject.SetActive(true);
            currentRequirement.Interact();

            _directory.Remove(_currentInteractive.requirements[i]);
        }
        _currentInteractive.Interact();

        ClearInteractive();
    }

    private void LookForQuitCombination()
    {
        if (_inCombinationMode)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _inCombinationMode = false;

                _currentInteractive = _comboInteractive;
                _currentInteractive.GetComponent<BoxCollider>().enabled = true;
                
                _movement.enabled       = true;
                _memoryTravel.enabled   = true;
                _camera.position        = _originalCamera.position;
                _ui.HideCursor();
            }
        }
    }

    private void CombinationSolved()
    {
        _inCombinationMode = false;

        _currentInteractive = _comboInteractive;
        _currentInteractive.GetComponent<BoxCollider>().enabled = true;
        
        _movement.enabled       = true;
        _memoryTravel.enabled   = true;
        _camera.position        = _originalCamera.position;
        _ui.HideCursor();

        Interaction();
    }

    private void LookForInspectMode()
    {
        if(_isInspecting && Input.GetMouseButtonDown(1))
            _ui.HideInspectMode();
    }
}
