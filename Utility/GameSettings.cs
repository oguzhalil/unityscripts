using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public enum VSync
    {
        Off = 0,
        OneBuffer = 1,
        TwoBuffer = 2
    }

    public VSync m_vSync;
    public int m_RefreshRate;
    public float m_TargetFrameRate;
    public bool m_ShowFPS;

    private void Awake ()
    {
        SetBuffer( m_vSync );
    }

    public void SetBuffer ( VSync vSync )
    {
        QualitySettings.vSyncCount = ( int ) m_vSync;
        m_RefreshRate = Screen.currentResolution.refreshRate;

        if ( m_vSync == VSync.Off )
        {
            m_TargetFrameRate = m_RefreshRate;
        }
        else
        {
            m_TargetFrameRate = m_RefreshRate / ( int ) m_vSync;
        }

        Application.targetFrameRate = ( int ) m_TargetFrameRate;

        Debug.Log( $"Vsync is {m_vSync} target frameRate {m_TargetFrameRate} refresh rate {m_RefreshRate}" );
    }

    private void OnGUI ()
    {
        if ( m_ShowFPS )
        {
            GUI.skin.box.fontSize = 35;
            GUILayout.Label( string.Format( "FPS : {0:0.#}" , 1.0f / Time.deltaTime ) , GUI.skin.box );
        }
    }
}
