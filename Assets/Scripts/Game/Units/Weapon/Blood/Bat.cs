using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public class Bat : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeed;

        [Header("Variables")]
        private float _currentSpeed;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        }

        #endregion

        #region Control Methods

        public void Move(Transform hitPoint)
        {
            Vector3 direction = hitPoint.position - transform.position;
            direction.Normalize();

            if (direction.x <= 0) transform.eulerAngles = Vector3.zero;
            else transform.eulerAngles = new Vector3(0, -180, 0);

            transform.position += direction * _currentSpeed * Time.fixedDeltaTime / 100f;

            float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, targetAngleZ), Time.deltaTime * _rotationSpeed);

            if (hitPoint == null) { Destroy(gameObject); }
        }

        #endregion
    }
}