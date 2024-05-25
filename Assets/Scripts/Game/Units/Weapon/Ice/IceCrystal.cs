using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;

namespace WOFL.Game
{
    public class IceCrystal : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        #endregion

        #region Unity Methods

        private void Start()
        {
            GameManager.Instance.OnBattleFinished += CallDestroy;
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnBattleFinished -= CallDestroy;
        }

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
        private void CallDestroy() => Destroy(gameObject);

        #endregion
    }
}