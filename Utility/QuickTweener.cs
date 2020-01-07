using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTweener : MonoBehaviour
{
    public enum Type
    {
        HearthBeat
    }

    public float duration;
    public bool loop;

    [Range( 0f , 2f )]
    public float multiplier;

    private void Start ()
    {
        LeanTween.scale( gameObject , Vector3.one * multiplier , duration ).setLoopPingPong(0).setEaseInOutQuad();
    }
}
