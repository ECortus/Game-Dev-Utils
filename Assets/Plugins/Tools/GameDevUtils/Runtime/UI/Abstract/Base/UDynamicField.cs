using System;
using Cysharp.Threading.Tasks;
using GameDevUtils.Runtime.Simultaneous;
using UnityEngine;

namespace GameDevUtils.Runtime.UI.Abstract
{
    public abstract class UDynamicField : MonoBehaviour
    {
        enum EUpdateMethod
        {
            Manual, Update, FixedUpdate, InvokeRepeating
        }
        
        [SerializeField] private EUpdateMethod updateMethod = EUpdateMethod.Update;
        
        [SerializeField, DrawIf("updateMethod", EUpdateMethod.InvokeRepeating), Range(0.05f, 2f)] 
        private float invokeDelay = 1f;
        
        private void Start()
        {
            OnStart();
            
            TryStartAsync();
            
            UpdateField();
        }

        void Update()
        {
            if (updateMethod != EUpdateMethod.Update)
                return;
            
            UpdateField();
            OnUpdate();
        }
        
        void FixedUpdate()
        {
            if (updateMethod != EUpdateMethod.FixedUpdate)
                return;
            
            UpdateField();
            OnFixedUpdate();
        }
        
        void TryStartAsync()
        {
            if (updateMethod != EUpdateMethod.InvokeRepeating)
                return;
            
            AsyncTaskHelper.CreateTask(async () =>
            {
                while (true)
                {
                    await UniTask.Delay((int)(invokeDelay * 1000));
                    
                    UpdateField();
                    OnInvoke();
                }
            });
        }
        
        protected virtual void OnStart()
        {
            
        }

        protected virtual void OnUpdate()
        {
            
        }

        protected virtual void OnFixedUpdate()
        {
            
        }

        protected virtual void OnInvoke()
        {
            
        }

        protected abstract void UpdateField();

        protected float DeltaTime
        {
            get
            {
                if (updateMethod == EUpdateMethod.Update)
                {
                    return Time.deltaTime;
                }
                else if (updateMethod == EUpdateMethod.FixedUpdate)
                {
                    return Time.fixedDeltaTime;
                }
                else if (updateMethod == EUpdateMethod.InvokeRepeating)
                {
                    return invokeDelay;
                }
                else if (updateMethod == EUpdateMethod.Manual)
                {
                    return 0;
                }
                else
                {
                    throw new System.NotImplementedException();
                }
            }
        }
    }
}