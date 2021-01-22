using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    
    private UserInterface       _ui;
    private PlayerInteraction   _player;
    private bool                _isPaused;

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
                Resume();
            else
                Pause ();
    }

    private void Pause ()
    {
        _ui.ShowCursor("free");

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    void Resume ()
    {
        _ui.HideCursor();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void QuitGame ()
    {
        Application.Quit();
    }

}
