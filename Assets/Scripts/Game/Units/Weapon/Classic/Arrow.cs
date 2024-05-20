using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public class Arrow : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        #endregion

        #region Control Methods

        public void Move(Transform hitPoint)
        {
            Vector3 direction = hitPoint.transform.position - transform.position;
            direction.Normalize();

            transform.position += direction * _speed * Time.fixedDeltaTime / 100f;

            float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, targetAngleZ), Time.deltaTime * _rotationSpeed);
        }

        #endregion
    }
}