using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class OrientationDebugger : MonoBehaviour
{
    public Mesh arrow;
    public float size = 0.1f;

    private void Start ()
    {
        Logger.Info( "Abc" );
    }

    private void OnDrawGizmos ()
    {
        if ( name.Contains( "End" ) )
        {
            Gizmos.color = Color.red;
        }
        else
            Gizmos.color = Color.green;
        //DrawArrow.ForGizmo( transform.position , transform.forward );
//#if UNITY_EDITOR

//        Handles.Label( transform.position + Vector3.up , name.Substring( 0 , "Route ".Length ) [ 0 ].ToString() );
//             #endif
        Gizmos.DrawMesh( arrow , transform.position , transform.rotation , Vector3.one * size * GetGizmoSize( transform.position ) );
    }

    public static float GetGizmoSize ( Vector3 position )
    {
        Camera current = Camera.current;
        position = Gizmos.matrix.MultiplyPoint( position );

        if ( current )
        {
            Transform transform = current.transform;
            Vector3 position2 = transform.position;
            float z = Vector3.Dot( position - position2 , transform.TransformDirection( new Vector3( 0f , 0f , 1f ) ) );
            Vector3 a = current.WorldToScreenPoint( position2 + transform.TransformDirection( new Vector3( 0f , 0f , z ) ) );
            Vector3 b = current.WorldToScreenPoint( position2 + transform.TransformDirection( new Vector3( 1f , 0f , z ) ) );
            float magnitude = ( a - b ).magnitude;
            return 80f / Mathf.Max( magnitude , 0.0001f );
        }

        return 20f;
    }
}

public static class DrawArrow
{
    public static void ForGizmo ( Vector3 pos , Vector3 direction , float arrowHeadLength = 0.25f , float arrowHeadAngle = 20.0f )
    {
        Gizmos.DrawRay( pos , direction );

        Vector3 right = Quaternion.LookRotation( direction ) * Quaternion.Euler( 0 , 180 + arrowHeadAngle , 0 ) * new Vector3( 0 , 0 , 1 );
        Vector3 left = Quaternion.LookRotation( direction ) * Quaternion.Euler( 0 , 180 - arrowHeadAngle , 0 ) * new Vector3( 0 , 0 , 1 );
        Gizmos.DrawRay( pos + direction , right * arrowHeadLength );
        Gizmos.DrawRay( pos + direction , left * arrowHeadLength );
    }

    public static void ForGizmo ( Vector3 pos , Vector3 direction , Color color , float arrowHeadLength = 0.25f , float arrowHeadAngle = 20.0f )
    {
        Gizmos.color = color;
        Gizmos.DrawRay( pos , direction );

        Vector3 right = Quaternion.LookRotation( direction ) * Quaternion.Euler( 0 , 180 + arrowHeadAngle , 0 ) * new Vector3( 0 , 0 , 1 );
        Vector3 left = Quaternion.LookRotation( direction ) * Quaternion.Euler( 0 , 180 - arrowHeadAngle , 0 ) * new Vector3( 0 , 0 , 1 );
        Gizmos.DrawRay( pos + direction , right * arrowHeadLength );
        Gizmos.DrawRay( pos + direction , left * arrowHeadLength );
    }

    public static void ForDebug ( Vector3 pos , Vector3 direction , float arrowHeadLength = 0.25f , float arrowHeadAngle = 20.0f )
    {
        Debug.DrawRay( pos , direction );

        Vector3 right = Quaternion.LookRotation( direction ) * Quaternion.Euler( 0 , 180 + arrowHeadAngle , 0 ) * new Vector3( 0 , 0 , 1 );
        Vector3 left = Quaternion.LookRotation( direction ) * Quaternion.Euler( 0 , 180 - arrowHeadAngle , 0 ) * new Vector3( 0 , 0 , 1 );
        Debug.DrawRay( pos + direction , right * arrowHeadLength );
        Debug.DrawRay( pos + direction , left * arrowHeadLength );
    }
    public static void ForDebug ( Vector3 pos , Vector3 direction , Color color , float arrowHeadLength = 0.25f , float arrowHeadAngle = 20.0f )
    {
        Debug.DrawRay( pos , direction , color );

        Vector3 right = Quaternion.LookRotation( direction ) * Quaternion.Euler( 0 , 180 + arrowHeadAngle , 0 ) * new Vector3( 0 , 0 , 1 );
        Vector3 left = Quaternion.LookRotation( direction ) * Quaternion.Euler( 0 , 180 - arrowHeadAngle , 0 ) * new Vector3( 0 , 0 , 1 );
        Debug.DrawRay( pos + direction , right * arrowHeadLength , color );
        Debug.DrawRay( pos + direction , left * arrowHeadLength , color );
    }
}