using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using LitJson;
using Viveport.Core;
using Viveport.Internal;

namespace Viveport
{
	// Token: 0x0200020E RID: 526
	public class IAPurchase
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x0005E1A7 File Offset: 0x0005C3A7
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void IsReadyIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.isReadyIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0005E1B5 File Offset: 0x0005C3B5
		public static void IsReady(IAPurchase.IAPurchaseListener listener, string pchAppKey)
		{
			IAPurchase.isReadyIl2cppCallback = new IAPurchase.IAPHandler(listener).getIsReadyHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.IsReady_64(new IAPurchaseCallback(IAPurchase.IsReadyIl2cppCallback), pchAppKey);
				return;
			}
			IAPurchase.IsReady(new IAPurchaseCallback(IAPurchase.IsReadyIl2cppCallback), pchAppKey);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0005E1F4 File Offset: 0x0005C3F4
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void Request01Il2cppCallback(int errorCode, string message)
		{
			IAPurchase.request01Il2cppCallback(errorCode, message);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0005E202 File Offset: 0x0005C402
		public static void Request(IAPurchase.IAPurchaseListener listener, string pchPrice)
		{
			IAPurchase.request01Il2cppCallback = new IAPurchase.IAPHandler(listener).getRequestHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.Request_64(new IAPurchaseCallback(IAPurchase.Request01Il2cppCallback), pchPrice);
				return;
			}
			IAPurchase.Request(new IAPurchaseCallback(IAPurchase.Request01Il2cppCallback), pchPrice);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0005E241 File Offset: 0x0005C441
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void Request02Il2cppCallback(int errorCode, string message)
		{
			IAPurchase.request02Il2cppCallback(errorCode, message);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0005E250 File Offset: 0x0005C450
		public static void Request(IAPurchase.IAPurchaseListener listener, string pchPrice, string pchUserData)
		{
			IAPurchase.request02Il2cppCallback = new IAPurchase.IAPHandler(listener).getRequestHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.Request_64(new IAPurchaseCallback(IAPurchase.Request02Il2cppCallback), pchPrice, pchUserData);
				return;
			}
			IAPurchase.Request(new IAPurchaseCallback(IAPurchase.Request02Il2cppCallback), pchPrice, pchUserData);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0005E29C File Offset: 0x0005C49C
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void PurchaseIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.purchaseIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0005E2AA File Offset: 0x0005C4AA
		public static void Purchase(IAPurchase.IAPurchaseListener listener, string pchPurchaseId)
		{
			IAPurchase.purchaseIl2cppCallback = new IAPurchase.IAPHandler(listener).getPurchaseHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.Purchase_64(new IAPurchaseCallback(IAPurchase.PurchaseIl2cppCallback), pchPurchaseId);
				return;
			}
			IAPurchase.Purchase(new IAPurchaseCallback(IAPurchase.PurchaseIl2cppCallback), pchPurchaseId);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0005E2E9 File Offset: 0x0005C4E9
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void Query01Il2cppCallback(int errorCode, string message)
		{
			IAPurchase.query01Il2cppCallback(errorCode, message);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0005E2F7 File Offset: 0x0005C4F7
		public static void Query(IAPurchase.IAPurchaseListener listener, string pchPurchaseId)
		{
			IAPurchase.query01Il2cppCallback = new IAPurchase.IAPHandler(listener).getQueryHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.Query_64(new IAPurchaseCallback(IAPurchase.Query01Il2cppCallback), pchPurchaseId);
				return;
			}
			IAPurchase.Query(new IAPurchaseCallback(IAPurchase.Query01Il2cppCallback), pchPurchaseId);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0005E336 File Offset: 0x0005C536
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void Query02Il2cppCallback(int errorCode, string message)
		{
			IAPurchase.query02Il2cppCallback(errorCode, message);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0005E344 File Offset: 0x0005C544
		public static void Query(IAPurchase.IAPurchaseListener listener)
		{
			IAPurchase.query02Il2cppCallback = new IAPurchase.IAPHandler(listener).getQueryListHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.Query_64(new IAPurchaseCallback(IAPurchase.Query02Il2cppCallback));
				return;
			}
			IAPurchase.Query(new IAPurchaseCallback(IAPurchase.Query02Il2cppCallback));
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0005E381 File Offset: 0x0005C581
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void GetBalanceIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.getBalanceIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0005E38F File Offset: 0x0005C58F
		public static void GetBalance(IAPurchase.IAPurchaseListener listener)
		{
			IAPurchase.getBalanceIl2cppCallback = new IAPurchase.IAPHandler(listener).getBalanceHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.GetBalance_64(new IAPurchaseCallback(IAPurchase.GetBalanceIl2cppCallback));
				return;
			}
			IAPurchase.GetBalance(new IAPurchaseCallback(IAPurchase.GetBalanceIl2cppCallback));
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0005E3CC File Offset: 0x0005C5CC
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void RequestSubscriptionIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.requestSubscriptionIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0005E3DC File Offset: 0x0005C5DC
		public static void RequestSubscription(IAPurchase.IAPurchaseListener listener, string pchPrice, string pchFreeTrialType, int nFreeTrialValue, string pchChargePeriodType, int nChargePeriodValue, int nNumberOfChargePeriod, string pchPlanId)
		{
			IAPurchase.requestSubscriptionIl2cppCallback = new IAPurchase.IAPHandler(listener).getRequestSubscriptionHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.RequestSubscription_64(new IAPurchaseCallback(IAPurchase.RequestSubscriptionIl2cppCallback), pchPrice, pchFreeTrialType, nFreeTrialValue, pchChargePeriodType, nChargePeriodValue, nNumberOfChargePeriod, pchPlanId);
				return;
			}
			IAPurchase.RequestSubscription(new IAPurchaseCallback(IAPurchase.RequestSubscriptionIl2cppCallback), pchPrice, pchFreeTrialType, nFreeTrialValue, pchChargePeriodType, nChargePeriodValue, nNumberOfChargePeriod, pchPlanId);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0005E43A File Offset: 0x0005C63A
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void RequestSubscriptionWithPlanIDIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.requestSubscriptionWithPlanIDIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0005E448 File Offset: 0x0005C648
		public static void RequestSubscriptionWithPlanID(IAPurchase.IAPurchaseListener listener, string pchPlanId)
		{
			IAPurchase.requestSubscriptionWithPlanIDIl2cppCallback = new IAPurchase.IAPHandler(listener).getRequestSubscriptionWithPlanIDHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.RequestSubscriptionWithPlanID_64(new IAPurchaseCallback(IAPurchase.RequestSubscriptionWithPlanIDIl2cppCallback), pchPlanId);
				return;
			}
			IAPurchase.RequestSubscriptionWithPlanID(new IAPurchaseCallback(IAPurchase.RequestSubscriptionWithPlanIDIl2cppCallback), pchPlanId);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0005E487 File Offset: 0x0005C687
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void SubscribeIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.subscribeIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0005E495 File Offset: 0x0005C695
		public static void Subscribe(IAPurchase.IAPurchaseListener listener, string pchSubscriptionId)
		{
			IAPurchase.subscribeIl2cppCallback = new IAPurchase.IAPHandler(listener).getSubscribeHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.Subscribe_64(new IAPurchaseCallback(IAPurchase.SubscribeIl2cppCallback), pchSubscriptionId);
				return;
			}
			IAPurchase.Subscribe(new IAPurchaseCallback(IAPurchase.SubscribeIl2cppCallback), pchSubscriptionId);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0005E4D4 File Offset: 0x0005C6D4
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void QuerySubscriptionIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.querySubscriptionIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0005E4E2 File Offset: 0x0005C6E2
		public static void QuerySubscription(IAPurchase.IAPurchaseListener listener, string pchSubscriptionId)
		{
			IAPurchase.querySubscriptionIl2cppCallback = new IAPurchase.IAPHandler(listener).getQuerySubscriptionHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.QuerySubscription_64(new IAPurchaseCallback(IAPurchase.QuerySubscriptionIl2cppCallback), pchSubscriptionId);
				return;
			}
			IAPurchase.QuerySubscription(new IAPurchaseCallback(IAPurchase.QuerySubscriptionIl2cppCallback), pchSubscriptionId);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0005E521 File Offset: 0x0005C721
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void QuerySubscriptionListIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.querySubscriptionListIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0005E52F File Offset: 0x0005C72F
		public static void QuerySubscriptionList(IAPurchase.IAPurchaseListener listener)
		{
			IAPurchase.querySubscriptionListIl2cppCallback = new IAPurchase.IAPHandler(listener).getQuerySubscriptionListHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.QuerySubscriptionList_64(new IAPurchaseCallback(IAPurchase.QuerySubscriptionListIl2cppCallback));
				return;
			}
			IAPurchase.QuerySubscriptionList(new IAPurchaseCallback(IAPurchase.QuerySubscriptionListIl2cppCallback));
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0005E56C File Offset: 0x0005C76C
		[MonoPInvokeCallback(typeof(IAPurchaseCallback))]
		private static void CancelSubscriptionIl2cppCallback(int errorCode, string message)
		{
			IAPurchase.cancelSubscriptionIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0005E57A File Offset: 0x0005C77A
		public static void CancelSubscription(IAPurchase.IAPurchaseListener listener, string pchSubscriptionId)
		{
			IAPurchase.cancelSubscriptionIl2cppCallback = new IAPurchase.IAPHandler(listener).getCancelSubscriptionHandler();
			if (IntPtr.Size == 8)
			{
				IAPurchase.CancelSubscription_64(new IAPurchaseCallback(IAPurchase.CancelSubscriptionIl2cppCallback), pchSubscriptionId);
				return;
			}
			IAPurchase.CancelSubscription(new IAPurchaseCallback(IAPurchase.CancelSubscriptionIl2cppCallback), pchSubscriptionId);
		}

		// Token: 0x04000EF3 RID: 3827
		private static IAPurchaseCallback isReadyIl2cppCallback;

		// Token: 0x04000EF4 RID: 3828
		private static IAPurchaseCallback request01Il2cppCallback;

		// Token: 0x04000EF5 RID: 3829
		private static IAPurchaseCallback request02Il2cppCallback;

		// Token: 0x04000EF6 RID: 3830
		private static IAPurchaseCallback purchaseIl2cppCallback;

		// Token: 0x04000EF7 RID: 3831
		private static IAPurchaseCallback query01Il2cppCallback;

		// Token: 0x04000EF8 RID: 3832
		private static IAPurchaseCallback query02Il2cppCallback;

		// Token: 0x04000EF9 RID: 3833
		private static IAPurchaseCallback getBalanceIl2cppCallback;

		// Token: 0x04000EFA RID: 3834
		private static IAPurchaseCallback requestSubscriptionIl2cppCallback;

		// Token: 0x04000EFB RID: 3835
		private static IAPurchaseCallback requestSubscriptionWithPlanIDIl2cppCallback;

		// Token: 0x04000EFC RID: 3836
		private static IAPurchaseCallback subscribeIl2cppCallback;

		// Token: 0x04000EFD RID: 3837
		private static IAPurchaseCallback querySubscriptionIl2cppCallback;

		// Token: 0x04000EFE RID: 3838
		private static IAPurchaseCallback querySubscriptionListIl2cppCallback;

		// Token: 0x04000EFF RID: 3839
		private static IAPurchaseCallback cancelSubscriptionIl2cppCallback;

		// Token: 0x0200059F RID: 1439
		private class IAPHandler : IAPurchase.BaseHandler
		{
			// Token: 0x06002924 RID: 10532 RVA: 0x000C5B9B File Offset: 0x000C3D9B
			public IAPHandler(IAPurchase.IAPurchaseListener cb)
			{
				IAPurchase.IAPHandler.listener = cb;
			}

			// Token: 0x06002925 RID: 10533 RVA: 0x000C5BA9 File Offset: 0x000C3DA9
			public IAPurchaseCallback getIsReadyHandler()
			{
				return new IAPurchaseCallback(this.IsReadyHandler);
			}

			// Token: 0x06002926 RID: 10534 RVA: 0x000C5BB8 File Offset: 0x000C3DB8
			protected override void IsReadyHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[IsReadyHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				string text2 = "";
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text2 = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[IsReadyHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[IsReadyHandler] statusCode =",
						num,
						",errMessage=",
						text2
					}));
					if (num == 0)
					{
						try
						{
							text = (string)jsonData["currencyName"];
						}
						catch (Exception arg2)
						{
							Logger.Log("[IsReadyHandler] currencyName ex=" + arg2);
						}
						Logger.Log("[IsReadyHandler] currencyName=" + text);
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.IAPHandler.listener.OnSuccess(text);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text2);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x06002927 RID: 10535 RVA: 0x000C5CE0 File Offset: 0x000C3EE0
			public IAPurchaseCallback getRequestHandler()
			{
				return new IAPurchaseCallback(this.RequestHandler);
			}

			// Token: 0x06002928 RID: 10536 RVA: 0x000C5CF0 File Offset: 0x000C3EF0
			protected override void RequestHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[RequestHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				string text2 = "";
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text2 = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[RequestHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[RequestHandler] statusCode =",
						num,
						",errMessage=",
						text2
					}));
					if (num == 0)
					{
						try
						{
							text = (string)jsonData["purchase_id"];
						}
						catch (Exception arg2)
						{
							Logger.Log("[RequestHandler] purchase_id ex=" + arg2);
						}
						Logger.Log("[RequestHandler] purchaseId =" + text);
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.IAPHandler.listener.OnRequestSuccess(text);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text2);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x06002929 RID: 10537 RVA: 0x000C5E18 File Offset: 0x000C4018
			public IAPurchaseCallback getPurchaseHandler()
			{
				return new IAPurchaseCallback(this.PurchaseHandler);
			}

			// Token: 0x0600292A RID: 10538 RVA: 0x000C5E28 File Offset: 0x000C4028
			protected override void PurchaseHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[PurchaseHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				string text2 = "";
				long num2 = 0L;
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text2 = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[PurchaseHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[PurchaseHandler] statusCode =",
						num,
						",errMessage=",
						text2
					}));
					if (num == 0)
					{
						try
						{
							text = (string)jsonData["purchase_id"];
							num2 = (long)jsonData["paid_timestamp"];
						}
						catch (Exception arg2)
						{
							Logger.Log("[PurchaseHandler] purchase_id,paid_timestamp ex=" + arg2);
						}
						Logger.Log(string.Concat(new object[]
						{
							"[PurchaseHandler] purchaseId =",
							text,
							",paid_timestamp=",
							num2
						}));
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.IAPHandler.listener.OnPurchaseSuccess(text);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text2);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x0600292B RID: 10539 RVA: 0x000C5F84 File Offset: 0x000C4184
			public IAPurchaseCallback getQueryHandler()
			{
				return new IAPurchaseCallback(this.QueryHandler);
			}

			// Token: 0x0600292C RID: 10540 RVA: 0x000C5F94 File Offset: 0x000C4194
			protected override void QueryHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[QueryHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				string text5 = "";
				string text6 = "";
				long num2 = 0L;
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text2 = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[QueryHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[QueryHandler] statusCode =",
						num,
						",errMessage=",
						text2
					}));
					if (num == 0)
					{
						try
						{
							text = (string)jsonData["purchase_id"];
							text3 = (string)jsonData["order_id"];
							text4 = (string)jsonData["status"];
							text5 = (string)jsonData["price"];
							text6 = (string)jsonData["currency"];
							num2 = (long)jsonData["paid_timestamp"];
						}
						catch (Exception arg2)
						{
							Logger.Log("[QueryHandler] purchase_id, order_id ex=" + arg2);
						}
						Logger.Log(string.Concat(new string[]
						{
							"[QueryHandler] status =",
							text4,
							",price=",
							text5,
							",currency=",
							text6
						}));
						Logger.Log(string.Concat(new object[]
						{
							"[QueryHandler] purchaseId =",
							text,
							",order_id=",
							text3,
							",paid_timestamp=",
							num2
						}));
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.QueryResponse queryResponse = new IAPurchase.QueryResponse();
							queryResponse.purchase_id = text;
							queryResponse.order_id = text3;
							queryResponse.price = text5;
							queryResponse.currency = text6;
							queryResponse.paid_timestamp = num2;
							queryResponse.status = text4;
							IAPurchase.IAPHandler.listener.OnQuerySuccess(queryResponse);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text2);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x0600292D RID: 10541 RVA: 0x000C61D8 File Offset: 0x000C43D8
			public IAPurchaseCallback getQueryListHandler()
			{
				return new IAPurchaseCallback(this.QueryListHandler);
			}

			// Token: 0x0600292E RID: 10542 RVA: 0x000C61E8 File Offset: 0x000C43E8
			protected override void QueryListHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[QueryListHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				int total = 0;
				int from = 0;
				int to = 0;
				List<IAPurchase.QueryResponse2> list = new List<IAPurchase.QueryResponse2>();
				string text = "";
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[QueryListHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[QueryListHandler] statusCode =",
						num,
						",errMessage=",
						text
					}));
					if (num == 0)
					{
						try
						{
							JsonData jsonData2 = JsonMapper.ToObject(text);
							total = (int)jsonData2["total"];
							from = (int)jsonData2["from"];
							to = (int)jsonData2["to"];
							JsonData jsonData3 = jsonData2["purchases"];
							bool isArray = jsonData3.IsArray;
							foreach (object obj in ((IEnumerable)jsonData3))
							{
								JsonData jsonData4 = (JsonData)obj;
								IAPurchase.QueryResponse2 queryResponse = new IAPurchase.QueryResponse2();
								IDictionary dictionary = jsonData4;
								queryResponse.app_id = (dictionary.Contains("app_id") ? ((string)jsonData4["app_id"]) : "");
								queryResponse.currency = (dictionary.Contains("currency") ? ((string)jsonData4["currency"]) : "");
								queryResponse.purchase_id = (dictionary.Contains("purchase_id") ? ((string)jsonData4["purchase_id"]) : "");
								queryResponse.order_id = (dictionary.Contains("order_id") ? ((string)jsonData4["order_id"]) : "");
								queryResponse.price = (dictionary.Contains("price") ? ((string)jsonData4["price"]) : "");
								queryResponse.user_data = (dictionary.Contains("user_data") ? ((string)jsonData4["user_data"]) : "");
								if (dictionary.Contains("paid_timestamp"))
								{
									if (jsonData4["paid_timestamp"].IsLong)
									{
										queryResponse.paid_timestamp = (long)jsonData4["paid_timestamp"];
									}
									else if (jsonData4["paid_timestamp"].IsInt)
									{
										queryResponse.paid_timestamp = (long)((int)jsonData4["paid_timestamp"]);
									}
								}
								list.Add(queryResponse);
							}
						}
						catch (Exception arg2)
						{
							Logger.Log("[QueryListHandler] purchase_id, order_id ex=" + arg2);
						}
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.QueryListResponse queryListResponse = new IAPurchase.QueryListResponse();
							queryListResponse.total = total;
							queryListResponse.from = from;
							queryListResponse.to = to;
							queryListResponse.purchaseList = list;
							IAPurchase.IAPHandler.listener.OnQuerySuccess(queryListResponse);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x0600292F RID: 10543 RVA: 0x000C656C File Offset: 0x000C476C
			public IAPurchaseCallback getBalanceHandler()
			{
				return new IAPurchaseCallback(this.BalanceHandler);
			}

			// Token: 0x06002930 RID: 10544 RVA: 0x000C657C File Offset: 0x000C477C
			protected override void BalanceHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log(string.Concat(new object[]
				{
					"[BalanceHandler] code=",
					code,
					",message= ",
					message
				}));
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string str = "";
				string text = "";
				string text2 = "";
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text2 = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[BalanceHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[BalanceHandler] statusCode =",
						num,
						",errMessage=",
						text2
					}));
					if (num == 0)
					{
						try
						{
							str = (string)jsonData["currencyName"];
							text = (string)jsonData["balance"];
						}
						catch (Exception arg2)
						{
							Logger.Log("[BalanceHandler] currencyName, balance ex=" + arg2);
						}
						Logger.Log("[BalanceHandler] currencyName=" + str + ",balance=" + text);
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.IAPHandler.listener.OnBalanceSuccess(text);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text2);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x06002931 RID: 10545 RVA: 0x000C66E4 File Offset: 0x000C48E4
			public IAPurchaseCallback getRequestSubscriptionHandler()
			{
				return new IAPurchaseCallback(this.RequestSubscriptionHandler);
			}

			// Token: 0x06002932 RID: 10546 RVA: 0x000C66F4 File Offset: 0x000C48F4
			protected override void RequestSubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[RequestSubscriptionHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				string text2 = "";
				try
				{
					num = (int)jsonData["statusCode"];
					text2 = (string)jsonData["message"];
				}
				catch (Exception arg)
				{
					Logger.Log("[RequestSubscriptionHandler] statusCode, message ex=" + arg);
				}
				Logger.Log(string.Concat(new object[]
				{
					"[RequestSubscriptionHandler] statusCode =",
					num,
					",errMessage=",
					text2
				}));
				if (num == 0)
				{
					try
					{
						text = (string)jsonData["subscription_id"];
					}
					catch (Exception arg2)
					{
						Logger.Log("[RequestSubscriptionHandler] subscription_id ex=" + arg2);
					}
					Logger.Log("[RequestSubscriptionHandler] subscription_id =" + text);
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.IAPHandler.listener.OnRequestSubscriptionSuccess(text);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text2);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x06002933 RID: 10547 RVA: 0x000C6818 File Offset: 0x000C4A18
			public IAPurchaseCallback getRequestSubscriptionWithPlanIDHandler()
			{
				return new IAPurchaseCallback(this.RequestSubscriptionWithPlanIDHandler);
			}

			// Token: 0x06002934 RID: 10548 RVA: 0x000C6828 File Offset: 0x000C4A28
			protected override void RequestSubscriptionWithPlanIDHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[RequestSubscriptionWithPlanIDHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				string text2 = "";
				try
				{
					num = (int)jsonData["statusCode"];
					text2 = (string)jsonData["message"];
				}
				catch (Exception arg)
				{
					Logger.Log("[RequestSubscriptionWithPlanIDHandler] statusCode, message ex=" + arg);
				}
				Logger.Log(string.Concat(new object[]
				{
					"[RequestSubscriptionWithPlanIDHandler] statusCode =",
					num,
					",errMessage=",
					text2
				}));
				if (num == 0)
				{
					try
					{
						text = (string)jsonData["subscription_id"];
					}
					catch (Exception arg2)
					{
						Logger.Log("[RequestSubscriptionWithPlanIDHandler] subscription_id ex=" + arg2);
					}
					Logger.Log("[RequestSubscriptionWithPlanIDHandler] subscription_id =" + text);
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.IAPHandler.listener.OnRequestSubscriptionWithPlanIDSuccess(text);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text2);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x06002935 RID: 10549 RVA: 0x000C694C File Offset: 0x000C4B4C
			public IAPurchaseCallback getSubscribeHandler()
			{
				return new IAPurchaseCallback(this.SubscribeHandler);
			}

			// Token: 0x06002936 RID: 10550 RVA: 0x000C695C File Offset: 0x000C4B5C
			protected override void SubscribeHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[SubscribeHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				string text2 = "";
				string text3 = "";
				long num2 = 0L;
				try
				{
					num = (int)jsonData["statusCode"];
					text2 = (string)jsonData["message"];
				}
				catch (Exception arg)
				{
					Logger.Log("[SubscribeHandler] statusCode, message ex=" + arg);
				}
				Logger.Log(string.Concat(new object[]
				{
					"[SubscribeHandler] statusCode =",
					num,
					",errMessage=",
					text2
				}));
				if (num == 0)
				{
					try
					{
						text = (string)jsonData["subscription_id"];
						text3 = (string)jsonData["plan_id"];
						num2 = (long)jsonData["subscribed_timestamp"];
					}
					catch (Exception arg2)
					{
						Logger.Log("[SubscribeHandler] subscription_id, plan_id ex=" + arg2);
					}
					Logger.Log(string.Concat(new string[]
					{
						"[SubscribeHandler] subscription_id =",
						text,
						", plan_id=",
						text3,
						", timestamp=",
						num2.ToString()
					}));
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.IAPHandler.listener.OnSubscribeSuccess(text);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text2);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x06002937 RID: 10551 RVA: 0x000C6ADC File Offset: 0x000C4CDC
			public IAPurchaseCallback getQuerySubscriptionHandler()
			{
				return new IAPurchaseCallback(this.QuerySubscriptionHandler);
			}

			// Token: 0x06002938 RID: 10552 RVA: 0x000C6AEC File Offset: 0x000C4CEC
			protected override void QuerySubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[QuerySubscriptionHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				List<IAPurchase.Subscription> list = null;
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[QuerySubscriptionHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[QuerySubscriptionHandler] statusCode =",
						num,
						",errMessage=",
						text
					}));
					if (num == 0)
					{
						try
						{
							list = JsonMapper.ToObject<IAPurchase.QuerySubscritionResponse>(message).subscriptions;
						}
						catch (Exception arg2)
						{
							Logger.Log("[QuerySubscriptionHandler] ex =" + arg2);
						}
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0 && list != null && list.Count > 0)
						{
							IAPurchase.IAPHandler.listener.OnQuerySubscriptionSuccess(list.ToArray());
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x06002939 RID: 10553 RVA: 0x000C6C0C File Offset: 0x000C4E0C
			public IAPurchaseCallback getQuerySubscriptionListHandler()
			{
				return new IAPurchaseCallback(this.QuerySubscriptionListHandler);
			}

			// Token: 0x0600293A RID: 10554 RVA: 0x000C6C1C File Offset: 0x000C4E1C
			protected override void QuerySubscriptionListHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[QuerySubscriptionListHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				string text = "";
				List<IAPurchase.Subscription> list = null;
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[QuerySubscriptionListHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[QuerySubscriptionListHandler] statusCode =",
						num,
						",errMessage=",
						text
					}));
					if (num == 0)
					{
						try
						{
							list = JsonMapper.ToObject<IAPurchase.QuerySubscritionResponse>(message).subscriptions;
						}
						catch (Exception arg2)
						{
							Logger.Log("[QuerySubscriptionListHandler] ex =" + arg2);
						}
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0 && list != null && list.Count > 0)
						{
							IAPurchase.IAPHandler.listener.OnQuerySubscriptionListSuccess(list.ToArray());
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x0600293B RID: 10555 RVA: 0x000C6D3C File Offset: 0x000C4F3C
			public IAPurchaseCallback getCancelSubscriptionHandler()
			{
				return new IAPurchaseCallback(this.CancelSubscriptionHandler);
			}

			// Token: 0x0600293C RID: 10556 RVA: 0x000C6D4C File Offset: 0x000C4F4C
			protected override void CancelSubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log("[CancelSubscriptionHandler] message=" + message);
				JsonData jsonData = JsonMapper.ToObject(message);
				int num = -1;
				bool bCanceled = false;
				string text = "";
				if (code == 0)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text = (string)jsonData["message"];
					}
					catch (Exception arg)
					{
						Logger.Log("[CancelSubscriptionHandler] statusCode, message ex=" + arg);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[CancelSubscriptionHandler] statusCode =",
						num,
						",errMessage=",
						text
					}));
					if (num == 0)
					{
						bCanceled = true;
						Logger.Log("[CancelSubscriptionHandler] isCanceled = " + bCanceled.ToString());
					}
				}
				if (IAPurchase.IAPHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							IAPurchase.IAPHandler.listener.OnCancelSubscriptionSuccess(bCanceled);
							return;
						}
						IAPurchase.IAPHandler.listener.OnFailure(num, text);
						return;
					}
					else
					{
						IAPurchase.IAPHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x04002685 RID: 9861
			private static IAPurchase.IAPurchaseListener listener;
		}

		// Token: 0x020005A0 RID: 1440
		private abstract class BaseHandler
		{
			// Token: 0x0600293D RID: 10557
			protected abstract void IsReadyHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x0600293E RID: 10558
			protected abstract void RequestHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x0600293F RID: 10559
			protected abstract void PurchaseHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002940 RID: 10560
			protected abstract void QueryHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002941 RID: 10561
			protected abstract void QueryListHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002942 RID: 10562
			protected abstract void BalanceHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002943 RID: 10563
			protected abstract void RequestSubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002944 RID: 10564
			protected abstract void RequestSubscriptionWithPlanIDHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002945 RID: 10565
			protected abstract void SubscribeHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002946 RID: 10566
			protected abstract void QuerySubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002947 RID: 10567
			protected abstract void QuerySubscriptionListHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x06002948 RID: 10568
			protected abstract void CancelSubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
		}

		// Token: 0x020005A1 RID: 1441
		public class IAPurchaseListener
		{
			// Token: 0x0600294A RID: 10570 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnSuccess(string pchCurrencyName)
			{
			}

			// Token: 0x0600294B RID: 10571 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnRequestSuccess(string pchPurchaseId)
			{
			}

			// Token: 0x0600294C RID: 10572 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnPurchaseSuccess(string pchPurchaseId)
			{
			}

			// Token: 0x0600294D RID: 10573 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnQuerySuccess(IAPurchase.QueryResponse response)
			{
			}

			// Token: 0x0600294E RID: 10574 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnQuerySuccess(IAPurchase.QueryListResponse response)
			{
			}

			// Token: 0x0600294F RID: 10575 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnBalanceSuccess(string pchBalance)
			{
			}

			// Token: 0x06002950 RID: 10576 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnFailure(int nCode, string pchMessage)
			{
			}

			// Token: 0x06002951 RID: 10577 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnRequestSubscriptionSuccess(string pchSubscriptionId)
			{
			}

			// Token: 0x06002952 RID: 10578 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnRequestSubscriptionWithPlanIDSuccess(string pchSubscriptionId)
			{
			}

			// Token: 0x06002953 RID: 10579 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnSubscribeSuccess(string pchSubscriptionId)
			{
			}

			// Token: 0x06002954 RID: 10580 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnQuerySubscriptionSuccess(IAPurchase.Subscription[] subscriptionlist)
			{
			}

			// Token: 0x06002955 RID: 10581 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnQuerySubscriptionListSuccess(IAPurchase.Subscription[] subscriptionlist)
			{
			}

			// Token: 0x06002956 RID: 10582 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnCancelSubscriptionSuccess(bool bCanceled)
			{
			}
		}

		// Token: 0x020005A2 RID: 1442
		public class QueryResponse
		{
			// Token: 0x1700030D RID: 781
			// (get) Token: 0x06002958 RID: 10584 RVA: 0x000C6E44 File Offset: 0x000C5044
			// (set) Token: 0x06002959 RID: 10585 RVA: 0x000C6E4C File Offset: 0x000C504C
			public string order_id { get; set; }

			// Token: 0x1700030E RID: 782
			// (get) Token: 0x0600295A RID: 10586 RVA: 0x000C6E55 File Offset: 0x000C5055
			// (set) Token: 0x0600295B RID: 10587 RVA: 0x000C6E5D File Offset: 0x000C505D
			public string purchase_id { get; set; }

			// Token: 0x1700030F RID: 783
			// (get) Token: 0x0600295C RID: 10588 RVA: 0x000C6E66 File Offset: 0x000C5066
			// (set) Token: 0x0600295D RID: 10589 RVA: 0x000C6E6E File Offset: 0x000C506E
			public string status { get; set; }

			// Token: 0x17000310 RID: 784
			// (get) Token: 0x0600295E RID: 10590 RVA: 0x000C6E77 File Offset: 0x000C5077
			// (set) Token: 0x0600295F RID: 10591 RVA: 0x000C6E7F File Offset: 0x000C507F
			public string price { get; set; }

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06002960 RID: 10592 RVA: 0x000C6E88 File Offset: 0x000C5088
			// (set) Token: 0x06002961 RID: 10593 RVA: 0x000C6E90 File Offset: 0x000C5090
			public string currency { get; set; }

			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06002962 RID: 10594 RVA: 0x000C6E99 File Offset: 0x000C5099
			// (set) Token: 0x06002963 RID: 10595 RVA: 0x000C6EA1 File Offset: 0x000C50A1
			public long paid_timestamp { get; set; }
		}

		// Token: 0x020005A3 RID: 1443
		public class QueryResponse2
		{
			// Token: 0x17000313 RID: 787
			// (get) Token: 0x06002965 RID: 10597 RVA: 0x000C6EAA File Offset: 0x000C50AA
			// (set) Token: 0x06002966 RID: 10598 RVA: 0x000C6EB2 File Offset: 0x000C50B2
			public string order_id { get; set; }

			// Token: 0x17000314 RID: 788
			// (get) Token: 0x06002967 RID: 10599 RVA: 0x000C6EBB File Offset: 0x000C50BB
			// (set) Token: 0x06002968 RID: 10600 RVA: 0x000C6EC3 File Offset: 0x000C50C3
			public string app_id { get; set; }

			// Token: 0x17000315 RID: 789
			// (get) Token: 0x06002969 RID: 10601 RVA: 0x000C6ECC File Offset: 0x000C50CC
			// (set) Token: 0x0600296A RID: 10602 RVA: 0x000C6ED4 File Offset: 0x000C50D4
			public string purchase_id { get; set; }

			// Token: 0x17000316 RID: 790
			// (get) Token: 0x0600296B RID: 10603 RVA: 0x000C6EDD File Offset: 0x000C50DD
			// (set) Token: 0x0600296C RID: 10604 RVA: 0x000C6EE5 File Offset: 0x000C50E5
			public string user_data { get; set; }

			// Token: 0x17000317 RID: 791
			// (get) Token: 0x0600296D RID: 10605 RVA: 0x000C6EEE File Offset: 0x000C50EE
			// (set) Token: 0x0600296E RID: 10606 RVA: 0x000C6EF6 File Offset: 0x000C50F6
			public string price { get; set; }

			// Token: 0x17000318 RID: 792
			// (get) Token: 0x0600296F RID: 10607 RVA: 0x000C6EFF File Offset: 0x000C50FF
			// (set) Token: 0x06002970 RID: 10608 RVA: 0x000C6F07 File Offset: 0x000C5107
			public string currency { get; set; }

			// Token: 0x17000319 RID: 793
			// (get) Token: 0x06002971 RID: 10609 RVA: 0x000C6F10 File Offset: 0x000C5110
			// (set) Token: 0x06002972 RID: 10610 RVA: 0x000C6F18 File Offset: 0x000C5118
			public long paid_timestamp { get; set; }
		}

		// Token: 0x020005A4 RID: 1444
		public class QueryListResponse
		{
			// Token: 0x1700031A RID: 794
			// (get) Token: 0x06002974 RID: 10612 RVA: 0x000C6F21 File Offset: 0x000C5121
			// (set) Token: 0x06002975 RID: 10613 RVA: 0x000C6F29 File Offset: 0x000C5129
			public int total { get; set; }

			// Token: 0x1700031B RID: 795
			// (get) Token: 0x06002976 RID: 10614 RVA: 0x000C6F32 File Offset: 0x000C5132
			// (set) Token: 0x06002977 RID: 10615 RVA: 0x000C6F3A File Offset: 0x000C513A
			public int from { get; set; }

			// Token: 0x1700031C RID: 796
			// (get) Token: 0x06002978 RID: 10616 RVA: 0x000C6F43 File Offset: 0x000C5143
			// (set) Token: 0x06002979 RID: 10617 RVA: 0x000C6F4B File Offset: 0x000C514B
			public int to { get; set; }

			// Token: 0x04002696 RID: 9878
			public List<IAPurchase.QueryResponse2> purchaseList;
		}

		// Token: 0x020005A5 RID: 1445
		public class StatusDetailTransaction
		{
			// Token: 0x1700031D RID: 797
			// (get) Token: 0x0600297B RID: 10619 RVA: 0x000C6F54 File Offset: 0x000C5154
			// (set) Token: 0x0600297C RID: 10620 RVA: 0x000C6F5C File Offset: 0x000C515C
			public long create_time { get; set; }

			// Token: 0x1700031E RID: 798
			// (get) Token: 0x0600297D RID: 10621 RVA: 0x000C6F65 File Offset: 0x000C5165
			// (set) Token: 0x0600297E RID: 10622 RVA: 0x000C6F6D File Offset: 0x000C516D
			public string payment_method { get; set; }

			// Token: 0x1700031F RID: 799
			// (get) Token: 0x0600297F RID: 10623 RVA: 0x000C6F76 File Offset: 0x000C5176
			// (set) Token: 0x06002980 RID: 10624 RVA: 0x000C6F7E File Offset: 0x000C517E
			public string status { get; set; }
		}

		// Token: 0x020005A6 RID: 1446
		public class StatusDetail
		{
			// Token: 0x17000320 RID: 800
			// (get) Token: 0x06002982 RID: 10626 RVA: 0x000C6F87 File Offset: 0x000C5187
			// (set) Token: 0x06002983 RID: 10627 RVA: 0x000C6F8F File Offset: 0x000C518F
			public long date_next_charge { get; set; }

			// Token: 0x17000321 RID: 801
			// (get) Token: 0x06002984 RID: 10628 RVA: 0x000C6F98 File Offset: 0x000C5198
			// (set) Token: 0x06002985 RID: 10629 RVA: 0x000C6FA0 File Offset: 0x000C51A0
			public IAPurchase.StatusDetailTransaction[] transactions { get; set; }

			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06002986 RID: 10630 RVA: 0x000C6FA9 File Offset: 0x000C51A9
			// (set) Token: 0x06002987 RID: 10631 RVA: 0x000C6FB1 File Offset: 0x000C51B1
			public string cancel_reason { get; set; }
		}

		// Token: 0x020005A7 RID: 1447
		public class TimePeriod
		{
			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06002989 RID: 10633 RVA: 0x000C6FBA File Offset: 0x000C51BA
			// (set) Token: 0x0600298A RID: 10634 RVA: 0x000C6FC2 File Offset: 0x000C51C2
			public string time_type { get; set; }

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x0600298B RID: 10635 RVA: 0x000C6FCB File Offset: 0x000C51CB
			// (set) Token: 0x0600298C RID: 10636 RVA: 0x000C6FD3 File Offset: 0x000C51D3
			public int value { get; set; }
		}

		// Token: 0x020005A8 RID: 1448
		public class Subscription
		{
			// Token: 0x17000325 RID: 805
			// (get) Token: 0x0600298E RID: 10638 RVA: 0x000C6FDC File Offset: 0x000C51DC
			// (set) Token: 0x0600298F RID: 10639 RVA: 0x000C6FE4 File Offset: 0x000C51E4
			public string app_id { get; set; }

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06002990 RID: 10640 RVA: 0x000C6FED File Offset: 0x000C51ED
			// (set) Token: 0x06002991 RID: 10641 RVA: 0x000C6FF5 File Offset: 0x000C51F5
			public string order_id { get; set; }

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06002992 RID: 10642 RVA: 0x000C6FFE File Offset: 0x000C51FE
			// (set) Token: 0x06002993 RID: 10643 RVA: 0x000C7006 File Offset: 0x000C5206
			public string subscription_id { get; set; }

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06002994 RID: 10644 RVA: 0x000C700F File Offset: 0x000C520F
			// (set) Token: 0x06002995 RID: 10645 RVA: 0x000C7017 File Offset: 0x000C5217
			public string price { get; set; }

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06002996 RID: 10646 RVA: 0x000C7020 File Offset: 0x000C5220
			// (set) Token: 0x06002997 RID: 10647 RVA: 0x000C7028 File Offset: 0x000C5228
			public string currency { get; set; }

			// Token: 0x1700032A RID: 810
			// (get) Token: 0x06002998 RID: 10648 RVA: 0x000C7031 File Offset: 0x000C5231
			// (set) Token: 0x06002999 RID: 10649 RVA: 0x000C7039 File Offset: 0x000C5239
			public long subscribed_timestamp { get; set; }

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x0600299A RID: 10650 RVA: 0x000C7042 File Offset: 0x000C5242
			// (set) Token: 0x0600299B RID: 10651 RVA: 0x000C704A File Offset: 0x000C524A
			public IAPurchase.TimePeriod free_trial_period { get; set; }

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x0600299C RID: 10652 RVA: 0x000C7053 File Offset: 0x000C5253
			// (set) Token: 0x0600299D RID: 10653 RVA: 0x000C705B File Offset: 0x000C525B
			public IAPurchase.TimePeriod charge_period { get; set; }

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x0600299E RID: 10654 RVA: 0x000C7064 File Offset: 0x000C5264
			// (set) Token: 0x0600299F RID: 10655 RVA: 0x000C706C File Offset: 0x000C526C
			public int number_of_charge_period { get; set; }

			// Token: 0x1700032E RID: 814
			// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000C7075 File Offset: 0x000C5275
			// (set) Token: 0x060029A1 RID: 10657 RVA: 0x000C707D File Offset: 0x000C527D
			public string plan_id { get; set; }

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000C7086 File Offset: 0x000C5286
			// (set) Token: 0x060029A3 RID: 10659 RVA: 0x000C708E File Offset: 0x000C528E
			public string plan_name { get; set; }

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x060029A4 RID: 10660 RVA: 0x000C7097 File Offset: 0x000C5297
			// (set) Token: 0x060029A5 RID: 10661 RVA: 0x000C709F File Offset: 0x000C529F
			public string status { get; set; }

			// Token: 0x17000331 RID: 817
			// (get) Token: 0x060029A6 RID: 10662 RVA: 0x000C70A8 File Offset: 0x000C52A8
			// (set) Token: 0x060029A7 RID: 10663 RVA: 0x000C70B0 File Offset: 0x000C52B0
			public IAPurchase.StatusDetail status_detail { get; set; }
		}

		// Token: 0x020005A9 RID: 1449
		public class QuerySubscritionResponse
		{
			// Token: 0x17000332 RID: 818
			// (get) Token: 0x060029A9 RID: 10665 RVA: 0x000C70B9 File Offset: 0x000C52B9
			// (set) Token: 0x060029AA RID: 10666 RVA: 0x000C70C1 File Offset: 0x000C52C1
			public int statusCode { get; set; }

			// Token: 0x17000333 RID: 819
			// (get) Token: 0x060029AB RID: 10667 RVA: 0x000C70CA File Offset: 0x000C52CA
			// (set) Token: 0x060029AC RID: 10668 RVA: 0x000C70D2 File Offset: 0x000C52D2
			public string message { get; set; }

			// Token: 0x17000334 RID: 820
			// (get) Token: 0x060029AD RID: 10669 RVA: 0x000C70DB File Offset: 0x000C52DB
			// (set) Token: 0x060029AE RID: 10670 RVA: 0x000C70E3 File Offset: 0x000C52E3
			public List<IAPurchase.Subscription> subscriptions { get; set; }
		}
	}
}
