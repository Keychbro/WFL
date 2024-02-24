using UnityEngine;
using static Kamen.HapticManager;

namespace Kamen
{
    public class HapticExecuter : SingletonComponent<HapticExecuter>
    {
        #region Haptic Methods

        public void Execute(HapticManager.Haptic type)
        {
        //    HapticPatterns.PresetType realType = GetRealHaptic(type);
        //    HapticPatterns.PlayPreset(realType);
        }
        //private HapticPatterns.PresetType GetRealHaptic(HapticManager.Haptic type)
        //{
        //    switch (type)
        //    {
        //        case HapticManager.Haptic.Selection: return HapticPatterns.PresetType.Selection;
        //        case HapticManager.Haptic.Success: return HapticPatterns.PresetType.Success;
        //        case HapticManager.Haptic.Warning: return HapticPatterns.PresetType.Warning;
        //        case HapticManager.Haptic.Failure: return HapticPatterns.PresetType.Failure;
        //        case HapticManager.Haptic.LightImpact: return HapticPatterns.PresetType.LightImpact;
        //        case HapticManager.Haptic.MediumImpact: return HapticPatterns.PresetType.MediumImpact;
        //        case HapticManager.Haptic.HeavyImpact: return HapticPatterns.PresetType.HeavyImpact;
        //        case HapticManager.Haptic.RigidImpact: return HapticPatterns.PresetType.RigidImpact;
        //        case HapticManager.Haptic.SoftImpact: return HapticPatterns.PresetType.SoftImpact;
        //        default: return HapticPatterns.PresetType.None;
        //    }
        //}

        #endregion
    }
}