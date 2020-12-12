using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private Text       interactionText;

    void Start()
    {
        HideInventory();
        HideInteractionMsg();
    }
    
    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
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
}
