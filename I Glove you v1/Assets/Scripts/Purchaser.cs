using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener
{

	private string purchasedTempText;




	private static IStoreController m_StoreController;
	// The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider;
	// The store-specific Purchasing subsystems.
        
	// Product identifiers for all products capable of being purchased:
	// "convenience" general identifiers for use with Purchasing, and their store-specific identifier
	// counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers
	// also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

	// General product identifiers for the consumable, non-consumable, and subscription products.
	// Use these handles in the code to reference which product to purchase. Also use these values
	// when defining the Product Identifiers on the store. Except, for illustration purposes, the
	// kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
	// specific mapping to Unity Purchasing's AddProduct, below.
	public static string kProductIDNonConsumable = "nonconsumable";
	public static string kProductIDNonConsumable2 = "nonconsumable2";
	public static string kProductIDNonConsumable3 = "nonconsumable3";

         
	// Apple App Store-specific product identifier for the subscription product.
	private static string kProductNameAppleNonConsumable = "igu_iap1";
	private static string kProductNameAppleNonConsumable2 = "igu_iap2";
	private static string kProductNameAppleNonConsumable3 = "igu_iap3";


        
	// Google Play Store-specific product identifier subscription product.
	private static string kProductNameGooglePlayNonConsumable = "igu_iap1";
	private static string kProductNameGooglePlayNonConsumable2 = "igu_iap2";
	private static string kProductNameGooglePlayNonConsumable3 = "igu_iap3";


	void Start ()
	{

		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing ();
			Debug.Log ("initializing iap");

		}

	}



	public void InitializePurchasing ()
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized ())
		{
			// ... we are done here.
			return;
		}
            
		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance (StandardPurchasingModule.Instance ());
            
		

		// And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
		// if the Product ID was configured differently between Apple and Google stores. Also note that
		// one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
		// must only be referenced here. 

		builder.AddProduct (kProductIDNonConsumable, ProductType.NonConsumable, new IDs () { {
				kProductNameAppleNonConsumable,
				AppleAppStore.Name
			}, {
				kProductNameGooglePlayNonConsumable,
				GooglePlay.Name
			},
		});

		builder.AddProduct (kProductIDNonConsumable2, ProductType.NonConsumable, new IDs () { {
				kProductNameAppleNonConsumable2,
				AppleAppStore.Name
			}, {
				kProductNameGooglePlayNonConsumable2,
				GooglePlay.Name
			},
		});

		builder.AddProduct (kProductIDNonConsumable3, ProductType.NonConsumable, new IDs () { {
				kProductNameAppleNonConsumable3,
				AppleAppStore.Name
			}, {
				kProductNameGooglePlayNonConsumable3,
				GooglePlay.Name
			},
		});
		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize (this, builder);
	}

        
	private bool IsInitialized ()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

        
	

        
	public void BuyNonConsumable ()
	{
		// Buy the non-consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		//BuyProductID (kProductIDNonConsumable);

		if (MainMenuController.Instance.currentPayStep == 1)
		{
			BuyProductID (kProductIDNonConsumable);
			
		}
		else if (MainMenuController.Instance.currentPayStep == 2)
		{
			BuyProductID (kProductIDNonConsumable2);
			
		}
		else if (MainMenuController.Instance.currentPayStep == 3)
		{
			BuyProductID (kProductIDNonConsumable3);
			
		}
	}

        
	
        
	void BuyProductID (string productId)
	{
		// If Purchasing has been initialized ...
		if (IsInitialized ())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID (productId);
                
			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase (product);
			}
                // Otherwise ...
                else
			{
				// ... report the product look-up failure situation  
				Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
            // Otherwise ...
            else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log ("BuyProductID FAIL. Not initialized.");
		}
	}
        
        
	// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google.
	// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
	public void RestorePurchases ()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized ())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log ("RestorePurchases FAIL. Not initialized.");
			return;
		}
            
		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer ||
		    Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			Debug.Log ("RestorePurchases started ...");
                
			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions> ();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
			// the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions ((result) =>
			{
				// The first phase of restoration. If no more responses are received on ProcessPurchase then 
				// no purchases are available to be restored.
				Debug.Log ("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
            // Otherwise ...
            else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log ("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}
        
        
	//
	// --- IStoreListener
	//
        
	public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log ("OnInitialized: PASS");
            
		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;


		int i = 0;
		foreach (var product in controller.products.all)
		{
			
			MainMenuController.Instance.IapTexts [i].text = product.metadata.localizedPriceString;
			i++;
			Debug.Log (product.metadata.localizedPriceString);
		}


	}

        
	public void OnInitializeFailed (InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log ("OnInitializeFailed InitializationFailureReason:" + error);
	}

        
	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs args)
	{
		// Or ... a non-consumable product has been purchased by this user.
		if (String.Equals (args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
		{
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
			purchasedTempText = MainMenuController.Instance.IapTexts [0].text;
			OnPurchaseSuccess ();
		}
		else if (String.Equals (args.purchasedProduct.definition.id, kProductIDNonConsumable2, StringComparison.Ordinal))
		{
			Debug.Log ("item2 purchased");// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
			purchasedTempText = MainMenuController.Instance.IapTexts [1].text;
			OnPurchaseSuccess ();

		}
		else if (String.Equals (args.purchasedProduct.definition.id, kProductIDNonConsumable3, StringComparison.Ordinal))
		{
			Debug.Log ("item3 purchased");
			purchasedTempText = MainMenuController.Instance.IapTexts [2].text;
			OnPurchaseSuccess ();

		}
           
            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else
		{
			Debug.Log (string.Format ("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed. 
		return PurchaseProcessingResult.Complete;
	}

        
	public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log (string.Format ("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	void OnPurchaseSuccess ()
	{
		
		//add to paidForApp playerprefs
		PlayerPrefs.SetInt ("paidForApp", 1);
		//MainMenuController.Instance.paidForApp = true;
		MainMenuController.Instance.thanksPanel.SetActive (true);
		MainMenuController.Instance.purchasedText.text = purchasedTempText;
		//also add purchasedText.text to a string and remember it in playerprefs
		PlayerPrefs.SetString ("purchasedAmount", purchasedTempText);
		MainMenuController.Instance.purchasedPanel.SetActive (false);
	}
}

