using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace PabloGames
{
    public class Report : MonoBehaviour
    {
        public enum IconSprite { NONE = 0, COIN = 1 }

        public Sprite coinIcon;

        private static Report Instance { set; get; }

        [SerializeField] private Text labelHeader;
        [SerializeField] private Text labelMessage;
        [SerializeField] private Text labelButton;
        [SerializeField] private Image imageIcon;

        private Action onPressed;

        private GameObject WrappedObject { get { return transform.GetChild( 0 ).gameObject; } }

        bool onExitCallOnPressed = false;

        private void Awake ()
        {
            if ( Instance == null )
            {
                Instance = this;
                DontDestroyOnLoad( gameObject );
            }
            else
            {
                Destroy( gameObject );
                return;
            }
        }

        public Sprite GetIcon ( IconSprite iconSprite )
        {
            switch ( iconSprite )
            {
                case IconSprite.NONE:
                    return null;
                case IconSprite.COIN:
                    return coinIcon;
                //case IconSprite.RANK:
                //    return null;
                //case IconSprite.ERROR:
                //    return null;
                default:
                    return null;
            }

        }

        public static void Run ( string header , string message , string button , IconSprite iconSprite = 0 , Action onOkay = null , float multiplier = 1 , bool onExitCallOnPressed = false)
        {
            Instance.Scale( multiplier );

            Instance.IRun( header , message , button , iconSprite , onOkay , onExitCallOnPressed: onExitCallOnPressed );
        }



        public static void FatalError ()
        {
            float time = 1.5f;

            if ( UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1 )
                time = 0;

            Instance.IRun( "Fatal Error" ,
                "Check Internet Connectivity" ,
                "OKAY" ,
                IconSprite.NONE ,
                () => Instance.Invoke( "DelayedLoadScene" , time ) , true );
        }

        void DelayedLoadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene( 0 );
        }

        private void IRun ( string header , string message , string button , IconSprite iconSprite , Action onPressed , bool onExitCallOnPressed = false )
        {
            this.onPressed = onPressed;
            this.onExitCallOnPressed = onExitCallOnPressed;

            labelHeader.text = header;
            labelMessage.text = message;
            labelButton.text = button;

            bool icon = iconSprite != IconSprite.NONE;

            if ( icon )
                imageIcon.sprite = GetIcon( iconSprite );

            imageIcon.gameObject.SetActive( icon );
            WrappedObject.SetActive( true );

        }

        void Scale ( float multiplier )
        {
            WrappedObject.transform.localScale = Vector2.one * multiplier;
        }

        public void OnPressedOkay ()
        {
            WrappedObject.gameObject.SetActive( false );

            if ( onPressed != null )
            {
                onPressed();
                Scale( 1f );
                onPressed = null;
                onExitCallOnPressed = false;
            }
        }

        public void OnPressedExit ()
        {
            WrappedObject.gameObject.SetActive( false );

            if(onExitCallOnPressed == true)
            {
                onPressed();
                Scale( 1f );
                onPressed = null;
                onExitCallOnPressed = false;
            }
        }

    }
}
