using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor( typeof( Page ) )]
public class PageInspector : Editor
{
    public void Awake ()
    {
        if ( UnityEditor.EditorApplication.isPlaying )
        {
            return;
        }

        //var curPage = target as Page;
        //var pages = curPage.transform.parent.GetComponentsInChildren<Page>( true );

        //foreach ( var page in pages )
        //{
        //    if ( page != curPage )
        //    {
        //        page.gameObject.SetActive( false );
        //    }
        //}

        //curPage.gameObject.SetActive( true );
    }

    //public void OnDestroy ()
    //{
    //    var curPage = target as Page;
    //    var pages = curPage.transform.parent.GetComponentsInChildren<Page>( true );

    //    foreach ( var page in pages )
    //    {
    //        page.gameObject.SetActive( true );
    //    }
    //}

    //public override void OnInspectorGUI ()
    //{
    //    //base.OnInspectorGUI();

    //    EditorPage.Show( serializedObject.FindProperty( "panels" ) , true );

    //    EditorGUILayout.PropertyField( serializedObject.FindProperty( "sprBackground" ) );

    //    EditorGUILayout.PropertyField( serializedObject.FindProperty( "panelDefault" ) );

    //    serializedObject.ApplyModifiedProperties();

    //    if ( GUILayout.Button( "Update" ) )
    //    {
    //        Page page = target as Page;

    //        Panel [] panels = page.GetComponentsInChildren<Panel>( true );

    //        page.panels = panels;

    //        foreach ( var panel in panels )
    //        {
    //            panel.exposeElements = panel.GetComponentsInChildren<ExposeElement>( true );
    //        }
    //    }
    //}
}

//[CustomEditor( typeof( Transform ) )]
//public class PageOp : Editor
//{
//    public void Awake ()
//    {
//        var curPage = ( target as Transform ).GetComponent<Page>();

//        if ( !curPage )
//        {
//            return;
//        }

//        var pages = curPage.transform.parent.GetComponentsInChildren<Page>( true );

//        foreach ( var page in pages )
//        {
//            if ( page != curPage )
//            {
//                page.gameObject.SetActive( false );
//            }
//        }

//        curPage.gameObject.SetActive( true );
//    }

//    public void OnDestroy ()
//    {
//        var curPage = ( target as Transform ).GetComponent<Page>();

//        if ( !curPage )
//        {
//            return;
//        }

//        var pages = curPage.transform.parent.GetComponentsInChildren<Page>( true );

//        foreach ( var page in pages )
//        {
//            page.gameObject.SetActive( true );
//        }
//    }

//    //public override void OnInspectorGUI ()
//    //{
//    //    //base.OnInspectorGUI();

//    //    EditorPage.Show( serializedObject.FindProperty( "panels" ) , true );

//    //    EditorGUILayout.PropertyField( serializedObject.FindProperty( "sprBackground" ) );

//    //    EditorGUILayout.PropertyField( serializedObject.FindProperty( "panelDefault" ) );

//    //    serializedObject.ApplyModifiedProperties();

//    //    if ( GUILayout.Button( "Update" ) )
//    //    {
//    //        Page page = target as Page;

//    //        Panel [] panels = page.GetComponentsInChildren<Panel>( true );

//    //        page.panels = panels;

//    //        foreach ( var panel in panels )
//    //        {
//    //            panel.exposeElements = panel.GetComponentsInChildren<ExposeElement>( true );
//    //        }
//    //    }
//    //}
////}

//[CustomEditor( typeof( Canvas ) )]
//public class PageOp : Editor
//{
//    //public void Awake ()
//    //{
//    //    var curPage = ( target as Transform ).GetComponent<Page>();

//    //    if ( !curPage )
//    //    {
//    //        return;
//    //    }

//    //    var pages = curPage.transform.parent.GetComponentsInChildren<Page>( true );

//    //    foreach ( var page in pages )
//    //    {
//    //        if ( page != curPage )
//    //        {
//    //            page.gameObject.SetActive( false );
//    //        }
//    //    }

//    //    curPage.gameObject.SetActive( true );
//    //}

//    //public void OnDestroy ()
//    //{
//    //    var curPage = ( target as Transform ).GetComponent<Page>();

//    //    if ( !curPage )
//    //    {
//    //        return;
//    //    }

//    //    var pages = curPage.transform.parent.GetComponentsInChildren<Page>( true );

//    //    foreach ( var page in pages )
//    //    {
//    //        page.gameObject.SetActive( true );
//    //    }
//    //}

//    //public override void OnInspectorGUI ()
//    //{
//    //    DrawDefaultInspector();

//    //    if ( GUILayout.Button( "Activate All Pages" ) )
//    //    {
//    //        var pages = Resources.FindObjectsOfTypeAll<Page>();

//    //        foreach ( var page in pages )
//    //        {
//    //            page.gameObject.SetActive( true );
//    //        }
//    //    }

//    //    if ( GUILayout.Button( "Deactivate All Pages" ) )
//    //    {
//    //        var pages = Resources.FindObjectsOfTypeAll<Page>();

//    //        foreach ( var page in pages )
//    //        {
//    //            page.gameObject.SetActive( false );
//    //        }
//    //    }

//    //}

//}
