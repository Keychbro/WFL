using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using WOFL.BattlePass;
using WOFL.Control;
using WOFL.Settings;

namespace WOFL.UI
{
    public class ShopView : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] protected ShopPack _shopPackPrefab;
        [SerializeField] protected PassShopPack _passShopPackPrefab;

        [Header("Objects")]
        [SerializeField] protected GameObject _packsScrollHolder;

        [Header("Settings")]
        [SerializeField] protected ShopPackInfo[] _shopPackInfos;

        [Header("Variables")]
        protected ShopScreen _shopScreen;
        protected List<ShopPack> _usedShopPacks = new List<ShopPack>();

        #endregion

        #region Control Methods

        public virtual void Initialize(ShopScreen shopScreen)
        {
            _shopScreen = shopScreen;

            for (int i = 0; i < _shopPackInfos.Length; i++)
            {
                ShopPack newShopPack = null;
                if (_shopPackInfos[i] is ShopPackPassesInfo) CreateNewPassShopPack((ShopPackPassesInfo)_shopPackInfos[i], out newShopPack);
                else CreateNewShopPack(_shopPackInfos[i], out newShopPack);
                _usedShopPacks.Add(newShopPack);
            }
        }
        protected virtual void CreateNewShopPack(ShopPackInfo shopPackInfo, out ShopPack newShopPack)
        {
            newShopPack = Instantiate(_shopPackPrefab, _packsScrollHolder.transform);
            CreateProductPanel(shopPackInfo.ProductPanelInfos, newShopPack.ProductsHolder.transform, out List<ProductPanel> createdProductPanels);

            newShopPack.Initialize(shopPackInfo.Name, createdProductPanels);
        }
        protected virtual void CreateNewPassShopPack(ShopPackPassesInfo shopPackInfo, out ShopPack newShopPack)
        {
            PassShopPack newPassShopPack = Instantiate(_passShopPackPrefab, _packsScrollHolder.transform);
            CreateProductPanel(shopPackInfo.ProductPanelInfos, newPassShopPack.ProductsHolder.transform, out List<ProductPanel> createdProductPanels);

            newPassShopPack.Initialize(shopPackInfo.Name, createdProductPanels);

            if (BattlePassManager.Instance.CurrentSeasonInfo != null)
            {
                CreatePass(newPassShopPack.PassHolder.transform, out List<GameObject> createdPasses);
                newPassShopPack.AddPasses(createdPasses);
            }

            newShopPack = newPassShopPack;
        }

        protected virtual void CreateProductPanel(ProductPanelInfo[] productPanelInfos, Transform panelsHolder, out List<ProductPanel> createdProductPanels)
        {
            createdProductPanels = new List<ProductPanel>();
            for (int i = 0; i < productPanelInfos.Length; i++)
            {
                ProductPanel productPanel = Instantiate(_shopScreen.GetProductPanelByName(GetProductNameByInfo(productPanelInfos[i])), panelsHolder);
                productPanel.Initialize(productPanelInfos[i]);
            }
        }
        protected virtual void CreatePass(Transform passHolder, out List<GameObject> createdPasses)
        {
            createdPasses = new List<GameObject>();
            BattlePassView battlePassView = Instantiate(_shopScreen.GetBattlePass(), passHolder);
            battlePassView.Initialize(
                BattlePassManager.Instance.CurrentSeasonNumber,
                BattlePassManager.Instance.CurrentSeasonInfo,
                DataSaveManager.Instance.MyData.GetBattlePassDataByName(BattlePassManager.Instance.CurrentSeasonInfo.BattlePassLine.SeasonName));
           //for (int i = 0; i < productPanelInfos.Length; i++)
           //{
           //    Bat
           //    ProductPanel productPanel = Instantiate(_shopScreen.GetProductPanelByName(GetProductNameByInfo(productPanelInfos[i])), panelsHolder);
           //    productPanel.Initialize(productPanelInfos[i]);
           //}
        }
        protected virtual string GetProductNameByInfo(ProductPanelInfo productPanelInfo)
        {
            if (productPanelInfo is ProductDiamondPanelInfo) return "Diamonds";
            else if (productPanelInfo is ProductGoldPanelInfo) return "Gold";
            else if (productPanelInfo is ProductToolsPanelInfo) return "Tools";
            else if (productPanelInfo is ProductChestPanelInfo) return "Chest";
            else if (productPanelInfo is ProductDailyBonusPanelInfo) return "DailyBonus";
            else if (productPanelInfo is ProductRemoveAdsPanelInfo) return "RemoveAds";
            else if (productPanelInfo is ProductUnitPanelInfo) return "Unit";
            else return "Classic";
        }

        #endregion
    }
}