using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000288 RID: 648
	public class VRTK_ContentHandler : MonoBehaviour
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x0006C6D0 File Offset: 0x0006A8D0
		protected virtual void Start()
		{
			if (base.GetComponent<VRTK_InteractableObject>() == null)
			{
				foreach (VRTK_InteractableObject vrtk_InteractableObject in base.GetComponentsInChildren<VRTK_InteractableObject>())
				{
					if (vrtk_InteractableObject.GetComponent<VRTK_ContentHandler>() == null)
					{
						VRTK_ContentHandler vrtk_ContentHandler = vrtk_InteractableObject.gameObject.AddComponent<VRTK_ContentHandler>();
						vrtk_ContentHandler.control = this.control;
						vrtk_ContentHandler.inside = this.inside;
						vrtk_ContentHandler.outside = this.outside;
					}
				}
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0006C740 File Offset: 0x0006A940
		protected virtual void OnCollisionEnter(Collision collision)
		{
			Bounds bounds = VRTK_SharedMethods.GetBounds(this.inside, null, this.control.GetContent().transform);
			if (VRTK_SharedMethods.GetBounds(base.transform, null, null).Intersects(bounds))
			{
				base.transform.SetParent(this.control.GetContent().transform);
				return;
			}
			base.transform.SetParent(this.outside);
		}

		// Token: 0x04001100 RID: 4352
		[Tooltip("The 3D control responsible for the content.")]
		public VRTK_Control control;

		// Token: 0x04001101 RID: 4353
		[Tooltip("The transform containing the meshes or colliders that define the inside of the control.")]
		public Transform inside;

		// Token: 0x04001102 RID: 4354
		[Tooltip("Any transform that will act as the parent while the object is not inside the control.")]
		public Transform outside;
	}
}
