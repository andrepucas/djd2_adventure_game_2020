using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool             isActive;
    public InteractiveType  type;
    public PickableType     pickableType;
    public GameObject       viewPoint;
    public Sprite           icon;
    public Interactive[]    requirements;

    [SerializeField] private string         _requirementMsg;
    [SerializeField] private string[]       _interactionMsgs;
    [SerializeField] private Interactive[]  _activationChain;
    [SerializeField] private Interactive[]  _interactionChain;

    private Animator     _animator;
    private int          _currentMsgID;

    private void Start()
    {
        _animator       = GetComponent<Animator>();
        _currentMsgID   = 0;
    }

    public string GetInteractionMsg()
    {
        return _interactionMsgs[_currentMsgID];
    }

    public string GetRequirementMsg()
    {
        return _requirementMsg;
    }

    public void Interact()
    {
        if (_animator != null)
            _animator.SetTrigger("Interact");

        if (isActive)
        {
            ProcessActivationChain();
            ProcessInteractionChain();

            if (type == InteractiveType.MULTIPLE || 
                type == InteractiveType.TV_REMOTE)
            {
                _currentMsgID = (_currentMsgID + 1) % _interactionMsgs.Length;
            }

            else
                GetComponent<Collider>().enabled = false;
        }
    }

    private void ProcessActivationChain()
    {
        if (_activationChain != null)
        {
            for (int i = 0; i < _activationChain.Length; i++)
            {
                if(!_activationChain[i].isActive)
                    _activationChain[i].isActive = true;
            }
        }
    }
    
    private void ProcessInteractionChain()
    {
        if (_interactionChain != null)
        {
            for (int i = 0; i < _interactionChain.Length; i++)
            {
                _interactionChain[i].Interact();
            }
        }
    }
}
