using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.IAP
{
    public class IAPManager : SingletonComponent<IAPManager>
    {
        #region Variables



        #endregion

        #region Control Methods

        public string GetPriceByName(string priceName) => "10$"; //TODO: fix this method, when i add iap

        #endregion
    }
}