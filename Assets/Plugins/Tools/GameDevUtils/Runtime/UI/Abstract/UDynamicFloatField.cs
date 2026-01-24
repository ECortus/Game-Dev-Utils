using System;
using TMPro;
using UnityEngine;

namespace GameDevUtils.Runtime.UI.Abstract
{
    public abstract class UDynamicFloatField : UDynamicField
    {
        [Space(5)]
        [SerializeField] private TMP_Text text;

        [Space(5)] 
        [SerializeField] private bool useFloatSpeed = true;
        [SerializeField] private int digitNumber = 0;
        [SerializeField, DrawIf("useFloatSpeed", true)] private float floatSpeed = 100f;

        float currentValue;
        float targetValue;

        protected override void OnStart()
        {
            base.OnStart();
            
            currentValue = GetRoundedTargetValue();
            targetValue = currentValue;
        }

        protected override void UpdateField()
        {
            targetValue = GetRoundedTargetValue();

            if (useFloatSpeed)
            {
                if (!Mathf.Approximately(targetValue, currentValue))
                {
                    currentValue = Mathf.MoveTowards(currentValue, targetValue, floatSpeed * DeltaTime);
                }
            }
            else
            {
                currentValue = targetValue;
            }
            
            UpdateText();
        }

        protected abstract float GetTargetValue();

        protected float GetRoundedTargetValue()
        {
            var value = GetTargetValue();
            return GetRoundedValue(value);
        }
        
        protected float GetRoundedValue(float value)
        {
            return MathF.Round(value, digitNumber);
        }

        protected virtual string GetText(float value)
        {
            return $"{GetRoundedValue(value)}";
        }
        
        void UpdateText()
        {
            text.text = GetText(currentValue);
        }
    }
}