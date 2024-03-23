using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateDoor : MonoBehaviour
{
    [SerializeField] private float doorOpenHigh;
    [SerializeField] private float doorOpenDuration;
    [SerializeField] private float doorCloseDuration;
    [SerializeField] private float doorClosingDelay;
    private Vector3 _onOpenPos;
    private Vector3 _onClosePos;
    private Vector3 _originalPos;
    private bool _isDoorOpening;
    private bool _isDoorClosing;
    private Coroutine _doorCoroutine;

    private void Start()
    {
       _originalPos = transform.position;
    }

    public void OpenDoor()
    {
        if (_isDoorOpening) return;
        if (_isDoorClosing)
        {
            StopCoroutine(_doorCoroutine);
            _isDoorClosing = false;
        }
        _onOpenPos = transform.position;
        _doorCoroutine = StartCoroutine(MoveDoorUp());
    }

    public void CloseDoor()
    {
        if (_isDoorClosing) return;
        if (_isDoorOpening)
        {
            StopCoroutine(_doorCoroutine);
            _isDoorOpening = false;
        }
        _onClosePos = transform.position;
        _doorCoroutine = StartCoroutine(MoveDoorDown());
    }

    private IEnumerator MoveDoorUp()
    {
        float timeCount = 0;
        Vector3 doorOpenPos = _originalPos + new Vector3(0, doorOpenHigh, 0);

        while (timeCount < doorOpenDuration)
        {
            _isDoorOpening = true;
            transform.position = Vector3.Lerp(_onOpenPos, doorOpenPos, timeCount / doorOpenDuration);
            timeCount += Time.deltaTime;
            yield return null;
        }
        
        _isDoorOpening = false;
    }

    private IEnumerator MoveDoorDown()
    {
        yield return new WaitForSeconds(doorClosingDelay);
        float timeCount = 0;

        while (timeCount < doorCloseDuration)
        {
            _isDoorClosing = true;
            transform.position = Vector3.Lerp(_onClosePos, _originalPos, timeCount / doorCloseDuration);
            timeCount += Time.deltaTime;
            yield return null;
        }

        _isDoorClosing = false;
    }
}

