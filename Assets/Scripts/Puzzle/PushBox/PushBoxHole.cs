using DG.Tweening;
using UnityEngine;

public class PushBoxHole : MonoBehaviour
{
    [SerializeField] private PushBoxColor boxColor;
    [SerializeField] [Range(0,90)] private float angle;
    [SerializeField] private float distanceThreshold = 0.5f;
    private bool _isCorrectBoxVfxPlayed;
    private bool _isBoxInsideVfxPlayed;
    public bool IsBoxInside { get; private set; }
    public bool IsCorrectBox
    {
        get
        {
            if (!_pushBoxInside) return false;
            
            float boxInsideAngle = _pushBoxInside.transform.rotation.eulerAngles.y;
            switch (Mathf.Abs(boxInsideAngle))
            {
                case >= 360:
                    boxInsideAngle -= 360;
                    break;
                case >= 270:
                    boxInsideAngle -= 270;
                    break;
                case >= 180:
                    boxInsideAngle -= 180;
                    break;
                case >= 90:
                    boxInsideAngle -= 90;
                    break;
            }

            if (boxInsideAngle + 5 >= 90)
                boxInsideAngle -= 90;
            
     
            bool isAngleCorrect = Mathf.Abs(Mathf.DeltaAngle(boxInsideAngle, angle)) <= 5f;
            return _pushBoxInside.BoxColor == boxColor && isAngleCorrect;
        }
    }
    Tween _scaleTween;
    
    private PushBox _pushBoxInside;
    
    private void Awake()
    {
        Color color = PushBox.GetBoxColor(boxColor);
        GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.3f);
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        Color color = PushBox.GetBoxColor(boxColor);
   
        if (IsCorrectBox)
        {
            if (_pushBoxInside.IsRotating) return;
            Debug.Log("Correct Box");
            if (_isCorrectBoxVfxPlayed) return;
            if (_scaleTween.IsActive()) return;
            _scaleTween = transform.DOScale(new Vector3(1,0.25f,1) * 1.5f, 0.1f).SetLoops(2, LoopType.Yoyo); ;
            GetComponent<MeshRenderer>().material.DOColor(new Color(color.r, color.g, color.b, 0.75f), 0.15f);
            _isCorrectBoxVfxPlayed = true;
            _isBoxInsideVfxPlayed = false;
            return;
        }
        _isCorrectBoxVfxPlayed = false;

        if (IsBoxInside)
        {
            if (_isBoxInsideVfxPlayed) return;
            if (_scaleTween.IsActive()) return;
            _scaleTween = transform.DOScale(new Vector3(1,0.25f,1) * 1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
            GetComponent<MeshRenderer>().material.DOColor(new Color(color.r, color.g, color.b, 0.5f), 0.15f);
            _isBoxInsideVfxPlayed = true;
            return;
        }
        if (!other.CompareTag("PushBox")) return;
        if (Vector3.Distance(transform.position, other.transform.position) > distanceThreshold) return; IsBoxInside = true;
        _pushBoxInside = other.GetComponent<PushBox>();
        if (_scaleTween.IsActive()) return;
        _scaleTween = transform.DOScale(new Vector3(1,0.25f,1) * 1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
        GetComponent<MeshRenderer>().material.DOColor(new Color(color.r, color.g, color.b, 0.5f), 0.15f);
        Vector3 endPos = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Rigidbody>().DOMove(endPos, 0.15f);
        _isBoxInsideVfxPlayed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("PushBox")) return;
        _scaleTween = transform.DOScale(new Vector3(1,0.25f,1) * 1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
        Color color = PushBox.GetBoxColor(boxColor);
        GetComponent<MeshRenderer>().material.DOColor(new Color(color.r, color.g, color.b, 0.3f), 0.15f);
        IsBoxInside = false;
        _pushBoxInside = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(distanceThreshold, 0.2f, distanceThreshold));
    }
}
