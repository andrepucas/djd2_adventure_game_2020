using UnityEngine;
using UnityEngine.UI;

public class DirectorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    
    public void AddSlot(Interactive item)
    {
        icon.sprite     = item.icon;
        icon.enabled    = true;
    }

    public void ClearSlot()
    {
        icon.sprite     = null;
        icon.enabled    = false;
    }
}
