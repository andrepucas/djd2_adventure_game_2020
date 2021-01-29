using UnityEngine;
using System.Collections.Generic;

public class PlayerDirectory : MonoBehaviour
{
    private UserInterface _ui;
    
    [SerializeField] private List<Interactive> _inventory;
    [SerializeField] private List<Interactive> _vhsTapes;

    #region Singleton
    public static PlayerDirectory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one directory.");
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
    }

    public void Add(Interactive item)
    {
        if(item.pickableType != PickableType.NULL)
        {
            _ui.ShowInspectMode(item.icon);

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
        }

        else
            Debug.LogWarning("Pickable object is missing a type.");
        
    }

    public void Remove(Interactive item)
    {
        _ui.HideInspectMode();

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
    }

    public bool HasItem(Interactive item)
    {
        return _inventory.Contains(item);
    }

    public bool HasVHS(Interactive item)
    {
        return _vhsTapes.Contains(item);
    }

    public bool VHSCount(int i)
    {
        if (_vhsTapes.Count == i)
            return true;

        else
            return false;
    }
}
