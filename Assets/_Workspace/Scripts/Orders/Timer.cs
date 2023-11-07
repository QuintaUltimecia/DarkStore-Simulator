using TMPro;
using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : BaseBehaviour
{
    public int Value { get; private set; }
    private TextMeshProUGUI _text;

    private Coroutine _timerRoutine;
    private bool _isInitialized = false;

    public event Action OnEndTimer;

    public void Init(int value)
    {
        Value = value;
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = Value.ToString();

        _isInitialized = true;
    }

    public void StartTimer()
    {
        if (_isInitialized == false)
        {
            Debug.LogWarning($"{this} is not initialized!");
            return;
        }

        _timerRoutine = StartCoroutine(TimerRoutine());
    }

    public void StopTimer()
    {
        if (_isInitialized == false)
        {
            Debug.LogWarning($"{this} is not initialized!");
            return;
        }

        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
            _timerRoutine = null;
        }
    }

    private IEnumerator TimerRoutine()
    {
        int value = Value;

        while (value != 0)
        {
            value--;
            _text.text = value.ToString();
            yield return new WaitForSeconds(1);
        }

        OnEndTimer?.Invoke();

        _timerRoutine = null;
    }
}
