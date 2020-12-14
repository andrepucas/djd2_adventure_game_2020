using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private const float INTERACT_RADIUS = 1.5f;

    [SerializeField]
    private UserInterface userInterface;
    
    private Transform           _camera;
    private Interactive         _currentInteractive;
    //private List<Interactive>   _inventory;
    private bool                _hasRequiredInteractive;

    private void Start()
    {
        _camera                 = GetComponentInChildren<Camera>().transform;
        //_inventory              = new List<Interactive>();
        _hasRequiredInteractive = false;
    }

    private void Update()
    {
        LookForInteractive();
        LookForAction();
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
        userInterface.HideInteractionMsg();
    }

    private void NewInteractive(Interactive interactive)
    {
        _currentInteractive = interactive;

        if (PlayerHasRequiredInteractive())
        {
            _hasRequiredInteractive = true;
            userInterface.ShowInteractionMsg(interactive.GetInteractionMsg());
        }
        else
        {
            _hasRequiredInteractive = false;
            userInterface.ShowInteractionMsg(interactive.GetRequirementMsg());
        }
    }

    private bool PlayerHasRequiredInteractive()
    {
        if (_currentInteractive.requirements == null) 
            return true;

        for (int i = 0; i < _currentInteractive.requirements.Length; ++i)
        {
            // if (!InInventory(_currentInteractive.requirements[i]))
            //     return false;

            if (Directory.instance.Contains(_currentInteractive.requirements[i]))
                return false;
        }
        return true;
    }

    // private bool InInventory(Interactive item)
    // {
    //     return _inventory.Contains(item);
    // }

    private void LookForAction()
    {
        if (Input.GetMouseButtonDown(0) && _currentInteractive != null)
        {
            if (_currentInteractive.type == InteractiveType.PICKABLE)
                PickUp();

            else if (_hasRequiredInteractive)
                Interaction();
        }
    }

    private void PickUp()
    {
        _currentInteractive.gameObject.SetActive(false);
        //AddToInventory(_currentInteractive);
        Directory.instance.Add(_currentInteractive);
    }

    private void Interaction()
    {
        for (int i = 0; i < _currentInteractive.requirements.Length; ++i)
        {
            Interactive currentRequirement = _currentInteractive.requirements[i];
            currentRequirement.gameObject.SetActive(true);
            currentRequirement.Interact();
            //RemoveFromInventory(_currentInteractive.requirements[i]);
            Directory.instance.Remove(_currentInteractive.requirements[i]);
        }
        _currentInteractive.Interact();

        ClearInteractive();
    }

    // private void AddToInventory(Interactive item)
    // {
    //     _inventory.Add(item);
    //     // Add to physical UI
    // }

    // private void RemoveFromInventory(Interactive item)
    // {
    //     _inventory.Remove(item);
        
    //     // Remove from physical UI

    //     // Reorganize inventory icons
    // }
}
