using GameDevUtils.Runtime;
using UnityEngine;

namespace Plugins.Tools.GameDevUtils.Runtime.Extensions
{
    public static class TransformExtensions
    {
        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                ObjectHelper.Destroy(child.gameObject, true);
            }
            
            transform.DetachChildren();
        }
    }
}