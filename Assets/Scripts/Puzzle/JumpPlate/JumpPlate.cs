using System.Collections;
using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class JumpPlate : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDelay;
    [SerializeField] private Vector3 animationScale;
    [SerializeField] private float animationDuration = 0.3f;
    private bool _isPlayerOnPlate;
    private bool _isWaitingForJump;
    private bool _canPushPlayer = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_isWaitingForJump) return;
        if (!_canPushPlayer) return;
        _canPushPlayer = false;
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
            _canPushPlayer = true;
            yield break;
        }
        ThirdPersonController playerController = player.GetComponent<ThirdPersonController>();
        playerController.AddJumpForce(jumpForce);
        transform.DOScale(animationScale, animationDuration).SetLoops(4, LoopType.Yoyo).OnComplete(() => _canPushPlayer = true);
        _isWaitingForJump = false;
    }
}
