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
            [SerializeField] private ShopViewButton _shopViewButton;
            [SerializeField] private ShopView _shopView;

            #endregion

            #region ShopViewInfo Methods

            public ShopViewButton ShopViewButton { get => _shopViewButton; }
            public ShopView ShopView { get => _shopView; }

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

            AdjustShopViews();
        }
        private void AdjustShopViews()
        {
            for (int i = 0; i < _shopViewsInfo.Length; i++)
            {
                _shopViewsInfo[i].ShopViewButton.Initialize();
                _shopViewsInfo[i].ShopViewButton.OnButtonClicked += ChangeShopView;

                if (i == 0) ChangeShopView(_shopViewsInfo[i].ShopViewButton);
            }

        }
        private void ChangeShopView(ShopViewButton button)
        {
            if (button == _activeButton) return;
            
            if (_activeButton != null)
            {
                _activeButton.SwitchActive(false);
                _shopViewsInfo.First(shopViewInfo => shopViewInfo.ShopViewButton == _activeButton).ShopView.gameObject.SetActive(false);
            }
            button.SwitchActive(true);
            _shopViewsInfo.First(shopViewInfo => shopViewInfo.ShopViewButton == button).ShopView.gameObject.SetActive(true);

            _activeButton = button;
        }
        public ProductPanel GetProductPanelByName(string name) => _productPrefabInfos.First(productPrefabInfo => productPrefabInfo.Name == name).ProductPanel;

        #endregion
    }
}