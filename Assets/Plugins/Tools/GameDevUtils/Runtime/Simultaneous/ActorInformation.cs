using System;
using UnityEngine;

namespace GameDevUtils.Runtime.Simultaneous
{
    public interface IActorInformation
    {
        string Id { get; }
        
        string Name { get; }
        string Description { get; }
        
        Sprite Icon { get; }
    }
    
    [Serializable]
    public sealed class ActorInformation : IActorInformation
    {
        [SerializeField, ReadOnly] private string id = "id_default_actor";

        [Space(5)] 
        [SerializeField] private string name = "Default Actor Name";
        [TextArea] [SerializeField] private string description = "Default actor description about specific info.";

        [Space(5)] 
        [SerializeField] private Sprite icon = null;

        #region Interface

        public string Id => id;
        public string Name => name;
        public string Description => description;
        public Sprite Icon => icon;

        #endregion
    }
}