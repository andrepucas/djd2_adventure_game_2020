using UnityEngine;
using UnityEngine.Audio;

public class Interactive : MonoBehaviour
{
    public bool             isActive;
    public InteractiveType  type;
    public PickableType     pickableType;
    public GameObject       viewPoint;
    public Sprite           icon;
    public Collider         col;
    public Interactive[]    requirements;

    [SerializeField] private string         _requirementMsg;
    [SerializeField] private string[]       _interactionMsgs;
    [SerializeField] private Interactive[]  _activationChain;
    [SerializeField] private Interactive[]  _interactionChain;
    [SerializeField] private AudioClip[]    _audioClips;
    [SerializeField] private bool           _colliderOffAfter;

    private Animator    _animator;
    private AudioSource _audioSource;
    private int         _currentMsgID;

    private void Start()
    {
        col             = GetComponent<Collider>();
        _animator       = GetComponent<Animator>();
        _audioSource    = GetComponent<AudioSource>();
        _currentMsgID   = 0;

        if (!isActive && col != null && type != InteractiveType.CONTROLLED)
            col.enabled = false;
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

        if (_audioSource != null)
        {
            _audioSource.clip = _audioClips[0];
            if (_audioSource.enabled) _audioSource.Play();
        }
        
        if (isActive)
        {
            if (type == InteractiveType.MULTIPLE || type == InteractiveType.BOOK||
                type == InteractiveType.TV_REMOTE)
            {
                if (_interactionMsgs.Length > 1)
                    _currentMsgID = (_currentMsgID + 1) % _interactionMsgs.Length;
                //if (_audioClips.Length > 1)
                    //_audioSource.clip = (_audioClips + 1)
            }
            else
            {
                if (type != InteractiveType.CONTROLLED)
                    isActive = false;

                if(_colliderOffAfter) 
                    col.enabled = false;
            }
            ProcessActivationChain();
            ProcessInteractionChain();
        }
    }

    private void ProcessActivationChain()
    {
        if (_activationChain != null)
        {
            for (int i = 0; i < _activationChain.Length; i++)
            {
                if(!_activationChain[i].isActive)
                {
                    _activationChain[i].isActive = true;
                    
                    if (col != null) 
                        _activationChain[i].col.enabled = true;
                }
                
                else 
                {
                    _activationChain[i].isActive = false;
                    
                    if (col != null) 
                        _activationChain[i].col.enabled = false;
                }
                    
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

    public void PlayAudio(int clip)
    {
        if (_audioSource != null)
        {
            if (clip == 0)
                _audioSource.clip = _audioClips[0];
            else 
                _audioSource.clip = _audioClips[1];
            
            //_audioSource.Play(0);
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position);
            //_audioSource.PlayOneShot(_audioSource.clip, 1f);
        }
    }

    public bool HasAudioClip(int clip)
    {
        if (clip < _audioClips.Length)
            return true;
        else 
            return false;
    }
}
