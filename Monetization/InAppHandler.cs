
#if ENABLE_INAPP
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace UtilityScripts
{
    public class InAppHandler : UniqueSingleton<InAppHandler>, IStoreListener
    {
        public InAppPurchaseSettings inAppPurchaseSettings;

        private IStoreController controller;
        private IExtensionProvider extensions;

        // Constructor
        void Start ()
        {
            var builder = ConfigurationBuilder.Instance( StandardPurchasingModule.Instance() );

            for ( int i = 0; i < inAppPurchaseSettings.products.Length; i++ )
            {
                InAppPurchaseSettings.GameProduct gameProduct = inAppPurchaseSettings.products [ i ];
                IDs ids = new IDs();
                ids.Add( inAppPurchaseSettings.mainId + "." + gameProduct.id , GooglePlay.Name );
                ids.Add( inAppPurchaseSettings.mainId + "." + gameProduct.id , AppleAppStore.Name );
                builder.AddProduct( gameProduct.id , gameProduct.type , ids );
            }

            UnityPurchasing.Initialize( this , builder );
        }

        /// <summary>
        /// Called when Unity IAP is ready to make purchases.
        /// </summary>
        public void OnInitialized ( IStoreController controller , IExtensionProvider extensions )
        {
            this.controller = controller;
            this.extensions = extensions;

            for ( int i = 0; i < inAppPurchaseSettings.products.Length; i++ )
            {
                InAppPurchaseSettings.GameProduct gameProduct = inAppPurchaseSettings.products [ i ];
                Product product = controller.products.WithID( gameProduct.id );
                if ( product == null )
                {
                    Debug.LogError( "$ Critical error product with id returns null." );
                }
                foreach ( var function in inAppPurchaseSettings.onInitialized )
                {
                    function.SafeInvoke( product );
                }
            }
        }

        /// <summary>
        /// Called when Unity IAP encounters an unrecoverable initialization error.
        ///
        /// Note that this will not be called if Internet is unavailable; Unity IAP
        /// will attempt initialization until it becomes available.
        /// </summary>
        public void OnInitializeFailed ( InitializationFailureReason error )
        {
            Debug.LogError( GetType().Name + ".cs " + " OnInitializeFailed " + error );
        }

        public void BuyProduct ( string productId )
        {
            // If Purchasing has been initialized ...
            if ( IsInitialized() )
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = controller.products.WithID( productId );

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if ( product != null && product.availableToPurchase )
                {
                    Debug.Log( string.Format( "Purchasing product asychronously: '{0}'" , product.definition.id ) );
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    controller.InitiatePurchase( product );
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log( "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase" );
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log( "BuyProductID FAIL. Not initialized." );
            }
        }

        /// <summary>
        /// Called when a purchase completes.
        ///
        /// May be called at any time after OnInitialized().
        /// </summary>
        public PurchaseProcessingResult ProcessPurchase ( PurchaseEventArgs e )
        {
            for ( int i = 0; i < inAppPurchaseSettings.products.Length; i++ )
            {
                InAppPurchaseSettings.GameProduct gameProduct = inAppPurchaseSettings.products [ i ];

                if ( string.Equals( e.purchasedProduct.definition.id , gameProduct.id , System.StringComparison.Ordinal ) )
                {
                    Debug.Log( string.Format( "ProcessPurchase : PASS. Product {0}" , e.purchasedProduct.definition.id ) );

                    foreach ( var function in inAppPurchaseSettings.onBuyProduct )
                    {
                        function.SafeInvoke( e.purchasedProduct );
                    }

                    return gameProduct.result;
                }
            }

            return PurchaseProcessingResult.Complete;
        }

        public void ConfirmPendingPurchase( Product product )
        {
            controller.ConfirmPendingPurchase( product );
        }

        /// <summary>
        /// Called when a purchase fails.
        /// </summary>
        public void OnPurchaseFailed ( Product i , PurchaseFailureReason p )
        {

        }

        public bool IsInitialized ()
        {
            return controller != null && extensions != null;
        }

    }
}
#endif