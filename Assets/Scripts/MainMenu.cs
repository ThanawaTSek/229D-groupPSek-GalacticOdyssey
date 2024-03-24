using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditButton;
    /*[SerializeField] private Button exitButton; */
    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        creditButton.onClick.AddListener(ShowCredits);
        /*exitButton.onClick.AddListener(ExitGame);*/
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Map");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("EndCredit");
    }

    /*public void ExitGame()
    {
        Application.Quit();
    }*/
    
}
