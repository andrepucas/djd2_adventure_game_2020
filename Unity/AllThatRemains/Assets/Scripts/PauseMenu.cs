using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    
    private UserInterface       _ui;
    private bool                _isPaused;

    private void Start()
    {
        _ui             = UserInterface.instance;
        _isPaused       = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
            if (_isPaused)
                Resume();
            else
                Pause ();
    }

    private void Resume ()
    {
        _ui.HideCursor();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    private void Pause ()
    {
        _ui.ShowCursor("free");

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    private void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void QuitGame ()
    {
        Application.Quit();
    }

}
