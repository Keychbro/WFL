using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WOFL.UI
{
    public class LoadingIcon : AnimateIcon
    {
        #region Variables

        [Header("Rotate Settings")]
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _rotationSpeed;

        #endregion

        #region Control Methods

        public override void CallAppear()
        {
            if (_iconView != null) _iconView.transform.rotation = Quaternion.identity;
            base.CallAppear();
        }
        private void Update()
        {
            if (gameObject.activeSelf) _iconView.transform.Rotate(_direction, _rotationSpeed * Time.deltaTime);
        }

        #endregion
    }
}