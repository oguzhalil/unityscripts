//using PlayFab;
//using PlayFab.ServerModels;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace PabloGames
//{
//    public class PGSessionApprover : MonoBehaviour
//    {
//        //@NOTE : Facebook authenticate activity takes away focus from us!
//        void OnApplicationFocus ( bool hasFocus )
//        {
//            if ( hasFocus )
//                Run();
//        }

//        private void OnApplicationPause ( bool pause )
//        {
//            if ( !pause )
//                Run();
//        }

//        void Run ()
//        {
//            if ( Bridge.authentication.loginResult != null )
//                PlayFabServerAPI.AuthenticateSessionTicket(
//                       new PlayFab.ServerModels.AuthenticateSessionTicketRequest() { SessionTicket = Bridge.authentication.loginResult.SessionTicket } , SessionTrue , SessionFalse );
//        }

//        void SessionTrue ( AuthenticateSessionTicketResult result )
//        {

//            Debug.Log( GetType().Name + ".cs " + " session is true." );
//        }

//        void SessionFalse ( PlayFabError error )
//        {
//            Report.FatalError();
//        }

//        private void OnEnable ()
//        {
//            Run();
//        }


//    }
//}
