using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class BrowseForFolder
{
    // message from browser
    const int BFFM_INITIALIZED        = 1;
    const int BFFM_SELCHANGED         = 2;
    const int BFFM_VALIDATEFAILEDA    = 3;   // lParam:szPath ret:1(cont),0(EndDialog)
    const int BFFM_VALIDATEFAILEDW    = 4;   // lParam:wzPath ret:1(cont),0(EndDialog)
    const int BFFM_IUNKNOWN = 5;   // provides IUnknown to client. lParam: IUnknown*

    [DllImport( "shell32.dll" )]
    static extern IntPtr SHBrowseForFolder ( ref BROWSEINFO lpbi );

    // Note that the BROWSEINFO object's pszDisplayName only gives you the name of the folder.
    // To get the actual path, you need to parse the returned PIDL
    [DllImport( "shell32.dll" , CharSet = CharSet.Unicode )]
    // static extern uint SHGetPathFromIDList(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)]
    //StringBuilder pszPath);
    static extern bool SHGetPathFromIDList ( IntPtr pidl , IntPtr pszPath );

    [DllImport( "user32.dll" , PreserveSig = true )]
    public static extern IntPtr SendMessage ( HandleRef hWnd , uint Msg , int wParam , IntPtr lParam );

    [DllImport( "user32.dll" , CharSet = CharSet.Auto )]
    public static extern IntPtr SendMessage ( HandleRef hWnd , int msg , int wParam , string lParam );

    private string _initialPath;

    public delegate int BrowseCallBackProc ( IntPtr hwnd , int msg , IntPtr lp , IntPtr wp );
    struct BROWSEINFO
    {
        public IntPtr hwndOwner;
        public IntPtr pidlRoot;
        public string pszDisplayName;
        public string lpszTitle;
        public uint ulFlags;
        public BrowseCallBackProc lpfn;
        public IntPtr lParam;
        public int iImage;
    }
    public int OnBrowseEvent ( IntPtr hWnd , int msg , IntPtr lp , IntPtr lpData )
    {
        //Debug.Log( msg );

        switch ( msg ) // needs to changed
        {
            case BFFM_INITIALIZED: // Required to set initialPath
            {
                //Win32.SendMessage(new HandleRef(null, hWnd), BFFM_SETSELECTIONA, 1, lpData);
                // Use BFFM_SETSELECTIONW if passing a Unicode string, i.e. native CLR Strings.
                SendMessage( new HandleRef( null , hWnd ) , 0x400 + 103 , 1 , _initialPath );
                break;
            }
            case BFFM_SELCHANGED:
            {
                IntPtr pathPtr = Marshal.AllocHGlobal( ( int ) ( 260 * Marshal.SystemDefaultCharSize ) );
                if ( SHGetPathFromIDList( lp , pathPtr ) )
                    SendMessage( new HandleRef( null , hWnd ) , 0x0400 + 104 , 0 , pathPtr );
                Marshal.FreeHGlobal( pathPtr );
                break;
            }
        }

        return 0;
    }

    public string SelectFolder ( string caption , string initialPath , IntPtr parentHandle )
    {
        _initialPath = initialPath;
        StringBuilder sb = new StringBuilder( 256 );
        IntPtr bufferAddress = Marshal.AllocHGlobal( 256 ); ;
        IntPtr pidl = IntPtr.Zero;
        BROWSEINFO bi = new BROWSEINFO();
        bi.hwndOwner = parentHandle;
        bi.pidlRoot = IntPtr.Zero;
        bi.lpszTitle = caption;
        bi.ulFlags = 0x00000040 | 0x00008000;
        bi.lpfn = new BrowseCallBackProc( OnBrowseEvent );
        bi.lParam = IntPtr.Zero;
        bi.iImage = 0;

        try
        {
            pidl = SHBrowseForFolder( ref bi );
            if ( true != SHGetPathFromIDList( pidl , bufferAddress ) )
            {
                return null;
            }
            sb.Append( Marshal.PtrToStringAuto( bufferAddress ) );
        }
        finally
        {
            // Caller is responsible for freeing this memory.
            Marshal.FreeCoTaskMem( pidl );
        }

        return sb.ToString();
    }
}