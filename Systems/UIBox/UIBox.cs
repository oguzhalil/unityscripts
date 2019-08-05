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

public enum Pages { Homepage = 0, Company = 1, Spin = 2, Rank = 3, DailyGift = 4, Garage = 5, Map = 6 , Store = 7 , Settings = 8, Currency = 9  }