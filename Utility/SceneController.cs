using UtilityScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UtilityScripts
{
    public class SceneController : UniqueSingleton<SceneController>
    {
        public Image fade;
        public GameObject loading;
        public Image imgBar;
        private float progress;

        [System.Serializable]
        public class Scenes
        {
            public int id;
            public string name;
        }

        public Scenes [] scenes;

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
        }

        IEnumerator LoadYourAsyncScene ( int sceneIndex )
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

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
        }
        public static bool Is ( string sceneName )
        {
            return SceneManager.GetActiveScene().name == sceneName;
        }
    }
}
