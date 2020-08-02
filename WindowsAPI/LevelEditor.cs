using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
//using System.Windows.Forms;
using UnityEngine;

// https://docs.microsoft.com/en-us/dotnet/standard/native-interop/best-practices

public class LevelEditor : MonoBehaviour
{
    [DllImport( "user32.dll" )] static extern IntPtr GetForegroundWindow ();

    [DllImport( "user32.dll" , EntryPoint = "MoveWindow" )]
    static extern int MoveWindow ( int hwnd , int x , int y , int nWidth , int nHeight , int bRepaint );

    [DllImport( "user32.dll" , EntryPoint = "SetWindowLongA" )]
    static extern int SetWindowLong ( int hwnd , int nIndex , int dwNewLong );

    [DllImport( "user32.dll" )]
    static extern bool ShowWindowAsync ( int hWnd , int nCmdShow );

    [DllImport( "user32.dll" )]
    static extern int MessageBox ( IntPtr hwnd , string lpText , string lpCaption , long type );

    [DllImport( "Shell32.dll" )]
    static extern IntPtr SHBrowseForFolderW ( ref _browseinfoW info );

    [DllImport( "shell32.dll" , CharSet = CharSet.Unicode )]
    // static extern uint SHGetPathFromIDList(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)]
    //StringBuilder pszPath);
    static extern bool SHGetPathFromIDList ( IntPtr pidl , IntPtr pszPath );

    [DllImport( "Comdlg32.dll" , CharSet = CharSet.Auto )]
    public static extern bool GetOpenFileName ( [In, Out] OpenFileName ofn );

    IntPtr handle;
    BrowseForFolder browser;
    private string initialPath;


    [DllImport( "Comdlg32.dll" , CharSet = CharSet.Auto , SetLastError = true )]
    private static extern bool GetOpenFileName ( ref OpenFileName ofn );

    void Start ()
    {
        browser = new BrowseForFolder();
        handle = GetForegroundWindow();
        initialPath = @"C:\Users\oguz\Desktop\";
        //#if UNITY_EDITOR
        //int fWidth = Screen.width;
        //int fHeight = Screen.height;
        //MoveWindow( handle , 2000 , 0 , fWidth , fHeight , 1 ); // move the Unity Projet windows >>> 2000,0 Secondary monitor ;)

        //ShowWindowAsync( handle , 3 ); // full screen  // SW_SHOWMAXIMIZED

        ////MessageBox( handle , "Hello From Windows Api" , "Title Goes Here" , 0x00000010L );
        //var info = new _browseinfoW();
        //info.hwnd = handle;
        //info.root = IntPtr.Zero;
        ////info.root = Application.dataPath;
        //info.displayName = "Select Folder";
        //info.callback = new BrowseCallbackProc( OnBrowseCallbackProc );
        //info.flags = 0x00000040;
        //info.lParam = IntPtr.Zero;
        //info.image = 0;

        //StringBuilder sb = new StringBuilder( 256 );
        //IntPtr bufferAddress = Marshal.AllocHGlobal( 256 ); ;

        //var pointer = SHBrowseForFolderW( ref info );
        //if ( SHGetPathFromIDList( pointer , bufferAddress ) )
        //{
        //    sb.Append( Marshal.PtrToStringAuto( pointer ) );
        //}

        //Debug.Log( sb.ToString() );



        //OpenFileName ofn = new OpenFileName();
        //ofn.structSize = Marshal.SizeOf( ofn );
        //ofn.filter = "Image Files\0*.jpg\0Batch files\0*.bat\0";
        //ofn.file = new String( new char [ 256 ] );
        //ofn.maxFile = ofn.file.Length;
        //ofn.fileTitle = new String( new char [ 64 ] );
        //ofn.maxFileTitle = ofn.fileTitle.Length;
        //ofn.initialDir = "C:\\";
        //ofn.title = "Open file called using platform invoke...";
        //ofn.defExt = "txt";
        //ofn.flags = 0x00000200 | 0x00000001;
        //var dialog = new OpenFileDialog();
        //dialog.Multiselect = true;
        //dialog.ShowDialog();

       // const string message =
       //"Are you sure that you would like to close the form?";
       // const string caption = "Form Closing";
       // var result = System.Windows.Forms.MessageBox.Show( message , caption ,
       //                              MessageBoxButtons.YesNo ,
       //                              MessageBoxIcon.Question );

        //if ( GetOpenFileName( ofn ) )
        //{
        //    Debug.LogFormat( "Selected file with full path: {0}" , ofn.file );
        //    Debug.LogFormat( "Selected file name: {0}" , ofn.fileTitle );
        //    Debug.LogFormat( "Offset from file name: {0}" , ofn.fileOffset );
        //    Debug.LogFormat( "Offset from file extension: {0}" , ofn.fileExtension );
        //}
    }

    public void LoadAlpha()
    {
        string folder = browser.SelectFolder( "Hello" , initialPath , handle );
        Debug.Log( folder );
        initialPath = folder;
    }

    public void LoadAnimationsFiles ()
    {
        string folder = browser.SelectFolder( "Hello" , initialPath , handle );
        Debug.Log( folder );
        initialPath = folder;
    }

    public void LoadColoredFile ()
    {
        string folder = browser.SelectFolder( "Hello" , initialPath , handle );
        Debug.Log( folder );
        initialPath = folder;
    }

    public void LoadGreyScale ()
    {
        string folder = browser.SelectFolder( "Hello" , initialPath  , handle );
        Debug.Log( folder );
        initialPath = folder;
    }

    int OnBrowseCallbackProc ( IntPtr hwnd , uint msg , IntPtr lParam , IntPtr lData )
    {
        Debug.Log( msg );
        return 1;
    }
}


[StructLayout( LayoutKind.Sequential )]
public struct _browseinfoW
{
    public IntPtr hwnd;
    public IntPtr root;
    public string displayName;
    public uint flags;
    public BrowseCallbackProc callback;
    public IntPtr lParam;
    public int image;
}

public delegate int BrowseCallbackProc ( IntPtr hwnd , uint msg , IntPtr lParam , IntPtr lData );

//typedef struct _browseinfoW
//{
//    HWND hwndOwner; // IntPtr
//    PCIDLIST_ABSOLUTE pidlRoot; // string
//    LPWSTR pszDisplayName; // string
//    LPCWSTR lpszTitle; // string
//    UINT ulFlags; // uint
//    BFFCALLBACK lpfn; // delegate
//    LPARAM lParam; // IntPtr
//    int iImage; // int
//} BROWSEINFOW, * PBROWSEINFOW, * LPBROWSEINFOW;

// typedef int ( CALLBACK *BrowseCallbackProc)(
//HWND hwnd,
//UINT   uMsg,
//   LPARAM lParam,
//   LPARAM lpData
//);

    // Copyright
// Microsoft Corporation
// All rights reserved

// OpenFileDlg.cs

/*
typedef struct tagOFN { 
  DWORD         lStructSize; 
  HWND          hwndOwner; 
  HINSTANCE     hInstance; 
  LPCTSTR       lpstrFilter; 
  LPTSTR        lpstrCustomFilter; 
  DWORD         nMaxCustFilter; 
  DWORD         nFilterIndex; 
  LPTSTR        lpstrFile; 
  DWORD         nMaxFile; 
  LPTSTR        lpstrFileTitle; 
  DWORD         nMaxFileTitle; 
  LPCTSTR       lpstrInitialDir; 
  LPCTSTR       lpstrTitle; 
  DWORD         Flags; 
  WORD          nFileOffset; 
  WORD          nFileExtension; 
  LPCTSTR       lpstrDefExt; 
  LPARAM        lCustData; 
  LPOFNHOOKPROC lpfnHook; 
  LPCTSTR       lpTemplateName; 
#if (_WIN32_WINNT >= 0x0500)
  void *        pvReserved;
  DWORD         dwReserved;
  DWORD         FlagsEx;
#endif // (_WIN32_WINNT >= 0x0500)
} OPENFILENAME, *LPOPENFILENAME; 
*/

[StructLayout( LayoutKind.Sequential , CharSet = CharSet.Auto )]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;

    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;

    public String file = null;
    public int maxFile = 0;

    public String fileTitle = null;
    public int maxFileTitle = 0;

    public String initialDir = null;

    public String title = null;

    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;

    public String defExt = null;

    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;

    public String templateName = null;

    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

