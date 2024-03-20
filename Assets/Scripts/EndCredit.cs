using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCredit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI creditText;
    private string[] creditLine;
    [SerializeField] private float speed;
    [SerializeField] private float delay;

    private float timer;
    private int currentLineIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
        currentLineIndex = 0;
        
        // Start Credit
        CreditLine();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            CreditLine();
            timer = delay;
        }
    }

    private void CreditLine()
    {
        if (currentLineIndex < creditLine.Length)
        {
            creditText.text += creditLine[currentLineIndex] + "\n";
            currentLineIndex++;
        }
        else
        {
            // End Credit
        }
    }
}
