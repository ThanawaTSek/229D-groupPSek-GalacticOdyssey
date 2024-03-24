using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private GameObject howToPlay;
    private bool _isFirstTime = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        ShowHowToPlay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            howToPlay.SetActive(!howToPlay.activeSelf);
            if (!_isFirstTime) return;
            _isFirstTime = false;
            Time.timeScale = 1f;
        }
    }

    void ShowHowToPlay()
    {
        howToPlay.SetActive(true);
    }

    void CloseHowToPlay()
    {
        Time.timeScale = 1f;
        howToPlay.SetActive(false);
    }
}
