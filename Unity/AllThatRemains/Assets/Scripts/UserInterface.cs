using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject directoryPanel;
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private Text       interactionText;

    private void Start()
    {
        HideInventory();
        HideInteractionMsg();
    }
    
    public void ShowInventory()
    {
        directoryPanel.SetActive(true);
    }

    public void HideInventory()
    {
        directoryPanel.SetActive(false);
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
