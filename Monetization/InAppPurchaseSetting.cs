
#if ENABLE_INAPP
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace UtilityScripts
{
    public class InAppPurchaseSettings : ScriptableObject
    {
        public string mainId = "com.bundle.id";
        public GameProduct [] products;
        [NonSerialized]
        public List<UnityEvent<Product>> onInitialized;
        [NonSerialized]
        public List<UnityEvent<Product>> onBuyProduct;

        public void RegisterOnInitialized ( UnityEvent<Product> uEvent )
        {
            onInitialized.Add( uEvent );
        }

        public void RegisterOnBuyProduct ( UnityEvent<Product> uEvent )
        {
            onBuyProduct.Add( uEvent );
        }

        public void UnregisterOnInitialized ( UnityEvent<Product> uEvent )
        {
            onInitialized.Remove( uEvent );
        }

        public void UnregisterOnBuyProduct ( UnityEvent<Product> uEvent )
        {
            onBuyProduct.Remove( uEvent );
        }

        [Serializable]
        public class GameProduct
        {
            public string id;
            public ProductType type;
            public PurchaseProcessingResult result;
        }
    }
}
#endif