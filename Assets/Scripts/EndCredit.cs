using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCredit : MonoBehaviour
{
    public TextMeshProUGUI creditText;
    [SerializeField] public string[] creditLiine;
    private float speed = 50f;

    private float scrollPositiion;

    private void Start()
    {
        scrollPositiion = 0;
        DisplayCredits();
    }

    private void Update()
    {
        ScrollCrediits();
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
}
