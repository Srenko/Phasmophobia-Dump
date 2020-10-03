using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000445 RID: 1093
	public class BalloonColliders : MonoBehaviour
	{
		// Token: 0x060021A5 RID: 8613 RVA: 0x000A68C4 File Offset: 0x000A4AC4
		private void Awake()
		{
			this.rb = base.GetComponent<Rigidbody>();
			this.colliderLocalPositions = new Vector3[this.colliders.Length];
			this.colliderLocalRotations = new Quaternion[this.colliders.Length];
			for (int i = 0; i < this.colliders.Length; i++)
			{
				this.colliderLocalPositions[i] = this.colliders[i].transform.localPosition;
				this.colliderLocalRotations[i] = this.colliders[i].transform.localRotation;
				this.colliders[i].name = base.gameObject.name + "." + this.colliders[i].name;
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000A6984 File Offset: 0x000A4B84
		private void OnEnable()
		{
			for (int i = 0; i < this.colliders.Length; i++)
			{
				this.colliders[i].transform.SetParent(base.transform);
				this.colliders[i].transform.localPosition = this.colliderLocalPositions[i];
				this.colliders[i].transform.localRotation = this.colliderLocalRotations[i];
				this.colliders[i].transform.SetParent(null);
				FixedJoint fixedJoint = this.colliders[i].AddComponent<FixedJoint>();
				fixedJoint.connectedBody = this.rb;
				fixedJoint.breakForce = float.PositiveInfinity;
				fixedJoint.breakTorque = float.PositiveInfinity;
				fixedJoint.enableCollision = false;
				fixedJoint.enablePreprocessing = true;
				this.colliders[i].SetActive(true);
			}
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000A6A5C File Offset: 0x000A4C5C
		private void OnDisable()
		{
			for (int i = 0; i < this.colliders.Length; i++)
			{
				if (this.colliders[i] != null)
				{
					Object.Destroy(this.colliders[i].GetComponent<FixedJoint>());
					this.colliders[i].SetActive(false);
				}
			}
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000A6AAC File Offset: 0x000A4CAC
		private void OnDestroy()
		{
			for (int i = 0; i < this.colliders.Length; i++)
			{
				Object.Destroy(this.colliders[i]);
			}
		}

		// Token: 0x04001F08 RID: 7944
		public GameObject[] colliders;

		// Token: 0x04001F09 RID: 7945
		private Vector3[] colliderLocalPositions;

		// Token: 0x04001F0A RID: 7946
		private Quaternion[] colliderLocalRotations;

		// Token: 0x04001F0B RID: 7947
		private Rigidbody rb;
	}
}
