using UnityEngine;
using UnityEngine.UI;

public class DirectorySlot : MonoBehaviour
{
    [SerializeField] private Button button;
    
    public void AddSlot(Interactive item)
    {
        button.image.sprite     = item.icon;
        button.image.enabled    = true;
        button.enabled          = true;
    }

    public void ClearSlot()
    {
        button.image.sprite     = null;
        button.image.enabled    = false;
        button.enabled          = false;
    }
}
