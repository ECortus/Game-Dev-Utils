using GameDevUtils.Runtime.UI.Abstract;
using UnityEngine;

namespace Plugins.Tools.GameDevUtils.Tests
{
    public class TestFloatField : UDynamicFloatField
    {
        [Space]
        [SerializeField] private float value;
        
        protected override float GetTargetValue()
        {
            return value;
        }
    }
}
