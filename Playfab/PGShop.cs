//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//namespace PabloGames
//{

//    public class PGShop : MonoBehaviour
//    {
//        public bool Test;

//        public Tab[] tabs;
//        public SkinTab[] skinTabs;

//        public Text labelWeapon;
//        public Text labelPrice;

//        PGCustomWeapon customWeapon;

//        private Tab currentTab;
//        private SkinTab currentSkinTab;
//        private CWeapon currentWeapon;
//        private int currentWeaponId;

//        public SelectedCWeapon<CWeapon_AK47> selectedAk47 = new SelectedCWeapon<CWeapon_AK47>(PGCustomWeapon.keyAk47 , CWeapon_AK47.Default);
//        public SelectedCWeapon<CWeapon_M4A1> selectedM4a1 = new SelectedCWeapon<CWeapon_M4A1>(PGCustomWeapon.keyM4a1, CWeapon_M4A1.Default);
//        public SelectedCWeapon<CWeapon_Famas> selectedFamas = new SelectedCWeapon<CWeapon_Famas>(PGCustomWeapon.keyFamas, CWeapon_Famas.Default);
//        public SelectedCWeapon<CWeapon_Aug> selectedAug = new SelectedCWeapon<CWeapon_Aug>(PGCustomWeapon.keyAug, CWeapon_Aug.Default);

//        public Button btnLeft;
//        public Button btnRight;

//        string selectedWeaponClass;

//        public Button btnPurchase;
//        public Button btnEquip;

//        public GameObject overlay;

//        void GetCatalog ()
//        {
//            PlayFab.PlayFabClientAPI.GetCatalogItems( new PlayFab.ClientModels.GetCatalogItemsRequest() { CatalogVersion = "1.0" } ,

//                ( result ) =>
//                {
//                    foreach ( var item in result.Catalog )
//                    {

//                        if ( item.ItemClass == "ak47" )
//                        {
//                            foreach ( var weapon in Bridge.SOCWeapons [ CWeaponExtension.akIndex ].weapons )
//                            {
//                                if ( weapon.name == item.ItemId )
//                                {
//                                    weapon.price = Convert.ToInt32( item.VirtualCurrencyPrices [ "VC" ] );

//                                    if ( !weapon.purchased )
//                                    {
//                                        bool b = Bridge.authentication.loginResult.InfoResultPayload.UserInventory.Any(x => x.ItemId == weapon.name);
//                                        weapon.purchased = b;
//                                    }

//                                    Debug.LogFormat( "Item Id : {0} , Price : {1} " , item.ItemId , weapon.price );

//                                }
//                            }
//                        }
//                        else if ( item.ItemClass == "m4a1" )
//                        {
//                            foreach ( var weapon in Bridge.SOCWeapons [ CWeaponExtension.m4Index ].weapons )
//                            {
//                                if ( weapon.name == item.ItemId )
//                                {
//                                    weapon.price = Convert.ToInt32( item.VirtualCurrencyPrices [ "VC" ] );
//                                    Debug.LogFormat( "Item Id : {0} , Price : {1} " , item.ItemId , weapon.price );


//                                    if ( !weapon.purchased )
//                                    {
//                                        bool b = Bridge.authentication.loginResult.InfoResultPayload.UserInventory.Any(x => x.ItemId == weapon.name);
//                                        weapon.purchased = b;
//                                    }
//                                }
//                            }
//                        }
//                        else if ( item.ItemClass == "famas" )
//                        {
//                            foreach ( var weapon in Bridge.SOCWeapons [ CWeaponExtension.famasIndex ].weapons )
//                            {
//                                if ( weapon.name == item.ItemId )
//                                {
//                                    weapon.price = Convert.ToInt32( item.VirtualCurrencyPrices [ "VC" ] );
//                                    Debug.LogFormat( "Item Id : {0} , Price : {1} " , item.ItemId , weapon.price );


//                                    if ( !weapon.purchased )
//                                    {
//                                        bool b = Bridge.authentication.loginResult.InfoResultPayload.UserInventory.Any(x => x.ItemId == weapon.name);
//                                        weapon.purchased = b;
//                                    }
//                                }
//                            }
//                        }
//                        else if ( item.ItemClass == "aug" )
//                        {
//                            foreach ( var weapon in Bridge.SOCWeapons [ CWeaponExtension.augIndex ].weapons )
//                            {
//                                if ( weapon.name == item.ItemId )
//                                {
//                                    weapon.price = Convert.ToInt32( item.VirtualCurrencyPrices [ "VC" ] );
//                                    Debug.LogFormat( "Item Id : {0} , Price : {1} " , item.ItemId , weapon.price );

//                                    if ( !weapon.purchased )
//                                    {
//                                        bool b = Bridge.authentication.loginResult.InfoResultPayload.UserInventory.Any(x => x.ItemId == weapon.name);
//                                        weapon.purchased = b;
//                                    }
//                                }
//                            }
//                        }
//                    }


//                } , null );
//        }

//        private void Start ()
//        {
//            //Bridge.authentication.RunWithCustomId( GetCatalog );

//            //if ( Test )
//            //Bridge.SOCWeapons = Resources.LoadAll<SOCWeapon>( "weapons" );

//            //Invoke( "GetCatalog" , 2.0f );

//            currentTab = tabs [ 1 ]; // rifle tab is default ;
//            currentSkinTab = skinTabs [ 0 ]; // as default ak47 is default

//            btnLeft.onClick.AddListener( delegate { ChangeSkin( -1 ); } );
//            btnRight.onClick.AddListener( delegate { ChangeSkin( 1 ); } );

//            btnEquip.onClick.AddListener( Equip );
//            btnPurchase.onClick.AddListener( Purchase );

//            foreach ( var skinTab in skinTabs )
//                skinTab.button.onClick.AddListener( delegate { ChangeClass( skinTab.name ); } );

//            foreach ( var tab in tabs )
//                tab.button.onClick.AddListener( delegate { ActivateTab( tab ); } );

//            customWeapon = GetComponent<PGCustomWeapon>();


//            selectedAk47.integer = PlayerPrefs.GetInt( PGCustomWeapon.keyAk47 , 0 );
//            selectedAk47.id = ( CWeapon_AK47 ) selectedAk47.integer;

//            selectedM4a1.integer = PlayerPrefs.GetInt( PGCustomWeapon.keyM4a1 , 0 );
//            selectedM4a1.id = ( CWeapon_M4A1 ) selectedM4a1.integer;

//            selectedAug.integer = PlayerPrefs.GetInt( PGCustomWeapon.keyAug , 0 );
//            selectedAug.id = ( CWeapon_Aug ) selectedAug.integer;

//            selectedFamas.integer = PlayerPrefs.GetInt( PGCustomWeapon.keyFamas , 0 );
//            selectedFamas.id = ( CWeapon_Famas ) selectedFamas.integer;

//            ChangeClass( PGCustomWeapon.keyAk47 ); // as default

//            Bridge.UpdateProperties();
//        }

//        public void ChangeSkin ( int direction )
//        {
//            int id = 0;
//            CWeapon weapon = null;
//            switch ( selectedWeaponClass )
//            {
//                case PGCustomWeapon.keyAk47:
//                    id = ( int ) selectedAk47.Select( direction );
//                    weapon = Bridge.SOCWeapons [ CWeaponExtension.akIndex ].weapons [ id ];
//                    customWeapon.Run( selectedWeaponClass , weapon );
//                    break;
//                case PGCustomWeapon.keyM4a1:
//                    id = ( int ) selectedM4a1.Select( direction );
//                    weapon = Bridge.SOCWeapons [ CWeaponExtension.m4Index ].weapons [ id ];
//                    customWeapon.Run( selectedWeaponClass , weapon );

//                    break;
//                case PGCustomWeapon.keyFamas:
//                    id = ( int ) selectedFamas.Select( direction );
//                    weapon = Bridge.SOCWeapons [ CWeaponExtension.famasIndex ].weapons [ id ];
//                    customWeapon.Run( selectedWeaponClass , weapon );

//                    break;
//                case PGCustomWeapon.keyAug:
//                    id = ( int ) selectedAug.Select( direction );
//                    weapon = Bridge.SOCWeapons [ CWeaponExtension.augIndex ].weapons [ id ];
//                    customWeapon.Run( selectedWeaponClass , weapon );

//                    break;
//                default:
//                    break;
//            }

//            if ( weapon == null )
//                return;


//            if ( PlayerPrefs.HasKey( selectedWeaponClass ) && PlayerPrefs.GetInt( selectedWeaponClass ) == id )
//                labelWeapon.text = "SELECTED " + ( selectedWeaponClass + " - " + weapon.name ).ToUpper();
//            else
//                labelWeapon.text = ( selectedWeaponClass + " - " + weapon.name ).ToUpper();

//            bool b = string.Equals(weapon.name , "default") ? true : weapon.purchased;

//            labelPrice.text = "BUY " + weapon.price;


//            currentWeapon = weapon;
//            currentWeaponId = id;

//            btnPurchase.gameObject.SetActive( !b );
//            btnEquip.gameObject.SetActive( b );
//        }

//        public void ChangeClass ( string weaponClass )
//        {
//            this.selectedWeaponClass = weaponClass;

//            foreach ( var skinTab in skinTabs )
//            {
//                bool b = string.Equals( weaponClass , skinTab.name );

//                if ( b )
//                {
//                    currentSkinTab.gameObject.SetActive( false );
//                    currentSkinTab.gameObjectFrame.SetActive( false );

//                    skinTab.gameObject.SetActive( b );
//                    skinTab.gameObjectFrame.SetActive( b );

//                    currentSkinTab = skinTab;

//                    ChangeSkin( 0 );

//                    break;
//                }

//            }
//        }

//        public void ShowWeapon()
//        {
//            ActivateTab( tabs [ 1 ] );
//        }

//        public void ShowCoin()
//        {
//            ActivateTab( tabs [ 0 ] );
//        }

//        public void ActivateTab ( Tab activate )
//        {
//            foreach ( var tab in tabs )
//            {
//                if ( tab != activate )
//                    tab.gameObject.SetActive( false );
//            }

//            activate.gameObject.SetActive( true );
//        }

//        public void Equip ()
//        {
//            Debug.Log( "Equip " + currentWeapon.name + " id : " + currentWeaponId );
//            PlayerPrefs.SetInt( selectedWeaponClass , currentWeaponId );
//            Bridge.UpdateProperties();
//            ChangeSkin( 0 );
//        }

//        public void Purchase ()
//        {
//            Lock( true );

//            Debug.Log( "Purchase " + currentWeapon.name );

//            PlayFab.PlayFabClientAPI.PurchaseItem(
//                new PlayFab.ClientModels.PurchaseItemRequest() { CatalogVersion = "1.0" , ItemId = currentWeapon.name.ToLower() , VirtualCurrency = "VC" , Price = currentWeapon.price } ,
//                ( result ) =>
//                {
//                    if ( result.Items == null || result.Items [ 0 ] == null )
//                    {
//                        Lock( false );
//                        return;
//                    }

//                    PGMenuFeatures.Instance.AddCoin( -result.Items [ 0 ].UnitPrice );
//                    currentWeapon.purchased = true;
//                    ChangeSkin( 0 );
//                    Report.Run( "Congratulations" , "You bought " + result.Items [ 0 ].ItemClass.ToUpper() + " - " + result.Items [ 0 ].ItemId.ToUpper() , "EQUIP" , onOkay: Equip );

//                    Lock( false );
//                } ,
//                ( error ) =>
//                {
//                    Lock( false );
//                    if ( error.Error == PlayFab.PlayFabErrorCode.InsufficientFunds )
//                        Report.Run( "Error" , "Not Enough Coin!" , "BUY COIN" , onOkay: () => { ActivateTab( tabs [ 0 ] ); } );
//                    else
//                        Report.Run( "Error " , error.ErrorMessage , "OKAY" );
//                    //else
//                    //    Report.FatalError();
//                }
//                );
//        }

//        void Lock ( bool b )
//        {
//            overlay.SetActive( b );
//        }

//        public float rotSpeed = 90f;

//        public void OnDrag ( BaseEventData eventData )
//        {

//            float x = ((PointerEventData)eventData).delta.x;
//            //float y = ((PointerEventData)eventData).delta.y;

//            float absX = Mathf.Abs( x );
//            //float abxY = Mathf.Abs( y );

//            if ( Mathf.Abs( x ) > 1 )
//                currentSkinTab.gameObject.transform.RotateAround( currentSkinTab.gameObject.transform.position , Vector3.up , x );

//            //else if ( abxY > absX && Mathf.Abs( y ) > 1 )
//            //viewAK.transform.RotateAround( viewAK.transform.position , Vector3.forward , y );

//        }
//    }

//    /* DataStructures */
//    public class SelectedCWeapon<T> where T : System.Enum
//    {
//        public string sclass;
//        public T id;
//        public int integer;

//        public SelectedCWeapon ( string sclass , T id )
//        {
//            this.sclass = sclass;
//            this.id = id;
//            this.integer = System.Convert.ToInt32( id );
//        }

//        public T Select ( int direction )
//        {
//            int length = Enum.GetValues( typeof( T ) ).Length;

//            int desired = direction + integer;

//            if ( desired < length && desired > -1 )
//            {
//                integer = desired;
//                return ( T ) ( ( object ) integer );
//            }

//            return ( T ) ( ( object ) integer );
//        }
//    }

//    [System.Serializable]
//    public class Tab
//    {
//        public GameObject gameObject;
//        public Button button;
//    }

//    [System.Serializable]
//    public class SkinTab : Tab
//    {
//        public string name;
//        public GameObject gameObjectFrame;

//    }
//}
