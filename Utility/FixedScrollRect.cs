
#if ENABLE_LEANTWEEN
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class FixedScrollRect : ScrollRect
{
    [Header( "Only Support One Direction" )]

    float contentSize;
    float step;

    new bool horizontal;
    new bool vertical;

    float from;

    LTDescr tween;

    float animationDuration = .25f;

    public int Value
    {
        get
        {
            if ( from == 0 )
                return -1;
            else if ( from == 1 )
                return 1;

            return 0;
        }
    }

    protected override void Awake ()
    {
        base.Awake();

        contentSize = content.transform.childCount - 1; // value 0.0 == first item so dont count the item at 0.0

        step = 1.0f / contentSize;

        horizontal = this.horizontalScrollbar != null;
        vertical = this.verticalScrollbar != null;

    }

    public void ToZero ()
    {
        if ( horizontal )
            horizontalScrollbar.value = 0f;

        else if ( vertical )
            verticalScrollbar.value = 0f;

        from = 0;
    }

    public void Scroll ( int direction )
    {
        float to = from + direction * step;

        to = Mathf.Clamp( to , 0f , 1f );

        if ( tween != null )
            LeanTween.cancelAll( true );

        tween = LeanTween.value( from , to , animationDuration ).setEaseInQuad().setOnUpdate( OnValueUpdate ).setOnComplete( () => { OnAC( to ); } );
    }

    void OnValueUpdate ( float value )
    {
        if ( horizontal )
            horizontalScrollbar.value = value;

        else if ( vertical )
            verticalScrollbar.value = value;

    }

    //public void Reset()
    //{
    //    if ( horizontal )
    //        horizontalScrollbar.value = 0f;

    //    else if ( vertical )
    //        verticalScrollbar.value = 0f;

    //}

    void OnAC ( float value )
    {
        from = value;
        //OnValueUpdate( from );

        tween = null;
    }
}
#endif