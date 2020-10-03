using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002BA RID: 698
	public sealed class VRTK_PlayerObject : MonoBehaviour
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x0007C450 File Offset: 0x0007A650
		public static void SetPlayerObject(GameObject obj, VRTK_PlayerObject.ObjectTypes objType)
		{
			VRTK_PlayerObject vrtk_PlayerObject = obj.GetComponent<VRTK_PlayerObject>();
			if (vrtk_PlayerObject == null)
			{
				vrtk_PlayerObject = obj.AddComponent<VRTK_PlayerObject>();
			}
			vrtk_PlayerObject.objectType = objType;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0007C47C File Offset: 0x0007A67C
		public static bool IsPlayerObject(GameObject obj, VRTK_PlayerObject.ObjectTypes ofType = VRTK_PlayerObject.ObjectTypes.Null)
		{
			foreach (VRTK_PlayerObject vrtk_PlayerObject in obj.GetComponentsInParent<VRTK_PlayerObject>(true))
			{
				if (ofType == VRTK_PlayerObject.ObjectTypes.Null || ofType == vrtk_PlayerObject.objectType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040012FE RID: 4862
		public VRTK_PlayerObject.ObjectTypes objectType;

		// Token: 0x020005E1 RID: 1505
		public enum ObjectTypes
		{
			// Token: 0x040027CC RID: 10188
			Null,
			// Token: 0x040027CD RID: 10189
			CameraRig,
			// Token: 0x040027CE RID: 10190
			Headset,
			// Token: 0x040027CF RID: 10191
			Controller,
			// Token: 0x040027D0 RID: 10192
			Pointer,
			// Token: 0x040027D1 RID: 10193
			Highlighter,
			// Token: 0x040027D2 RID: 10194
			Collider
		}
	}
}
