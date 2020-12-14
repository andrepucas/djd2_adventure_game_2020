using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    private PlayerMovement  _playerMovement;
    private MemoryTravel _playerMemory;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerMemory = _player.GetComponent<MemoryTravel>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause ();
            }
    }

    private void HideCursor()
    {
        Cursor.visible      = false;
        Cursor.lockState    = CursorLockMode.Locked;
    }

    private void ShowCursor()
    {
        Cursor.visible      = true;
        Cursor.lockState    = CursorLockMode.Confined;
    }

    private void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        _playerMovement.enabled = true;
        _playerMemory.enabled = true;
        HideCursor();
    }

    private void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        _playerMovement.enabled = false;
        _playerMemory.enabled = false;
        ShowCursor();
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
