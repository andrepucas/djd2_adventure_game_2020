using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    
    private UserInterface       _ui;
    private PlayerMovement      _movement;
    private PlayerMemoryTravel  _memoryTravel;
    private bool                _isPaused;

    private void Start()
    {
        _ui             = UserInterface.instance;
        _movement       = PlayerMovement.instance;
        _memoryTravel   = PlayerMemoryTravel.instance;
        _isPaused       = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause ();
            }
    }

    

    private void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        _movement.enabled = true;
        _memoryTravel.enabled = true;
        _ui.HideCursor();
    }

    private void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        _movement.enabled = false;
        _memoryTravel.enabled = false;
        _ui.ShowCursor();
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
