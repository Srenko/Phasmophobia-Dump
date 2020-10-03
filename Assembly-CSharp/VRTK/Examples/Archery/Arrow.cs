using System;
using UnityEngine;

namespace VRTK.Examples.Archery
{
	// Token: 0x0200037C RID: 892
	public class Arrow : MonoBehaviour
	{
		// Token: 0x06001EAD RID: 7853 RVA: 0x0009BE86 File Offset: 0x0009A086
		public void SetArrowHolder(GameObject holder)
		{
			this.arrowHolder = holder;
			this.arrowHolder.SetActive(false);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0009BE9B File Offset: 0x0009A09B
		public void OnNock()
		{
			this.collided = false;
			this.inFlight = false;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0009BEAB File Offset: 0x0009A0AB
		public void Fired()
		{
			this.DestroyArrow(this.maxArrowLife);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0009BEB9 File Offset: 0x0009A0B9
		public void ResetArrow()
		{
			this.collided = true;
			this.inFlight = false;
			this.RecreateNotch();
			this.ResetTransform();
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0009BED5 File Offset: 0x0009A0D5
		private void Start()
		{
			this.rigidBody = base.GetComponent<Rigidbody>();
			this.SetOrigns();
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0009BEE9 File Offset: 0x0009A0E9
		private void SetOrigns()
		{
			this.originalPosition = base.transform.localPosition;
			this.originalRotation = base.transform.localRotation;
			this.originalScale = base.transform.localScale;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0009BF1E File Offset: 0x0009A11E
		private void FixedUpdate()
		{
			if (!this.collided)
			{
				base.transform.LookAt(base.transform.position + this.rigidBody.velocity);
			}
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0009BF4E File Offset: 0x0009A14E
		private void OnCollisionEnter(Collision collision)
		{
			if (this.inFlight)
			{
				this.ResetArrow();
			}
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0009BF60 File Offset: 0x0009A160
		private void RecreateNotch()
		{
			this.arrowHolder.transform.SetParent(null);
			this.arrowHolder.SetActive(true);
			base.transform.SetParent(this.arrowHolder.transform);
			base.GetComponent<Rigidbody>().isKinematic = true;
			base.GetComponent<Collider>().enabled = false;
			this.arrowHolder.GetComponent<Rigidbody>().isKinematic = false;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0009BFCC File Offset: 0x0009A1CC
		private void ResetTransform()
		{
			this.arrowHolder.transform.position = base.transform.position;
			this.arrowHolder.transform.rotation = base.transform.rotation;
			base.transform.localPosition = this.originalPosition;
			base.transform.localRotation = this.originalRotation;
			base.transform.localScale = this.originalScale;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0009C042 File Offset: 0x0009A242
		private void DestroyArrow(float time)
		{
			Object.Destroy(this.arrowHolder, time);
			Object.Destroy(base.gameObject, time);
		}

		// Token: 0x040017E8 RID: 6120
		public float maxArrowLife = 10f;

		// Token: 0x040017E9 RID: 6121
		[HideInInspector]
		public bool inFlight;

		// Token: 0x040017EA RID: 6122
		private bool collided;

		// Token: 0x040017EB RID: 6123
		private Rigidbody rigidBody;

		// Token: 0x040017EC RID: 6124
		private GameObject arrowHolder;

		// Token: 0x040017ED RID: 6125
		private Vector3 originalPosition;

		// Token: 0x040017EE RID: 6126
		private Quaternion originalRotation;

		// Token: 0x040017EF RID: 6127
		private Vector3 originalScale;
	}
}
