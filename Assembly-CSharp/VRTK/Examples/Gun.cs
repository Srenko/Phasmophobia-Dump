using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000356 RID: 854
	public class Gun : VRTK_InteractableObject
	{
		// Token: 0x06001DA3 RID: 7587 RVA: 0x0009715F File Offset: 0x0009535F
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			this.FireBullet();
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0009716E File Offset: 0x0009536E
		protected void Start()
		{
			this.bullet = base.transform.Find("Bullet").gameObject;
			this.bullet.SetActive(false);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00097198 File Offset: 0x00095398
		private void FireBullet()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.bullet, this.bullet.transform.position, this.bullet.transform.rotation);
			gameObject.SetActive(true);
			gameObject.GetComponent<Rigidbody>().AddForce(-this.bullet.transform.forward * this.bulletSpeed);
			Object.Destroy(gameObject, this.bulletLife);
		}

		// Token: 0x0400175E RID: 5982
		private GameObject bullet;

		// Token: 0x0400175F RID: 5983
		private float bulletSpeed = 1000f;

		// Token: 0x04001760 RID: 5984
		private float bulletLife = 5f;
	}
}
