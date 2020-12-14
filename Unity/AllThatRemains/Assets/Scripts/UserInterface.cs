using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject directoryPanel;
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private Text       interactionText;

    private void Start()
    {
        ToggleDirectory();
        HideInteractionMsg();
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
}
