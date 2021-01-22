using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    private const float INTERACT_RADIUS = 1.5f;
    
    private UserInterface       _ui;
    private PlayerDirectory     _directory;
    private Transform           _camera;
    private Vector3             _originalCameraPos;
    private Quaternion          _originalCameraRot;
    private Interactive         _currentInteractive;
    private Interactive         _comboInteractive;
    private TVControl           _tv;
    private bool                _hasRequiredInteractive;
    private bool                _inCombinationMode;
    private bool                _isInspecting;

    private void Start()
    {
        _ui                     = UserInterface.instance;
        _directory              = PlayerDirectory.instance;
        _camera                 = GetComponentInChildren<Camera>().transform;
        _originalCameraPos      = new Vector3(0f, 0.7f, 0f);
        _originalCameraRot      = new Quaternion(0f, 0f, 0f, 0f);
        _tv                     = GameObject.Find("TV").GetComponent<TVControl>();
        _hasRequiredInteractive = false;
        _inCombinationMode      = false;
        _isInspecting           = false;

        SafeLockControl.Solved += CombinationSolved;
    }

    private void Update()
    {
        LookForInteractive();
        LookForAction();
        QuitCombination();
        QuitInspectMode();
    }

    private void LookForInteractive()
    {
        if (Physics.Raycast(_camera.position, _camera.forward, 
            out RaycastHit hit, INTERACT_RADIUS))
        {
            Interactive interactive = hit.transform.GetComponent<Interactive>();
            Debug.DrawRay(_camera.position, _camera.forward, Color.red, .2f);

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
        // Fixes bug where player would be able to interact with a non active 
        // interactive if he was looking at an active one right before 
        // using memory travel. (Present/Past Doors)
        else ClearInteractive();
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

            else if (_currentInteractive.type == InteractiveType.TV_REMOTE)
                ControlTV();

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

        MoveCameraTo(_currentInteractive.viewPoint);
        _ui.ShowCursor("");
    }

    private void MoveCameraTo(GameObject viewPoint)
    {
        _camera.position = viewPoint.transform.position;
        _camera.rotation = viewPoint.transform.rotation;
    }

    private void QuitCombination()
    {
        if (_inCombinationMode)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _inCombinationMode = false;

                _currentInteractive = _comboInteractive;
                _currentInteractive.GetComponent<BoxCollider>().enabled = true;

                _camera.localPosition   = _originalCameraPos;
                _camera.localRotation   = _originalCameraRot;
                _ui.HideCursor();
            }
        }
    }

    private void CombinationSolved()
    {
        _inCombinationMode = false;

        _currentInteractive = _comboInteractive;
        _currentInteractive.GetComponent<BoxCollider>().enabled = true;

        _camera.localPosition   = _originalCameraPos;
        _ui.HideCursor();

        Interaction();
    }

    private void ControlTV()
    {
        if (_tv.isPlaying != "OFF")
            _tv.Play("OFF");

        else
            _tv.Play("VHS_1");
        
        Interaction();
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

    private void QuitInspectMode()
    {
        if (_isInspecting && 
           (Input.GetMouseButtonDown(1) || Input.GetButtonDown("Pause")))
            _ui.HideInspectMode();
    }
}
