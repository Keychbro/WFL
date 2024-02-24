using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamen.UI
{
    public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] protected Canvas _currentCanvas;
        [SerializeField] protected ScrollRect _scrollRect;
        [SerializeField] protected ScrollContent _scrollContent;

        [Header("Main Settings")]
        [SerializeField] protected float _outOfBoundsThreshold;
        [SerializeField] protected bool _isUseSnap;

        [Header("Snap Settigns")]
        [SerializeField] protected float _snapSpeed;
        [SerializeField] protected float _minVelocityToDisableInertia;

        [Header("Infinity scroll variables")]
        protected Vector2 _lastDragPosition;
        protected float _currentOutOfBoundsThreshold;
        protected int _currentItemIndex;
        protected Transform _currentItem;
        protected int _endItemIndex;
        protected Transform _endItem;
        protected Vector3 _newPosition;
        protected bool _isPositiveScroll;
        protected int _scrollDirection;
        protected float _thresholdValue;
        protected float _itemPositionValue;
        public Action<int, int> OnPanelMoved;

        [Header("Snap scroll variables")]
        protected int _selectedPanelId;
        protected Vector3 _snapPosition;
        protected bool _isScrolling;
        protected float _scrollVelocity;

        [Header("Nearest panel variables")]
        protected float _nearestPosition;
        protected float _nearestDistance;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }
        protected virtual void FixedUpdate()
        {
            if (!_isUseSnap) return;

            SearchNearestPanel();
            _scrollVelocity = Mathf.Abs(_scrollContent.Type == ScrollContent.ScrollType.Horizontal ? _scrollRect.velocity.x : _scrollRect.velocity.y);

            if (_scrollVelocity < _minVelocityToDisableInertia && !_isScrolling) _scrollRect.inertia = false;
            if (_isScrolling || _scrollVelocity > _minVelocityToDisableInertia) return;

            SetSnapPosition();
        }

        #endregion

        #region Control Methods

        protected virtual void Initialize()
        {
            _endItemIndex = _scrollRect.content.childCount - 1;

            _isPositiveScroll = _scrollContent.Direction switch
            {
                ScrollContent.SpawnDirection.Forward => true,
                ScrollContent.SpawnDirection.Backward => true,
                _ => throw new NotImplementedException()
            };

            _currentOutOfBoundsThreshold = _outOfBoundsThreshold * _currentCanvas.transform.localScale.x;
            _scrollContent.Initialize();

            _scrollRect.onValueChanged.AddListener(HandleScroll);
        }

        #endregion

        #region Scroll Methods

        public void OnScroll(PointerEventData eventData)
        {
            SetDirection(eventData);
        }
        protected virtual void HandleScroll(Vector2 handlePosition)
        {
            _currentItemIndex = _isPositiveScroll ? _scrollRect.content.childCount - 1 : 0;
            _currentItem = _scrollRect.content.GetChild(_currentItemIndex);
            if (!ReachedThreshold(_currentItem)) return;

            _endItemIndex = _isPositiveScroll ? 0 : _scrollRect.content.childCount - 1;
            _endItem = _scrollRect.content.GetChild(_endItemIndex);
            _newPosition = CalculateNewPosition(_endItem);

            _currentItem.localPosition = _newPosition;
            _currentItem.SetSiblingIndex(_endItemIndex);
            OnPanelMoved?.Invoke(_currentItemIndex, _endItemIndex);
        }
        public void Scrolling(bool scroll)
        {
            _isScrolling = scroll;
            if (scroll) _scrollRect.inertia = true;
        }

        #endregion

        #region Drag Methods

        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastDragPosition = eventData.position;
            Scrolling(true);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Scrolling(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetDirection(eventData);
        }

        #endregion

        #region Calculate Methods

        protected void SetDirection(PointerEventData eventData)
        {
            switch (_scrollContent.Type)
            {
                case ScrollContent.ScrollType.Horizontal:
                    _isPositiveScroll = _scrollContent.Direction switch
                    {
                        ScrollContent.SpawnDirection.Forward => eventData.position.x > _lastDragPosition.x,
                        ScrollContent.SpawnDirection.Backward => eventData.position.x < _lastDragPosition.x,
                        _ => false
                    };
                    break;
                case ScrollContent.ScrollType.Vertical:
                    _isPositiveScroll = _scrollContent.Direction switch
                    {
                        ScrollContent.SpawnDirection.Forward => eventData.position.y > _lastDragPosition.y,
                        ScrollContent.SpawnDirection.Backward => eventData.position.y < _lastDragPosition.y,
                        _ => false
                    };
                    break;
            }
            _lastDragPosition = eventData.position;
            SetDirection();
        }
        protected void SetDirection()
        {
            _scrollDirection = _scrollContent.Direction switch
            {
                ScrollContent.SpawnDirection.Forward => _isPositiveScroll ? -1 : 1,
                ScrollContent.SpawnDirection.Backward => _isPositiveScroll ? 1 : -1,
                _ => 1
            };
        }

        protected Vector3 CalculateNewPosition(Transform endItem)
        {
            Vector3 newPosition = endItem.localPosition;
            switch (_scrollContent.Type)
            {
                case ScrollContent.ScrollType.Horizontal:
                    newPosition.x = endItem.localPosition.x - (_scrollContent.LocalChildWidth + _scrollContent.LocalItemSpacing) * _scrollDirection;
                    newPosition.z = 0;
                    break;
                case ScrollContent.ScrollType.Vertical:
                    newPosition.y = endItem.localPosition.y + (_scrollContent.LocalChildHeight + _scrollContent.LocalItemSpacing) * _scrollDirection;
                    newPosition.z = 0;
                    break;
            }
            return newPosition;
        }
        protected bool ReachedThreshold(Transform item)
        {
            switch (_scrollContent.Type)
            {
                case ScrollContent.ScrollType.Horizontal:
                    _thresholdValue = transform.localPosition.x + (_scrollContent.LocalWidth / 2f + _currentOutOfBoundsThreshold) * _scrollDirection;
                    _itemPositionValue = item.localPosition.x - _scrollContent.LocalChildWidth / 2f * _scrollDirection;

                    return _scrollContent.Direction switch
                    {
                        ScrollContent.SpawnDirection.Forward => _isPositiveScroll ? _itemPositionValue < _thresholdValue : _itemPositionValue > _thresholdValue,
                        ScrollContent.SpawnDirection.Backward => _isPositiveScroll ? _itemPositionValue > _thresholdValue : _itemPositionValue < _thresholdValue,
                        _ => false
                    };

                case ScrollContent.ScrollType.Vertical:
                    _thresholdValue = transform.localPosition.y - (_scrollContent.LocalHeight / 2f + _currentOutOfBoundsThreshold) * _scrollDirection;
                    _itemPositionValue = item.localPosition.y + _scrollContent.transform.localPosition.y + _scrollContent.LocalChildHeight / 2f * _scrollDirection;

                    return _scrollContent.Direction switch
                    {
                        ScrollContent.SpawnDirection.Forward => _isPositiveScroll ? _itemPositionValue > _thresholdValue : _itemPositionValue < _thresholdValue,
                        ScrollContent.SpawnDirection.Backward => _isPositiveScroll ? _itemPositionValue < _thresholdValue : _itemPositionValue > _thresholdValue,
                        _ => false
                    };
            }

            return false;
        }
        protected void SetSnapPosition()
        {
            _snapPosition = _scrollRect.content.localPosition;
            Vector3 currentVelocity = Vector3.zero;
            _snapPosition = Vector3.SmoothDamp(_scrollRect.content.position, _scrollRect.content.position + CalculateDifference2(), ref currentVelocity, _snapSpeed * Time.fixedDeltaTime);
            _scrollRect.content.position = _snapPosition;
        }
        protected void SearchNearestPanel()
        {
            _nearestPosition = float.MaxValue;
            for (int i = 0; i < _scrollRect.content.childCount; i++)
            {
                _nearestDistance = Vector3.Distance(transform.position, _scrollRect.content.GetChild(i).position);
                if (_nearestDistance < _nearestPosition)
                {
                    _nearestPosition = _nearestDistance;
                    _selectedPanelId = i;
                }
            }
        }
        private Vector3 CalculateDifference2()
        {
            return transform.position - _scrollRect.content.GetChild(_selectedPanelId).position;
        }

        #endregion
    }
}