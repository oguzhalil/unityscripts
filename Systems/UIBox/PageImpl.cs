using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PageImpl : MonoBehaviour
{
    public Page m_Page;
    public abstract void VOnRestore (); // Restore page to default values
}
