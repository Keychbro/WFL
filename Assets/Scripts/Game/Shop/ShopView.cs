using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using WOFL.Settings;

namespace WOFL.UI
{
    public class ShopView : MonoBehaviour
    {
        #region Classes

        [Serializable] private class ShopPackInfo
        {
            #region ShopPackInfo Variables

            [SerializeField] private string _name;
            [SerializeField] private ProductPanelInfo[] _productPanelInfos;

            #endregion

            #region ShopPackInfo Properties

            public string Name { get => _name; }
            public ProductPanelInfo[] ProductPanelInfos { get => _productPanelInfos; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Prefabs")]
        [SerializeField] private ShopPack _shopPackPrefab;

        [Header("Objects")]
        [SerializeField] private GameObject _packsScrollHolder;

        [Header("Settings")]
        [SerializeField] private ShopPackInfo[] _shopPackInfos;

        [Header("Variables")]
        private ShopScreen _shopScreen;
        private List<ShopPack> _usedShopPacks = new List<ShopPack>();

        #endregion

        #region Control Methods

        public void Initialize(ShopScreen shopScreen)
        {
            _shopScreen = shopScreen;

            for (int i = 0; i < _shopPackInfos.Length; i++)
            {
                CreateNewShopPack(_shopPackInfos[i], out ShopPack newShopPack);
                _usedShopPacks.Add(newShopPack);
            }
        }
        private void CreateNewShopPack(ShopPackInfo shopPackInfo, out ShopPack newShopPack)
        {
            newShopPack = Instantiate(_shopPackPrefab, _packsScrollHolder.transform);
            CreateProductPanel(shopPackInfo.ProductPanelInfos, newShopPack.ProductsHolder.transform, out List<ProductPanel> createdProductPanels);

            newShopPack.Initialize(shopPackInfo.Name, createdProductPanels);
        }
        private void CreateProductPanel(ProductPanelInfo[] productPanelInfos, Transform panelsHolder, out List<ProductPanel> createdProductPanels)
        {
            createdProductPanels = new List<ProductPanel>();
            for (int i = 0; i <= productPanelInfos.Length; i++)
            {
                if (productPanelInfos[i] is ProductDiamondPanelInfo)
                {
                    ProductDiamondPanel diamondPanel = Instantiate((ProductDiamondPanel)_shopScreen.GetProductPanelByName("Diamonds"), panelsHolder);
                    diamondPanel.Initialize(productPanelInfos[i]);
                    //diamondPanel.
                }
                else
                {

                }
            }
        }

        #endregion
    }
}