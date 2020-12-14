using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject directoryPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject vhsPanel;
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private Text       interactionText;

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
        ToggleDirectory();
        HideInteractionMsg();

        _inventorySlots = inventoryPanel.GetComponentsInChildren<DirectorySlot>();
        _vhsSlots       = vhsPanel.GetComponentsInChildren<DirectorySlot>();
        _journalSlots   = journalPanel.GetComponentsInChildren<DirectorySlot>();
    }

    private void Update()
    {
        LookForDirectory();
    }

    private void LookForDirectory()
    {
        if (Input.GetButtonDown("Directory"))
            ToggleDirectory();
    }

    private void ToggleDirectory()
    {
        directoryPanel.SetActive(!directoryPanel.activeSelf);
    }

    public void ShowInteractionMsg(string message)
    {
        interactionText.text = message;
        interactionPanel.SetActive(true);
    }

    public void HideInteractionMsg()
    {
        interactionPanel.SetActive(false);
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
