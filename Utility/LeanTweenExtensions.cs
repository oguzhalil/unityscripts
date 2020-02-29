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

    public static void AnimateTextWithAbbreviation( this Text tx , int from , int to , float time = 1.0f )
    {
        LeanTween.value( from , to , time )
            .setOnUpdate( ( float f ) =>
            {
                tx.text = ( ( int ) f ).ToCoinValues();
            } )
             .setOnComplete( () =>
             {
                 tx.text = ( ( int ) to ).ToCoinValues();
             } );
    }

    public static string ToCoinValues ( this int value )
    {
        if ( value > 999999999 || value < -999999999 )
        {
            return value.ToString( "0,,,.###B" , System.Globalization.CultureInfo.InvariantCulture );
        }
        else if ( value > 999999 || value < -999999 )
        {
            return value.ToString( "0,,.##M" , System.Globalization.CultureInfo.InvariantCulture );
        }
        else
        {
            return value.ToString( "n0" );
        }
    }
}
