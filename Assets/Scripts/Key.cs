using System;
using DG.Tweening;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool _isCollected;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_isCollected) return;
        GameManager.Instance.CollectKey();
        _isCollected = true;
        
        transform.DOScale(Vector3.one * 1.5f, 0.15f).OnComplete(() => 
            transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => 
                Destroy(gameObject)));
    }
}
