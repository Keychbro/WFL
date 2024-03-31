using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace WOFL.UI
{
    public class ProductGoodOfferView : MonoBehaviour
    {
        #region Enums

        public enum OfferType
        {
            None,
            Like,
            Popular,
            Limeted,
            BestValue,
        }

        #endregion

        #region Classes

        [Serializable]
        private struct GoodOfferInfo
        {
            #region GoodOfferInfo Variables

            [SerializeField] private OfferType _type;
            [SerializeField] private GameObject _offerObject;

            #endregion

            #region GoodOfferInfo Properties

            public OfferType Type { get => _type; }
            public GameObject OfferObject { get => _offerObject; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private List<GoodOfferInfo> _goodOffersInfo = new List<GoodOfferInfo>();

        #endregion

        #region Control Methods

        public void AdjustView(OfferType choosenType)
        {
            List<GoodOfferInfo> notUsedOffers = _goodOffersInfo.Where(goodOfferInfo => goodOfferInfo.Type != choosenType).ToList();

            for (int i = 0; i < notUsedOffers.Count; i++)
            {
                _goodOffersInfo.Remove(notUsedOffers[i]);
                Destroy(notUsedOffers[i].OfferObject);
            }

            for (int i = 0; i < _goodOffersInfo.Count; i++)
            {
                _goodOffersInfo[i].OfferObject.SetActive(true);
            }
        }

        #endregion
    }
}
