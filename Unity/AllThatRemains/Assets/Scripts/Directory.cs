using UnityEngine;
using System.Collections.Generic;

public class Directory : MonoBehaviour
{
    [SerializeField] 
    private List<Interactive> _inventory = new List<Interactive>();

    [SerializeField] 
    private List<Interactive> _vhsTapes  = new List<Interactive>();

    [SerializeField] 
    private List<Interactive> _memories  = new List<Interactive>();

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

    public void Add(Interactive item)
    {
        if (item.pickableType == PickableType.INVENTORY)
            _inventory.Add(item);

        else if (item.pickableType == PickableType.VHS)
            _vhsTapes.Add(item);

        else if (item.pickableType == PickableType.MEMORY)
            _memories.Add(item);

        else
            Debug.LogWarning("Pickable object is missing a type.");
    }

    public void Remove(Interactive item)
    {
        if (item.pickableType == PickableType.INVENTORY)
            _inventory.Remove(item);

        else if (item.pickableType == PickableType.VHS)
            _vhsTapes.Remove(item);

        else if (item.pickableType == PickableType.MEMORY)
            _memories.Remove(item);
    }

    public bool Contains(Interactive item)
    {
        return _inventory.Contains(item);
    }


}
