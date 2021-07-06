﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]  private GameObject _mainMenu;
    [SerializeField]  private GameObject _settings;
    [SerializeField]  private GameObject _controls;
    [SerializeField]  private GameObject _credits;
    [SerializeField]  private AudioSource _audioSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ToMenu();
    }

    public void Play()
    {
        _audioSource.Play(0);
        SceneManager.LoadScene(1);
    }

    public void ToSettings()
    {
        _mainMenu.SetActive(false);
        _settings.SetActive(true);
    }

    public void ToControls()
    {
        _mainMenu.SetActive(false);
        _controls.SetActive(true);
    }

    public void ToCredits()
    {
        _mainMenu.SetActive(false);
        _credits.SetActive(true);
    }

    public void ToMenu()
    {
        _settings.SetActive(false);
        _controls.SetActive(false);
        _credits.SetActive(false);

        _mainMenu.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        _audioSource.Play(0);
        Application.Quit();
    }
}
