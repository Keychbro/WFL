using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Online;
using System.Threading.Tasks;
using TMPro;
using WOFL.Control;
using System;

namespace WOFL.UI
{
    public class OtherPlayerMessage : Message
    {
        #region Variables

        [Header("Addition Message Objects")]
        [SerializeField] private Image _playerIcon;
        [SerializeField] private TextMeshProUGUI _playerName;

        #endregion

        #region Control Methods

        public override async void AdjustMessage(GetMessageInfo getMessageInfo)
        {
            _canvasGroup.alpha = 0f;

            await Task.Yield();

            int iconNumber = Convert.ToInt32(getMessageInfo.player_icon);

            _playerIcon.sprite = FractionManager.Instance.CurrentFraction.PlayerProfileSettings.IconsList[iconNumber > FractionManager.Instance.CurrentFraction.PlayerProfileSettings.IconsList.Length ? 0 : iconNumber];
            _playerName.text = getMessageInfo.player_name;
            _playerName.color = FractionManager.Instance.CurrentFraction.MainColor;

            _messageText.text = getMessageInfo.text;
            _sendingTimeText.text = $"{getMessageInfo.created_at.Hour}:{getMessageInfo.created_at.Minute}";

            _playerName.ForceMeshUpdate();
            _messageText.ForceMeshUpdate();

            await Task.Yield();
            await Task.Yield();

            if (_messageText.textBounds.size.x >= _maxWidth || _playerName.textBounds.size.x >= _maxWidth)
            {
                _layoutElement.preferredWidth = _preferredWidth;
            }

            _canvasGroup.alpha = 1f;
        }
        public override async void AdjustSupportMessage(GetSupportMessageInfo getSupportMessageInfo)
        {
            _canvasGroup.alpha = 0f;

            await Task.Yield();

            _playerName.text = "Support";
            _playerName.color = Color.white;

            _messageText.text = getSupportMessageInfo.text;
            _sendingTimeText.text = $"{getSupportMessageInfo.created_at.Hour}:{getSupportMessageInfo.created_at.Minute}";

            _playerName.ForceMeshUpdate();
            _messageText.ForceMeshUpdate();

            await Task.Yield();
            await Task.Yield();

            if (_messageText.textBounds.size.x >= _maxWidth || _playerName.textBounds.size.x >= _maxWidth)
            {
                _layoutElement.preferredWidth = _preferredWidth;
            }

            _canvasGroup.alpha = 1f;
        }
        public void SetSupportMessageIcon(Sprite icon)
        {
            _playerIcon.sprite = icon;
        }

        #endregion
    }
}

