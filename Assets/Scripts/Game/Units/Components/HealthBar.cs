using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace WOFL.Game.Components
{
    public class HealthBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Slider _instantSlider;
        [SerializeField] private Slider _animationSlider;

        [Header("Settings")]
        [SerializeField] private float _animationDuration;
        [SerializeField] private Ease _animationEase;

        [Header("Variables")]
        private IDamagable _damagableObject;
        private IHealable _healableObject;

        #endregion

        #region Unity Methods
        private void Start()
        {
            Initialize();
        }
        private void OnDestroy()
        {
            _damagableObject.OnTakedDamage -= CallMinusValue;
            _healableObject.OnHealed += CallPlusValue;
        }

        #endregion

        #region Control Methods


        private void Initialize()
        {
            _damagableObject = GetComponent<IDamagable>();
            _damagableObject.OnTakedDamage += CallMinusValue;

            _healableObject = GetComponent<IHealable>();
            _healableObject.OnHealed += CallPlusValue;

            SetUpSlider(_instantSlider, _damagableObject.MaxHealth);
            SetUpSlider(_animationSlider, _damagableObject.MaxHealth);
        }
        private void CallMinusValue(int value) => UpdateBarView(_instantSlider.value - value);
        private void CallPlusValue(int value) => UpdateBarView(_instantSlider.value + value);
        private void UpdateBarView(float value)
        {
            _instantSlider.value = value;
            _animationSlider?.DOValue(value, _animationDuration).SetEase(_animationEase);
        }
        private void SetUpSlider(Slider slider, int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }

        #endregion
    }
}

