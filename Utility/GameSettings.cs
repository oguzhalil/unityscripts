using UnityEngine;

namespace UtilityScripts
{
    public class GameSettings : UniqueSingleton<GameSettings>
    {
        public enum VSync
        {
            Off = 0,
            OneBuffer = 1,
            TwoBuffer = 2
        }

        public VSync vsync;
        public int refreshRate;
        public float targetFrameRate;
        public bool showFps;

        protected override void Awake ()
        {
            base.Awake();
            SetBuffer( vsync );
        }

        public void SetBuffer ( VSync vSync )
        {
            QualitySettings.vSyncCount = ( int ) vsync;
            refreshRate = Screen.currentResolution.refreshRate;

            if ( vsync == VSync.Off )
            {
                targetFrameRate = 0;
            }
            else
            {
                targetFrameRate = refreshRate / ( int ) vsync;
            }

            Application.targetFrameRate = ( int ) targetFrameRate;

            Debug.Log( $"Vsync is {vsync} target frameRate {targetFrameRate} refresh rate {refreshRate}" );
        }

        private void OnGUI ()
        {
            if ( showFps )
            {
                GUI.skin.box.fontSize = 35;
                GUILayout.Label( string.Format( "FPS : {0:0.#}" , 1.0f / Time.deltaTime ) , GUI.skin.box );
            }
        }
    }
}
