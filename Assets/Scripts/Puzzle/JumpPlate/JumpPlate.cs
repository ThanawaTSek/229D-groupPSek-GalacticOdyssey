using System.Collections;
using DG.Tweening;
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
        if (!_isPlayerOnPlate)
        {
            _isWaitingForJump = false;
            yield break;
        }
        ThirdPersonController playerController = player.GetComponent<ThirdPersonController>();
        playerController.AddJumpForce(jumpForce);
        transform.DOScale((Vector3.up * 2f) + transform.localScale, 0.3f).SetLoops(2, LoopType.Yoyo);
        _isWaitingForJump = false;
    }
}
