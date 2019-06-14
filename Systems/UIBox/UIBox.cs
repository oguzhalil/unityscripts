using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EboxGames;

public class UIBox : Singleton<UIBox>
{
    [SerializeField] private Page currentPage;
    private Stack<Page> previousPages = new Stack<Page>();
    public int bufferSize = 3;
    public EnumMonoDictionary m_PairPages;

    public static UIElem GetExposure ( Page page , string elementId )
    {
        foreach ( var panel in page.panels )
        {
            foreach ( var exposedElement in panel.elems )
            {
                if ( exposedElement.id == elementId )
                    return exposedElement;
            }
        }

        return null;

    }

    public void PreviousPage ()
    {
        var previousPage = previousPages.Pop();

        currentPage.Hide();
        previousPage.Show();

        currentPage = previousPage;
    }

    public void ChangePage ( Page newPage )
    {
        var previousPage = currentPage;
        currentPage = newPage;

        previousPage.Hide();
        currentPage.Show();

        if ( previousPages.Count > bufferSize ) // if out buffer is more than bufferSize dump the latest one
            previousPages.Pop();

        previousPages.Push( previousPage );

    }

    public void ChangePage ( Pages page )
    {
        if ( !m_PairPages.ContainsKey( page ) )
        {
            Debug.LogError( GetType().Name + ".cs  method id : ChangePage()" );
            return;
        }

        var previousPage = currentPage;
        currentPage = m_PairPages [ page ] as Page;

        if ( previousPage == currentPage )
            return;

        previousPage.Hide();
        currentPage.Show();

        if ( previousPages.Count > bufferSize ) // if out buffer is more than bufferSize dump the latest one
            previousPages.Pop();

        previousPages.Push( previousPage );
    }

    private void OnGUI ()
    {
        //GUILayout.Label( "Stack : " + previousPages.Count , GUI.skin.box );
    }

    public override bool DontDestroyWhenLoad ()
    {
        return false;
    }
}

public enum Pages { Homepage = 0, Customize = 1, Shop = 2, Profile = 3, Rooms = 4, Settings = 5, Leaderboard = 6, LootBag = 7, Rewards = 8, Versus = 9, Friends = 10, Facebook = 11 }