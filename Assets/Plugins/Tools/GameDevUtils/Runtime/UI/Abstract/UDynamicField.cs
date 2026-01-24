using Cysharp.Threading.Tasks;
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
                }
            });
        }

        void Update()
        {
            if (updateMethod != EUpdateMethod.Update)
                return;
            
            UpdateField();
        }
        
        void FixedUpdate()
        {
            if (updateMethod != EUpdateMethod.FixedUpdate)
                return;
            
            UpdateField();
        }
        
        protected virtual void OnStart()
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