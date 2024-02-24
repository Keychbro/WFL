using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.UI
{
    public class PopupManager : SingletonComponent<PopupManager>
    {
        #region Classes

        [Serializable] private struct PopupInfo
        {
            #region PopupInfo Variables

            [SerializeField] [Tooltip("Should be unique")] private string _id;
            [SerializeField] private Popup _popup;

            #endregion

            #region PopupInfo Properties

            public string ID { get => _id; }
            public Popup ThisPopup { get => _popup; }

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private PopupInfo[] _popupInfos;
        private List<Popup> _activePopups = new List<Popup>();

        #endregion

        #region Unity Methods 

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < _popupInfos.Length; i++)
            {
                _popupInfos[i].ThisPopup.Initialize();
            }
        }

        #endregion

        #region Control Methods

        public void Show(string id)
        {
            Popup popup = GetPopupByID(id);

            if (popup != null)
            {
                popup.Show();
                _activePopups.Add(popup);
            }
            else Debug.LogError($"[Kamen - PopupManager] Popup with id \"{id}\" does not exist in the scene!");
        }
        public void Hide(string id)
        {
            Popup popup = GetPopupByID(id);

            if (popup != null)
            {
                popup.Hide();
                _activePopups.Remove(popup);
            }
            else Debug.LogError($"[Kamen - PopupManager] Popup with id \"{id}\" does not exist in the scene!");
        }
        public void HideAllPopups()
        {
            while (_activePopups.Count > 0)
            {
                _activePopups[0].Hide();
                _activePopups.Remove(_activePopups[0]);
            }
        }

        #endregion

        #region Calculate Methods

        private Popup GetPopupByID(string id)
        {
            for (int i = 0; i < _popupInfos.Length; i++)
            {
                if (id == _popupInfos[i].ID) return _popupInfos[i].ThisPopup;
            }
            return null;
        }

        #endregion
    }
}