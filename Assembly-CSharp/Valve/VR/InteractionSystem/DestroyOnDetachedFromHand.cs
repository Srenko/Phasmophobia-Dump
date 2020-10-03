using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000417 RID: 1047
	[RequireComponent(typeof(Interactable))]
	public class DestroyOnDetachedFromHand : MonoBehaviour
	{
		// Token: 0x06002056 RID: 8278 RVA: 0x00021808 File Offset: 0x0001FA08
		private void OnDetachedFromHand(Hand hand)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
