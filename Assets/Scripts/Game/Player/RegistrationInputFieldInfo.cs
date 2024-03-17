using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WOFL.UI
{
    [Serializable] public class RegistrationInputFieldInfo
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private RequestResultView _requesResultView;

        #endregion

        #region Properties

        public TMP_InputField InputField { get => _inputField; }
        public RequestResultView RequesResultView { get => _requesResultView; }
 
        #endregion
    }
}