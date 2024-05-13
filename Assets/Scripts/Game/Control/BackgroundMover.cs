using Kamen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WOFL.Control
{
    public class BackgroundMover : SingletonComponent<BackgroundMover>
    {
        #region Variables
        
        [Header("Settings")]
        [SerializeField] private float _leftBoarder;
        [SerializeField] private float _rightBoarder;
        [SerializeField] private float _speed;

        [Header("Variables")]
        private Vector3 _previousPoint;
        private Vector3 _currentPoint;
        private float _offset;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }
        private void Update()
        {
            
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            InputSystem.Instance.OnMouseDown += SetStartPoint;
            InputSystem.Instance.OnMouseDrag += MoveScreen;
        }
        private void SetStartPoint(Vector3 point)
        {
            _previousPoint = point;
        }
        private void MoveScreen(Vector3 point)
        {
            _offset = Mathf.Clamp((float)Math.Round(point.x, 3) - (float)Math.Round(_previousPoint.x, 3), -1, 1);
            _currentPoint = point;
            _previousPoint = _currentPoint;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + _offset * _speed, _leftBoarder, _rightBoarder), transform.position.y, transform.position.z);
        }

        #endregion
    }
}