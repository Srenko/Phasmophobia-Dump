using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000448 RID: 1096
	public class ExplosionWobble : MonoBehaviour
	{
		// Token: 0x060021B1 RID: 8625 RVA: 0x000A6D58 File Offset: 0x000A4F58
		public void ExplosionEvent(Vector3 explosionPos)
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			if (component)
			{
				component.AddExplosionForce(2000f, explosionPos, 10f);
			}
		}
	}
}
