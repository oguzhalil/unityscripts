using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LauncherTask  
{
    public abstract bool IsDone ();
    public abstract bool IsError ();
    public abstract void Run ();
    public bool m_bRunning;
}

