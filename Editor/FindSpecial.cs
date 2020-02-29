using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class FindSpecial : EditorWindow
{
    enum Search
    {
        ByRadius = 0,
        ByHeight = 1,
        ByWidth = 2,
        ByName = 3,
        ByOutsideOfBound = 4,
        CenterParent = 5,
        DeactiveObjects = 6,
        HiddenObjects = 7
    }

    enum Space
    {
        Local,
        Global
    }

    enum Direction
    {
        NegativeX = -1,
        PositiveX = 1,
        NegativeY = -2,
        PositiveY = 2,
        NegativeZ = -3,
        PositiveZ = 3,
    }

    //private SnapTo around = SnapTo.BoundingBox;
    private Search search = Search.ByRadius;
    private Space space = Space.Global;
    private Direction dir = Direction.NegativeX;
    private int count;
    public bool useEpsilon;
    private bool randomizeYRotation;
    private bool randomizeScale;
    private bool randomizeSpacing;
    private float radius;
    private bool root = false;
    private List<GameObject> instantiatedObjects;
    private bool findEmptyObjects;
    private Bounds boundingBox;
    private BoxCollider boundingBoxObj;
    private bool IsGreater = false;

    [MenuItem( "Tools/Windows/Find Special" )]
    public static void ShowWindow ()
    {
        FindSpecial d = EditorWindow.GetWindow( typeof( FindSpecial ) ) as FindSpecial;
        //d.instantiatedObjects = new List<GameObject>();
    }

    private Bounds bounds;
    private GameObject referenceObject;

    void OnGUI ()
    {
        EditorGUILayout.LabelField( "Find Special" , EditorStyles.boldLabel );
        //around = ( SnapTo ) EditorGUILayout.EnumPopup( "Operation" , around );

        //EditorGUI.BeginChangeCheck();
        radius = EditorGUILayout.FloatField( "Radius" , radius );
        root = EditorGUILayout.Toggle( "Transform Root" , root );
        findEmptyObjects = EditorGUILayout.Toggle( "Find Empty Objects" , findEmptyObjects );
        search = ( Search ) EditorGUILayout.EnumPopup( "Search" , search );

        if ( search == Search.ByOutsideOfBound )
        {
            boundingBoxObj = EditorGUILayout.ObjectField( boundingBoxObj , typeof( BoxCollider ) , true ) as BoxCollider;
            if(boundingBoxObj)
            boundingBox = boundingBoxObj.bounds;
        }

        if(search == Search.ByRadius)
        {
            IsGreater = EditorGUILayout.Toggle( "Is Greater" , IsGreater );
        }
        //space = ( Space ) EditorGUILayout.EnumPopup( "Space" , space );
        //dir = ( Direction ) EditorGUILayout.EnumPopup( "Direction" , dir );
        //useEpsilon = EditorGUILayout.Toggle( "Use Epsilon" , useEpsilon );
        //randomizeYRotation = EditorGUILayout.Toggle( "Randomize Y Rotation" , randomizeYRotation );
        //randomizeScale = EditorGUILayout.Toggle( "Randomize Scale" , randomizeScale );
        //randomizeSpacing = EditorGUILayout.Toggle( "Randomize Spacing" , randomizeSpacing );
        //else
        //{
        //    radius = 0;
        //}

        //if ( EditorGUI.EndChangeCheck() )
        //{
        //    SceneView.RepaintAll();
        //}

        // greater than, less than,
        // 0.5 to 0.7 
        // string and type name bla bla bla...
        //Selection.GetFiltered

        count = EditorGUILayout.IntField( "Count" , count );

        if ( GUILayout.Button( "Find" ) )
        {
            switch ( search )
            {
                case Search.ByRadius:

                    //var renderes = ExtensionMethods.GetAllObjectsOnlyInSceneByType<MeshRenderer>();
                    List<Object> selections = new List<Object>();
                    if(Selection.gameObjects.Length > 0)
                    {
                        List<MeshRenderer> renderers = new List<MeshRenderer>( 10000 );

                        foreach ( var obj in Selection.gameObjects )
                        {
                            var f = obj.GetComponentsInChildren<MeshRenderer>( true );

                            foreach ( var item in f )
                            {
                                renderers.Add( item );
                            }
                        }


                        foreach ( var renderer in renderers )
                        {
                            if(IsGreater && renderer.bounds.size.magnitude >= radius * 2 )
                            {
                                selections.Add( renderer.gameObject );
                            }
                            else if (!IsGreater && renderer.bounds.size.magnitude <= radius * 2)
                            {
                                selections.Add( renderer.gameObject );
                            }
                        }

                        if(!IsGreater)
                        Debug.Log( $"FindSpecial : Found { selections.Count } object smaller than {radius * 2} diameter" );
                        else
                            Debug.Log( $"FindSpecial : Found { selections.Count } object greater than {radius * 2} diameter" );

                        Selection.objects = selections.ToArray();
                    }
                    
                    break;
                case Search.ByHeight:
                    break;
                case Search.ByWidth:
                    break;
                case Search.ByName:

                    break;
                case Search.HiddenObjects:

                    var objects = ExtensionMethods.GetAllObjectsOnlyInScene();

                    List<GameObject> sele = new List<GameObject>();

                    foreach ( var obj in objects )
                    {
                        if(obj.hideFlags == HideFlags.HideInHierarchy)
                        {
                            Debug.Log( obj.name );
                            sele.Add( obj );
                        }
                    }

                    Selection.objects = sele.ToArray();

                    break;
                case Search.ByOutsideOfBound:

                    List<GameObject> gameObjects = ExtensionMethods.GetAllObjectsOnlyInScene();
                    List<GameObject> outofBounds = new List<GameObject>( gameObjects.Count );
                    foreach ( var obj in gameObjects )
                    {
                        //MeshRenderer m = obj.GetComponent<MeshRenderer>();
                        //if(m && !boundingBox.Contains( m.bounds.center))
                        if( !boundingBox.Contains( obj.transform.position ) )
                        {
                            outofBounds.Add( obj.gameObject );
                        }
                    }

                    Selection.objects = outofBounds.ToArray();
                    return;
                    break;
                case Search.CenterParent:
                    if ( Selection.objects.Length > 0 )
                    {
                        foreach ( Object r in Selection.objects )
                        {
                            Transform root = ( ( GameObject ) r ).transform;
                            if ( root.GetComponent<MeshRenderer>() && root.parent != null )
                            {
                                Vector3 position = root.position;
                                root.parent.SetPositionAndRotation( root.GetComponent<MeshRenderer>().bounds.center , root.parent.transform.rotation );
                                root.SetPositionAndRotation( position , root.rotation );
                                Debug.Log( "Centered" );
                            }
                        }

                        return;
                    }
                    break;
                default:
                    break;
            }

            //if ( Selection.objects.Length > 0 )
            //{
            //    foreach ( Object r in Selection.objects )
            //    {
            //        Transform root = ( ( GameObject ) r ).transform;
            //        if ( root.GetComponent<MeshRenderer>() && root.parent != null )
            //        {
            //            Vector3 position = root.position;
            //            root.parent.SetPositionAndRotation( root.GetComponent<MeshRenderer>().bounds.center , root.parent.transform.rotation );
            //            root.SetPositionAndRotation( position , root.rotation );
            //            Debug.Log( "Centered" );
            //        }
            //    }

            //    return;
            //}

            //if ( Selection.activeGameObject )
            //{
            //    Transform root = Selection.activeGameObject.transform;
            //    List<Object> objects = new List<Object>( 100 );

            //    if ( root.GetComponent<MeshRenderer>() )
            //    {
            //        Vector3 position = root.position;
            //        root.parent.SetPositionAndRotation( root.GetComponent<MeshRenderer>().bounds.center , root.parent.transform.rotation );
            //        root.SetPositionAndRotation( position , root.rotation );
            //        Debug.Log( "Centered" );
            //    }

            //    //foreach ( Transform child in root )
            //    //{
            //    //    if ( child.childCount == 1 && child.GetChild( 0 ).GetComponent<MeshRenderer>() )
            //    //    {
            //    //        Vector3 localPos = child.GetChild( 0 ).transform.localPosition;
            //    //        child.SetPositionAndRotation( child.GetChild( 0 ).GetComponent<MeshRenderer>().bounds.center , child.transform.rotation );
            //    //        child.GetChild( 0 ).transform.localPosition = localPos;
            //    //        Debug.Log( "Centered" );
            //    //        //= child.GetChild( 0 ).GetComponent<MeshRenderer>().bounds.center;
            //    //    }
            //    //    //if ( child.childCount == 0 || child.GetComponentsInChildren<MeshRenderer>().Length == 0 )
            //    //    //{
            //    //    //    objects.Add( child.gameObject );
            //    //    //}
            //    //}

            //    return;

            //    if ( findEmptyObjects )
            //    {
            //        foreach ( Transform child in root )
            //        {
            //            if ( child.childCount == 0 || child.GetComponentsInChildren<MeshRenderer>().Length == 0 )
            //            {

            //                objects.Add( child.gameObject );
            //            }
            //        }

            //        Selection.objects = objects.ToArray();
            //    }

               

            //    //Selection.set


            //    //instantiatedObjects.Clear();
            //    //Transform selection = Selection.activeGameObject.transform;
            //    //referenceObject = selection.gameObject;

            //    //if ( space == Space.Global )
            //    //{

            //    //    int index = Mathf.Abs( ( int ) dir );

            //    //    Vector3 vDir = indexToDirection( index );
            //    //    bounds = new Bounds( selection.position , Vector3.zero );
            //    //    MeshRenderer [] renderers = selection.GetComponentsInChildren<MeshRenderer>();
            //    //    foreach ( var renderer in renderers )
            //    //    {
            //    //        bounds.Encapsulate( renderer.bounds );
            //    //    }
            //    //    float f = bounds.extents [ Mathf.Abs( ( int ) dir ) - 1 ] * 2;

            //    //    if ( useEpsilon )
            //    //    {
            //    //        f -= Mathf.Epsilon;
            //    //    }

            //    //    for ( int i = 1; i <= count; i++ )
            //    //    {
            //    //        Vector3 position = selection.position + vDir * f * i;
            //    //        Quaternion rotation = selection.rotation;
            //    //        Vector3 scale = selection.localScale;

            //    //        if ( randomizeSpacing )
            //    //        {
            //    //            position = selection.position + vDir * i * radius;
            //    //            Vector2 rndCircle = UnityEngine.Random.insideUnitCircle * radius * .5f;
            //    //            position += new Vector3( rndCircle.x , 0f , rndCircle.y );
            //    //        }

            //    //        if ( randomizeYRotation )
            //    //        {
            //    //            rotation = Quaternion.AngleAxis( UnityEngine.Random.value * 360f , Vector3.up ) * Quaternion.AngleAxis( UnityEngine.Random.value * 10f , Vector3.right ) * Quaternion.AngleAxis( UnityEngine.Random.value * 10f , Vector3.forward );
            //    //        }
            //    //        if ( randomizeScale )
            //    //        {
            //    //            scale = new Vector3( UnityEngine.Random.Range( 1f , 1.5f ) , UnityEngine.Random.Range( 1f , 1.5f ) , UnityEngine.Random.Range( 1f , 1.5f ) );
            //    //        }

            //    //        if ( PrefabUtility.IsPartOfAnyPrefab( selection.gameObject ) )
            //    //        {
            //    //            GameObject go = ( GameObject ) PrefabUtility.InstantiatePrefab( AssetDatabase.LoadAssetAtPath(
            //    //               PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot( Selection.activeObject ) , typeof( GameObject ) ) , selection.parent );
            //    //            go.transform.position = position;
            //    //            go.transform.rotation = rotation;
            //    //            go.transform.localScale = scale;
            //    //            instantiatedObjects.Add( go );
            //    //        }
            //    //        else
            //    //        {
            //    //            GameObject go = Instantiate( selection.gameObject , position , rotation );
            //    //            go.transform.SetParent( selection.parent , true );
            //    //            go.transform.localScale = scale;
            //    //            instantiatedObjects.Add( go );
            //    //        }

            //    //    }
            //    //}
            //    //else if ( space == Space.Local )
            //    //{

            //    //}

            //}
            //else
            //{
            //    Debug.LogError( "DuplicateSpecial : Selected object is null." );
            //}
        }

        if ( GUILayout.Button( "Undo" ) )
        {
            instantiatedObjects.ForEach( x => UnityEngine.Object.DestroyImmediate( x ) );
        }
    }

    public Vector3 indexToDirection ( int index )
    {
        switch ( index )
        {
            case 1:
                return Vector3.right * Mathf.Sign( ( int ) dir );
            case 2:
                return Vector3.up * Mathf.Sign( ( int ) dir );
            case 3:
                return Vector3.forward * Mathf.Sign( ( int ) dir );
            default:
                return Vector3.zero * Mathf.Sign( ( int ) dir );
        }
    }

    // Window has been selected
    void OnFocus ()
    {
        // Remove delegate listener if it has previously
        // been assigned.
        // Add (or re-add) the delegate.
        SceneView.duringSceneGui -= this.OnSceneGUI;
        SceneView.duringSceneGui += this.OnSceneGUI;

        if ( instantiatedObjects == null )
        {
            instantiatedObjects = new List<GameObject>();
        }
    }

    void OnDestroy ()
    {
        // When the window is destroyed, remove the delegate
        // so that it will no longer do any drawing.
        SceneView.duringSceneGui -= this.OnSceneGUI;
    }

    void OnSceneGUI ( SceneView sceneView )
    {
        if ( Event.current.type == EventType.Repaint )
        {
            if ( Selection.activeGameObject && Selection.activeGameObject.GetComponentsInChildren<MeshRenderer>().Length > 0 )
            {
                var vector = indexToDirection( Mathf.Abs( ( int ) dir ) );
                //Debug.Log( vector );
                Handles.color = Color.white;
                MeshRenderer [] renderers = Selection.activeGameObject.GetComponentsInChildren<MeshRenderer>();
                Bounds bounds = new Bounds( Selection.activeGameObject.transform.position , Vector3.zero );
                foreach ( var renderer in renderers )
                {
                    bounds.Encapsulate( renderer.bounds );
                }
                Handles.CircleHandleCap( 0 , Selection.activeGameObject.transform.position , Quaternion.AngleAxis( -90f , Vector3.right ) , radius , EventType.Repaint );
                Vector3 pos = bounds.center + Vector3.up * 3.0f;
                Handles.ArrowHandleCap( 0 , pos , Quaternion.FromToRotation( Vector3.forward , vector ) , 3.0f , EventType.Repaint );
                //Handles.ArrowHandleCap( 0 , Selection.activeGameObject.transform.position , Quaternion.LookRotation( -Vector3.right , Vector3.up ) , 1f , EventType.Repaint );

            }
        }
    }
}
