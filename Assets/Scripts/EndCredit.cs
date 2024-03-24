using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredit : MonoBehaviour
{
    public TextMeshProUGUI creditText;
    [SerializeField] public string[] creditLiine;
    private float speed = 60f;

    private float scrollPositiion;

    private void Start()
    {
        scrollPositiion = 0;
        DisplayCredits();
    }

    private void Update()
    {
        ScrollCrediits();
        BackToMenu();
    }

    void DisplayCredits()
    {
        foreach (string line in creditLiine)
        {
            creditText.text += line + "\n";
        }
    }

    void ScrollCrediits()
    {
        Vector3 currentPosition = creditText.rectTransform.position;
        currentPosition.y += speed * Time.deltaTime;
        creditText.rectTransform.position = currentPosition;
    }

    void BackToMenu()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("Menu 3D");
        }
    }
}
