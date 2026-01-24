using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevUtils.Runtime.UI.Abstract
{
    public abstract class UDynamicSliderField : UDynamicField
    {
        [Space(5)] 
        [SerializeField] private bool useDoubleSlider = true;
        [SerializeField] private Slider slider;
        [SerializeField, DrawIf("useDoubleSlider", true)] private float mainSliderSpeed = 50f;
        [SerializeField, DrawIf("useDoubleSlider", true)] private float mainSliderSpeedModOnUp = 1f;
        [SerializeField, DrawIf("useDoubleSlider", true)] private float mainSliderSpeedModOnDown = 2f;
        
        [Space(2)]
        [SerializeField, DrawIf("useDoubleSlider", true)] private Slider doubleSlider;
        [SerializeField, DrawIf("useDoubleSlider", true)] private float doubleSliderDelay = 0.5f;
        [SerializeField, DrawIf("useDoubleSlider", true)] private float doubleSliderSpeed = 75f;
        [SerializeField, DrawIf("useDoubleSlider", true)] private float doubleSliderSpeedModOnUp = 1f;
        [SerializeField, DrawIf("useDoubleSlider", true)] private float doubleSliderSpeedModOnDown = 2f;
        
        [Space(5)]
        [SerializeField] private TMP_Text label;
        
        float targetValue;
        
        float mainCurrentValue;
        float doubleCurrentValue;

        float doubleSliderLastTime;
        
        protected override void OnStart()
        {
            base.OnStart();
            
            targetValue = GetSliderValue();
            
            if (!useDoubleSlider)
            {
                if (doubleSlider)
                {
                    doubleSlider.gameObject.SetActive(false);
                }
            }
            else
            {
                mainCurrentValue = targetValue;
                doubleCurrentValue = targetValue;
                
                doubleSlider.minValue = slider.minValue;
                doubleSlider.maxValue = slider.maxValue;
            }
        }

        protected override void UpdateField()
        {
            targetValue = GetSliderValue();
            
            if (useDoubleSlider)
            {
                UpdateSliderAsMain();
                UpdateDoubleSlider();
            }
            else
            {
                UpdateSliderAsRegular();
            }
            
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
        
        void UpdateSliderAsMain()
        {
            var speed = mainSliderSpeed;
            if (targetValue < mainCurrentValue)
            {
                speed *= mainSliderSpeedModOnDown;
            }
            else
            {
                speed *= mainSliderSpeedModOnUp;
            }
            
            mainCurrentValue = Mathf.MoveTowards(mainCurrentValue, targetValue, speed * DeltaTime);
            slider.value = mainCurrentValue;
        }
        
        void UpdateDoubleSlider()
        {
            if (Mathf.Approximately(targetValue, doubleCurrentValue))
            {
                doubleSliderLastTime = Time.time;
                doubleCurrentValue = targetValue;
            }
            else
            {
                var speed = doubleSliderSpeed;
                
                if (targetValue < doubleCurrentValue)
                {
                    var currentTime = Time.time;
                    if (currentTime - doubleSliderLastTime > doubleSliderDelay)
                    {
                        speed *= doubleSliderSpeedModOnDown;
                    }
                    else
                    {
                        speed *= 0;
                    }
                }
                else
                {
                    speed *= doubleSliderSpeedModOnUp;
                }
                
                doubleCurrentValue = Mathf.MoveTowards(doubleCurrentValue, targetValue, speed * DeltaTime);
            }
            
            doubleSlider.value = doubleCurrentValue;
        }
        
        protected virtual string GetLabelText() { return ""; }
        
        void UpdateLabel()
        {
            label.text = GetLabelText();
        }
    }
}