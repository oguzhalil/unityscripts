using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ColorChanger : MonoBehaviour
{
    public Type m_type;
    public Color m_targetColor;
    public Color m_defaultColor;
    public float m_duration = .5f;
    public bool m_resetOnComplete = true;

    public Sprite m_targetSprite;
    public Sprite m_defaultSprite;

    public LoopType m_loopType = LoopType.once;
    public CurveType m_curveType = CurveType.linear;
    public int m_loopCount = 1; // 0 means infinity
    private Graphic m_graphic;
    private Image m_image;
    private LTDescr m_tween;
    private int m_tweenId = 0;
    public enum LoopType { once = 35, clamp = 36, pingPong = 37 }
    public enum CurveType
    {
        notUsed, linear, easeOutQuad, easeInQuad, easeInOutQuad, easeInCubic, easeOutCubic, easeInOutCubic, easeInQuart, easeOutQuart, easeInOutQuart,
        easeInQuint, easeOutQuint, easeInOutQuint, easeInSine, easeOutSine, easeInOutSine, easeInExpo, easeOutExpo, easeInOutExpo, easeInCirc, easeOutCirc, easeInOutCirc,
        easeInBounce, easeOutBounce, easeInOutBounce, easeInBack, easeOutBack, easeInOutBack, easeInElastic, easeOutElastic, easeInOutElastic, easeSpring, easeShake
    };
    public enum Type { INSTANT, TIME, TWEEN, IMAGE }
    private bool bInitialized;
    [NonSerialized]public bool bDefault = true;

    private void Start ()
    {
        if ( !bInitialized )
        {
            Initialize();
        }
    }

    private void Initialize ()
    {
        m_graphic = GetComponent<Graphic>();
        m_defaultColor = m_graphic.color;

        if ( m_type == Type.IMAGE )
        {
            m_image = GetComponent<Image>();
        }

        LeanTween.init();
        bInitialized = true;
    }

    [ContextMenu( "Run" )]
    public void Run ()
    {
        Stop();

        var tween = LeanTween.value( gameObject , m_defaultColor , m_targetColor , m_duration ).setLoopType( ( LeanTweenType ) ( int ) m_loopType ).setEase( ( LeanTweenType ) ( int ) m_curveType )
            .setOnUpdateColor( color => { m_graphic.color = color; } );

        if ( m_resetOnComplete )
        {
            tween.setOnComplete( () => m_graphic.color = m_defaultColor );
        }

        m_tweenId = tween.uniqueId;


    }

    public void Stop ()
    {
        m_graphic.color = m_defaultColor;
        LeanTween.cancel( m_tweenId , true );
    }

    public void Switch ()
    {
        if ( LeanTween.isTweening( m_tweenId ) )
        {
            Stop();
        }
        else
        {
            Run();
        }
    }

    public void Switch ( bool bValue )
    {
        if ( bValue )
        {
            ToTarget();
        }
        else
        {
            ToDefault();
        }
    }

    public void ToTarget ()
    {
        if ( !bInitialized )
        {
            Initialize();
        }

        switch ( m_type )
        {
            case Type.INSTANT:
                m_graphic.color = m_targetColor;
                break;
            case Type.TIME:
                m_graphic.color = m_targetColor;
                LeanTween.delayedCall( m_duration , () => { m_graphic.color = m_defaultColor; } );
                break;
            case Type.TWEEN:
                LeanTween.value( gameObject , m_defaultColor , m_targetColor , m_duration ).setLoopType( ( LeanTweenType ) ( int ) m_loopType ).setEase( ( LeanTweenType ) ( int ) m_curveType )
            .setOnUpdateColor( color => { m_graphic.color = color; } );
                break;
            case Type.IMAGE:
                m_image.sprite = m_targetSprite;
                break;
            default:
                break;
        }

        bDefault = false;
    }

    public void ToDefault ()
    {
        if ( !bInitialized )
        {
            Initialize();
        }

        switch ( m_type )
        {
            case Type.INSTANT:
                m_graphic.color = m_defaultColor;
                break;
            case Type.TIME:
                m_graphic.color = m_defaultColor;
                //LeanTween.delayedCall( m_duration , () => { m_graphic.color = m_defaultColor; } );
                break;
            case Type.TWEEN:
                //    LeanTween.value( gameObject , m_defaultColor , m_targetColor , m_duration ).setLoopType( ( LeanTweenType ) ( int ) m_loopType ).setEase( ( LeanTweenType ) ( int ) m_curveType )
                //.setOnUpdateColor( color => { m_graphic.color = color; } );
                break;
            case Type.IMAGE:
                m_image.sprite = m_defaultSprite;
                break;
            default:
                break;
        }

        bDefault = true;
    }



#if UNITY_EDITOR

    [CustomEditor( typeof( ColorChanger ) )]
    [CanEditMultipleObjects]
    public class ColorChangerEditor : Editor
    {
        private ColorChanger m_target;

        private void OnEnable ()
        {
            m_target = target as ColorChanger;
            m_target.m_defaultColor = m_target.GetComponent<Graphic>().color;
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_type" ) );

            switch ( m_target.m_type )
            {
                case Type.INSTANT:
                {
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_targetColor" ) );
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_defaultColor" ) );

                    GUILayout.BeginVertical();

                    GUILayout.EndVertical();
                    break;
                }
                case Type.TIME:
                {
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_targetColor" ) );
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_defaultColor" ) );

                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_duration" ) );

                    break;
                }
                case Type.TWEEN:
                {
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_targetColor" ) );
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_defaultColor" ) );

                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_duration" ) );
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_loopType" ) );
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_curveType" ) );
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_loopCount" ) );


                    GUILayout.BeginHorizontal();
                    if ( GUILayout.Button( "Animate" ) && IsEditorPlaying() )
                    {
                        m_target.Run();
                    }
                    if ( GUILayout.Button( "Stop" ) && IsEditorPlaying() )
                    {
                        m_target.Stop();
                    }
                    GUILayout.EndHorizontal();
                    break;
                }

                case Type.IMAGE:
                {
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_targetSprite" ) );
                    EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_defaultSprite" ) );
                    break;
                }
            }

            //if ( UnityEditor.EditorApplication.isPlaying && m_target.m_tween != null)
            //{
            //    GUILayout.BeginVertical();
            //    EditorGUILayout.TextField( "Is Tweening " + LeanTween.isTweening(m_target.m_tweenId) );
            //    GUILayout.EndVertical();
            //}


            serializedObject.ApplyModifiedProperties();
        }

        private bool IsEditorPlaying ()
        {
            if ( !UnityEditor.EditorApplication.isPlaying )
            {
                EditorUtility.DisplayDialog( "Error" , "Editor must be in play mode!" , "Okay" );
            }

            return UnityEditor.EditorApplication.isPlaying;
        }
    }
#endif
}

