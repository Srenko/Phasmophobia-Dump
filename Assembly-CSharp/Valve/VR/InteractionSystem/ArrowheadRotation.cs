using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000443 RID: 1091
	public class ArrowheadRotation : MonoBehaviour
	{
		// Token: 0x06002199 RID: 8601 RVA: 0x000A62EC File Offset: 0x000A44EC
		private void Start()
		{
			float x = Random.Range(0f, 180f);
			base.transform.localEulerAngles = new Vector3(x, -90f, 90f);
		}
	}
}
