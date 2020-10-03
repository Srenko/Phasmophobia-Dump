using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x0200049A RID: 1178
	public class CamGeometryCapture : MonoBehaviour
	{
		// Token: 0x060024AC RID: 9388 RVA: 0x000B37FC File Offset: 0x000B19FC
		public void Init()
		{
			if (this.t != null)
			{
				return;
			}
			this.t = base.transform;
			this.cam = base.GetComponent<Camera>();
			this.cam.aspect = 1f;
			this.cam.orthographic = true;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000B384C File Offset: 0x000B1A4C
		private void OnDestroy()
		{
			this.DisposeRTCapture();
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000B3854 File Offset: 0x000B1A54
		private void DisposeRenderTexture(ref RenderTexture rt)
		{
			if (rt == null)
			{
				return;
			}
			rt.Release();
			Object.Destroy(rt);
			rt = null;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000B3872 File Offset: 0x000B1A72
		public void DisposeRTCapture()
		{
			this.cam.targetTexture = null;
			this.DisposeRenderTexture(ref this.rtCapture);
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000B388C File Offset: 0x000B1A8C
		public void RemoveTrianglesBelowSurface(Transform t, MeshCombineJobManager.MeshCombineJob meshCombineJob, MeshCache.SubMeshCache newMeshCache, ref byte[] vertexIsBelow)
		{
			if (vertexIsBelow == null)
			{
				vertexIsBelow = new byte[65534];
			}
			Vector3 vector = Vector3.zero;
			int collisionMask = meshCombineJob.meshCombiner.surfaceLayerMask;
			Vector3[] vertices = newMeshCache.vertices;
			int[] triangles = newMeshCache.triangles;
			FastList<MeshObject> meshObjects = meshCombineJob.meshObjectsHolder.meshObjects;
			int startIndex = meshCombineJob.startIndex;
			int endIndex = meshCombineJob.endIndex;
			for (int i = startIndex; i < endIndex; i++)
			{
				MeshObject meshObject = meshObjects.items[i];
				this.Capture(meshObject.cachedGO.mr.bounds, collisionMask, new Vector3(0f, -1f, 0f), new Int2(1024, 1024));
				int startNewTriangleIndex = meshObject.startNewTriangleIndex;
				int num = meshObject.newTriangleCount + startNewTriangleIndex;
				for (int j = startNewTriangleIndex; j < num; j += 3)
				{
					bool flag = false;
					for (int k = 0; k < 3; k++)
					{
						int num2 = triangles[j + k];
						if (num2 != -1)
						{
							byte b = vertexIsBelow[num2];
							if (b == 0)
							{
								vector = t.TransformPoint(vertices[num2]);
								float height = this.GetHeight(vector);
								b = ((vector.y < height) ? 1 : 2);
								vertexIsBelow[num2] = b;
								if (vector.y < height)
								{
									b = (vertexIsBelow[num2] = 1);
								}
								else
								{
									b = (vertexIsBelow[num2] = 2);
								}
							}
							if (b != 1)
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						meshCombineJob.trianglesRemoved += 3;
						triangles[j] = -1;
					}
				}
			}
			Array.Clear(vertexIsBelow, 0, vertices.Length);
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000B3A20 File Offset: 0x000B1C20
		public void Capture(Bounds bounds, int collisionMask, Vector3 direction, Int2 resolution)
		{
			if (this.rtCapture == null || this.rtCapture.width != resolution.x || this.rtCapture.height != resolution.y)
			{
				if (this.rtCapture != null)
				{
					this.DisposeRTCapture();
				}
				this.rtCapture = new RenderTexture(resolution.x, resolution.y, 16, RenderTextureFormat.Depth, RenderTextureReadWrite.Linear);
			}
			bounds.size *= 1.1f;
			this.bounds = bounds;
			this.cam.targetTexture = this.rtCapture;
			this.cam.cullingMask = collisionMask;
			this.SetCamera(direction);
			this.cam.Render();
			int num = resolution.x * resolution.y;
			ComputeBuffer computeBuffer = new ComputeBuffer(num, 4);
			this.computeDepthToArray.SetTexture(0, "rtDepth", this.rtCapture);
			this.computeDepthToArray.SetBuffer(0, "heightBuffer", computeBuffer);
			this.computeDepthToArray.SetInt("resolution", resolution.x);
			this.computeDepthToArray.SetFloat("captureHeight", this.t.position.y);
			this.computeDepthToArray.SetFloat("distance", bounds.size.y + 256f);
			this.computeDepthToArray.SetInt("direction", (direction.y == 1f) ? 1 : -1);
			this.computeDepthToArray.Dispatch(0, Mathf.CeilToInt((float)(resolution.x / 8)), Mathf.CeilToInt((float)(resolution.y / 8)), 1);
			if (this.heights == null || this.heights.Length != num)
			{
				this.heights = new float[num];
			}
			computeBuffer.GetData(this.heights);
			computeBuffer.Dispose();
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000B3BF8 File Offset: 0x000B1DF8
		public void SetCamera(Vector3 direction)
		{
			if (direction == new Vector3(0f, 1f, 0f))
			{
				this.t.position = this.bounds.center - new Vector3(0f, this.bounds.extents.y + 256f, 0f);
			}
			else if (direction == new Vector3(0f, -1f, 0f))
			{
				this.t.position = this.bounds.center + new Vector3(0f, this.bounds.extents.y + 256f, 0f);
			}
			this.t.forward = direction;
			this.maxSize = this.bounds.size.x;
			if (this.bounds.size.z > this.maxSize)
			{
				this.maxSize = this.bounds.size.z;
			}
			this.cam.orthographicSize = this.maxSize / 2f;
			this.cam.nearClipPlane = 0f;
			this.cam.farClipPlane = this.bounds.size.y + 256f;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000B3D58 File Offset: 0x000B1F58
		public float GetHeight(Vector3 pos)
		{
			pos -= this.bounds.min;
			pos.x += (this.maxSize - this.bounds.size.x) / 2f;
			pos.z += (this.maxSize - this.bounds.size.z) / 2f;
			float num = this.maxSize / (float)this.resolution.x;
			float num2 = this.maxSize / (float)this.resolution.y;
			float num3 = (float)((int)(pos.x / num));
			float num4 = (float)((int)(pos.z / num2));
			if (num3 > (float)(this.resolution.x - 2) || num3 < 0f || num4 > (float)(this.resolution.y - 2) || num4 < 0f)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Out of bounds ",
					num3,
					" ",
					num4
				}));
				return 0f;
			}
			int num5 = (int)num3;
			int num6 = (int)num4;
			float num7 = num3 - (float)num5;
			float a = this.heights[num5 + num6 * this.resolution.y];
			float b = this.heights[num5 + 1 + num6 * this.resolution.y];
			float a2 = Mathf.Lerp(a, b, num7);
			float a3 = this.heights[num5 + (num6 + 1) * this.resolution.y];
			b = this.heights[num5 + 1 + (num6 + 1) * this.resolution.y];
			float b2 = Mathf.Lerp(a3, b, num7);
			num7 = num4 - (float)num6;
			return Mathf.Lerp(a2, b2, num7);
		}

		// Token: 0x040021D4 RID: 8660
		public ComputeShader computeDepthToArray;

		// Token: 0x040021D5 RID: 8661
		public Int2 resolution = new Int2(1024, 1024);

		// Token: 0x040021D6 RID: 8662
		public Camera cam;

		// Token: 0x040021D7 RID: 8663
		public Transform t;

		// Token: 0x040021D8 RID: 8664
		public RenderTexture rtCapture;

		// Token: 0x040021D9 RID: 8665
		private float[] heights;

		// Token: 0x040021DA RID: 8666
		private Bounds bounds;

		// Token: 0x040021DB RID: 8667
		private float maxSize;
	}
}
