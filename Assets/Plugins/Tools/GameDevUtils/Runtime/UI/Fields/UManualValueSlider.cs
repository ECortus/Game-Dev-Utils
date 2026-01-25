using GameDevUtils.Runtime.UI.Abstract;

namespace GameDevUtils.Runtime.UI.Fields
{
    public class UManualValueSlider : UDynamicSliderField
    {
        private float value;
        
        public void SetValue(float value)
        {
            this.value = value;
        }

        protected override float GetSliderValue()
        {
            return value;
        }
    }
}