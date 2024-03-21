using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateDoor : MonoBehaviour
{
    [SerializeField] private float doorOpenHigh;
    [SerializeField] private float doorOpenDuration;
    [SerializeField] private float doorCloseDuration;
    private Vector3 _originalPos;
    private Vector3 _onClosePos;
    private bool _isDoorOpening;
    private bool _isDoorClosing;
    

    private void Start()
    {
        _originalPos = transform.position;
    }

    public void OpenDoor()
    {
        if (_isDoorOpening) return;
        StartCoroutine(MoveDoorUp());
    }

    public void CloseDoor()
    {
        _originalPos = transform.position;
        
    }

    private IEnumerator MoveDoorUp()
    {
        float timeCount = 0;
        Vector3 doorOpenPos = _originalPos + new Vector3(0, doorOpenHigh, 0);

        while (timeCount < doorOpenDuration)
        {
            _isDoorOpening = true;
            transform.position = Vector3.Lerp(_originalPos, doorOpenPos, timeCount / doorOpenDuration);
            timeCount += Time.deltaTime;
            yield return null;
        }
        
        _isDoorOpening = false;
    }

    private IEnumerator MoveDoorDown()
    {
        float timeCount = 0;

        while (timeCount < doorCloseDuration)
        {
            _isDoorClosing = true;
            transform.position = Vector3.Lerp(_onClosePos, _originalPos, timeCount / doorCloseDuration);
            timeCount += Time.deltaTime;
            yield return null;
        }

        _isDoorClosing = true;
    }
}
