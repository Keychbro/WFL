using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Shop Pack Info", menuName = "WOFL/Shop/Shop Pack Info", order = 1)]
    public class ShopPackInfo : ScriptableObject
    {
        #region ShopPackInfo Variables

        [SerializeField] protected string _name;
        [SerializeField] protected ProductPanelInfo[] _productPanelInfos;

        #endregion

        #region ShopPackInfo Properties

        public string Name { get => _name; }
        public ProductPanelInfo[] ProductPanelInfos { get => _productPanelInfos; }

        #endregion
    }
}