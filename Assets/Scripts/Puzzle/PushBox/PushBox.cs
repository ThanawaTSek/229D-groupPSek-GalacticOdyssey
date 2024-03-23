using System.Linq;
using UnityEngine;
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
    }

    private void Update()
    {
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
            float torque = torqueForce * _inputValue;
            _rigidbody.AddTorque(Vector3.up * torque, ForceMode.Impulse);
            ResetInput();
        }
    }
    
    private void ResetInput()
    {
        scrollbar.gameObject.SetActive(false);
        _inputValue = 0;
        _isInputToggleDown = false;
    }
    private void Push(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
    
    private Color GetBoxColor(PushBoxColor color)
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
