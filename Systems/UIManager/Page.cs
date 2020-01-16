using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Page : MonoBehaviour
{
    public Panel [] panels;
    private Stack<Panel> previousPanels;
    private Panel currentPanel;
    public Panel panelDefault;
    private AnimationEvent animationEvent;
    private Animation animation;

    void Awake ()
    {
        Init();
        animation = GetComponent<Animation>();

        if ( animation )
        {
            animationEvent = new AnimationEvent();
            float time = animation.clip.length * .5f;
            animationEvent.time = time;
            animationEvent.functionName = "OnAnimationEvent";
            animation.clip.AddEvent( animationEvent );
        }
    }

    private void OnEnable ()
    {
        if ( panelDefault != null )
            ChangePanel( panelDefault );
    }

    void Init ()
    {
        if ( panels != null && panels.Length > 0 )
            currentPanel = panels [ 0 ];

        previousPanels = new Stack<Panel>();
    }

    public Panel GetPanel ( string id )
    {
        foreach ( var panel in panels )
        {
            if ( id == panel.id )
                return panel;
        }

        return null;
    }

    public void PreviousPanel ()
    {
        var previousPanel = previousPanels.Pop();
        currentPanel.Hide();
        previousPanel.Show();
        currentPanel = previousPanel;
    }

    public void ChangePanel ( Panel panel )
    {
        if ( !panels.Any( obj => obj.id == panel.id ) )
        {
            Debug.LogError( GetType().Name + ".cs  method id : ChangePage()" );
            return;
        }

        var previousPanel = currentPanel;
        currentPanel = panel;

        if ( previousPanel == currentPanel )
            return;

        previousPanel.Hide();
        currentPanel.Show();

        if ( previousPanels.Count > 3 ) // if out buffer is more than bufferSize dump the latest one
            previousPanels.Pop();

        previousPanels.Push( previousPanel );
    }


    public void Show ()
    {
        gameObject.SetActive( true );
    }
    public void Hide ()
    {
        gameObject.SetActive( false );
    }

    void OnAnimationEvent ()
    {

    }
}
