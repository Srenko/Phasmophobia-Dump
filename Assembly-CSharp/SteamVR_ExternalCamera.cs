using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using Valve.VR;

// Token: 0x020001E6 RID: 486
public class SteamVR_ExternalCamera : MonoBehaviour
{
	// Token: 0x06000D77 RID: 3447 RVA: 0x00054228 File Offset: 0x00052428
	public void ReadConfig()
	{
		try
		{
			HmdMatrix34_t pose = default(HmdMatrix34_t);
			bool flag = false;
			object obj = this.config;
			string[] array = File.ReadAllLines(this.configPath);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Length == 2)
				{
					string text = array2[0];
					if (text == "m")
					{
						string[] array3 = array2[1].Split(new char[]
						{
							','
						});
						if (array3.Length == 12)
						{
							pose.m0 = float.Parse(array3[0]);
							pose.m1 = float.Parse(array3[1]);
							pose.m2 = float.Parse(array3[2]);
							pose.m3 = float.Parse(array3[3]);
							pose.m4 = float.Parse(array3[4]);
							pose.m5 = float.Parse(array3[5]);
							pose.m6 = float.Parse(array3[6]);
							pose.m7 = float.Parse(array3[7]);
							pose.m8 = float.Parse(array3[8]);
							pose.m9 = float.Parse(array3[9]);
							pose.m10 = float.Parse(array3[10]);
							pose.m11 = float.Parse(array3[11]);
							flag = true;
						}
					}
					else if (text == "disableStandardAssets")
					{
						FieldInfo field = obj.GetType().GetField(text);
						if (field != null)
						{
							field.SetValue(obj, bool.Parse(array2[1]));
						}
					}
					else
					{
						FieldInfo field2 = obj.GetType().GetField(text);
						if (field2 != null)
						{
							field2.SetValue(obj, float.Parse(array2[1]));
						}
					}
				}
			}
			this.config = (SteamVR_ExternalCamera.Config)obj;
			if (flag)
			{
				SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(pose);
				this.config.x = rigidTransform.pos.x;
				this.config.y = rigidTransform.pos.y;
				this.config.z = rigidTransform.pos.z;
				Vector3 eulerAngles = rigidTransform.rot.eulerAngles;
				this.config.rx = eulerAngles.x;
				this.config.ry = eulerAngles.y;
				this.config.rz = eulerAngles.z;
			}
		}
		catch
		{
		}
		this.target = null;
		if (this.watcher == null)
		{
			FileInfo fileInfo = new FileInfo(this.configPath);
			this.watcher = new FileSystemWatcher(fileInfo.DirectoryName, fileInfo.Name);
			this.watcher.NotifyFilter = NotifyFilters.LastWrite;
			this.watcher.Changed += this.OnChanged;
			this.watcher.EnableRaisingEvents = true;
		}
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00054528 File Offset: 0x00052728
	private void OnChanged(object source, FileSystemEventArgs e)
	{
		this.ReadConfig();
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00054530 File Offset: 0x00052730
	public void AttachToCamera(SteamVR_Camera vrcam)
	{
		if (this.target == vrcam.head)
		{
			return;
		}
		this.target = vrcam.head;
		Transform parent = base.transform.parent;
		Transform parent2 = vrcam.head.parent;
		parent.parent = parent2;
		parent.localPosition = Vector3.zero;
		parent.localRotation = Quaternion.identity;
		parent.localScale = Vector3.one;
		vrcam.enabled = false;
		GameObject gameObject = Object.Instantiate<GameObject>(vrcam.gameObject);
		vrcam.enabled = true;
		gameObject.name = "camera";
		Object.DestroyImmediate(gameObject.GetComponent<SteamVR_Camera>());
		Object.DestroyImmediate(gameObject.GetComponent<SteamVR_Fade>());
		this.cam = gameObject.GetComponent<Camera>();
		this.cam.stereoTargetEye = StereoTargetEyeMask.None;
		this.cam.fieldOfView = this.config.fov;
		this.cam.useOcclusionCulling = false;
		this.cam.enabled = false;
		this.colorMat = new Material(Shader.Find("Custom/SteamVR_ColorOut"));
		this.alphaMat = new Material(Shader.Find("Custom/SteamVR_AlphaOut"));
		this.clipMaterial = new Material(Shader.Find("Custom/SteamVR_ClearAll"));
		Transform transform = gameObject.transform;
		transform.parent = base.transform;
		transform.localPosition = new Vector3(this.config.x, this.config.y, this.config.z);
		transform.localRotation = Quaternion.Euler(this.config.rx, this.config.ry, this.config.rz);
		transform.localScale = Vector3.one;
		while (transform.childCount > 0)
		{
			Object.DestroyImmediate(transform.GetChild(0).gameObject);
		}
		this.clipQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
		this.clipQuad.name = "ClipQuad";
		Object.DestroyImmediate(this.clipQuad.GetComponent<MeshCollider>());
		MeshRenderer component = this.clipQuad.GetComponent<MeshRenderer>();
		component.material = this.clipMaterial;
		component.shadowCastingMode = ShadowCastingMode.Off;
		component.receiveShadows = false;
		component.lightProbeUsage = LightProbeUsage.Off;
		component.reflectionProbeUsage = ReflectionProbeUsage.Off;
		Transform transform2 = this.clipQuad.transform;
		transform2.parent = transform;
		transform2.localScale = new Vector3(1000f, 1000f, 1f);
		transform2.localRotation = Quaternion.identity;
		this.clipQuad.SetActive(false);
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00054790 File Offset: 0x00052990
	public float GetTargetDistance()
	{
		if (this.target == null)
		{
			return this.config.near + 0.01f;
		}
		Transform transform = this.cam.transform;
		Vector3 normalized = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
		Vector3 inPoint = this.target.position + new Vector3(this.target.forward.x, 0f, this.target.forward.z).normalized * this.config.hmdOffset;
		return Mathf.Clamp(-new Plane(normalized, inPoint).GetDistanceToPoint(transform.position), this.config.near + 0.01f, this.config.far - 0.01f);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x00054880 File Offset: 0x00052A80
	public void RenderNear()
	{
		int num = Screen.width / 2;
		int num2 = Screen.height / 2;
		if (this.cam.targetTexture == null || this.cam.targetTexture.width != num || this.cam.targetTexture.height != num2)
		{
			RenderTexture renderTexture = new RenderTexture(num, num2, 24, RenderTextureFormat.ARGB32);
			renderTexture.antiAliasing = ((QualitySettings.antiAliasing == 0) ? 1 : QualitySettings.antiAliasing);
			this.cam.targetTexture = renderTexture;
		}
		this.cam.nearClipPlane = this.config.near;
		this.cam.farClipPlane = this.config.far;
		CameraClearFlags clearFlags = this.cam.clearFlags;
		Color backgroundColor = this.cam.backgroundColor;
		this.cam.clearFlags = CameraClearFlags.Color;
		this.cam.backgroundColor = Color.clear;
		this.clipMaterial.color = new Color(this.config.r, this.config.g, this.config.b, this.config.a);
		float d = Mathf.Clamp(this.GetTargetDistance() + this.config.nearOffset, this.config.near, this.config.far);
		Transform parent = this.clipQuad.transform.parent;
		this.clipQuad.transform.position = parent.position + parent.forward * d;
		MonoBehaviour[] array = null;
		bool[] array2 = null;
		if (this.config.disableStandardAssets)
		{
			array = this.cam.gameObject.GetComponents<MonoBehaviour>();
			array2 = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				MonoBehaviour monoBehaviour = array[i];
				if (monoBehaviour.enabled && monoBehaviour.GetType().ToString().StartsWith("UnityStandardAssets."))
				{
					monoBehaviour.enabled = false;
					array2[i] = true;
				}
			}
		}
		this.clipQuad.SetActive(true);
		this.cam.Render();
		Graphics.DrawTexture(new Rect(0f, 0f, (float)num, (float)num2), this.cam.targetTexture, this.colorMat);
		MonoBehaviour monoBehaviour2 = this.cam.gameObject.GetComponent("PostProcessingBehaviour") as MonoBehaviour;
		if (monoBehaviour2 != null && monoBehaviour2.enabled)
		{
			monoBehaviour2.enabled = false;
			this.cam.Render();
			monoBehaviour2.enabled = true;
		}
		Graphics.DrawTexture(new Rect((float)num, 0f, (float)num, (float)num2), this.cam.targetTexture, this.alphaMat);
		this.clipQuad.SetActive(false);
		if (array != null)
		{
			for (int j = 0; j < array.Length; j++)
			{
				if (array2[j])
				{
					array[j].enabled = true;
				}
			}
		}
		this.cam.clearFlags = clearFlags;
		this.cam.backgroundColor = backgroundColor;
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x00054B84 File Offset: 0x00052D84
	public void RenderFar()
	{
		this.cam.nearClipPlane = this.config.near;
		this.cam.farClipPlane = this.config.far;
		this.cam.Render();
		int num = Screen.width / 2;
		int num2 = Screen.height / 2;
		Graphics.DrawTexture(new Rect(0f, (float)num2, (float)num, (float)num2), this.cam.targetTexture, this.colorMat);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x00003F60 File Offset: 0x00002160
	private void OnGUI()
	{
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x00054C00 File Offset: 0x00052E00
	private void OnEnable()
	{
		this.cameras = Object.FindObjectsOfType<Camera>();
		if (this.cameras != null)
		{
			int num = this.cameras.Length;
			this.cameraRects = new Rect[num];
			for (int i = 0; i < num; i++)
			{
				Camera camera = this.cameras[i];
				this.cameraRects[i] = camera.rect;
				if (!(camera == this.cam) && !(camera.targetTexture != null) && !(camera.GetComponent<SteamVR_Camera>() != null))
				{
					camera.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
				}
			}
		}
		if (this.config.sceneResolutionScale > 0f)
		{
			this.sceneResolutionScale = SteamVR_Camera.sceneResolutionScale;
			SteamVR_Camera.sceneResolutionScale = this.config.sceneResolutionScale;
		}
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x00054CD8 File Offset: 0x00052ED8
	private void OnDisable()
	{
		if (this.cameras != null)
		{
			int num = this.cameras.Length;
			for (int i = 0; i < num; i++)
			{
				Camera camera = this.cameras[i];
				if (camera != null)
				{
					camera.rect = this.cameraRects[i];
				}
			}
			this.cameras = null;
			this.cameraRects = null;
		}
		if (this.config.sceneResolutionScale > 0f)
		{
			SteamVR_Camera.sceneResolutionScale = this.sceneResolutionScale;
		}
	}

	// Token: 0x04000DD8 RID: 3544
	public SteamVR_ExternalCamera.Config config;

	// Token: 0x04000DD9 RID: 3545
	public string configPath;

	// Token: 0x04000DDA RID: 3546
	private FileSystemWatcher watcher;

	// Token: 0x04000DDB RID: 3547
	private Camera cam;

	// Token: 0x04000DDC RID: 3548
	private Transform target;

	// Token: 0x04000DDD RID: 3549
	private GameObject clipQuad;

	// Token: 0x04000DDE RID: 3550
	private Material clipMaterial;

	// Token: 0x04000DDF RID: 3551
	private Material colorMat;

	// Token: 0x04000DE0 RID: 3552
	private Material alphaMat;

	// Token: 0x04000DE1 RID: 3553
	private Camera[] cameras;

	// Token: 0x04000DE2 RID: 3554
	private Rect[] cameraRects;

	// Token: 0x04000DE3 RID: 3555
	private float sceneResolutionScale;

	// Token: 0x02000574 RID: 1396
	[Serializable]
	public struct Config
	{
		// Token: 0x040025D9 RID: 9689
		public float x;

		// Token: 0x040025DA RID: 9690
		public float y;

		// Token: 0x040025DB RID: 9691
		public float z;

		// Token: 0x040025DC RID: 9692
		public float rx;

		// Token: 0x040025DD RID: 9693
		public float ry;

		// Token: 0x040025DE RID: 9694
		public float rz;

		// Token: 0x040025DF RID: 9695
		public float fov;

		// Token: 0x040025E0 RID: 9696
		public float near;

		// Token: 0x040025E1 RID: 9697
		public float far;

		// Token: 0x040025E2 RID: 9698
		public float sceneResolutionScale;

		// Token: 0x040025E3 RID: 9699
		public float frameSkip;

		// Token: 0x040025E4 RID: 9700
		public float nearOffset;

		// Token: 0x040025E5 RID: 9701
		public float farOffset;

		// Token: 0x040025E6 RID: 9702
		public float hmdOffset;

		// Token: 0x040025E7 RID: 9703
		public float r;

		// Token: 0x040025E8 RID: 9704
		public float g;

		// Token: 0x040025E9 RID: 9705
		public float b;

		// Token: 0x040025EA RID: 9706
		public float a;

		// Token: 0x040025EB RID: 9707
		public bool disableStandardAssets;
	}
}
