using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityScripts
{
    public class Processing : UniqueSingleton<Processing>
    {
        public GameObject m_processCanvas;
        public Transform m_transIndicator;

        public static Action Wait ()
        {
            Instance.m_processCanvas.gameObject.SetActive( true );
            return Stop;
        }

        static void Stop ()
        {
            Instance.m_processCanvas.gameObject.SetActive( false );
        }

        private void Update ()
        {
            if ( m_processCanvas.activeSelf )
            {
                m_transIndicator.Rotate( Vector3.forward , -180f * Time.deltaTime );
            }
        }
    }
}
