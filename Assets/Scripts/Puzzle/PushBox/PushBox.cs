using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum PushBoxColor
{
    Red,
    Blue,
    Green,
    Yellow
}

public class PushBox : MonoBehaviour
{
    [SerializeField] private PushBoxColor boxColor;
    [SerializeField] private float interactionDistance;
    [SerializeField] private float pushForce = 5f;
    [SerializeField] private float torqueForce = 5f;
    [SerializeField] Scrollbar scrollbar;

    private float _inputValue;
    private Rigidbody _rigidbody;
    private bool _isInputValueDown;
    private bool _isInputToggleDown;
    private float _lastTimePressed;
    private Vector3 _startPosition;
    [FormerlySerializedAs("_isRotating")] public bool IsRotating;

    public PushBoxColor BoxColor => boxColor;

    public Vector3 CameraForward
    {
        get 
        {
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0;
            return forward.normalized;
        }
    }

    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = GetBoxColor(boxColor);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < GameManager.Instance.deadZone)
        {
            transform.position = _startPosition;
        }
        
        Collider[] objInArea = Physics.OverlapSphere(transform.position, interactionDistance, LayerMask.GetMask("Player"));
        if (objInArea.Length <= 0)
        {
            ResetInput();
            return;
        }

        if (objInArea.All(obj => !obj.CompareTag("Player")))
        {
            ResetInput();
            return;
        }
        
        Transform playerTransform = objInArea.FirstOrDefault(obj => obj.CompareTag("Player")).transform;
        Vector3 directionToPlayer = (transform.position - playerTransform.position).normalized;
        directionToPlayer.y = 0;
        float angle = Vector3.Angle(playerTransform.forward.normalized, directionToPlayer);

        if (angle > 45)
        {
            ResetInput();
            return;
        }
        
        if (scrollbar.gameObject.activeSelf)
        {
            if (_inputValue >= 1)
                _isInputValueDown = true;
            else if (_inputValue <= 0)
                _isInputValueDown = false;
            
            _inputValue += _isInputValueDown ? -Time.deltaTime : Time.deltaTime;
            scrollbar.size = _inputValue;
        }
        else
        {
            _inputValue = 0;
        }
        
        if (_lastTimePressed + 0.15f > Time.time) return;
        
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.T))
        {
            scrollbar.gameObject.SetActive(true);
            scrollbar.transform.forward = CameraForward;
            _isInputToggleDown = true;
        }
        
        if (Input.GetKeyUp(KeyCode.F) && _isInputToggleDown)
        {
            Vector3 direction = CameraForward;
            Push(direction, pushForce * _inputValue);
            ResetInput();
        }
        else if (Input.GetKeyUp(KeyCode.T) && _isInputToggleDown)
        {
            int angleValue;
            switch (_inputValue)
            {
                case >= 0.75f:
                    angleValue = 60;
                    break;
                case >= 0.5f:
                    angleValue = 45;
                    break;
                case >= 0.25f:
                    angleValue = 30;
                    break;
                default:
                    angleValue = 15;
                    break;
            }
            
            IsRotating = true;
            transform.DORotate(transform.rotation.eulerAngles + (Vector3.up * angleValue), 0.15f).OnComplete(() => IsRotating = false);
            ResetInput();
        }
    }
    
    private void ResetInput()
    {
        scrollbar.gameObject.SetActive(false);
        _inputValue = 0;
        _isInputToggleDown = false;
        _lastTimePressed = Time.time;
    }
    private void Push(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
    
    public static Color GetBoxColor(PushBoxColor color)
    {
        switch (color)
        {
            case PushBoxColor.Red:
                return Color.red;
            case PushBoxColor.Blue:
                return Color.blue;
            case PushBoxColor.Green:
                return Color.green;
            case PushBoxColor.Yellow:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
    
    private void OnDrawGizmos()
    {
        // Set the color for the Gizmos
        Gizmos.color = Color.red;

        // Draw a sphere cast from the transform position
        Gizmos.DrawWireSphere(transform.position, interactionDistance);

        // Draw a line to show the direction of the sphere cast
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * interactionDistance);
    }
}
