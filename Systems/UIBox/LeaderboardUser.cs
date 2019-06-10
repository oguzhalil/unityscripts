using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUser : MonoBehaviour
{
    public Text labelPosition;
    public Text labelPoints;
    public Text labelName;
    public Image imgPicture;

    public void UpdateBoard( int position , int prize , string name , Sprite picture = null )
    {
        labelPosition.text = position.ToString();
        labelPoints.text = prize.ToString();
        labelName.text = name;

        UpdatePicture( picture );
    }

    void UpdatePicture( Sprite sprite )
    {
        if ( sprite == null )
        {
            imgPicture.gameObject.SetActive( false );
            return;
        }

        imgPicture.sprite = sprite;
        imgPicture.gameObject.SetActive( true );

    }
}
