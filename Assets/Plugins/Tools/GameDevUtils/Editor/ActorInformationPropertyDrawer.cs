using GameDevUtils.Runtime.Simultaneous;
using UnityEditor;
using UnityEngine;

namespace GameDevUtils.Editor
{
    [CustomPropertyDrawer(typeof(ActorInformation), true)]
    public class ActorInformationPropertyDrawer : CustomPropertyDrawerModule
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var idProperty = property.FindPropertyRelative("id");
            var nameProperty = property.FindPropertyRelative("name");

            var id = "id_" + nameProperty.stringValue;
            id = id.Replace(" ", "_").ToLower();

            idProperty.stringValue = id;
            
            base.OnGUI(position, property, label);
            DrawProperty(property, label);
        }
    }
}