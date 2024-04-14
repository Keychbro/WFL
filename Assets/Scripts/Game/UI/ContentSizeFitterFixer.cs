using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTranslator.UI
{
    [RequireComponent(typeof(RectTransform), typeof(ContentSizeFitter))]
    public class ContentSizeFitterFixer : MonoBehaviour
    {
        #region Variables

        [Header("Variables")]
        private ContentSizeFitter _fitter;
        private RectTransform _rectTransform;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _fitter = GetComponent<ContentSizeFitter>();
        }
        private void OnEnable()
        {
            StartCoroutine(WaitToUpdate());
        }
        public IEnumerator WaitToUpdate()
        {
            yield return null;

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        }
        public IEnumerator WaitToUpdate2()
        {
            yield return null;
            yield return null;

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        }

        #endregion
    }
}