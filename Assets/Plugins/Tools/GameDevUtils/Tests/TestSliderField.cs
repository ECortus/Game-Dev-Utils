using GameDevUtils.Runtime.UI.Abstract;
using UnityEngine;

namespace Plugins.Tools.GameDevUtils.Tests
{
    public class TestSliderField : UDoubleSliderField
    {
        [Space]
        [SerializeField] private float value;
        
        protected override float GetSliderValue()
        {
            return value;
        }
    }
}