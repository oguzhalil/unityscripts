﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UtilityScripts;

// Basic scene loader script with dot animation
// Launcher tasks are the task should complete before launching.
namespace UtilityScript
{
    public class Launcher : MonoBehaviour
    {
        public string m_SceneName;
        public bool m_DevelopmentMode;
        //public bool m_AllowSceneActivation = true;
        public AsyncOperation m_AsyncSceneLoader;
        public LauncherTask [] m_LauncherTasks;
        public Text m_LabelLoading;
        //public Slider.SliderEvent m_LoadingProgress;
        public bool m_DotAnimation;
        private int m_Dots = 0;
        public int m_MaxDots = 4;
        private float m_DotInterval = .2f;
        private float m_DotTimer = 0.0f;
        private string m_DotAnimationString = "LOADING";
        private int m_NumCompletedTasks;

        private IEnumerator Start ()
        {
            //m_AsyncSceneLoader = SceneManager.LoadSceneAsync( m_SceneName );
            //m_AsyncSceneLoader.allowSceneActivation = false;
            //yield return new WaitUntil( () => ( m_AsyncSceneLoader.progress >= .9f && m_AsyncSceneLoader.isDone ) );
            yield return new WaitUntil( () => IsLauncherTaskComplete() );
            //m_AsyncSceneLoader.allowSceneActivation = m_AllowSceneActivation;

            SceneController.Instance.LoadSceneFadeOut( m_SceneName );
        }

        private void Update ()
        {
            //if ( m_AsyncSceneLoader != null )
            //{
            //    float progress = ( ( ( float ) ( m_NumCompletedTasks + 1 ) / ( float ) ( m_LauncherTasks.Length + 1 ) )
            //        + ( m_AsyncSceneLoader.progress / 0.9f ) ) / 2.0f;
            //    m_LoadingProgress.Invoke( progress );
            //}

            if ( m_DotAnimation && Time.time > m_DotTimer )
            {
                m_LabelLoading.text = m_DotAnimationString + new string( '.' , m_Dots );
                m_Dots++;

                if ( m_Dots > m_MaxDots )
                {
                    m_Dots = 0;
                }

                m_DotTimer = Time.time + m_DotInterval;
            }
        }

        public bool IsLauncherTaskComplete ()
        {
            if(m_LauncherTasks == null || m_LauncherTasks.Length == 0)
            {
                return true;
            }

            m_NumCompletedTasks = 0;

            for ( int i = 0; i < m_LauncherTasks.Length; i++ )
            {
                LauncherTask launcherTask = m_LauncherTasks [ i ];

                if ( !launcherTask.m_bRunning )
                {
                    launcherTask.Run();
                }

                if ( launcherTask.IsError() )
                {
                    // show error
                    break;
                }
                else if ( !launcherTask.IsDone() )
                {
                    return false;
                }
                else
                {
                    m_NumCompletedTasks++;
                }
            }

            return true;
        }
    }
}
