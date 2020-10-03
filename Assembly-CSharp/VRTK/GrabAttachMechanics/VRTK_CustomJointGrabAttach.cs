using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x02000347 RID: 839
	[AddComponentMenu("VRTK/Scripts/Interactions/Grab Attach Mechanics/VRTK_CustomJointGrabAttach")]
	public class VRTK_CustomJointGrabAttach : VRTK_BaseJointGrabAttach
	{
		// Token: 0x06001D65 RID: 7525 RVA: 0x000963C6 File Offset: 0x000945C6
		protected override void Initialise()
		{
			base.Initialise();
			this.CopyCustomJoint();
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000963D4 File Offset: 0x000945D4
		protected override void CreateJoint(GameObject obj)
		{
			if (!this.jointHolder)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_CustomJointGrabAttach",
					"Joint",
					"customJoint",
					"the same"
				}));
				return;
			}
			Joint component = this.jointHolder.GetComponent<Joint>();
			string name = base.gameObject.name;
			VRTK_SharedMethods.CloneComponent(component, obj, true);
			base.gameObject.name = name;
			this.givenJoint = (obj.GetComponent(component.GetType()) as Joint);
			base.CreateJoint(obj);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0009646B File Offset: 0x0009466B
		protected override void DestroyJoint(bool withDestroyImmediate, bool applyGrabbingObjectVelocity)
		{
			base.DestroyJoint(true, true);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00096478 File Offset: 0x00094678
		protected virtual void CopyCustomJoint()
		{
			if (this.customJoint)
			{
				this.jointHolder = new GameObject();
				this.jointHolder.transform.SetParent(base.transform);
				this.jointHolder.AddComponent<Rigidbody>().isKinematic = true;
				VRTK_SharedMethods.CloneComponent(this.customJoint, this.jointHolder, true);
				this.jointHolder.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					"JointHolder"
				});
				this.jointHolder.SetActive(false);
				Object.Destroy(this.customJoint);
				this.customJoint = this.jointHolder.GetComponent<Joint>();
			}
		}

		// Token: 0x04001738 RID: 5944
		[Tooltip("The joint to use for the grab attach joint.")]
		public Joint customJoint;

		// Token: 0x04001739 RID: 5945
		protected GameObject jointHolder;
	}
}
