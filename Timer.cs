using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Timer : MonoBehaviour {

    [SerializeField] bool _isRegressive;
    [SerializeField] float _totalTime;
    [SerializeField] float _timeLeft;

    public float TimeLeft
    {
        get
        {
            return _timeLeft;
        }
    }

    [SerializeField] bool _timeOver;
    [SerializeField] bool _startOnAwake;
    [SerializeField] UnityEvent _onTimeOver;
    bool _isRunning;

    #region MonoBehavior Methods

    void Awake()
    {
        ResetTimer();
    }

    // Use this for initialization
    void Start () {
        if (_startOnAwake)
            StartRunning();
    }
	
	// Update is called once per frame
	void Update () {
        updateTime();
    }

    #endregion

    #region Public Members
    public void StartRunning()
    {
        _isRunning = true;
    }

    public void StopRunning()
    {
        _isRunning = false;
    }

    public bool ToggleRunning()
    {
        _isRunning = !_isRunning;
        return _isRunning;
    }

    public void ResetTimer()
    {
        _timeLeft = _isRegressive ? _totalTime : 0;
    }
    #endregion

    void SetTimeOver()
    {
        _timeOver = true;
        StopRunning();
        _onTimeOver.Invoke();
    }

    void updateTime()
    {
        if (_isRunning)
        {
            if (_isRegressive)
            {
                _timeLeft -= Time.deltaTime;
                if (_timeLeft <= 0.1)
                {
                    SetTimeOver();
                }
            }
            else
            {
                _timeLeft += Time.deltaTime;
                if (_timeLeft > _totalTime)
                {
                    SetTimeOver();
                }
            }
        }
    }

}
