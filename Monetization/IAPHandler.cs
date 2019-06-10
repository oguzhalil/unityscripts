//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Purchasing;
//using UnityEngine.UI;

//public class IAPHandler : MonoBehaviour, IStoreListener
//{
//    private IStoreController controller;
//    private IExtensionProvider extensions;

//    const string premium = "premium";

//    const string coin_500 =  "coin_500";
//    const string coin_1100 = "coin_1100";
//    const string coin_2500 = "coin_2500";
//    const string coin_5800 = "coin_5800";
//    const string coin_8500 = "coin_8500";

//    const string premium_google = "com.strike.missionwar.premium";

//    const string coin_500_google = "com.strike.missionwar.coin_500";
//    const string coin_1100_google = "com.strike.missionwar.coin_1100";
//    const string coin_2500_google = "com.strike.missionwar.coin_2500";
//    const string coin_5800_google = "com.strike.missionwar.coin_5800";
//    const string coin_8500_google = "com.strike.missionwar.coin_8500";

//    [SerializeField] Text price_01;
//    [SerializeField] Text price_02;
//    [SerializeField] Text price_03;
//    [SerializeField] Text price_04;
//    [SerializeField] Text price_05;
//    [SerializeField] Text price_06;

//    // Constructor
//    void Start ()
//    {
//        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

//        builder.AddProduct( premium , ProductType.NonConsumable , new IDs
//        {
//            {premium_google, GooglePlay.Name},
//        } );

//        builder.AddProduct( coin_500 , ProductType.Consumable , new IDs
//        {
//            {coin_500_google , GooglePlay.Name }
//        } );

//        builder.AddProduct( coin_1100 , ProductType.Consumable , new IDs
//        {
//            {coin_1100_google , GooglePlay.Name }
//        } );

//        builder.AddProduct( coin_2500 , ProductType.Consumable , new IDs
//        {
//            {coin_2500_google , GooglePlay.Name }
//        } );

//        builder.AddProduct( coin_5800 , ProductType.Consumable , new IDs
//        {
//            {coin_5800_google , GooglePlay.Name }
//        } );

//        builder.AddProduct( coin_8500 , ProductType.Consumable , new IDs
//        {
//            {coin_8500_google , GooglePlay.Name }
//        } );

//        UnityPurchasing.Initialize( this , builder );
//    }

//    /// <summary>
//    /// Called when Unity IAP is ready to make purchases.
//    /// </summary>
//    public void OnInitialized ( IStoreController controller , IExtensionProvider extensions )
//    {
//        this.controller = controller;
//        this.extensions = extensions;

//        price_01.text = controller.products.WithID( premium ).metadata.localizedPriceString;
//        price_02.text = controller.products.WithID( coin_500 ).metadata.localizedPriceString;
//        price_03.text = controller.products.WithID( coin_1100 ).metadata.localizedPriceString;
//        price_04.text = controller.products.WithID( coin_2500 ).metadata.localizedPriceString;
//        price_05.text = controller.products.WithID( coin_5800 ).metadata.localizedPriceString;
//        price_06.text = controller.products.WithID( coin_8500 ).metadata.localizedPriceString;
//    }

//    /// <summary>
//    /// Called when Unity IAP encounters an unrecoverable initialization error.
//    ///
//    /// Note that this will not be called if Internet is unavailable; Unity IAP
//    /// will attempt initialization until it becomes available.
//    /// </summary>
//    public void OnInitializeFailed ( InitializationFailureReason error )
//    {
//        PlayerPrefs.DeleteAll();
//        Debug.LogError( GetType().Name + ".cs " + " OnInitializeFailed " + error );
//    }

//    public void BuyProduct ( string productId )
//    {
//        // If Purchasing has been initialized ...
//        if ( IsInitialized() )
//        {
//            // ... look up the Product reference with the general product identifier and the Purchasing 
//            // system's products collection.
//            Product product = controller.products.WithID(productId);

//            // If the look up found a product for this device's store and that product is ready to be sold ... 
//            if ( product != null && product.availableToPurchase )
//            {
//                Debug.Log( string.Format( "Purchasing product asychronously: '{0}'" , product.definition.id ) );
//                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
//                // asynchronously.
//                controller.InitiatePurchase( product );
//            }
//            // Otherwise ...
//            else
//            {
//                // ... report the product look-up failure situation  
//                Debug.Log( "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase" );
//            }
//        }
//        // Otherwise ...
//        else
//        {
//            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
//            // retrying initiailization.
//            Debug.Log( "BuyProductID FAIL. Not initialized." );
//        }
//    }

//    /// <summary>
//    /// Called when a purchase completes.
//    ///
//    /// May be called at any time after OnInitialized().
//    /// </summary>
//    public PurchaseProcessingResult ProcessPurchase ( PurchaseEventArgs e )
//    {
//        if ( string.Equals( e.purchasedProduct.definition.id , premium , System.StringComparison.Ordinal ) )
//        {
//            Debug.Log( string.Format( "ProcessPurchase : PASS. Product {0}" , e.purchasedProduct.definition.id ) );

//            PlayerPrefs.SetInt( AdmobHandler.premium , 1 );

//            if ( AdmobHandler.Instance != null )
//                AdmobHandler.Instance.ActivatePremium();

//            return PurchaseProcessingResult.Complete;
//        }

//        else if ( string.Equals( e.purchasedProduct.definition.id , coin_500 , System.StringComparison.Ordinal ) )
//        {

//            Debug.Log( string.Format( "ProcessPurchase : PASS. Product {0}" , e.purchasedProduct.definition.id ) );

//            PlayFab.PlayFabServerAPI.AddUserVirtualCurrency( GetCurrencyRequest( 500 ) ,
//              ( result ) =>
//              {
//                  VCPurchaseTrue( result , e.purchasedProduct );
//              }
//              , VCPurchaseFalse );

//            return PurchaseProcessingResult.Pending;

//        }

//        else if ( string.Equals( e.purchasedProduct.definition.id , coin_1100 , System.StringComparison.Ordinal ) )
//        {
//            Debug.Log( string.Format( "ProcessPurchase : PASS. Product {0}" , e.purchasedProduct.definition.id ) );

//            PlayFab.PlayFabServerAPI.AddUserVirtualCurrency( GetCurrencyRequest( 1100 ) ,
//              ( result ) =>
//              {
//                  VCPurchaseTrue( result , e.purchasedProduct );
//              }
//              , VCPurchaseFalse );

//            return PurchaseProcessingResult.Pending;
//        }

//        else if ( string.Equals( e.purchasedProduct.definition.id , coin_2500 , System.StringComparison.Ordinal ) )
//        {
//            Debug.Log( string.Format( "ProcessPurchase : PASS. Product {0}" , e.purchasedProduct.definition.id ) );

//            PlayFab.PlayFabServerAPI.AddUserVirtualCurrency( GetCurrencyRequest( 2500 ) ,
//               ( result ) =>
//               {
//                   VCPurchaseTrue( result , e.purchasedProduct );
//               }
//               , VCPurchaseFalse );

//            return PurchaseProcessingResult.Pending;
//        }

//        else if ( string.Equals( e.purchasedProduct.definition.id , coin_5800 , System.StringComparison.Ordinal ) )
//        {
//            Debug.Log( string.Format( "ProcessPurchase : PASS. Product {0}" , e.purchasedProduct.definition.id ) );
//            PlayFab.PlayFabServerAPI.AddUserVirtualCurrency( GetCurrencyRequest( 5800 ) ,
//                ( result ) =>
//                {
//                    VCPurchaseTrue( result , e.purchasedProduct );
//                }
//                , VCPurchaseFalse );

//            return PurchaseProcessingResult.Pending;
//        }

//        else if ( string.Equals( e.purchasedProduct.definition.id , coin_8500 , System.StringComparison.Ordinal ) )
//        {
//            Debug.Log( string.Format( "ProcessPurchase : PASS. Product {0}" , e.purchasedProduct.definition.id ) );

//            PlayFab.PlayFabServerAPI.AddUserVirtualCurrency( GetCurrencyRequest( 8500 ) ,
//                ( result ) =>
//                {
//                    VCPurchaseTrue( result , e.purchasedProduct );
//                }
//                , VCPurchaseFalse );

//            // write data to the cloud!
//            //if true return return PurchaseProcessingResult.Complete;

//            return PurchaseProcessingResult.Pending;
//        }

//        return PurchaseProcessingResult.Complete;
//    }

//    void VCPurchaseTrue ( PlayFab.ServerModels.ModifyUserVirtualCurrencyResult result , Product product )
//    {
//        controller.ConfirmPendingPurchase( product );
//        PabloGames.PGMenuFeatures.Instance.AddCoin( result.BalanceChange );
//        PabloGames.Report.Run( "Purchase Completed!" , result.BalanceChange + " added your account. Enjoy!" , "OKAY" , PabloGames.Report.IconSprite.COIN );
//    }

//    void VCPurchaseFalse ( PlayFab.PlayFabError error )
//    {
//        PabloGames.Report.Run( "Purchase Error !" , " If you are charged, \ncontact us for refund!" , "OKAY" , PabloGames.Report.IconSprite.COIN );
//        Debug.LogError( "Error " + error.GenerateErrorReport() );
//    }

//    PlayFab.ServerModels.AddUserVirtualCurrencyRequest GetCurrencyRequest ( int amount )
//    {
//        var model = new PlayFab.ServerModels.AddUserVirtualCurrencyRequest();

//        model.Amount = amount;
//        model.VirtualCurrency = "VC";
//        model.PlayFabId = PabloGames.Bridge.authentication.loginResult.PlayFabId;

//        return model;
//    }

//    /// <summary>
//    /// Called when a purchase fails.
//    /// </summary>
//    public void OnPurchaseFailed ( Product i , PurchaseFailureReason p )
//    {

//    }

//    public bool IsInitialized ()
//    {
//        return controller != null && extensions != null;
//    }

//}
