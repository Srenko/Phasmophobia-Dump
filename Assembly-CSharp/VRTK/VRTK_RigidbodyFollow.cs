using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200030A RID: 778
	[AddComponentMenu("VRTK/Scripts/Utilities/Object Follow/VRTK_RigidbodyFollow")]
	public class VRTK_RigidbodyFollow : VRTK_ObjectFollow
	{
		// Token: 0x06001AFF RID: 6911 RVA: 0x0008DADB File Offset: 0x0008BCDB
		public override void Follow()
		{
			this.CacheRigidbodies();
			base.Follow();
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0008DAE9 File Offset: 0x0008BCE9
		protected virtual void OnDisable()
		{
			this.rigidbodyToFollow = null;
			this.rigidbodyToChange = null;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0008DAF9 File Offset: 0x0008BCF9
		protected virtual void FixedUpdate()
		{
			this.Follow();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0008DB01 File Offset: 0x0008BD01
		protected override Vector3 GetPositionToFollow()
		{
			return this.rigidbodyToFollow.position;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0008DB10 File Offset: 0x0008BD10
		protected override void SetPositionOnGameObject(Vector3 newPosition)
		{
			switch (this.movementOption)
			{
			case VRTK_RigidbodyFollow.MovementOption.Set:
				this.rigidbodyToChange.position = newPosition;
				return;
			case VRTK_RigidbodyFollow.MovementOption.Move:
				this.rigidbodyToChange.MovePosition(newPosition);
				return;
			case VRTK_RigidbodyFollow.MovementOption.Add:
				this.rigidbodyToChange.AddForce(newPosition - this.rigidbodyToChange.position);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0008DB6D File Offset: 0x0008BD6D
		protected override Quaternion GetRotationToFollow()
		{
			return this.rigidbodyToFollow.rotation;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0008DB7C File Offset: 0x0008BD7C
		protected override void SetRotationOnGameObject(Quaternion newRotation)
		{
			switch (this.movementOption)
			{
			case VRTK_RigidbodyFollow.MovementOption.Set:
				this.rigidbodyToChange.rotation = newRotation;
				return;
			case VRTK_RigidbodyFollow.MovementOption.Move:
				this.rigidbodyToChange.MoveRotation(newRotation);
				return;
			case VRTK_RigidbodyFollow.MovementOption.Add:
				this.rigidbodyToChange.AddTorque(newRotation * Quaternion.Inverse(this.rigidbodyToChange.rotation).eulerAngles);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0008DBE6 File Offset: 0x0008BDE6
		protected override Vector3 GetScaleToFollow()
		{
			return this.rigidbodyToFollow.transform.localScale;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0008DBF8 File Offset: 0x0008BDF8
		protected virtual void CacheRigidbodies()
		{
			if (this.gameObjectToFollow == null || this.gameObjectToChange == null || (this.rigidbodyToFollow != null && this.rigidbodyToChange != null))
			{
				return;
			}
			this.rigidbodyToFollow = this.gameObjectToFollow.GetComponent<Rigidbody>();
			this.rigidbodyToChange = this.gameObjectToChange.GetComponent<Rigidbody>();
		}

		// Token: 0x040015E0 RID: 5600
		[Tooltip("Specifies how to position and rotate the rigidbody.")]
		public VRTK_RigidbodyFollow.MovementOption movementOption;

		// Token: 0x040015E1 RID: 5601
		protected Rigidbody rigidbodyToFollow;

		// Token: 0x040015E2 RID: 5602
		protected Rigidbody rigidbodyToChange;

		// Token: 0x020005FB RID: 1531
		public enum MovementOption
		{
			// Token: 0x04002845 RID: 10309
			Set,
			// Token: 0x04002846 RID: 10310
			Move,
			// Token: 0x04002847 RID: 10311
			Add
		}
	}
}
