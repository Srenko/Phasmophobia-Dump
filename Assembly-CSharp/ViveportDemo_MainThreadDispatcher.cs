using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Viveport;

// Token: 0x020001FF RID: 511
public class ViveportDemo_MainThreadDispatcher : MonoBehaviour
{
	// Token: 0x06000E56 RID: 3670 RVA: 0x0005CAE1 File Offset: 0x0005ACE1
	private void Start()
	{
		ViveportDemo_MainThreadDispatcher.myApiResultText = this.uiText;
		Api.Init(new StatusCallback(this.InitCallback), ViveportDemo_MainThreadDispatcher.appId);
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0005CB05 File Offset: 0x0005AD05
	private void InitCallback(int errorCode)
	{
		if (errorCode == 0)
		{
			IAPurchase.IsReady(new ViveportDemo_MainThreadDispatcher.ThisCallbackWillWorkFine_01(), ViveportDemo_MainThreadDispatcher.apiKey);
		}
	}

	// Token: 0x04000EBB RID: 3771
	public Text uiText;

	// Token: 0x04000EBC RID: 3772
	private static Text myApiResultText;

	// Token: 0x04000EBD RID: 3773
	private static readonly string appId = "Your APP ID";

	// Token: 0x04000EBE RID: 3774
	private static readonly string apiKey = "Your API KEY";

	// Token: 0x0200058C RID: 1420
	private class ThisCallbackWillNotWorkFine : IAPurchase.IAPurchaseListener
	{
		// Token: 0x060028FA RID: 10490 RVA: 0x000C58FB File Offset: 0x000C3AFB
		public override void OnSuccess(string pchCurrencyName)
		{
			ViveportDemo_MainThreadDispatcher.myApiResultText.text = string.Format("The Currency is: {0}", pchCurrencyName);
		}
	}

	// Token: 0x0200058D RID: 1421
	private class ThisCallbackWillWorkFine_01 : IAPurchase.IAPurchaseListener
	{
		// Token: 0x060028FC RID: 10492 RVA: 0x000C591C File Offset: 0x000C3B1C
		public override void OnSuccess(string pchCurrencyName)
		{
			Action action = delegate()
			{
				ViveportDemo_MainThreadDispatcher.myApiResultText.text = string.Format("The Currency is: {0}", pchCurrencyName);
			};
			MainThreadDispatcher.Instance().Enqueue(action);
		}
	}

	// Token: 0x0200058E RID: 1422
	private class ThisCallbackWillWorkFine_02 : IAPurchase.IAPurchaseListener
	{
		// Token: 0x060028FE RID: 10494 RVA: 0x000C594C File Offset: 0x000C3B4C
		public override void OnSuccess(string pchCurrencyName)
		{
			MainThreadDispatcher.Instance().Enqueue(this.ShowResult(pchCurrencyName));
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000C595F File Offset: 0x000C3B5F
		private IEnumerator ShowResult(string pchCurrencyName)
		{
			ViveportDemo_MainThreadDispatcher.myApiResultText.text = string.Format("The Currency is: {0}", pchCurrencyName);
			yield return null;
			yield break;
		}
	}
}
