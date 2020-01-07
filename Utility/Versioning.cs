using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

public class Versioning : MonoBehaviour
{
    Text m_tx;

    static TextAsset m_textAsset;

    private void Start ()
    {
        m_tx = GetComponent<Text>();
        m_tx.text = "V" + Application.version;

        if ( m_textAsset == null )
        {
            m_textAsset = Resources.Load<TextAsset>( "version" );
        }

        if ( m_textAsset )
        {
            m_tx.text = "V" + m_textAsset.text;
        }
    }

    //[ContextMenu( "Test" )]
    //public void Test ()
    //{
    //    BuildPreProcessor.Test();
    //    //string version = BuildPreProcessor.IncrementVersion();
    //    //int bundleVersion = PlayerSettings.Android.bundleVersionCode;
    //    ////PlayerSettings.Android.bundleVersionCode = ( bundleVersion + 1 );
    //    ////PlayerSettings.bundleVersion = version;
    //    //Debug.Log( version + " " + ( bundleVersion + 1 ) );
    //}

#if UNITY_EDITOR

    class BuildPreProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public const string m_fileName = "/Resources/version.txt";

        public void OnPreprocessBuild ( BuildReport report )
        {
            string assetPath = Application.dataPath + m_fileName;
            bool bExist = System.IO.File.Exists( assetPath );
            string version = IncrementVersion();

            if ( bExist )
            {
                // The using statement automatically flushes AND CLOSES the stream and calls 
                using ( StreamWriter file = new StreamWriter( assetPath ) )
                {
                    file.Write( version );
                }
            }
            else
            {
                using ( StreamWriter file = File.CreateText( assetPath ) )
                {
                    file.Write( version );
                }
            }

            PlayerSettings.bundleVersion = version;

            if ( report.summary.platform == BuildTarget.Android )
            {
                PlayerSettings.Android.bundleVersionCode += 1;
            }
            else if ( report.summary.platform == BuildTarget.iOS )
            {
                PlayerSettings.iOS.buildNumber += 1;
            }

            Debug.Log( $"BuildPath {report.summary.outputPath} BuildTarget {report.summary.platform} Version {version} " );
        }
        public static void Test ()
        {
            string assetPath = Application.dataPath + m_fileName;
            Debug.Log( assetPath );
            bool bExist = System.IO.File.Exists( assetPath );
            string version = IncrementVersion();

            if ( bExist )
            {
                // The using statement automatically flushes AND CLOSES the stream and calls 
                using ( StreamWriter file = new StreamWriter( assetPath ) )
                {
                    file.Write( version );
                }
            }
            else
            {
                using ( StreamWriter file = File.CreateText( assetPath ) )
                {
                    file.Write( version );
                }
            }

            PlayerSettings.bundleVersion = version;

            Debug.Log( $"" );
        }


        public static string IncrementVersion ()
        {
            string [] lines = PlayerSettings.bundleVersion.Split( '.' );

            if ( lines.Length < 3 )
            {
                Debug.Log( "Version is wrong." );
            }

            int major = int.Parse( lines [ 0 ] );
            int minor = int.Parse( lines [ 1 ] );
            int build = int.Parse( lines [ 2 ] );
            //int androidBundleCode = PlayerSettings.Android.bundleVersionCode;

            build++;
            //androidBundleCode++;

            return $"{major}.{minor}.{build}";//.{androidBundleCode}";

            //for ( int i = 0; i < lines.Length; i++ )
            //{
            //    string str = lines [ i ];
            //    version += new string( '.' , (int)Mathf.Clamp01( i ) ) + str;
            //}
            //Debug.Log( version );
            //return version;
        }
    }

    //public class PostProcessBuild
    //{
    //    const string m_fileName = "\version.txt";

    //    [PostProcessBuild( 1 )]
    //    public static void OnPostProcessBuild ( BuildTarget target , string pathToBuildProject )
    //    {
    //        string buildPath = pathToBuildProject + m_fileName;
    //        string assetPath = Application.dataPath + m_fileName;

    //        bool bExist = System.IO.File.Exists( assetPath );

    //        if ( bExist )
    //        {
    //            // The using statement automatically flushes AND CLOSES the stream and calls 
    //            using ( StreamWriter file = new StreamWriter( assetPath ) )
    //            {
    //                file.Write( IncrementVersion() );
    //            }
    //        }

    //        Debug.Log( $"" );
    //    }

    //    static string IncrementVersion ()
    //    {
    //        string majorVersion = Application.version;
    //        string buildVersion = PlayerSettings.bundleVersion;

    //        return "";
    //    }
    //}

#endif

}
