using Coffee.UIExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UtilityScripts;

public class QuickTutorial : MonoBehaviour
{
    [System.Serializable]
    public class TutPiece
    {
        public RectTransform rect;
        [NonSerialized] public Button btn;
        [NonSerialized] public Image img;
        public UnityAction callback;
        public bool ready;
        public LTDescr anim;
        public string locId;

        public bool IsButton { get { return rect.GetComponent<Button>(); } }

        public void Bind ()
        {
            if ( !rect.GetComponent<Button>() )
            {
                rect.gameObject.AddComponent<Button>();
            }

            btn = rect.GetComponent<Button>();
            btn.onClick.AddListener( callback );
        }

        public void Release ()
        {
            btn.onClick.RemoveListener( callback );
            anim.setLoopPingPong( 1 );
        }
    }

    public List<TutPiece> tutPieces;
    private GameObject howTo;
    //private Canvas [] canvases;
    private int nextTutPiece;
    public GameObject maskObject;
    public GameObject instantiatedMaskObject;
    private Canvas canvas;
    private Unmask unmask;
    private TutPiece currentTutPiece;

    private void Awake ()
    {
        canvas = GetComponent<Canvas>();

        if ( canvas == null || PlayerPrefs.HasKey("#game_tut") )
        {
            Destroy( this );
            return;
        }

        PlayerPrefs.SetInt( "#game_tut" , 1 );
        instantiatedMaskObject = Instantiate( maskObject , canvas.transform );
        unmask = instantiatedMaskObject.GetComponentInChildren<Unmask>();
        instantiatedMaskObject.transform.SetAsLastSibling();
        //canvases = FindObjectsOfType<Canvas>();
        //foreach ( var elem in canvases )
        //{
        //    elem.GetComponent<GraphicRaycaster>().enabled = false;
        //}

        //howTo = new GameObject( "HowTo" );
        //howTo.AddComponent<RectTransform>();
        //var canvas = howTo.AddComponent<Canvas>();
        //canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //canvas.sortingOrder = 1000;
        //var canvasScaler = howTo.AddComponent<CanvasScaler>();
        //canvasScaler.referenceResolution = new Vector2( 1920 , 1080 );
        //canvasScaler.matchWidthOrHeight = .5f;
        //howTo.AddComponent<GraphicRaycaster>();

        //var overlay = new GameObject( "Overlay" );
        //var overlayImg = overlay.AddComponent<Image>();
        //overlayImg.color = new Color( 1f , 1f , 1f , .25f );
        //overlayImg.rectTransform.anchorMin = Vector2.zero;
        //overlayImg.rectTransform.anchorMax = Vector2.one;

        nextTutPiece = -1;
        IterateTutorial();
    }

    private IEnumerator  Start ()
    {
        yield return null;
        yield return null;
        RCC_SceneManager.Instance.activePlayerVehicle.StartCoroutine( "ChangeGear" , -1 );
    }

    private void Update ()
    {
        if ( !currentTutPiece.ready )
        {
            var rect = new GameObject().AddComponent<RectTransform>();
            rect.SetParent( currentTutPiece.rect.transform , false );
            rect.transform.SetAsFirstSibling();

            var target = currentTutPiece.rect;
            //rect.sizeDelta = target.sizeDelta;
            //rect.anchorMin = target.anchorMin;
            //rect.anchorMax = target.anchorMax;
            //rect.pivot = target.pivot;
            //rect.anchoredPosition = target.anchoredPosition;
            rect.sizeDelta = rect.sizeDelta * 1.5f;
            unmask.fitTarget = rect;
            currentTutPiece.callback = Dialog;
            currentTutPiece.Bind();
            currentTutPiece.anim = LeanTween.scale( currentTutPiece.btn.gameObject , new Vector3( 1.1f , 1.1f , 1.1f ) , .25f ).setEaseOutQuad().setLoopPingPong( -1 );
            currentTutPiece.ready = true;
        }
    }

    private void Dialog ()
    {
        InGameNotification.Instance.Show( InGameNotification.NT_INFO , Localization.Instance.GetText( currentTutPiece.locId ) , IterateTutorial , IterateTutorial );
    }

    private void IterateTutorial ()
    {
        if ( currentTutPiece != null )
        {
            currentTutPiece.Release();
        }

        if ( nextTutPiece + 1 >= tutPieces.Count ) // we're done
        {
            InGameNotification.Instance.Show( InGameNotification.NT_INFO , Localization.Instance.GetText( "game_tut_comp" ));
            DestroyImmediate( instantiatedMaskObject );
            return;
        }

        nextTutPiece++;
        currentTutPiece = tutPieces [ nextTutPiece ];

    }
}
