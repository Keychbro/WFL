using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using System;
using Cysharp.Threading.Tasks;
using Kamen.DataSave;
using WOFL.Game;
using System.Linq;

namespace WOFL.UI
{
    public class ShopScreen : Kamen.UI.Screen
    {
        #region Classes

        [Serializable] private struct ShopViewInfo
        {
            #region ShopViewInfo Variables

            [Header("Objects")]
            [SerializeField] private ShopViewButton _button;
            [SerializeField] private ShopView _view;

            #endregion

            #region ShopViewInfo Methods

            public ShopViewButton Button { get => _button; }
            public ShopView View { get => _view; }

            #endregion
        }
        [Serializable] private struct ProductPrefabInfo
        {
            #region ProductPrefabInfo Variables

            [SerializeField] private string _name;
            [SerializeField] private ProductPanel _productPanel;

            #endregion

            #region ProductPrefabInfo Propeties

            public string Name { get => _name; }
            public ProductPanel ProductPanel { get => _productPanel; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private ShopViewInfo[] _shopViewsInfo;
        [SerializeField] private ProductPrefabInfo[] _productPrefabInfos;

        [Header("Variables")]
        private ShopViewButton _activeButton;

        #endregion

        #region Control Methods

        public async override void Initialize()
        {
            base.Initialize();
            
            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            await UniTask.WaitUntil(() => DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None);
            
            Debug.LogError(12323412);
            
            AdjustShopViews();
        }
        private void AdjustShopViews()
        {
            for (int i = 0; i < _shopViewsInfo.Length; i++)
            {
                _shopViewsInfo[i].Button.Initialize();
                _shopViewsInfo[i].Button.OnButtonClicked += ChangeShopView;

                _shopViewsInfo[i].View.Initialize(this);

                if (i == 0) ChangeShopView(_shopViewsInfo[i].Button);
            }

        }
        private void ChangeShopView(ShopViewButton button)
        {
            if (button == _activeButton) return;
            
            if (_activeButton != null)
            {
                _activeButton.SwitchActive(false);
                _shopViewsInfo.First(shopViewInfo => shopViewInfo.Button == _activeButton).View.gameObject.SetActive(false);
            }
            button.SwitchActive(true);
            _shopViewsInfo.First(shopViewInfo => shopViewInfo.Button == button).View.gameObject.SetActive(true);

            _activeButton = button;
        }
        public ProductPanel GetProductPanelByName(string name) => _productPrefabInfos.First(productPrefabInfo => productPrefabInfo.Name == name).ProductPanel;

        #endregion
    }
}