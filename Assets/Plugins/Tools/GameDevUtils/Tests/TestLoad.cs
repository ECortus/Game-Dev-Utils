using System;
using GameDevUtils.Runtime.Scene;
using GameDevUtils.Runtime.Simultaneous;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Tools.GameDevUtils.Tests
{
    public class TestLoad : MonoBehaviour
    {
        [SerializeField] private ActorInformation infoTest;
        
        [Space(5)]
        [SerializeField] private int targetFrame = 1;
        [SerializeField] private Button button;

        private void Start()
        {
            Application.targetFrameRate = targetFrame;
            button.onClick.AddListener(() =>
            {
                SceneLoader.LoadScene(1);
            });
        }
    }
}