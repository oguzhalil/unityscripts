//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using EboxGames;

//public class UIBox : Singleton<UIBox>
//{
//    [SerializeField] private Page currentPage;

//    Stack<Page> previousPages = new Stack<Page>();

//    public int bufferSize = 3;

//    public EnumPage pairPages;

//    public static ExposeElement GetExposure ( Page page , string elementId )
//    {
//        foreach ( var panel in page.panels )
//        {
//            foreach ( var exposedElement in panel.exposeElements )
//            {
//                if ( exposedElement.id == elementId )
//                    return exposedElement;
//            }
//        }

//        return null;

//    }

//    public void PreviousPage ()
//    {
//        var previousPage = previousPages.Pop();

//        currentPage.Hide();
//        previousPage.Show();

//        currentPage = previousPage;
//    }

//    public void ChangePage ( Page newPage )
//    {
//        var previousPage = currentPage;
//        currentPage = newPage;

//        previousPage.Hide();
//        currentPage.Show();

//        if ( previousPages.Count > bufferSize ) // if out buffer is more than bufferSize dump the latest one
//            previousPages.Pop();

//        previousPages.Push( previousPage );

//    }

//    public void ChangePage ( Pages page )
//    {
//        if ( !pairPages.ContainsKey( page ) )
//        {
//            Debug.LogError( GetType().Name + ".cs  method id : ChangePage()" );
//            return;
//        }

//        var previousPage = currentPage;
//        currentPage = pairPages [ page ];

//        if ( previousPage == currentPage )
//            return;

//        previousPage.Hide();
//        currentPage.Show();

//        if ( previousPages.Count > bufferSize ) // if out buffer is more than bufferSize dump the latest one
//            previousPages.Pop();

//        previousPages.Push( previousPage );
//    }

//    private void OnGUI ()
//    {
//        //GUILayout.Label( "Stack : " + previousPages.Count , GUI.skin.box );
//    }
//}

//public enum Pages { Homepage = 0, Customize = 1, Shop = 2, Profile = 3, Rooms = 4, Options = 5, Leaderboard = 6, LootBag = 7, Rewards = 8, Versus = 9, Friends = 10, Facebook = 11 }