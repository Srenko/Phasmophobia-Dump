using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000441 RID: 1089
	public class Arrow : MonoBehaviour
	{
		// Token: 0x06002186 RID: 8582 RVA: 0x000A561F File Offset: 0x000A381F
		private void Start()
		{
			Physics.IgnoreCollision(this.shaftRB.GetComponent<Collider>(), Player.instance.headCollider);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000A563C File Offset: 0x000A383C
		private void FixedUpdate()
		{
			if (this.released && this.inFlight)
			{
				this.prevPosition = base.transform.position;
				this.prevRotation = base.transform.rotation;
				this.prevVelocity = base.GetComponent<Rigidbody>().velocity;
				this.prevHeadPosition = this.arrowHeadRB.transform.position;
				this.travelledFrames++;
			}
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000A56B0 File Offset: 0x000A38B0
		public void ArrowReleased(float inputVelocity)
		{
			this.inFlight = true;
			this.released = true;
			this.airReleaseSound.Play();
			if (this.glintParticle != null)
			{
				this.glintParticle.Play();
			}
			if (base.gameObject.GetComponentInChildren<FireSource>().isBurning)
			{
				this.fireReleaseSound.Play();
			}
			foreach (RaycastHit raycastHit in Physics.SphereCastAll(base.transform.position, 0.01f, base.transform.forward, 0.8f, -5, QueryTriggerInteraction.Ignore))
			{
				if (raycastHit.collider.gameObject != base.gameObject && raycastHit.collider.gameObject != this.arrowHeadRB.gameObject && raycastHit.collider != Player.instance.headCollider)
				{
					Object.Destroy(base.gameObject);
					return;
				}
			}
			this.travelledFrames = 0;
			this.prevPosition = base.transform.position;
			this.prevRotation = base.transform.rotation;
			this.prevHeadPosition = this.arrowHeadRB.transform.position;
			this.prevVelocity = base.GetComponent<Rigidbody>().velocity;
			Object.Destroy(base.gameObject, 30f);
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000A5808 File Offset: 0x000A3A08
		private void OnCollisionEnter(Collision collision)
		{
			if (this.inFlight)
			{
				float sqrMagnitude = base.GetComponent<Rigidbody>().velocity.sqrMagnitude;
				bool flag = this.targetPhysMaterial != null && collision.collider.sharedMaterial == this.targetPhysMaterial && sqrMagnitude > 0.2f;
				bool flag2 = collision.collider.gameObject.GetComponent<Balloon>() != null;
				if (this.travelledFrames < 2 && !flag)
				{
					base.transform.position = this.prevPosition - this.prevVelocity * Time.deltaTime;
					base.transform.rotation = this.prevRotation;
					Vector3 a = Vector3.Reflect(this.arrowHeadRB.velocity, collision.contacts[0].normal);
					this.arrowHeadRB.velocity = a * 0.25f;
					this.shaftRB.velocity = a * 0.25f;
					this.travelledFrames = 0;
					return;
				}
				if (this.glintParticle != null)
				{
					this.glintParticle.Stop(true);
				}
				if (sqrMagnitude > 0.1f)
				{
					this.hitGroundSound.Play();
				}
				FireSource componentInChildren = base.gameObject.GetComponentInChildren<FireSource>();
				FireSource componentInParent = collision.collider.GetComponentInParent<FireSource>();
				if (componentInChildren != null && componentInChildren.isBurning && componentInParent != null)
				{
					if (!this.hasSpreadFire)
					{
						collision.collider.gameObject.SendMessageUpwards("FireExposure", base.gameObject, SendMessageOptions.DontRequireReceiver);
						this.hasSpreadFire = true;
					}
				}
				else if (sqrMagnitude > 0.1f || flag2)
				{
					collision.collider.gameObject.SendMessageUpwards("ApplyDamage", SendMessageOptions.DontRequireReceiver);
					base.gameObject.SendMessage("HasAppliedDamage", SendMessageOptions.DontRequireReceiver);
				}
				if (flag2)
				{
					base.transform.position = this.prevPosition;
					base.transform.rotation = this.prevRotation;
					this.arrowHeadRB.velocity = this.prevVelocity;
					Physics.IgnoreCollision(this.arrowHeadRB.GetComponent<Collider>(), collision.collider);
					Physics.IgnoreCollision(this.shaftRB.GetComponent<Collider>(), collision.collider);
				}
				if (flag)
				{
					this.StickInTarget(collision, this.travelledFrames < 2);
				}
				if (Player.instance && collision.collider == Player.instance.headCollider)
				{
					Player.instance.PlayerShotSelf();
				}
			}
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000A5A84 File Offset: 0x000A3C84
		private void StickInTarget(Collision collision, bool bSkipRayCast)
		{
			Vector3 direction = this.prevRotation * Vector3.forward;
			if (!bSkipRayCast)
			{
				RaycastHit[] array = Physics.RaycastAll(this.prevHeadPosition - this.prevVelocity * Time.deltaTime, direction, this.prevVelocity.magnitude * Time.deltaTime * 2f);
				bool flag = false;
				foreach (RaycastHit raycastHit in array)
				{
					if (raycastHit.collider == collision.collider)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return;
				}
			}
			Object.Destroy(this.glintParticle);
			this.inFlight = false;
			this.shaftRB.velocity = Vector3.zero;
			this.shaftRB.angularVelocity = Vector3.zero;
			this.shaftRB.isKinematic = true;
			this.shaftRB.useGravity = false;
			this.shaftRB.transform.GetComponent<BoxCollider>().enabled = false;
			this.arrowHeadRB.velocity = Vector3.zero;
			this.arrowHeadRB.angularVelocity = Vector3.zero;
			this.arrowHeadRB.isKinematic = true;
			this.arrowHeadRB.useGravity = false;
			this.arrowHeadRB.transform.GetComponent<BoxCollider>().enabled = false;
			this.hitTargetSound.Play();
			this.scaleParentObject = new GameObject("Arrow Scale Parent");
			Transform transform = collision.collider.transform;
			if (!collision.collider.gameObject.GetComponent<ExplosionWobble>() && transform.parent)
			{
				transform = transform.parent;
			}
			this.scaleParentObject.transform.parent = transform;
			base.transform.parent = this.scaleParentObject.transform;
			base.transform.rotation = this.prevRotation;
			base.transform.position = this.prevPosition;
			base.transform.position = collision.contacts[0].point - base.transform.forward * (0.75f - (Util.RemapNumberClamped(this.prevVelocity.magnitude, 0f, 10f, 0f, 0.1f) + Random.Range(0f, 0.05f)));
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000A5CCC File Offset: 0x000A3ECC
		private void OnDestroy()
		{
			if (this.scaleParentObject != null)
			{
				Object.Destroy(this.scaleParentObject);
			}
		}

		// Token: 0x04001ED7 RID: 7895
		public ParticleSystem glintParticle;

		// Token: 0x04001ED8 RID: 7896
		public Rigidbody arrowHeadRB;

		// Token: 0x04001ED9 RID: 7897
		public Rigidbody shaftRB;

		// Token: 0x04001EDA RID: 7898
		public PhysicMaterial targetPhysMaterial;

		// Token: 0x04001EDB RID: 7899
		private Vector3 prevPosition;

		// Token: 0x04001EDC RID: 7900
		private Quaternion prevRotation;

		// Token: 0x04001EDD RID: 7901
		private Vector3 prevVelocity;

		// Token: 0x04001EDE RID: 7902
		private Vector3 prevHeadPosition;

		// Token: 0x04001EDF RID: 7903
		public SoundPlayOneshot fireReleaseSound;

		// Token: 0x04001EE0 RID: 7904
		public SoundPlayOneshot airReleaseSound;

		// Token: 0x04001EE1 RID: 7905
		public SoundPlayOneshot hitTargetSound;

		// Token: 0x04001EE2 RID: 7906
		public PlaySound hitGroundSound;

		// Token: 0x04001EE3 RID: 7907
		private bool inFlight;

		// Token: 0x04001EE4 RID: 7908
		private bool released;

		// Token: 0x04001EE5 RID: 7909
		private bool hasSpreadFire;

		// Token: 0x04001EE6 RID: 7910
		private int travelledFrames;

		// Token: 0x04001EE7 RID: 7911
		private GameObject scaleParentObject;
	}
}
