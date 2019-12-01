using EboxGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

// Notifies the user on certains events. Can show dialog, info box, errors etc.
public class InGameNotification : Singleton<InGameNotification>
{
    public const int NT_INFO = 0;
    public const int NT_ERR = 1;
    public const int NT_DIALOG = 2;
    private Action m_act1;
    private Action m_act2;
    public Text m_txMsg;
    public Text m_txTitle;
    public Button m_btnYes;
    public Button m_btnNo;
    public Button m_btnOk;
    private GameObject m_goCanvas;

    private void Start ()
    {
        m_goCanvas = transform.GetChild( 0 ).gameObject;
        m_btnYes.onClick.AddListener( OnPressedAct1 );
        m_btnNo.onClick.AddListener( OnPressedAct2 );
        m_btnOk.onClick.AddListener( OnPressedAct1 );
    }

    public void Show ( int type , string message , Action act1 = null, Action act2 = null)
    {
        m_act1 = act1;
        m_act2 = act2;
        m_txMsg.text = message;
        m_goCanvas.SetActive( true );
        m_btnNo.gameObject.SetActive( false );
        m_btnYes.gameObject.SetActive( false );
        m_btnOk.gameObject.SetActive( false );

        switch ( type )
        {
            case NT_INFO:
            {
                m_btnOk.gameObject.SetActive( true );
                break;
            }
            case NT_ERR:
            {
                m_btnYes.gameObject.SetActive( true );
                break;
            }

            case NT_DIALOG:
            {
                m_btnYes.gameObject.SetActive( true );
                m_btnNo.gameObject.SetActive( true );
                break;
            }

            default:
            {

                break;
            }
        }
    }

    private void OnPressedAct1 ()
    {
        m_goCanvas.SetActive( false );

        if ( m_act1 != null )
        {
            m_act1.Invoke();
        }
    }

    private void OnPressedAct2 ()
    {
        m_goCanvas.SetActive( false );

        if ( m_act2 != null)
        {
            m_act2.Invoke();
        }
    }

    public override bool DontDestroyWhenLoad () { return true; }
    protected override void Awake () { base.Awake(); }
}
