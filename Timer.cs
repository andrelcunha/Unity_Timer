using UnityEngine;
using UnityEngine.Events;

namespace _2MuchPines.Unity_Timer
{
    public class Timer : MonoBehaviour
    {

        [SerializeField] bool _isRegressive;
        [SerializeField] float _totalTime;

        public float TotalTime
        {
            get
            {
                return _totalTime;
            }
        }

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

        #region Properties
        public float TotalTime
        {
            get
            {
                return _totalTime;
            }
            set
            {
                _totalTime = value;
            }
        }
        public UnityEvent OnTimeOver
        {
            get
            {
                return _onTimeOver;
            }

            set
            {
                _onTimeOver = value;
            }
        }

        #endregion

        #region MonoBehavior_Methods

        void Awake()
        {
            ResetTimer();
        }

        void Start()
        {
            if (_startOnAwake)
                StartRunning();
        }

        void Update()
        {
            UpdateTime();
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
            OnTimeOver.Invoke();
        }

        void UpdateTime()
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
}
