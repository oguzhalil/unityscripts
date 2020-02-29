using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeColliderGenerator : MonoBehaviour
{
    public Transform [] serialization = new Transform [ 0 ];
    public float height = 5f;
    public float width = 2f;

    public void AddNode ()
    {
#if UNITY_EDITOR
        LinkedList<Transform> linkedList = new LinkedList<Transform>( serialization );

        GameObject gameObject = ( GameObject ) PrefabUtility.InstantiatePrefab
            ( AssetDatabase.LoadAssetAtPath( "Assets/UnityScripts/Utility/NodeColliderGenerator/Node.prefab" , typeof( GameObject ) ) );

        gameObject.transform.SetParent( transform );
        gameObject.name = "Node " + linkedList.Count;

        //Vector3 position = linkedList.Last.pre ;
        bool bPositionable = false;// linkedList.Last != null && linkedList.Last.Previous != null;

        if ( bPositionable )
        {
            // a - b = b -> a
            Vector3 position = linkedList.Last.Value.position - linkedList.Last.Previous.Value.position;
            gameObject.transform.position = position;
        }
        else
        {
            gameObject.transform.position = linkedList.Last != null ? linkedList.Last.Value.position : Vector3.zero;
        }

        linkedList.AddLast( gameObject.transform );
        serialization = new Transform [ linkedList.Count ];
        linkedList.CopyTo( serialization , 0 );
        Selection.activeGameObject = gameObject;
#endif
    }

    public void Clear ()
    {
        foreach ( var item in serialization )
        {
            DestroyImmediate( item.gameObject );
        }

        serialization = new Transform [ 0 ];
    }

    public void ReArrange ()
    {
#if UNITY_EDITOR
        LinkedList<Transform> linkedList = new LinkedList<Transform>();

        foreach ( var item in serialization )
        {
            if ( item != null )
            {
                linkedList.AddLast( item );
            }
        }

        serialization = new Transform [ linkedList.Count ];
        linkedList.CopyTo( serialization , 0 );

#endif
    }

    public void Generate ()
    {
        LinkedList<Transform> linkedList = new LinkedList<Transform>( serialization );
        LinkedListNode<Transform> node = linkedList.First;

        while ( node != null )
        {

            if ( !node.Value.GetComponent<BoxCollider>() )
            {
                node.Value.gameObject.AddComponent<BoxCollider>();
            }

            BoxCollider current = node.Value.GetComponent<BoxCollider>();

            if ( node.Value.CompareTag( "NoCollider" ) )
            {
                current.enabled = false;
            }

            if ( node.Next == null )
            {
                break;
            }

            Vector3 displacement = node.Next.Value.position - node.Value.position;
            node.Value.transform.forward = displacement;
            current.size = new Vector3( width , height , displacement.magnitude );
            current.center = new Vector3( 0f , height * .5f , displacement.magnitude * .5f ); // current node towarding z axis must be mozitive

            // iterator
            node = node.Next;


        }

    }

    public void Remove ( int index )
    {
        LinkedList<Transform> linkedList = new LinkedList<Transform>( serialization );
        if ( linkedList.Remove( serialization [ index ] ) )
        {
            DestroyImmediate( serialization [ index ].gameObject );
            serialization = new Transform [ linkedList.Count ];
            linkedList.CopyTo( serialization , 0 );
        }
    }

    public void AddBefore ( int index )
    {
#if UNITY_EDITOR
        LinkedList<Transform> linkedList = new LinkedList<Transform>( serialization );
        LinkedListNode<Transform> node = linkedList.Find( serialization [ index ] );
        GameObject gameObject = ( GameObject ) PrefabUtility.InstantiatePrefab
           ( AssetDatabase.LoadAssetAtPath( "Assets/UnityScripts/Utility/NodeColliderGenerator/Node.prefab" , typeof( GameObject ) ) );
        gameObject.transform.Translate( node.Value.transform.position , Space.World );
        gameObject.transform.SetParent( transform );
        gameObject.name = "Node " + linkedList.Count;
        linkedList.AddBefore( node , gameObject.transform );
        serialization = new Transform [ linkedList.Count ];
        linkedList.CopyTo( serialization , 0 );
#endif

    }

    public void AddAfter ( int index )
    {
#if UNITY_EDITOR
        LinkedList<Transform> linkedList = new LinkedList<Transform>( serialization );
        LinkedListNode<Transform> node = linkedList.Find( serialization [ index ] );
        GameObject gameObject = ( GameObject ) PrefabUtility.InstantiatePrefab
           ( AssetDatabase.LoadAssetAtPath( "Assets/UnityScripts/Utility/NodeColliderGenerator/Node.prefab" , typeof( GameObject ) ) );
        gameObject.transform.Translate( node.Value.transform.position , Space.World );
        gameObject.transform.SetParent( transform );
        gameObject.name = "Node " + linkedList.Count;
        linkedList.AddAfter( node , gameObject.transform );
        serialization = new Transform [ linkedList.Count ];
        linkedList.CopyTo( serialization , 0 );
#endif
    }

    private void OnDrawGizmos ()
    {
        foreach ( var node in serialization )
        {
            if ( node )
            {
                Gizmos.DrawSphere( node.transform.position , width );
            }
        }
    }
}
