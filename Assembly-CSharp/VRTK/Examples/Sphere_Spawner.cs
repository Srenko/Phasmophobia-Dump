using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000368 RID: 872
	public class Sphere_Spawner : MonoBehaviour
	{
		// Token: 0x06001E03 RID: 7683 RVA: 0x00098860 File Offset: 0x00096A60
		private void Start()
		{
			if (base.GetComponent<VRTK_ControllerEvents>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"Sphere_Spawner",
					"VRTK_ControllerEvents",
					"the same"
				}));
				return;
			}
			base.GetComponent<VRTK_ControllerEvents>().TriggerPressed += this.DoTriggerPressed;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += this.DoTouchpadPressed;
			this.spawnMe = GameObject.Find("SpawnMe");
			this.position = this.spawnMe.transform.position;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000988F9 File Offset: 0x00096AF9
		private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
		{
			base.Invoke("CreateSphere", 0f);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0009890C File Offset: 0x00096B0C
		private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
		{
			for (int i = 0; i < 20; i++)
			{
				base.Invoke("CreateSphere", 0f);
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00098936 File Offset: 0x00096B36
		private void CreateSphere()
		{
			Object.Instantiate<GameObject>(this.spawnMe, this.position, Quaternion.identity);
		}

		// Token: 0x040017AA RID: 6058
		private GameObject spawnMe;

		// Token: 0x040017AB RID: 6059
		private Vector3 position;
	}
}
