using UnityEngine;
using System.Collections.Generic;

public class Directory : MonoBehaviour
{
    private UserInterface _ui;
    
    [SerializeField] private List<Interactive> _inventory;
    [SerializeField] private List<Interactive> _vhsTapes;
    [SerializeField] private List<Interactive> _journal;

    #region Singleton
    public static Directory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one inventory.");
            return;
        }

        instance = this;
    }
    #endregion

    private void Start()
    {
        _ui         = UserInterface.instance;
        _inventory  = new List<Interactive>();
        _vhsTapes   = new List<Interactive>();
        _journal    = new List<Interactive>();
    }

    public void Add(Interactive item)
    {
        if(item.pickableType != PickableType.NULL)
        {
            _ui.ShowImage(item.icon);

            if (item.pickableType == PickableType.INVENTORY)
            {
                _inventory.Add(item);
                _ui.UpdateInventoryIcons(_inventory);
            }

            else if (item.pickableType == PickableType.VHS)
            {
                _vhsTapes.Add(item);
                _ui.UpdateVhsIcons(_vhsTapes);
            }

            else if (item.pickableType == PickableType.MEMORY)
            {
                _journal.Add(item);
                _ui.UpdateJournalIcons(_journal);
            }
        }

        else
            Debug.LogWarning("Pickable object is missing a type.");
        
    }

    public void Remove(Interactive item)
    {
        if (item.pickableType == PickableType.INVENTORY)
        {
            _inventory.Remove(item);
            _ui.UpdateInventoryIcons(_inventory);
            
        }

        else if (item.pickableType == PickableType.VHS)
        {
            _vhsTapes.Remove(item);
            _ui.UpdateVhsIcons(_vhsTapes);
        }

        else if (item.pickableType == PickableType.MEMORY)
        {
            _journal.Remove(item);
            _ui.UpdateJournalIcons(_journal);
        }
    }

    public bool Contains(Interactive item)
    {
        return _inventory.Contains(item);
    }
}
