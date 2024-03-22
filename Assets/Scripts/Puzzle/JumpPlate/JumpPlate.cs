using System.Collections;
using StarterAssets;
using UnityEngine;

public class JumpPlate : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDelay;
    private bool _isPlayerOnPlate;
    private bool _isWaitingForJump;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_isWaitingForJump) return;
        StartCoroutine(AddJumpForce(other.gameObject));
        _isPlayerOnPlate = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _isPlayerOnPlate = false;
    }
    
    private IEnumerator AddJumpForce(GameObject player)
    {
        _isWaitingForJump = true;
        yield return new WaitForSeconds(jumpDelay);
        if (!_isPlayerOnPlate) yield break;
        ThirdPersonController playerController = player.GetComponent<ThirdPersonController>();
        playerController.AddJumpForce(jumpForce);
        _isWaitingForJump = false;
    }
}
