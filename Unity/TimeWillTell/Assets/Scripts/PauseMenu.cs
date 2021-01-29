using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private UserInterface       _ui;
    private PlayerInteraction   _player;
    public  bool                _isPaused;

    private void Start()
    {
        _ui             = UserInterface.instance;
        _player         = GameObject.Find("Player").GetComponent<PlayerInteraction>();
        _isPaused       = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && _player.inCombination() == false)
            if (_isPaused)
                Continue();
            else
                Pause();
    }

    private void Pause ()
    {
        _ui.ShowCursor("free");
        _ui.ShowPauseMenu();

        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void Continue ()
    {
        _ui.HideCursor();
        _ui.HidePauseMenu();
        _ui.HideSettingsMenu();

        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void ToSettings()
    {
        _ui.HidePauseMenu();
        _ui.ShowSettingsMenu();
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ToDesktop()
    {
        Application.Quit();
    }
}
