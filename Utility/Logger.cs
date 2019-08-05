using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class Logger
{
    [Conditional( "ENABLE_LOGS" )]
    public static void Info ( string str )
    {
        UnityEngine.Debug.Log( str );
    }

    [Conditional( "ENABLE_LOGS" )]
    public static void Error ( string str )
    {
        UnityEngine.Debug.LogError( str );
    }

    [Conditional( "ENABLE_LOGS" )]
    public static void Warning ( string str )
    {
        UnityEngine.Debug.LogWarning( str );
    }
}
