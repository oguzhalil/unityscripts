using System.Diagnostics;
using UnityEngine;

using Debug = UnityEngine.Debug;

public static class Logger
{

    static Logger ()
    {
        Application.SetStackTraceLogType( LogType.Log , StackTraceLogType.None );
    }


    [Conditional( "ENABLE_LOGS" )]
    public static void Info ( string str )
    {
        UnityEngine.Debug.Log( str );
    }
}
