using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using WOFL.Online;

namespace WOFL.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Message : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected LayoutElement _layoutElement;
        [SerializeField] protected TextMeshProUGUI _messageText;
        [SerializeField] protected TextMeshProUGUI _sendingTimeText;

        [Header("Settings")]
        [SerializeField] protected int _maxWidth;
        [SerializeField] protected int _preferredWidth;

        #endregion

        #region Control Methods

        public virtual async void AdjustMessage(GetMessageInfo getMessageInfo)
        {
            _canvasGroup.alpha = 0f;

            await Task.Yield();

            _messageText.text = getMessageInfo.text;
            string additionalZero = getMessageInfo.created_at.Minute > 9 ? "" : "0";
            _sendingTimeText.text = $"{getMessageInfo.created_at.Hour}:{additionalZero + getMessageInfo.created_at.Minute}";
            _messageText.ForceMeshUpdate();

            await Task.Yield();
            if (_messageText.textBounds.size.x >= _maxWidth)
            {
                _layoutElement.preferredWidth = _preferredWidth;
            }

            _canvasGroup.alpha = 1f;
        }
        public virtual async void AdjustSupportMessage(GetSupportMessageInfo getSupportMessageInfo)
        {
            _canvasGroup.alpha = 0f;

            await Task.Yield();

            _messageText.text = getSupportMessageInfo.text;
            string additionalZero = getSupportMessageInfo.created_at.Minute > 9 ? "" : "0";
            _sendingTimeText.text = $"{getSupportMessageInfo.created_at.Hour}:{additionalZero + getSupportMessageInfo.created_at.Minute}";
            _messageText.ForceMeshUpdate();

            await Task.Yield();
            if (_messageText.textBounds.size.x >= _maxWidth)
            {
                _layoutElement.preferredWidth = _preferredWidth;
            }

            _canvasGroup.alpha = 1f;
        }

        #endregion
    }
}