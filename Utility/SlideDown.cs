using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DentedPixel;

public class SlideDown : MonoBehaviour
{
    public float m_Duration = .5f;
    public RectTransform [] m_RectTransforms;
    private Dictionary<int , Content> m_Pairs;
    public int m_SelectedIndex = 0;
    public float m_ShiftInPixel = 50.0f;

    [Serializable]
    public class Content
    {
        public Transform transform;
        public Vector3 defaultPosition;

        public Content ( RectTransform rectTransform )
        {
            transform = rectTransform.transform;
            defaultPosition = rectTransform.transform.position;
        }
    }

    IEnumerator Start ()
    {
        yield return null;

        m_Pairs = new Dictionary<int , Content>();

        for ( int i = 0; i < m_RectTransforms.Length; i++ )
        {
            RectTransform rectTransform = m_RectTransforms [ i ];
            m_Pairs.Add( i , new Content( rectTransform ) );
        }

        foreach ( var pair in m_Pairs )
        {
            int key = pair.Key;
            Content value = pair.Value;
            Button button = value.transform.GetComponent<Button>();
            button.onClick.AddListener( delegate { OnPressed( key , value ); } );
        }

        ExecuteEvents.Execute( m_Pairs [ m_SelectedIndex ].transform.gameObject , new BaseEventData( EventSystem.current ) , ExecuteEvents.submitHandler );

        Debug.Log( "SlideDown.cs elapsed time " + ( Time.realtimeSinceStartup - NewBehaviourScript.time ) );
    }

    private void OnPressed ( int index , Content content )
    {
        for ( int i = 0; i < m_Pairs.Count; i++ )
        {
            Content value = m_Pairs [ i ];
            LTSeq seq = LeanTween.sequence();
            seq.append( value.transform.LeanMove( value.defaultPosition , m_Duration ) );

            if ( i <= index )
            {
                seq.append( value.transform.LeanMove( value.defaultPosition + new Vector3( -m_ShiftInPixel , 0.0f , 0.0f ) , m_Duration ) );
            }
        }

        m_SelectedIndex = index;

        //int direction = 0;
        //int newSelectedIndex = 0;

        //for ( int i = 0; i < m_RectTransforms.Length; i++ )
        //{
        //    RectTransform rect = m_RectTransforms [ i ];

        //    if ( rect == selectedRect )
        //    {
        //        direction = ( int ) Mathf.Sign( m_SelectedIndex - i );
        //        newSelectedIndex = i;
        //        break;
        //    }
        //}

        //if ( direction == 0 )
        //{
        //    Debug.Log( "We cant choose a color twice!" );
        //    return;
        //}

        //for ( int i = m_SelectedIndex; i != newSelectedIndex; i++ )
        //{
        //    if ( i == 0 || i == m_RectTransforms.Length - 1 )
        //        continue;

        //    RectTransform rect = m_RectTransforms [ i ];
        //    LeanTween.moveX( rect.gameObject , rect.transform.position.x + m_ShiftInPixel * direction , m_Duration );
        //}

    }
}