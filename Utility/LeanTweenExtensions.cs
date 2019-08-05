using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.UI;

public static class LeanTweenExtensions
{
    public static void AnimateText ( this Text tx , int from , int to , float time = 1.0f)
    {
        LeanTween.value( from , to , time )
            .setOnUpdate( ( float f ) =>
             {
                 tx.text = ( ( int ) f ).ToString();
             } )
             .setOnComplete( () => 
             {
                 tx.text = ( ( int ) to ).ToString();
             } );
             
    }
}
