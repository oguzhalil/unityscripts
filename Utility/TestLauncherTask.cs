using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLauncherTask : LauncherTask
{
    private bool m_IsDone;

    private void Start ()
    {
        Invoke( "PrintHelloWorld" , 2.0f );
    }

    private void PrintHelloWorld()
    {
        Debug.Log( "HelloWorld." );
        m_IsDone = true;
    }

    public override bool IsDone ()
    {
        return m_IsDone;
    }
}
