using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtilityScripts;

public class PreviousPanel : MonoBehaviour
{
    private void Start ()
    {
        var button = GetComponent<Button>();
        //button.onClick.RemoveAllListeners();

        button.onClick.AddListener( () => { UIManager.Instance.currentPage.PreviousPanel(); } );
    }
}
