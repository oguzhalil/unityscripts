using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Assigns unique renderqueue parameters to all materials in the scene.
     */

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UtilityScripts
{
    public class RenderQueueManager : MonoBehaviour
    {
        public bool m_IncludeInActive;
        public Material [] m_Materials;
        private string m_MaterialTag = "RenderType";

        public int minGeometry = 2000;
        public int minAlphaTest = 2450;
        public int minTransparent = 3000;
        public void SortQueue ()
        {
            int geometry = minGeometry;
            int alphaTest = minAlphaTest;
            int transparent = minTransparent;

            for ( int i = 0; i < m_Materials.Length; i++ )
            {
                Material material = m_Materials [ i ];

                if ( material == null )
                    continue;

                int renderQueue = material.renderQueue;

                if ( renderQueue >= minGeometry && renderQueue < minAlphaTest )
                {
                    if ( geometry + 1 >= alphaTest )
                    {
                        Debug.Log( "We cant sort this queue " + material );
                        continue;
                    }

                    material.renderQueue = ++geometry;

                    Debug.Log( "Sorted " + material.renderQueue + " " + material.name );
                }

                else if ( renderQueue >= minAlphaTest && renderQueue < minTransparent )
                {
                    if ( alphaTest + 1 >= minTransparent )
                    {
                        Debug.Log( "We cant sort this queue " + material );
                        continue;
                    }

                    material.renderQueue = ++alphaTest;
                }

                else if ( renderQueue >= minTransparent && renderQueue < minTransparent + 500 )
                {
                    if ( transparent + 1 >= minTransparent + 500 )
                    {
                        Debug.Log( "We cant sort this queue " + material );
                        continue;
                    }

                    material.renderQueue = ++transparent;
                }
            }
        }

        public void FindAllMaterialsInScene ( bool includeInActive )
        {
            List<MeshRenderer> meshRenderers = new List<MeshRenderer>( 100 );


            foreach ( MeshRenderer meshRenderer in Resources.FindObjectsOfTypeAll( typeof( MeshRenderer ) ) as MeshRenderer [] )
            {
                if ( meshRenderer.hideFlags == HideFlags.NotEditable || meshRenderer.hideFlags == HideFlags.HideAndDontSave ||
                    !meshRenderer.gameObject.activeInHierarchy && !includeInActive
                    )
                    continue;

                //if ( !EditorUtility.IsPersistent( MeshRenderer.transform.root.gameObject ) )
                //continue;

                meshRenderers.Add( meshRenderer );
            }

            List<Material> materials = new List<Material>( meshRenderers.Count );

            foreach ( var objects in meshRenderers )
            {
                var sharedMaterials = objects.sharedMaterials;

                foreach ( var material in sharedMaterials )
                {
                    if ( !materials.Contains( material ) )
                        materials.Add( material );
                }
            }

            m_Materials = materials.ToArray();
        }
    }


#if UNITY_EDITOR

    [CustomEditor( typeof( RenderQueueManager ) )]
    public class RenderQueueEditor : Editor
    {
        public SerializedProperty m_IncludeInActive;
        private RenderQueueManager m_RenderQueueManager;

        private void OnEnable ()
        {
            m_RenderQueueManager = target as RenderQueueManager;
        }

        public override void OnInspectorGUI ()
        {
            m_IncludeInActive = serializedObject.FindProperty( "m_IncludeInActive" );
            //EditorGUILayout.PropertyField( m_IncludeInActive );
            //serializedObject.ApplyModifiedProperties();

            if ( GUILayout.Button( "Find All Materials In Scene" , new GUILayoutOption [] { GUILayout.Width( 200 ) } ) )
            {
                m_RenderQueueManager.FindAllMaterialsInScene( m_IncludeInActive.boolValue );
            }

            if ( GUILayout.Button( "Sort Queue" , new GUILayoutOption [] { GUILayout.Width( 200 ) } ) )
            {
                m_RenderQueueManager.SortQueue();
            }

            DrawDefaultInspector();
        }
    }

#endif

}