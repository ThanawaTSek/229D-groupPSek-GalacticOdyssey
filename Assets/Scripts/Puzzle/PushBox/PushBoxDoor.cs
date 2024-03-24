using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PushBoxDoor : MonoBehaviour
{
    [SerializeField] private List<PushBoxHole> holes;
    private bool _isOpen;
    private void Update()
    {
        if (_isOpen) return;
        if (holes.All(hole => hole.IsCorrectBox))
        {
            transform.DOShakePosition(2f, Vector3.one).SetLoops(2, LoopType.Yoyo);
            transform.DOShakeScale(2f, Vector3.one).SetLoops(2, LoopType.Yoyo).OnComplete((() =>
            {
                transform.DOScale(Vector3.zero, 0.2f);
                Destroy(gameObject, 0.2f);
            }));
            _isOpen = true;
            transform.DOShakePosition(1f, Vector3.one);
        }
    }
}
