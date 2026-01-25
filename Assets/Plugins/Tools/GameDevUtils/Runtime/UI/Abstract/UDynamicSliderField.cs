using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevUtils.Runtime.UI.Abstract
{
    public abstract class UDynamicSliderField : UDynamicField
    {
        [Space(5)] 
        [SerializeField] private Slider slider;
        
        [Space(5)]
        [SerializeField] private TMP_Text label;
        
        float targetValue;
        
        protected override void OnStart()
        {
            base.OnStart();
            targetValue = GetSliderValue();
        }

        protected override void UpdateField()
        {
            targetValue = GetSliderValue();
            UpdateSliderAsRegular();
            
            if (label)
            {
                UpdateLabel();
            }
        }
        
        protected abstract float GetSliderValue();

        void UpdateSliderAsRegular()
        {
            slider.value = targetValue;
        }
        
        protected virtual string GetLabelText() { return ""; }
        
        void UpdateLabel()
        {
            label.text = GetLabelText();
        }
    }
}