using System;

namespace BBX.Player
{
    public class PlayerSoundHandler
    {
        private Settings _settings;

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        public PlayerSoundHandler(Settings settings)
        {
            _settings = settings;
        }
        
        
        [Serializable]
        public class Settings
        {
        }
    }
}