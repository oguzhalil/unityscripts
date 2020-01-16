using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtilityScripts;

public class PrevPage : MonoBehaviour
{
    private void Start ()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener( () => { UIManager.Instance.PreviousPage(); } );
    }
}
