using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CheckPoint : MonoBehaviour
{
    [FormerlySerializedAs("_checkPointIndex")] [SerializeField] private int checkPointIndex;
    [SerializeField] private Color colorAfterChecked = new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f);
    Tween _colorChangeTween;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!GameManager.Instance.LastCheckPoint ||
            GameManager.Instance.LastCheckPoint.checkPointIndex < checkPointIndex)
        {
            GameManager.Instance.LastCheckPoint = this;
            if (_colorChangeTween.IsActive()) return;
            _colorChangeTween = GetComponent<MeshRenderer>().material.DOColor(colorAfterChecked, 0.5f);
            transform.DOScale(new  Vector3(1,0,1) * 1.5f, 0.15f).SetLoops(2, LoopType.Yoyo);
        }
    }
}
