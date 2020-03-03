using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

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

            if ( SystemInfo.graphicsDeviceVersion.Contains( "2.0" ) )
            {
                QualitySettings.SetQualityLevel( 0 , true );

                Debug.Log( $"Graphic Device is { SystemInfo.graphicsDeviceVersion } setting quality level to {QualitySettings.names [ 0 ]} " );
            }
            else
            {
                QualitySettings.SetQualityLevel( 2 , true );
                Debug.Log( $"Graphic Device is { SystemInfo.graphicsDeviceVersion } setting quality level to {QualitySettings.names [ 2 ]} " );
            }
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

        static void ListenForGCModeChange ()
        {
            // Listen on garbage collector mode changes.
            GarbageCollector.GCModeChanged += ( GarbageCollector.Mode mode ) =>
            {
                Debug.Log( "GCModeChanged: " + mode );
            };
        }

        static void LogMode ()
        {
            Debug.Log( "GCMode: " + GarbageCollector.GCMode );
        }

        static void EnableGC ()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
            // Trigger a collection to free memory.
            GC.Collect();
        }

        static void DisableGC ()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
        }
    }
}
