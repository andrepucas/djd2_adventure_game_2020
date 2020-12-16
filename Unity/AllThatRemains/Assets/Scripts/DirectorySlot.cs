using UnityEngine;
using UnityEngine.UI;

public class DirectorySlot : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    public void AddSlot(Interactive item)
    {
        _button.image.sprite     = item.icon;
        _button.image.enabled    = true;
        _button.enabled          = true;

    }

    public void ClearSlot()
    {
        _button.image.sprite     = null;
        _button.image.enabled    = false;
        _button.enabled          = false;
    }
}
