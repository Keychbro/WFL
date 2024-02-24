using System;
using UnityEngine;

namespace Kamen 
{
    public class HapticManager : SingletonComponent<HapticManager>
    {
        /// <summary>
        /// This script is only suitable for Nice Vibration assets. For other assets, you need to write other scripts
        /// </summary>

        #region Enums

        public enum Haptic
        {
            Selection = 0,
            Success = 1,
            Warning = 2,
            Failure = 3,
            LightImpact = 4,
            MediumImpact = 5,
            HeavyImpact = 6,
            RigidImpact = 7,
            SoftImpact = 8,
            None = -1
        }

        #endregion

        #region Classes

        [Serializable] private class HapticInfo
        {
            #region HapticInfo Variables

            [SerializeField] private string _id;
            [SerializeField] private Haptic _type;

            #endregion

            #region HapticInfo Properties

            public string ID { get => _id; }
            public Haptic Type { get => _type; }

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private HapticInfo[] _hapticInfos;

        #endregion

        #region Control Methods

        public void Play(string id) 
        {
            HapticInfo hapticInfo = GetHapticInfoByID(id);

            if (hapticInfo == null)
            {
                Debug.LogError($"[Kamen - HapticManager] Haptic with id \"{id}\" does not exist!");
                return;
            }

            try { HapticExecuter.Instance.Execute(hapticInfo.Type); }
            catch { Debug.LogError($"[Kamen - HapticManager] Haptic execute does not exist!"); }
        }

        #endregion

        #region Calculate Methods

        private HapticInfo GetHapticInfoByID(string id)
        {
            for (int i = 0; i < _hapticInfos.Length; i++)
            {
                if (id == _hapticInfos[i].ID) return _hapticInfos[i];
            }
            return null;
        }

        #endregion
    }
}