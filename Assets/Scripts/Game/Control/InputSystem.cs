using Kamen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Control
{
    public class InputSystem : SingletonComponent<InputSystem>
    {
        #region Variables

        [Header("Main Objects")]
        [SerializeField] private Camera _camera;

        [Header("Input Settings")]
        [SerializeField] private float _inputDistance;
        [SerializeField] private LayerMask _inputRayLayer;

        [Header("Input Variables")]
        private RaycastHit _inputHit;
        private bool _isDragging;
        private bool _isClicked;
        private bool _isMobilePlatform;
        public event Action<Vector3> OnMouseDown;
        public event Action<Vector3> OnMouseDrag;
        public event Action<Vector3> OnMouseUp;
        public event Action OnClicked;
        private Vector3 _startInputPosition;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }
        private void Update()
        {
            if (_isMobilePlatform) CheckMobileInput();
            else CheckNotMobileInput();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            _isMobilePlatform = false;
#else
        _isMobilePlatform = true;
#endif
        }
        private void CheckMobileInput()
        {
            if (Input.touchCount == 0) return;

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if (!_isDragging)
                {
                    _isDragging = true;
                    _isClicked = true;
                    _startInputPosition = TryGetMousePosition();
                }
                else
                {
                    if (_startInputPosition != TryGetMousePosition())
                    {
                        if (_isClicked)
                        {
                            _isClicked = false;
                            OnMouseDown?.Invoke(_startInputPosition);
                        }
                        OnMouseDrag?.Invoke(TryGetMousePosition());
                    }
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                if (_startInputPosition != TryGetMousePosition())
                {
                    if (_isClicked)
                    {
                        _isClicked = false;
                        OnMouseDown?.Invoke(_startInputPosition);
                    }
                    OnMouseDrag?.Invoke(TryGetMousePosition());
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
            {
                if (_isClicked && _isDragging)
                {
                    _isClicked = false;
                    OnClicked?.Invoke();
                }
                else if (_isDragging) OnMouseUp?.Invoke(TryGetMousePosition());
                _isDragging = false;
            }
        }
        private void CheckNotMobileInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_isDragging)
                {
                    _isDragging = true;
                    _isClicked = true;
                    _startInputPosition = TryGetMousePosition();
                }
            }
            else if (Input.GetMouseButton(0) && _isDragging)
            {
                if (_startInputPosition != TryGetMousePosition())
                {
                    if (_isClicked)
                    {
                        _isClicked = false;
                        OnMouseDown?.Invoke(_startInputPosition);
                    }
                    OnMouseDrag?.Invoke(TryGetMousePosition());
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (_isClicked && _isDragging)
                {
                    _isClicked = false;
                    OnClicked?.Invoke();
                }
                else if (_isDragging) OnMouseUp?.Invoke(TryGetMousePosition());
                _isDragging = false;
            }
        }
        public void ResetInput()
        {
            _isDragging = false;
            _isClicked = false;
        }

        #endregion

        #region Calculate Methods

        public Vector3 TryGetMousePosition()
        {
            Debug.DrawRay(_camera.transform.position, CalculateRayDirection() - _camera.transform.position, Color.red);
            if (Physics.Raycast(_camera.transform.position, CalculateRayDirection() - _camera.transform.position, out _inputHit, _inputDistance, _inputRayLayer))
            {
                return _inputHit.point;
            }
            else return Vector3.zero;
        }
        private Vector3 CalculateRayDirection()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = _camera.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 rayDirection = _camera.ScreenToWorldPoint(mousePosition);
            return rayDirection;
        }

        #endregion
    }
}