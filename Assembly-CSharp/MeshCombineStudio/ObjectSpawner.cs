using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004A1 RID: 1185
	[ExecuteInEditMode]
	public class ObjectSpawner : MonoBehaviour
	{
		// Token: 0x060024FF RID: 9471 RVA: 0x000B6707 File Offset: 0x000B4907
		private void Awake()
		{
			this.t = base.transform;
			if (this.spawnInRuntime && Application.isPlaying)
			{
				this.Spawn();
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000B672A File Offset: 0x000B492A
		private void Update()
		{
			if (this.spawn)
			{
				this.spawn = false;
				this.Spawn();
			}
			if (this.deleteChildren)
			{
				this.deleteChildren = false;
				this.DeleteChildren();
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000B6758 File Offset: 0x000B4958
		public void DeleteChildren()
		{
			Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (this.t != componentsInChildren[i] && componentsInChildren[i] != null)
				{
					Object.DestroyImmediate(componentsInChildren[i].gameObject);
				}
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000B67A4 File Offset: 0x000B49A4
		public void Spawn()
		{
			Bounds bounds = default(Bounds);
			bounds.center = base.transform.position;
			bounds.size = base.transform.lossyScale;
			float x = bounds.min.x;
			float x2 = bounds.max.x;
			float y = bounds.min.y;
			float y2 = bounds.max.y;
			float z = bounds.min.z;
			float z2 = bounds.max.z;
			int max = this.objects.Length;
			float num = this.metersBetweenSpawning * 0.5f;
			float num2 = base.transform.lossyScale.y * 0.5f;
			int num3 = 0;
			for (float num4 = z; num4 < z2; num4 += this.metersBetweenSpawning)
			{
				for (float num5 = x; num5 < x2; num5 += this.metersBetweenSpawning)
				{
					for (float num6 = y; num6 < y2; num6 += this.metersBetweenSpawning)
					{
						int num7 = Random.Range(0, max);
						if (Random.value < this.density)
						{
							Vector3 vector = new Vector3(num5 + Random.Range(-num, num), y + Random.Range(0f, bounds.size.y) * Random.Range(this.heightRange.x, this.heightRange.y), num4 + Random.Range(-num, num));
							if (vector.x >= x && vector.x <= x2 && vector.y >= y && vector.y <= y2 && vector.z >= z && vector.z <= z2)
							{
								vector.y += num2;
								Vector3 euler = new Vector3(Random.Range(0f, this.rotationRange.x), Random.Range(0f, this.rotationRange.y), Random.Range(0f, this.rotationRange.z));
								GameObject gameObject = Object.Instantiate<GameObject>(this.objects[num7], vector, Quaternion.Euler(euler));
								float num8 = Random.Range(this.scaleRange.x, this.scaleRange.y) * this.scaleMulti;
								gameObject.transform.localScale = new Vector3(num8, num8, num8);
								gameObject.transform.parent = this.t;
								num3++;
							}
						}
					}
				}
			}
			Debug.Log("Spawned " + num3);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000B6A48 File Offset: 0x000B4C48
		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireCube(base.transform.position + new Vector3(0f, base.transform.lossyScale.y * 0.5f, 0f), base.transform.lossyScale);
		}

		// Token: 0x04002258 RID: 8792
		public GameObject[] objects;

		// Token: 0x04002259 RID: 8793
		public float density = 0.5f;

		// Token: 0x0400225A RID: 8794
		public Vector2 scaleRange = new Vector2(0.5f, 2f);

		// Token: 0x0400225B RID: 8795
		public Vector3 rotationRange = new Vector3(5f, 360f, 5f);

		// Token: 0x0400225C RID: 8796
		public Vector2 heightRange = new Vector2(0f, 1f);

		// Token: 0x0400225D RID: 8797
		public float scaleMulti = 1f;

		// Token: 0x0400225E RID: 8798
		public float metersBetweenSpawning = 2f;

		// Token: 0x0400225F RID: 8799
		public bool spawnInRuntime;

		// Token: 0x04002260 RID: 8800
		public bool spawn;

		// Token: 0x04002261 RID: 8801
		public bool deleteChildren;

		// Token: 0x04002262 RID: 8802
		private Transform t;
	}
}
