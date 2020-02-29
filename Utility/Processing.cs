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
        public float timeout = 10f;
        private float? timer;

        public static Action Wait ()
        {
            Instance.m_processCanvas.gameObject.SetActive( true );
            Instance.timer = Time.time + Instance.timeout;
            return Stop;
        }

        static void Stop ()
        {
            Instance.m_processCanvas.gameObject.SetActive( false );
            Instance.timer = null;
        }

        private void Update ()
        {
            if ( m_processCanvas.activeSelf )
            {
                m_transIndicator.Rotate( Vector3.forward , -180f * Time.deltaTime );
            }

            if ( timer != null && Time.time > timer )
            {
                Stop();
            }
        }
    }
}
