using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000420 RID: 1056
	public class HideOnHandFocusLost : MonoBehaviour
	{
		// Token: 0x0600208F RID: 8335 RVA: 0x0000AC1C File Offset: 0x00008E1C
		private void OnHandFocusLost(Hand hand)
		{
			base.gameObject.SetActive(false);
		}
	}
}
