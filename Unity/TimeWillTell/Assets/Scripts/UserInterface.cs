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
    [SerializeField] private Text       _interactionText;
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private Text       _helpText;
    [SerializeField] private GameObject _inspectMode;
    [SerializeField] private Image      _inspectImage;
    [SerializeField] private Image      _crosshair;

    private DirectorySlot[] _inventorySlots;
    private DirectorySlot[] _vhsSlots;
    private DirectorySlot[] _journalSlots;

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
        HideHelpMsg();
        HideInspectMode();

        _crosshair.enabled = true;

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
        _directoryPanel.SetActive(false);
        HideCursor();
    }
    private void ShowDirectory()
    {
        _directoryPanel.SetActive(true);
        ShowCursor("");
    }

    public void ShowInteractionMsg(string message)
    {
        _interactionText.text = message;
        ShowInteractionMsg();
    }

    public void ShowInteractionMsg()
    {
        _interactionPanel.SetActive(true);
    }

    public void HideInteractionMsg()
    {
        _interactionPanel.SetActive(false);
    }

    public void ShowHelpMsg(string message)
    {
        _helpText.text = message;
        ShowHelpMsg();
    }

    public void ShowHelpMsg()
    {
        _helpPanel.SetActive(true);
    }

    public void HideHelpMsg()
    {
        _helpPanel.SetActive(false);
    }

    public void ShowInspectMode(Sprite icon)
    {
        ShowHelpMsg("right click to exit inspect mode");
        
        _inspectImage.sprite = icon;

        _inspectMode.SetActive(true);
    }

    public void ShowInspectModeFromImage(Image image)
    {
        ShowInspectMode(image.sprite);
    }

    public void HideInspectMode()
    {
        HideHelpMsg();
        
        _inspectImage.sprite = null;

        _inspectMode.SetActive(false);
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
    
    public void HideCursor()
    {
        _crosshair.enabled  = true;
        Cursor.visible      = false;
        Cursor.lockState    = CursorLockMode.Locked;
    }

    public void ShowCursor(string type)
    {
        _crosshair.enabled  = false;
        Cursor.visible      = true;

        if (type == "free")
            Cursor.lockState = CursorLockMode.None;

        else
            Cursor.lockState = CursorLockMode.Confined;
    }
}
