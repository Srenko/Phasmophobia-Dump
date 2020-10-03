using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002F1 RID: 753
	[AddComponentMenu("VRTK/Scripts/Presence/VRTK_HeadsetCollision")]
	public class VRTK_HeadsetCollision : MonoBehaviour
	{
		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06001A13 RID: 6675 RVA: 0x0008AB1C File Offset: 0x00088D1C
		// (remove) Token: 0x06001A14 RID: 6676 RVA: 0x0008AB54 File Offset: 0x00088D54
		public event HeadsetCollisionEventHandler HeadsetCollisionDetect;

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06001A15 RID: 6677 RVA: 0x0008AB8C File Offset: 0x00088D8C
		// (remove) Token: 0x06001A16 RID: 6678 RVA: 0x0008ABC4 File Offset: 0x00088DC4
		public event HeadsetCollisionEventHandler HeadsetCollisionEnded;

		// Token: 0x06001A17 RID: 6679 RVA: 0x0008ABF9 File Offset: 0x00088DF9
		public virtual void OnHeadsetCollisionDetect(HeadsetCollisionEventArgs e)
		{
			if (this.HeadsetCollisionDetect != null)
			{
				this.HeadsetCollisionDetect(this, e);
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0008AC10 File Offset: 0x00088E10
		public virtual void OnHeadsetCollisionEnded(HeadsetCollisionEventArgs e)
		{
			if (this.HeadsetCollisionEnded != null)
			{
				this.HeadsetCollisionEnded(this, e);
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0008AC27 File Offset: 0x00088E27
		public virtual bool IsColliding()
		{
			return this.headsetColliding;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0008AC2F File Offset: 0x00088E2F
		public virtual GameObject GetHeadsetColliderContainer()
		{
			return this.headsetColliderContainer;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0008AC37 File Offset: 0x00088E37
		protected virtual void OnEnable()
		{
			VRTK_ObjectCache.registeredHeadsetCollider = this;
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			if (this.headset != null)
			{
				this.headsetColliding = false;
				this.SetupHeadset();
				VRTK_PlayerObject.SetPlayerObject(this.GetHeadsetColliderContainer(), VRTK_PlayerObject.ObjectTypes.Headset);
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0008AC71 File Offset: 0x00088E71
		protected virtual void OnDisable()
		{
			if (this.headset != null && this.headsetColliderScript != null)
			{
				this.headsetColliderScript.EndCollision(this.collidingWith);
				VRTK_ObjectCache.registeredHeadsetCollider = null;
				this.TearDownHeadset();
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0008ACAC File Offset: 0x00088EAC
		protected virtual void Update()
		{
			if (this.headsetColliderContainer != null && this.headsetColliderContainer.transform.parent != this.headset)
			{
				this.headsetColliderContainer.transform.SetParent(this.headset);
				this.headsetColliderContainer.transform.localPosition = Vector3.zero;
				this.headsetColliderContainer.transform.localRotation = this.headset.localRotation;
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0008AD2C File Offset: 0x00088F2C
		protected virtual void CreateHeadsetColliderContainer()
		{
			if (this.headsetColliderContainer == null)
			{
				this.headsetColliderContainer = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					"HeadsetColliderContainer"
				}));
				this.headsetColliderContainer.transform.position = Vector3.zero;
				this.headsetColliderContainer.transform.localRotation = this.headset.localRotation;
				this.headsetColliderContainer.transform.localScale = Vector3.one;
				this.headsetColliderContainer.layer = LayerMask.NameToLayer("Ignore Raycast");
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0008ADC0 File Offset: 0x00088FC0
		protected virtual void SetupHeadset()
		{
			Rigidbody rigidbody = null;
			if (rigidbody == null)
			{
				this.CreateHeadsetColliderContainer();
				rigidbody = this.headsetColliderContainer.AddComponent<Rigidbody>();
				rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				this.generateRigidbody = true;
			}
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;
			Collider collider = null;
			if (collider == null)
			{
				this.CreateHeadsetColliderContainer();
				SphereCollider sphereCollider = this.headsetColliderContainer.gameObject.AddComponent<SphereCollider>();
				sphereCollider.radius = this.colliderRadius;
				collider = sphereCollider;
				this.generateCollider = true;
			}
			collider.isTrigger = true;
			if (this.headsetColliderScript == null)
			{
				GameObject gameObject = this.headsetColliderContainer ? this.headsetColliderContainer : this.headset.gameObject;
				this.headsetColliderScript = gameObject.AddComponent<VRTK_HeadsetCollider>();
				this.headsetColliderScript.SetParent(base.gameObject);
				this.headsetColliderScript.SetIgnoreTarget(this.targetListPolicy);
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0008AEA0 File Offset: 0x000890A0
		protected virtual void TearDownHeadset()
		{
			if (this.generateCollider)
			{
				Object.Destroy(this.headset.gameObject.GetComponent<BoxCollider>());
			}
			if (this.generateRigidbody)
			{
				Object.Destroy(this.headset.gameObject.GetComponent<Rigidbody>());
			}
			if (this.headsetColliderScript != null)
			{
				Object.Destroy(this.headsetColliderScript);
			}
			if (this.headsetColliderContainer != null)
			{
				Object.Destroy(this.headsetColliderContainer);
			}
		}

		// Token: 0x04001535 RID: 5429
		[Tooltip("If this is checked then the headset collision will ignore colliders set to `Is Trigger = true`.")]
		public bool ignoreTriggerColliders;

		// Token: 0x04001536 RID: 5430
		[Tooltip("The radius of the auto generated sphere collider for detecting collisions on the headset.")]
		public float colliderRadius = 0.1f;

		// Token: 0x04001537 RID: 5431
		[Tooltip("A specified VRTK_PolicyList to use to determine whether any objects will be acted upon by the Headset Collision.")]
		public VRTK_PolicyList targetListPolicy;

		// Token: 0x0400153A RID: 5434
		[HideInInspector]
		public bool headsetColliding;

		// Token: 0x0400153B RID: 5435
		[HideInInspector]
		public Collider collidingWith;

		// Token: 0x0400153C RID: 5436
		protected Transform headset;

		// Token: 0x0400153D RID: 5437
		protected VRTK_HeadsetCollider headsetColliderScript;

		// Token: 0x0400153E RID: 5438
		protected GameObject headsetColliderContainer;

		// Token: 0x0400153F RID: 5439
		protected bool generateCollider;

		// Token: 0x04001540 RID: 5440
		protected bool generateRigidbody;
	}
}
