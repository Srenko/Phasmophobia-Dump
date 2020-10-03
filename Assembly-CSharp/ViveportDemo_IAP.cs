using System;
using UnityEngine;
using UnityEngine.UI;
using Viveport;
using Viveport.Core;

// Token: 0x020001FE RID: 510
public class ViveportDemo_IAP : MonoBehaviour
{
	// Token: 0x06000E4F RID: 3663 RVA: 0x0005C0BC File Offset: 0x0005A2BC
	private void Start()
	{
		Viveport.Core.Logger.Log("mListener: " + this.mListener);
		this.mListener = new ViveportDemo_IAP.Result();
		Viveport.Core.Logger.Log("mListener end: " + this.mListener);
		Viveport.Core.Logger.Log("Version: " + Api.Version());
		Viveport.Core.Logger.Log("UserId: " + User.GetUserId());
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x00003F60 File Offset: 0x00002160
	private void Update()
	{
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0005C128 File Offset: 0x0005A328
	private void OnGUI()
	{
		GUIStyle style = new GUIStyle("button");
		GUI.Label(new Rect(15f, 5f, 100f, 20f), "TOP API");
		GUI.Label(new Rect(15f, 65f, 800f, 20f), "==========================================================================================================================================");
		GUI.Label(new Rect(15f, 80f, 100f, 20f), "IAP - Purchase");
		GUI.Label(new Rect(15f, 145f, 800f, 20f), "==========================================================================================================================================");
		GUI.Label(new Rect(15f, 165f, 200f, 20f), "IAP - Purchase With User Data");
		GUI.Label(new Rect(15f, 235f, 800f, 20f), "==========================================================================================================================================");
		GUI.Label(new Rect(15f, 260f, 120f, 20f), "IAP - Subscription");
		GUI.Label(new Rect(15f, 380f, 800f, 20f), "==========================================================================================================================================");
		GUI.Label(new Rect(15f, 400f, 120f, 20f), "IAP - Optional API");
		if ((!ViveportDemo_IAP.bInit_Done && !ViveportDemo_IAP.bIsReady_Done) || ViveportDemo_IAP.bShutdown_Done)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.gray;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart - 15), (float)this.nWidth, (float)this.nHeight), "1.Init", style))
		{
			Viveport.Core.Logger.Log("Init");
			Api.Init(new StatusCallback(ViveportDemo_IAP.InitStatusHandler), ViveportDemo_IAP.IAP_APP_TEST_ID);
		}
		if (ViveportDemo_IAP.bInit_Done && !ViveportDemo_IAP.bIsReady_Done)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.gray;
		}
		if (GUI.Button(new Rect((float)(this.nXStart + this.nWidth + 15), (float)(this.nYStart - 15), (float)this.nWidth, (float)this.nHeight), "2.IsReady", style))
		{
			Viveport.Core.Logger.Log("IsReady");
			IAPurchase.IsReady(this.mListener, ViveportDemo_IAP.IAP_APP_TEST_KEY);
		}
		if (!ViveportDemo_IAP.bInit_Done || ViveportDemo_IAP.bShutdown_Done)
		{
			GUI.contentColor = Color.grey;
		}
		else
		{
			GUI.contentColor = Color.white;
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * this.nWidth + 30), (float)(this.nYStart - 15), (float)(this.nWidth + 20), (float)this.nHeight), "3.Shutdown", style))
		{
			Viveport.Core.Logger.Log("Shutdown");
			Api.Shutdown(new StatusCallback(ViveportDemo_IAP.ShutdownStatusHandler));
		}
		if (ViveportDemo_IAP.bInit_Done && ViveportDemo_IAP.bIsReady_Done && !ViveportDemo_IAP.bShutdown_Done)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + 60), (float)(this.nWidth + 20), (float)this.nHeight), "1.1.Request", style))
		{
			Viveport.Core.Logger.Log("Request");
			this.mListener.mItem.items = new string[3];
			this.mListener.mItem.items[0] = "sword";
			this.mListener.mItem.items[1] = "knife";
			this.mListener.mItem.items[2] = "medicine";
			IAPurchase.Request(this.mListener, "1");
		}
		if (GUI.Button(new Rect((float)(this.nXStart + this.nWidth + 40), (float)(this.nYStart + 60), (float)(this.nWidth + 20), (float)this.nHeight), "1.2.Purchase", style))
		{
			Viveport.Core.Logger.Log("Purchase mListener.mItem.ticket=" + this.mListener.mItem.ticket);
			IAPurchase.Purchase(this.mListener, this.mListener.mItem.ticket);
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * this.nWidth + 80), (float)(this.nYStart + 60), (float)(this.nWidth + 20), (float)this.nHeight), "2.Query", style))
		{
			Viveport.Core.Logger.Log("Query");
			IAPurchase.Query(this.mListener, this.mListener.mItem.ticket);
		}
		if (ViveportDemo_IAP.bInit_Done && ViveportDemo_IAP.bIsReady_Done && !ViveportDemo_IAP.bShutdown_Done)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + this.nHeight + 110), (float)(this.nWidth + 90), (float)this.nHeight), "1.1.RequestWithUserData", style))
		{
			Viveport.Core.Logger.Log("Request");
			IAPurchase.Request(this.mListener, "1", "Knife");
		}
		if (GUI.Button(new Rect((float)(this.nXStart + this.nWidth + 105), (float)(this.nYStart + this.nHeight + 110), (float)(this.nWidth + 20), (float)this.nHeight), "1.2.Purchase", style))
		{
			Viveport.Core.Logger.Log("Purchase mListener.mItem.ticket=" + this.mListener.mItem.ticket);
			IAPurchase.Purchase(this.mListener, this.mListener.mItem.ticket);
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * this.nWidth + 140), (float)(this.nYStart + this.nHeight + 110), (float)(this.nWidth + 80), (float)this.nHeight), "2.QueryPurchaseList", style))
		{
			Viveport.Core.Logger.Log("QueryPurchaseList");
			IAPurchase.Query(this.mListener);
		}
		if (ViveportDemo_IAP.bInit_Done && ViveportDemo_IAP.bIsReady_Done && !ViveportDemo_IAP.bShutdown_Done)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + 2 * this.nHeight + 165), (float)(this.nWidth + 160), (float)this.nHeight), "1.1-1.RequestSubscription                 ", style))
		{
			Viveport.Core.Logger.Log("RequestSubscription");
			IAPurchase.RequestSubscription(this.mListener, "1", "month", 1, "day", 2, 3, "pID");
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + 3 * this.nHeight + 175), (float)(this.nWidth + 160), (float)this.nHeight), "1.1-2.RequestSubscriptionWithPlanID", style))
		{
			Viveport.Core.Logger.Log("RequestSubscriptionWithPlanID");
			IAPurchase.RequestSubscriptionWithPlanID(this.mListener, "pID");
		}
		if (GUI.Button(new Rect((float)(this.nXStart + this.nWidth + 180), (float)(this.nYStart + 2 * this.nHeight + 190), (float)(this.nWidth + 20), (float)this.nHeight), "1.2.Subscribe", style))
		{
			Viveport.Core.Logger.Log("Subscribe");
			IAPurchase.Subscribe(this.mListener, this.mListener.mItem.subscription_ticket);
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * this.nWidth + 218), (float)(this.nYStart + 2 * this.nHeight + 165), (float)(this.nWidth + 90), (float)this.nHeight), "2.1.QuerySubscription", style))
		{
			Viveport.Core.Logger.Log("QuerySubscription");
			IAPurchase.QuerySubscription(this.mListener, this.mListener.mItem.subscription_ticket);
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * this.nWidth + 218), (float)(this.nYStart + 3 * this.nHeight + 175), (float)(this.nWidth + 105), (float)this.nHeight), "2.2.QuerySubscriptionList", style))
		{
			Viveport.Core.Logger.Log("QuerySubscriptionList");
			IAPurchase.QuerySubscriptionList(this.mListener);
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 3 * this.nWidth + 345), (float)(this.nYStart + 2 * this.nHeight + 190), (float)(this.nWidth + 80), (float)this.nHeight), "3.CancelSubscription", style))
		{
			Viveport.Core.Logger.Log("CancelSubscription");
			IAPurchase.CancelSubscription(this.mListener, this.mListener.mItem.subscription_ticket);
		}
		if (ViveportDemo_IAP.bInit_Done && ViveportDemo_IAP.bIsReady_Done && !ViveportDemo_IAP.bShutdown_Done)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.gray;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + 2 * this.nHeight + 305), (float)(this.nWidth + 20), (float)this.nHeight), "GetBalance", style))
		{
			Viveport.Core.Logger.Log("GetBalance");
			IAPurchase.GetBalance(this.mListener);
		}
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0005CA3F File Offset: 0x0005AC3F
	private static void InitStatusHandler(int nResult)
	{
		ViveportDemo_IAP.bInit_Done = true;
		ViveportDemo_IAP.bShutdown_Done = false;
		Viveport.Core.Logger.Log("InitStatusHandler: " + nResult);
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0005CA62 File Offset: 0x0005AC62
	private static void ShutdownStatusHandler(int nResult)
	{
		ViveportDemo_IAP.bShutdown_Done = true;
		ViveportDemo_IAP.bInit_Done = false;
		ViveportDemo_IAP.bIsReady_Done = false;
		Viveport.Core.Logger.Log("ShutdownStatusHandler: " + nResult);
	}

	// Token: 0x04000EAF RID: 3759
	private int nWidth = 80;

	// Token: 0x04000EB0 RID: 3760
	private int nHeight = 40;

	// Token: 0x04000EB1 RID: 3761
	private int nXStart = 10;

	// Token: 0x04000EB2 RID: 3762
	private int nYStart = 40;

	// Token: 0x04000EB3 RID: 3763
	private static string IAP_APP_TEST_ID = "app_VIVEPORT_ID";

	// Token: 0x04000EB4 RID: 3764
	private static string IAP_APP_TEST_KEY = "app_API_Key";

	// Token: 0x04000EB5 RID: 3765
	private ViveportDemo_IAP.Result mListener;

	// Token: 0x04000EB6 RID: 3766
	private static bool bIsDuplicatedSubscription = false;

	// Token: 0x04000EB7 RID: 3767
	private static bool bInit_Done = false;

	// Token: 0x04000EB8 RID: 3768
	private static bool bIsReady_Done = false;

	// Token: 0x04000EB9 RID: 3769
	private static bool bShutdown_Done = false;

	// Token: 0x04000EBA RID: 3770
	private Text winText;

	// Token: 0x0200058A RID: 1418
	public class Item
	{
		// Token: 0x04002641 RID: 9793
		public string ticket = "test_id";

		// Token: 0x04002642 RID: 9794
		public string[] items;

		// Token: 0x04002643 RID: 9795
		public string subscription_ticket = "unity_test_subscriptionId";
	}

	// Token: 0x0200058B RID: 1419
	private class Result : IAPurchase.IAPurchaseListener
	{
		// Token: 0x060028EC RID: 10476 RVA: 0x000C54D1 File Offset: 0x000C36D1
		public override void OnSuccess(string pchCurrencyName)
		{
			ViveportDemo_IAP.bIsReady_Done = true;
			Viveport.Core.Logger.Log("[OnSuccess] pchCurrencyName=" + pchCurrencyName);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000C54E9 File Offset: 0x000C36E9
		public override void OnRequestSuccess(string pchPurchaseId)
		{
			this.mItem.ticket = pchPurchaseId;
			Viveport.Core.Logger.Log("[OnRequestSuccess] pchPurchaseId=" + pchPurchaseId + ",mItem.ticket=" + this.mItem.ticket);
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000C5517 File Offset: 0x000C3717
		public override void OnPurchaseSuccess(string pchPurchaseId)
		{
			Viveport.Core.Logger.Log("[OnPurchaseSuccess] pchPurchaseId=" + pchPurchaseId);
			if (this.mItem.ticket == pchPurchaseId)
			{
				Viveport.Core.Logger.Log("[OnPurchaseSuccess] give items to user");
			}
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000C5546 File Offset: 0x000C3746
		public override void OnQuerySuccess(IAPurchase.QueryResponse response)
		{
			Viveport.Core.Logger.Log("[OnQuerySuccess] purchaseId=" + response.purchase_id + ",status=" + response.status);
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000C5568 File Offset: 0x000C3768
		public override void OnQuerySuccess(IAPurchase.QueryListResponse response)
		{
			Viveport.Core.Logger.Log(string.Concat(new object[]
			{
				"[OnQueryListSuccess] total=",
				response.total,
				", from=",
				response.from,
				", to=",
				response.to
			}));
			foreach (IAPurchase.QueryResponse2 queryResponse in response.purchaseList)
			{
				Viveport.Core.Logger.Log(string.Concat(new object[]
				{
					"purchase_id=",
					queryResponse.purchase_id,
					", user_data=",
					queryResponse.user_data,
					", price=",
					queryResponse.price,
					", currency=",
					queryResponse.currency,
					", paid_timestamp=",
					queryResponse.paid_timestamp
				}));
			}
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000C5674 File Offset: 0x000C3874
		public override void OnBalanceSuccess(string pchBalance)
		{
			Viveport.Core.Logger.Log("[OnBalanceSuccess] pchBalance=" + pchBalance);
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000C5686 File Offset: 0x000C3886
		public override void OnRequestSubscriptionSuccess(string pchSubscriptionId)
		{
			this.mItem.subscription_ticket = pchSubscriptionId;
			Viveport.Core.Logger.Log("[OnRequestSubscriptionSuccess] pchSubscriptionId=" + pchSubscriptionId + ",mItem.subscription_ticket=" + this.mItem.subscription_ticket);
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000C56B4 File Offset: 0x000C38B4
		public override void OnRequestSubscriptionWithPlanIDSuccess(string pchSubscriptionId)
		{
			this.mItem.subscription_ticket = pchSubscriptionId;
			Viveport.Core.Logger.Log("[OnRequestSubscriptionWithPlanIDSuccess] pchSubscriptionId=" + pchSubscriptionId + ",mItem.subscription_ticket=" + this.mItem.subscription_ticket);
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000C56E2 File Offset: 0x000C38E2
		public override void OnSubscribeSuccess(string pchSubscriptionId)
		{
			Viveport.Core.Logger.Log("[OnSubscribeSuccess] pchSubscriptionId=" + pchSubscriptionId);
			if (this.mItem.subscription_ticket == pchSubscriptionId)
			{
				Viveport.Core.Logger.Log("[OnSubscribeSuccess] give virtual items to user");
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000C5714 File Offset: 0x000C3914
		public override void OnQuerySubscriptionSuccess(IAPurchase.Subscription[] subscriptionlist)
		{
			int num = subscriptionlist.Length;
			Viveport.Core.Logger.Log("[OnQuerySubscriptionSuccess] subscriptionlist size =" + num);
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					Viveport.Core.Logger.Log(string.Concat(new object[]
					{
						"[OnQuerySubscriptionSuccess] subscriptionlist[",
						i,
						"].status =",
						subscriptionlist[i].status,
						", subscriptionlist[",
						i,
						"].plan_id = ",
						subscriptionlist[i].plan_id
					}));
					if (subscriptionlist[i].plan_id == "pID" && subscriptionlist[i].status == "ACTIVE")
					{
						ViveportDemo_IAP.bIsDuplicatedSubscription = true;
					}
				}
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000C57DC File Offset: 0x000C39DC
		public override void OnQuerySubscriptionListSuccess(IAPurchase.Subscription[] subscriptionlist)
		{
			int num = subscriptionlist.Length;
			Viveport.Core.Logger.Log("[OnQuerySubscriptionListSuccess] subscriptionlist size =" + num);
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					Viveport.Core.Logger.Log(string.Concat(new object[]
					{
						"[OnQuerySubscriptionListSuccess] subscriptionlist[",
						i,
						"].status =",
						subscriptionlist[i].status,
						", subscriptionlist[",
						i,
						"].plan_id = ",
						subscriptionlist[i].plan_id
					}));
					if (subscriptionlist[i].plan_id == "pID" && subscriptionlist[i].status == "ACTIVE")
					{
						ViveportDemo_IAP.bIsDuplicatedSubscription = true;
					}
				}
			}
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000C58A1 File Offset: 0x000C3AA1
		public override void OnCancelSubscriptionSuccess(bool bCanceled)
		{
			Viveport.Core.Logger.Log("[OnCancelSubscriptionSuccess] bCanceled=" + bCanceled.ToString());
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000C58B9 File Offset: 0x000C3AB9
		public override void OnFailure(int nCode, string pchMessage)
		{
			Viveport.Core.Logger.Log(string.Concat(new object[]
			{
				"[OnFailed] ",
				nCode,
				", ",
				pchMessage
			}));
		}

		// Token: 0x04002644 RID: 9796
		public ViveportDemo_IAP.Item mItem = new ViveportDemo_IAP.Item();
	}
}
