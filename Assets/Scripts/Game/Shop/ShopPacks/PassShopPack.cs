using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.UI
{
    public class PassShopPack : ShopPack
    {
        #region Variables

        [Header("Variables")]
        private List<GameObject> _passes = new List<GameObject>();

        #endregion

        #region Control Methods

        public void InitializePass(string name, List<GameObject> passes)
        {
            _productPackBar.Initialize(name);
            _passes = passes;
        }

        #endregion
    }
}