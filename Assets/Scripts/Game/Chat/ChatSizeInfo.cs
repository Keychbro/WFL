using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Chat
{
    [Serializable] public struct ChatSizeInfo
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _changeSizeDuration;
        [SerializeField] private Ease _changeSizeEase;

        private Vector2 _chatViewSize;
        private Vector3 _inputFieldPosition;


        #endregion

        #region Properties

        public float ChangeSizeDuration { get => _changeSizeDuration; }
        public Ease ChangeSizeEase { get => _changeSizeEase; }

        public Vector2 ChatViewSize
        {
            get => _chatViewSize;
            set
            {
                if (_chatViewSize != Vector2.zero) return;
                _chatViewSize = value;
            }
        }

        public Vector3 InputFieldPosition
        {
            get => _inputFieldPosition;
            set
            {
                if (_inputFieldPosition != Vector3.zero) return;
                _inputFieldPosition = value;
            }
        }

        #endregion
    }
}