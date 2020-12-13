using UnityEngine;

public class Interactive : MonoBehaviour
{
    public InteractiveType  type;
    public Interactive[]    requirements;

    [SerializeField] private string[]       interactionMsgs;
    [SerializeField] private string         requirementMsg;
    [SerializeField] private bool           isActive;
    [SerializeField] private Interactive[]  activationChain;
    [SerializeField] private Interactive[]  interactionChain;

    private Animator     _animator;
    private int          _currentMsgID;

    private void Start()
    {
        _animator       = GetComponent<Animator>();
        _currentMsgID   = 0;
    }

    public string GetInteractionMsg()
    {
        return interactionMsgs[_currentMsgID];
    }

    public string GetRequirementMsg()
    {
        return requirementMsg;
    }

    public void Activate()
    {
        isActive = true;

        if (_animator != null)
        {
            _animator.SetTrigger("Activate");
        }
    }

    public void Interact()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Interact");
        }

        if (isActive)
        {
            ProcessActivationChain();
            ProcessInteractionChain();

            if (type == InteractiveType.SINGLE || type == InteractiveType.PICKABLE)
            {
                GetComponent<Collider>().enabled = false;
            }

            else if (type == InteractiveType.MULTIPLE)
            {
                _currentMsgID = (_currentMsgID + 1) % interactionMsgs.Length;
            }
        }
    }

    private void ProcessActivationChain()
    {
        if (activationChain != null)
        {
            for (int i = 0; i < activationChain.Length; i++)
            {
                activationChain[i].Activate();
            }
        }
    }
    
    private void ProcessInteractionChain()
    {
        if (interactionChain != null)
        {
            for (int i = 0; i < interactionChain.Length; i++)
            {
                interactionChain[i].Interact();
            }
        }
    }
}
