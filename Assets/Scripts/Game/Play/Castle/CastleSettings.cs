using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Castle Settings", menuName = "WOFL/Settings/Game/Castle Settings", order = 1)]
    public class CastleSettings : ScriptableObject
    {
        #region Variables

        [Header("Main Settings")]
        [SerializeField] private Sprite _castleView;
        [SerializeField] private int _startHealth;
        [SerializeField] private float _startManaSpeedCollectValue;

        [Header("UpgradeUnit Settings")]
        [SerializeField] private int _increaseHealthStep;
        [SerializeField] private float _increaseManaSpeedCollectStep;

        #endregion

        #region Properties

        public Sprite CastleView { get => _castleView; }
        public int StartHealth { get => _startHealth; }
        public float StartManaSpeedCollectValue { get => _startManaSpeedCollectValue; }
        public int IncreaseHealthStep { get => _increaseHealthStep; }
        public float IncreaseManaSpeedCollectStep { get => _increaseManaSpeedCollectStep; }

        #endregion
    }
}