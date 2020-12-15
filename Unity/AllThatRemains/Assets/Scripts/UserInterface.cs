using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject _directoryPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _vhsPanel;
    [SerializeField] private GameObject _journalPanel;
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private Image      _image;
    [SerializeField] private Text       _interactionText;

    private DirectorySlot[] _inventorySlots;
    private DirectorySlot[] _vhsSlots;
    private DirectorySlot[] _journalSlots;

    private PlayerMovement  _playerMovement;
    private GameObject      _player;

    #region Singleton
    public static UserInterface instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one user interface.");
            return;
        }

        instance = this;
    }
    #endregion

    private void Start()
    {
        HideDirectory();
        HideInteractionMsg();

        _inventorySlots = _inventoryPanel.GetComponentsInChildren<DirectorySlot>();
        _vhsSlots       = _vhsPanel.GetComponentsInChildren<DirectorySlot>();
        _journalSlots   = _journalPanel.GetComponentsInChildren<DirectorySlot>();
    }

    private void Update()
    {
        LookForDirectory();
    }

    private void LookForDirectory()
    {
        if (Input.GetButtonDown("Directory"))
            if (_directoryPanel.activeSelf)
                HideDirectory();
            else
                ShowDirectory();
    }

    private void HideDirectory()
    {
        Cursor.visible      = false;
        Cursor.lockState    = CursorLockMode.Locked;
        _directoryPanel.SetActive(false);
    }
    private void ShowDirectory()
    {
        Cursor.visible      = true;
        Cursor.lockState    = CursorLockMode.Confined;
        _directoryPanel.SetActive(true);
    }

    public void ShowInteractionMsg(string message)
    {
        _interactionText.text = message;
        _interactionPanel.SetActive(true);
    }

    public void HideInteractionMsg()
    {
        _interactionPanel.SetActive(false);
    }

    public void ShowImage(Sprite icon)
    {
        _image.sprite     = icon;
        _image.enabled    = true;
    }

    public void HideImage()
    {
        _image.sprite     = null;
        _image.enabled    = false;
    }

    public void UpdateInventoryIcons(List<Interactive> inventoryItems)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (i < inventoryItems.Count)
                _inventorySlots[i].AddSlot(inventoryItems[i]);
            
            else
                _inventorySlots[i].ClearSlot();
        }
    }

    public void UpdateVhsIcons(List<Interactive> vhsItems)
    {
        for (int i = 0; i < _vhsSlots.Length; i++)
        {
            if (i < vhsItems.Count)
                _vhsSlots[i].AddSlot(vhsItems[i]);
            
            else
                _vhsSlots[i].ClearSlot();
        }
    }

    public void UpdateJournalIcons(List<Interactive> journalItems)
    {
        for (int i = 0; i < _journalSlots.Length; i++)
        {
            if (i < journalItems.Count)
                _journalSlots[i].AddSlot(journalItems[i]);
            
            else
                _journalSlots[i].ClearSlot();
        }
    }
}
