using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int keysCollected;
    public static GameManager Instance;
    
    private int _totalKeys;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        _totalKeys = GameObject.FindGameObjectsWithTag("Key").Length;
    }
    
    public void CollectKey()
    {
        keysCollected++;
        if (keysCollected == _totalKeys)
        {
            Debug.Log("All keys collected!"); // win the game
        }
    }
    
}
