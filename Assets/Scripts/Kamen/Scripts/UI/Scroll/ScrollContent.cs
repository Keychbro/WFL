using UnityEngine;
using System.Collections.Generic;

namespace Kamen.UI
{
    public class ScrollContent : MonoBehaviour
    {
        #region Enums

        public enum ScrollType
        {
            Horizontal,
            Vertical
        }
        public enum SpawnDirection
        {
            Forward,
            Backward
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private Canvas _currentCanvas;
        [SerializeField] private RectTransform _rectContent;
        [SerializeField] private RectTransform[] _rectChildren;

        [Header("Settings")]
        [SerializeField] private float _itemSpacing;
        [SerializeField] private ScrollType _type;
        [SerializeField] private SpawnDirection _spawnDirection;
        [SerializeField] private float _horizontalMargin;
        [SerializeField] private float _verticalMargin;
        [SerializeField] private bool _isUseMiddleForStartPosition;

        [Header("Control")]
        [SerializeField] private bool _isManualConfiguration;

        #endregion

        #region Properties

        public RectTransform RectContent { get => _rectContent; }
        public ScrollType Type { get => _type; }
        public SpawnDirection Direction { get => _spawnDirection; }
        public float ItemSpacing { get; private set; }

        public float Width { get; private set; }
        public float Height { get; private set; }
        public float ChildWidth { get; private set; }
        public float ChildHeight { get; private set; }

        public float LocalItemSpacing { get => _itemSpacing; }
        public float LocalWidth { get; private set; }
        public float LocalHeight { get; private set; }
        public float LocalChildWidth { get; private set; }
        public float LocalChildHeight { get; private set; }

        #endregion

        #region Initialize Methods

        public void Initialize()
        {
            SetLocalAttributes();
            SetGlobalAttributes();

            if (!_isManualConfiguration) SetChildrenPositions();
        }
        public void ManualInitialize()
        {
            SetLocalAttributes();
            SetGlobalAttributes();

            SetChildrenPositions();
        }

        #endregion

        #region SetUp Chidren Methods

        private void SetChildrenPositions()
        {
            switch (_type)
            {
                case ScrollType.Horizontal:
                    SetUpForHorizontal();
                    break;
                case ScrollType.Vertical:
                    SetUpForVertical();
                    break;
            }
        }
        private void SetUpForHorizontal()
        {
            float originX = _isUseMiddleForStartPosition ? 0 : 0 - (LocalWidth / 2f);
            float offsetPosition = _isUseMiddleForStartPosition ? 0 : LocalChildWidth / 2f;
            for (int i = 0; i < _rectChildren.Length; i++)
            {
                Vector2 childPosition = Vector2.zero;
                int direction = CalculateDirection();
                childPosition.x = originX + direction * (offsetPosition + i * (LocalChildWidth + _itemSpacing));
                _rectChildren[i].localPosition = childPosition;
            }
        }
        private void SetUpForVertical()
        {
            float originY = _isUseMiddleForStartPosition ? 0 : 0 + (LocalHeight / 2f);
            float offsetPosition = _isUseMiddleForStartPosition ? 0 : LocalChildHeight / 2f;
            for (int i = 0; i < _rectChildren.Length; i++)
            {
                Vector2 childPosition = Vector2.zero;
                int direction = CalculateDirection();
                childPosition.y = originY + direction * (offsetPosition + i * (LocalChildHeight + _itemSpacing));
                _rectChildren[i].localPosition = childPosition;
            }
        }
        private int CalculateDirection()
        {
            return _spawnDirection switch
            {
                SpawnDirection.Forward => 1,
                SpawnDirection.Backward => -1,
                _ => 1
            };
        }

        #endregion

        #region Set Methods

        private void SetLocalAttributes()
        {
            LocalWidth = _rectContent.rect.width - (2 * _horizontalMargin);
            LocalHeight = _rectContent.rect.height - (2 * _verticalMargin);
            LocalChildWidth = _rectChildren[0].rect.width;
            LocalChildHeight = _rectChildren[0].rect.height;
        }
        private void SetGlobalAttributes()
        {
            ItemSpacing = _itemSpacing * _currentCanvas.transform.localScale.x;
            Width = LocalWidth * _currentCanvas.transform.localScale.x;
            Height = LocalHeight * _currentCanvas.transform.localScale.x;
            ChildWidth = LocalChildWidth * _currentCanvas.transform.localScale.x;
            ChildHeight = LocalChildHeight * _currentCanvas.transform.localScale.x;
        }

        #endregion
    }
}