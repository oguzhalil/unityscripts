using UtilityScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Scripting;
using System;

namespace UtilityScripts
{
    public class SceneController : UniqueSingleton<SceneController>
    {
        public Image fade;
        public GameObject loading;
        public Image imgBar;
        private float progress;
        public Text txLoading;

        // Dot Animation
        private int numDots = 0;
        public int maxDots = 4;
        private float dotAnimInterval = .2f;
        private float dotAnimTimer = 0.0f;
        private string dotAnimString = "LOADING";

        [System.Serializable]
        public class Scenes
        {
            public int id;
            public string name;
        }

        public Scenes [] scenes;

        private void Start ()
        {
            ListenForGCModeChange();
        }

        public void LoadSceneFadeOut ( string levelName )
        {
            foreach ( var item in scenes )
            {
                if ( levelName == item.name )
                {

                    LoadSceneFadeOut( item.id );
                    break;
                }
            }
        }

        public void LoadSceneFadeOut ( int levelIndex )
        {
            fade.color = Color.black;
            fade.gameObject.SetActive( false );
            loading.gameObject.SetActive( true );
            progress = 0.0f;
            StartCoroutine( LoadYourAsyncScene( levelIndex ) );
        }

        //protected override void Awake ()
        //{
        //    base.Awake();
        //    Application.backgroundLoadingPriority = ThreadPriority.Normal;
        //}

        private void Update ()
        {
            imgBar.fillAmount = progress;

            if ( txLoading && Time.time > dotAnimTimer )
            {
                txLoading.text = dotAnimString + new string( '.' , numDots );
                numDots++;

                if ( numDots > maxDots )
                {
                    numDots = 0;
                }

                dotAnimTimer = Time.time + dotAnimInterval;
            }
        }

        IEnumerator LoadYourAsyncScene ( int sceneIndex )
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            EnableGCAndCollect();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync( sceneIndex );

            // Wait until the asynchronous scene fully loads
            while ( !asyncLoad.isDone )
            {
                //Debug.Log( asyncLoad.progress );
                progress = asyncLoad.progress / 0.9f;
                yield return null;
            }
            fade.gameObject.SetActive( true );
            loading.SetActive( false );
            LeanTween.alpha( fade.rectTransform , 0f , 1.0f ).setOnComplete( () => { fade.gameObject.SetActive( false ); } );

            yield return new WaitForSeconds( 1.0f );
            EnableGCAndCollect(); // forces to collect again
            yield return null; // wait a frame
            yield return null; // wait a frame
            DisableGC(); // disable
        }
        public static bool Is ( string sceneName )
        {
            return SceneManager.GetActiveScene().name == sceneName;
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

        static void EnableGCAndCollect ()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
            // Trigger a collection to free memory.
            GC.Collect();
        }

        static void DisableGC ()
        {
#if UNITY_EDITOR
            return;
#endif
            GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
        }
    }
}
