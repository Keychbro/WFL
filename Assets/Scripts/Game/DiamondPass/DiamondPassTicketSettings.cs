using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Diamond Pass Ticket Settings", menuName = "WOFL/DiamondPass/Settings/Diamond Pass Ticket Settings", order = 1)]
    public class DiamondPassTicketSettings : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Sprite _onTicket;
        [SerializeField] private Sprite _offTicket;

        #endregion

        #region Properties

        public Sprite OnTicket { get => _onTicket; }
        public Sprite OffTicket { get => _offTicket; }

        #endregion
    }
}