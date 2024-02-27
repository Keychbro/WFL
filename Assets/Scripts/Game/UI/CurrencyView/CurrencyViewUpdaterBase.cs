using Kamen.DataSave;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WOFL.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class CurrencyViewUpdaterBase : MonoBehaviour
    {
        #region Variables

        [Header("Variables")]
        protected TextMeshProUGUI _viewText;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            Initialize();
        }
        protected virtual void OnDestroy()
        {
            Unsubscribe();
        }

        #endregion

        #region Control Methods

        protected virtual void Initialize()
        {
            _viewText = GetComponent<TextMeshProUGUI>();
        }
        protected abstract void Unsubscribe();
        protected abstract void UpdateText();

        #endregion
    }
}
