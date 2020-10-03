using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000426 RID: 1062
	public class ItemPackage : MonoBehaviour
	{
		// Token: 0x04001E24 RID: 7716
		public new string name;

		// Token: 0x04001E25 RID: 7717
		public ItemPackage.ItemPackageType packageType;

		// Token: 0x04001E26 RID: 7718
		public GameObject itemPrefab;

		// Token: 0x04001E27 RID: 7719
		public GameObject otherHandItemPrefab;

		// Token: 0x04001E28 RID: 7720
		public GameObject previewPrefab;

		// Token: 0x04001E29 RID: 7721
		public GameObject fadedPreviewPrefab;

		// Token: 0x0200077B RID: 1915
		public enum ItemPackageType
		{
			// Token: 0x04002920 RID: 10528
			Unrestricted,
			// Token: 0x04002921 RID: 10529
			OneHanded,
			// Token: 0x04002922 RID: 10530
			TwoHanded
		}
	}
}
