using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200034D RID: 845
	public class Breakable_Cube : MonoBehaviour
	{
		// Token: 0x06001D7C RID: 7548 RVA: 0x0009694B File Offset: 0x00094B4B
		private void Start()
		{
			base.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0009695C File Offset: 0x00094B5C
		private void OnCollisionEnter(Collision collision)
		{
			float collisionForce = this.GetCollisionForce(collision);
			if (collisionForce > 0f)
			{
				this.ExplodeCube(collisionForce);
			}
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00096980 File Offset: 0x00094B80
		private float GetCollisionForce(Collision collision)
		{
			if (collision.collider.name.Contains("Sword") && collision.collider.GetComponent<Sword>().CollisionForce() > this.breakForce)
			{
				return collision.collider.GetComponent<Sword>().CollisionForce() * 1.2f;
			}
			if (collision.collider.name.Contains("Arrow"))
			{
				return 500f;
			}
			return 0f;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000969F8 File Offset: 0x00094BF8
		private void ExplodeCube(float force)
		{
			foreach (Transform transform in base.GetComponentsInChildren<Transform>())
			{
				if (!(transform.transform.name == base.transform.name))
				{
					this.ExplodeFace(transform, force);
				}
			}
			Object.Destroy(base.gameObject, 10f);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x00096A54 File Offset: 0x00094C54
		private void ExplodeFace(Transform face, float force)
		{
			face.transform.SetParent(null);
			Rigidbody rigidbody = face.gameObject.AddComponent<Rigidbody>();
			rigidbody.isKinematic = false;
			rigidbody.useGravity = true;
			rigidbody.AddExplosionForce(force, Vector3.zero, 0f);
			Object.Destroy(face.gameObject, 10f);
		}

		// Token: 0x04001744 RID: 5956
		private float breakForce = 150f;
	}
}
