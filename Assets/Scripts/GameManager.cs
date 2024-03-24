using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int keysCollected;
    public float deadZone = -10;

    private CheckPoint _lastCheckPoint;
    private ThirdPersonController _player;
    private int _totalKeys;
    private Vector3 _playerStartPosition;

    public CheckPoint LastCheckPoint
    {
        get => _lastCheckPoint;
        set => _lastCheckPoint = value;
    }

    public static GameManager Instance;
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
        _player = FindObjectOfType<ThirdPersonController>();
        _playerStartPosition = _player.transform.position;
    }

    void LateUpdate()
    {
        if (_player.transform.position.y < deadZone)
        {
            if (!_lastCheckPoint)
            {
                _player.transform.position = _playerStartPosition;
                return;
            }
            _player.transform.position = _lastCheckPoint.transform.position;
        }
    }
    
    public void CollectKey()
    {
        keysCollected++;
        if (keysCollected == _totalKeys)
        {
            Debug.Log("All keys collected!"); // win the game
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(0, deadZone,0),new Vector3(100,0.5f,100) );
    }
}
