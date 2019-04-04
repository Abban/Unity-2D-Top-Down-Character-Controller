using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BBX.Debugger
{
    public class Debugger : IInitializable
    {
        private Settings _settings;
        
        public Debugger(Settings settings)
        {
            _settings = settings;
        }
        
        
        public void Initialize()
        {
#if !UNITY_EDITOR
            _settings.debugger.SetActive(false);
#endif
        }

        
        /// <summary>
        /// Event handler when a debug message is added
        /// </summary>
        /// <param name="debugMessageSignal"></param>
        public void OnDebugMessage(DebugMessageSignal debugMessageSignal)
        {
            _settings.text.text = $"{_settings.text.text}\n{debugMessageSignal.Message}";
            _settings.scrollRect.verticalNormalizedPosition = 0f;
        }

        
        /// <summary>
        /// Event handler for debugger clear event
        /// </summary>
        /// <param name="debugClearSignal"></param>
        public void OnDebugClear(DebugClearSignal debugClearSignal)
        {
            _settings.text.text = string.Empty;
        }
        

        [Serializable]
        public class Settings
        {
            public GameObject debugger;
            public ScrollRect scrollRect;
            public Text text;
        }
    }
}