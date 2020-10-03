using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Valve.VR;

// Token: 0x020001F0 RID: 496
[ExecuteInEditMode]
public class SteamVR_RenderModel : MonoBehaviour
{
	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00057E26 File Offset: 0x00056026
	// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x00057E2E File Offset: 0x0005602E
	public string renderModelName { get; private set; }

	// Token: 0x06000DD9 RID: 3545 RVA: 0x00057E37 File Offset: 0x00056037
	private void OnModelSkinSettingsHaveChanged(VREvent_t vrEvent)
	{
		if (!string.IsNullOrEmpty(this.renderModelName))
		{
			this.renderModelName = "";
			this.UpdateModel();
		}
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x00057E58 File Offset: 0x00056058
	private void OnHideRenderModels(bool hidden)
	{
		MeshRenderer component = base.GetComponent<MeshRenderer>();
		if (component != null)
		{
			component.enabled = !hidden;
		}
		MeshRenderer[] componentsInChildren = base.transform.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = !hidden;
		}
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x00057EA5 File Offset: 0x000560A5
	private void OnDeviceConnected(int i, bool connected)
	{
		if (i != (int)this.index)
		{
			return;
		}
		if (connected)
		{
			this.UpdateModel();
		}
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x00057EBC File Offset: 0x000560BC
	public void UpdateModel()
	{
		CVRSystem system = OpenVR.System;
		if (system == null)
		{
			return;
		}
		ETrackedPropertyError etrackedPropertyError = ETrackedPropertyError.TrackedProp_Success;
		uint stringTrackedDeviceProperty = system.GetStringTrackedDeviceProperty((uint)this.index, ETrackedDeviceProperty.Prop_RenderModelName_String, null, 0U, ref etrackedPropertyError);
		if (stringTrackedDeviceProperty <= 1U)
		{
			Debug.LogError("Failed to get render model name for tracked object " + this.index);
			return;
		}
		StringBuilder stringBuilder = new StringBuilder((int)stringTrackedDeviceProperty);
		system.GetStringTrackedDeviceProperty((uint)this.index, ETrackedDeviceProperty.Prop_RenderModelName_String, stringBuilder, stringTrackedDeviceProperty, ref etrackedPropertyError);
		string text = stringBuilder.ToString();
		if (this.renderModelName != text)
		{
			this.renderModelName = text;
			base.StartCoroutine(this.SetModelAsync(text));
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x00057F55 File Offset: 0x00056155
	private IEnumerator SetModelAsync(string renderModelName)
	{
		if (string.IsNullOrEmpty(renderModelName))
		{
			yield break;
		}
		using (SteamVR_RenderModel.RenderModelInterfaceHolder holder = new SteamVR_RenderModel.RenderModelInterfaceHolder())
		{
			CVRRenderModels renderModels = holder.instance;
			if (renderModels == null)
			{
				yield break;
			}
			uint componentCount = renderModels.GetComponentCount(renderModelName);
			string[] renderModelNames;
			if (componentCount > 0U)
			{
				renderModelNames = new string[componentCount];
				int num = 0;
				while ((long)num < (long)((ulong)componentCount))
				{
					uint num2 = renderModels.GetComponentName(renderModelName, (uint)num, null, 0U);
					if (num2 != 0U)
					{
						StringBuilder stringBuilder = new StringBuilder((int)num2);
						if (renderModels.GetComponentName(renderModelName, (uint)num, stringBuilder, num2) != 0U)
						{
							num2 = renderModels.GetComponentRenderModelName(renderModelName, stringBuilder.ToString(), null, 0U);
							if (num2 != 0U)
							{
								StringBuilder stringBuilder2 = new StringBuilder((int)num2);
								if (renderModels.GetComponentRenderModelName(renderModelName, stringBuilder.ToString(), stringBuilder2, num2) != 0U)
								{
									string text = stringBuilder2.ToString();
									SteamVR_RenderModel.RenderModel renderModel = SteamVR_RenderModel.models[text] as SteamVR_RenderModel.RenderModel;
									if (renderModel == null || renderModel.mesh == null)
									{
										renderModelNames[num] = text;
									}
								}
							}
						}
					}
					num++;
				}
			}
			else
			{
				SteamVR_RenderModel.RenderModel renderModel2 = SteamVR_RenderModel.models[renderModelName] as SteamVR_RenderModel.RenderModel;
				if (renderModel2 == null || renderModel2.mesh == null)
				{
					renderModelNames = new string[]
					{
						renderModelName
					};
				}
				else
				{
					renderModelNames = new string[0];
				}
			}
			for (;;)
			{
				bool flag = false;
				foreach (string text2 in renderModelNames)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						IntPtr zero = IntPtr.Zero;
						EVRRenderModelError evrrenderModelError = renderModels.LoadRenderModel_Async(text2, ref zero);
						if (evrrenderModelError == EVRRenderModelError.Loading)
						{
							flag = true;
						}
						else if (evrrenderModelError == EVRRenderModelError.None)
						{
							RenderModel_t renderModel_t = this.MarshalRenderModel(zero);
							Material material = SteamVR_RenderModel.materials[renderModel_t.diffuseTextureId] as Material;
							if (material == null || material.mainTexture == null)
							{
								IntPtr zero2 = IntPtr.Zero;
								evrrenderModelError = renderModels.LoadTexture_Async(renderModel_t.diffuseTextureId, ref zero2);
								if (evrrenderModelError == EVRRenderModelError.Loading)
								{
									flag = true;
								}
							}
						}
					}
				}
				if (!flag)
				{
					break;
				}
				yield return new WaitForSeconds(0.1f);
			}
			renderModels = null;
			renderModelNames = null;
		}
		SteamVR_RenderModel.RenderModelInterfaceHolder holder = null;
		bool arg = this.SetModel(renderModelName);
		SteamVR_Events.RenderModelLoaded.Send(this, arg);
		yield break;
		yield break;
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x00057F6C File Offset: 0x0005616C
	private bool SetModel(string renderModelName)
	{
		this.StripMesh(base.gameObject);
		using (SteamVR_RenderModel.RenderModelInterfaceHolder renderModelInterfaceHolder = new SteamVR_RenderModel.RenderModelInterfaceHolder())
		{
			if (this.createComponents)
			{
				if (this.LoadComponents(renderModelInterfaceHolder, renderModelName))
				{
					this.UpdateComponents(renderModelInterfaceHolder.instance);
					return true;
				}
				Debug.Log("[" + base.gameObject.name + "] Render model does not support components, falling back to single mesh.");
			}
			if (!string.IsNullOrEmpty(renderModelName))
			{
				SteamVR_RenderModel.RenderModel renderModel = SteamVR_RenderModel.models[renderModelName] as SteamVR_RenderModel.RenderModel;
				if (renderModel == null || renderModel.mesh == null)
				{
					CVRRenderModels instance = renderModelInterfaceHolder.instance;
					if (instance == null)
					{
						return false;
					}
					if (this.verbose)
					{
						Debug.Log("Loading render model " + renderModelName);
					}
					renderModel = this.LoadRenderModel(instance, renderModelName, renderModelName);
					if (renderModel == null)
					{
						return false;
					}
					SteamVR_RenderModel.models[renderModelName] = renderModel;
				}
				base.gameObject.AddComponent<MeshFilter>().mesh = renderModel.mesh;
				base.gameObject.AddComponent<MeshRenderer>().sharedMaterial = renderModel.material;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x00058090 File Offset: 0x00056290
	private SteamVR_RenderModel.RenderModel LoadRenderModel(CVRRenderModels renderModels, string renderModelName, string baseName)
	{
		IntPtr zero = IntPtr.Zero;
		EVRRenderModelError evrrenderModelError;
		for (;;)
		{
			evrrenderModelError = renderModels.LoadRenderModel_Async(renderModelName, ref zero);
			if (evrrenderModelError != EVRRenderModelError.Loading)
			{
				break;
			}
			SteamVR_RenderModel.Sleep();
		}
		if (evrrenderModelError != EVRRenderModelError.None)
		{
			Debug.LogError(string.Format("Failed to load render model {0} - {1}", renderModelName, evrrenderModelError.ToString()));
			return null;
		}
		RenderModel_t renderModel_t = this.MarshalRenderModel(zero);
		Vector3[] array = new Vector3[renderModel_t.unVertexCount];
		Vector3[] array2 = new Vector3[renderModel_t.unVertexCount];
		Vector2[] array3 = new Vector2[renderModel_t.unVertexCount];
		Type typeFromHandle = typeof(RenderModel_Vertex_t);
		int num = 0;
		while ((long)num < (long)((ulong)renderModel_t.unVertexCount))
		{
			RenderModel_Vertex_t renderModel_Vertex_t = (RenderModel_Vertex_t)Marshal.PtrToStructure(new IntPtr(renderModel_t.rVertexData.ToInt64() + (long)(num * Marshal.SizeOf(typeFromHandle))), typeFromHandle);
			array[num] = new Vector3(renderModel_Vertex_t.vPosition.v0, renderModel_Vertex_t.vPosition.v1, -renderModel_Vertex_t.vPosition.v2);
			array2[num] = new Vector3(renderModel_Vertex_t.vNormal.v0, renderModel_Vertex_t.vNormal.v1, -renderModel_Vertex_t.vNormal.v2);
			array3[num] = new Vector2(renderModel_Vertex_t.rfTextureCoord0, renderModel_Vertex_t.rfTextureCoord1);
			num++;
		}
		uint num2 = renderModel_t.unTriangleCount * 3U;
		short[] array4 = new short[num2];
		Marshal.Copy(renderModel_t.rIndexData, array4, 0, array4.Length);
		int[] array5 = new int[num2];
		int num3 = 0;
		while ((long)num3 < (long)((ulong)renderModel_t.unTriangleCount))
		{
			array5[num3 * 3] = (int)array4[num3 * 3 + 2];
			array5[num3 * 3 + 1] = (int)array4[num3 * 3 + 1];
			array5[num3 * 3 + 2] = (int)array4[num3 * 3];
			num3++;
		}
		Mesh mesh = new Mesh();
		mesh.vertices = array;
		mesh.normals = array2;
		mesh.uv = array3;
		mesh.triangles = array5;
		Material material = SteamVR_RenderModel.materials[renderModel_t.diffuseTextureId] as Material;
		if (material == null || material.mainTexture == null)
		{
			IntPtr zero2 = IntPtr.Zero;
			for (;;)
			{
				evrrenderModelError = renderModels.LoadTexture_Async(renderModel_t.diffuseTextureId, ref zero2);
				if (evrrenderModelError != EVRRenderModelError.Loading)
				{
					break;
				}
				SteamVR_RenderModel.Sleep();
			}
			if (evrrenderModelError == EVRRenderModelError.None)
			{
				RenderModel_TextureMap_t renderModel_TextureMap_t = this.MarshalRenderModel_TextureMap(zero2);
				Texture2D texture2D = new Texture2D((int)renderModel_TextureMap_t.unWidth, (int)renderModel_TextureMap_t.unHeight, TextureFormat.ARGB32, false);
				if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D11)
				{
					texture2D.Apply();
					for (;;)
					{
						evrrenderModelError = renderModels.LoadIntoTextureD3D11_Async(renderModel_t.diffuseTextureId, texture2D.GetNativeTexturePtr());
						if (evrrenderModelError != EVRRenderModelError.Loading)
						{
							break;
						}
						SteamVR_RenderModel.Sleep();
					}
				}
				else
				{
					byte[] array6 = new byte[(int)(renderModel_TextureMap_t.unWidth * renderModel_TextureMap_t.unHeight * '\u0004')];
					Marshal.Copy(renderModel_TextureMap_t.rubTextureMapData, array6, 0, array6.Length);
					Color32[] array7 = new Color32[(int)(renderModel_TextureMap_t.unWidth * renderModel_TextureMap_t.unHeight)];
					int num4 = 0;
					for (int i = 0; i < (int)renderModel_TextureMap_t.unHeight; i++)
					{
						for (int j = 0; j < (int)renderModel_TextureMap_t.unWidth; j++)
						{
							byte r = array6[num4++];
							byte g = array6[num4++];
							byte b = array6[num4++];
							byte a = array6[num4++];
							array7[i * (int)renderModel_TextureMap_t.unWidth + j] = new Color32(r, g, b, a);
						}
					}
					texture2D.SetPixels32(array7);
					texture2D.Apply();
				}
				material = new Material((this.shader != null) ? this.shader : Shader.Find("Standard"));
				material.mainTexture = texture2D;
				SteamVR_RenderModel.materials[renderModel_t.diffuseTextureId] = material;
				renderModels.FreeTexture(zero2);
			}
			else
			{
				Debug.Log("Failed to load render model texture for render model " + renderModelName);
			}
		}
		base.StartCoroutine(this.FreeRenderModel(zero));
		return new SteamVR_RenderModel.RenderModel(mesh, material);
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x00058476 File Offset: 0x00056676
	private IEnumerator FreeRenderModel(IntPtr pRenderModel)
	{
		yield return new WaitForSeconds(1f);
		using (SteamVR_RenderModel.RenderModelInterfaceHolder renderModelInterfaceHolder = new SteamVR_RenderModel.RenderModelInterfaceHolder())
		{
			renderModelInterfaceHolder.instance.FreeRenderModel(pRenderModel);
			yield break;
		}
		yield break;
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x00058488 File Offset: 0x00056688
	public Transform FindComponent(string componentName)
	{
		Transform transform = base.transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (child.name == componentName)
			{
				return child;
			}
		}
		return null;
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x000584C8 File Offset: 0x000566C8
	private void StripMesh(GameObject go)
	{
		MeshRenderer component = go.GetComponent<MeshRenderer>();
		if (component != null)
		{
			Object.DestroyImmediate(component);
		}
		MeshFilter component2 = go.GetComponent<MeshFilter>();
		if (component2 != null)
		{
			Object.DestroyImmediate(component2);
		}
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x00058504 File Offset: 0x00056704
	private bool LoadComponents(SteamVR_RenderModel.RenderModelInterfaceHolder holder, string renderModelName)
	{
		Transform transform = base.transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			child.gameObject.SetActive(false);
			this.StripMesh(child.gameObject);
		}
		if (string.IsNullOrEmpty(renderModelName))
		{
			return true;
		}
		CVRRenderModels instance = holder.instance;
		if (instance == null)
		{
			return false;
		}
		uint componentCount = instance.GetComponentCount(renderModelName);
		if (componentCount == 0U)
		{
			return false;
		}
		int num = 0;
		while ((long)num < (long)((ulong)componentCount))
		{
			uint num2 = instance.GetComponentName(renderModelName, (uint)num, null, 0U);
			if (num2 != 0U)
			{
				StringBuilder stringBuilder = new StringBuilder((int)num2);
				if (instance.GetComponentName(renderModelName, (uint)num, stringBuilder, num2) != 0U)
				{
					transform = this.FindComponent(stringBuilder.ToString());
					if (transform != null)
					{
						transform.gameObject.SetActive(true);
					}
					else
					{
						transform = new GameObject(stringBuilder.ToString()).transform;
						transform.parent = base.transform;
						transform.gameObject.layer = base.gameObject.layer;
						Transform transform2 = new GameObject("attach").transform;
						transform2.parent = transform;
						transform2.localPosition = Vector3.zero;
						transform2.localRotation = Quaternion.identity;
						transform2.localScale = Vector3.one;
						transform2.gameObject.layer = base.gameObject.layer;
					}
					transform.localPosition = Vector3.zero;
					transform.localRotation = Quaternion.identity;
					transform.localScale = Vector3.one;
					num2 = instance.GetComponentRenderModelName(renderModelName, stringBuilder.ToString(), null, 0U);
					if (num2 != 0U)
					{
						StringBuilder stringBuilder2 = new StringBuilder((int)num2);
						if (instance.GetComponentRenderModelName(renderModelName, stringBuilder.ToString(), stringBuilder2, num2) != 0U)
						{
							SteamVR_RenderModel.RenderModel renderModel = SteamVR_RenderModel.models[stringBuilder2] as SteamVR_RenderModel.RenderModel;
							if (renderModel == null || renderModel.mesh == null)
							{
								if (this.verbose)
								{
									Debug.Log("Loading render model " + stringBuilder2);
								}
								renderModel = this.LoadRenderModel(instance, stringBuilder2.ToString(), renderModelName);
								if (renderModel == null)
								{
									goto IL_227;
								}
								SteamVR_RenderModel.models[stringBuilder2] = renderModel;
							}
							transform.gameObject.AddComponent<MeshFilter>().mesh = renderModel.mesh;
							transform.gameObject.AddComponent<MeshRenderer>().sharedMaterial = renderModel.material;
						}
					}
				}
			}
			IL_227:
			num++;
		}
		return true;
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0005874C File Offset: 0x0005694C
	private SteamVR_RenderModel()
	{
		this.deviceConnectedAction = SteamVR_Events.DeviceConnectedAction(new UnityAction<int, bool>(this.OnDeviceConnected));
		this.hideRenderModelsAction = SteamVR_Events.HideRenderModelsAction(new UnityAction<bool>(this.OnHideRenderModels));
		this.modelSkinSettingsHaveChangedAction = SteamVR_Events.SystemAction(EVREventType.VREvent_ModelSkinSettingsHaveChanged, new UnityAction<VREvent_t>(this.OnModelSkinSettingsHaveChanged));
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x000587C0 File Offset: 0x000569C0
	private void OnEnable()
	{
		if (!string.IsNullOrEmpty(this.modelOverride))
		{
			Debug.Log("Model override is really only meant to be used in the scene view for lining things up; using it at runtime is discouraged.  Use tracked device index instead to ensure the correct model is displayed for all users.");
			base.enabled = false;
			return;
		}
		CVRSystem system = OpenVR.System;
		if (system != null && system.IsTrackedDeviceConnected((uint)this.index))
		{
			this.UpdateModel();
		}
		this.deviceConnectedAction.enabled = true;
		this.hideRenderModelsAction.enabled = true;
		this.modelSkinSettingsHaveChangedAction.enabled = true;
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0005882D File Offset: 0x00056A2D
	private void OnDisable()
	{
		this.deviceConnectedAction.enabled = false;
		this.hideRenderModelsAction.enabled = false;
		this.modelSkinSettingsHaveChangedAction.enabled = false;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00058853 File Offset: 0x00056A53
	private void Update()
	{
		if (this.updateDynamically)
		{
			this.UpdateComponents(OpenVR.RenderModels);
		}
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x00058868 File Offset: 0x00056A68
	public void UpdateComponents(CVRRenderModels renderModels)
	{
		if (renderModels == null)
		{
			return;
		}
		Transform transform = base.transform;
		if (transform.childCount == 0)
		{
			return;
		}
		VRControllerState_t vrcontrollerState_t = (this.index != SteamVR_TrackedObject.EIndex.None) ? SteamVR_Controller.Input((int)this.index).GetState() : default(VRControllerState_t);
		if (this.nameCache == null)
		{
			this.nameCache = new Dictionary<int, string>();
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			string name;
			if (!this.nameCache.TryGetValue(child.GetInstanceID(), out name))
			{
				name = child.name;
				this.nameCache.Add(child.GetInstanceID(), name);
			}
			RenderModel_ComponentState_t renderModel_ComponentState_t = default(RenderModel_ComponentState_t);
			if (renderModels.GetComponentState(this.renderModelName, name, ref vrcontrollerState_t, ref this.controllerModeState, ref renderModel_ComponentState_t))
			{
				SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(renderModel_ComponentState_t.mTrackingToComponentRenderModel);
				child.localPosition = rigidTransform.pos;
				child.localRotation = rigidTransform.rot;
				Transform transform2 = child.Find("attach");
				if (transform2 != null)
				{
					SteamVR_Utils.RigidTransform rigidTransform2 = new SteamVR_Utils.RigidTransform(renderModel_ComponentState_t.mTrackingToComponentLocal);
					transform2.position = transform.TransformPoint(rigidTransform2.pos);
					transform2.rotation = transform.rotation * rigidTransform2.rot;
				}
				bool flag = (renderModel_ComponentState_t.uProperties & 2U) > 0U;
				if (flag != child.gameObject.activeSelf)
				{
					child.gameObject.SetActive(flag);
				}
			}
		}
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x000589E2 File Offset: 0x00056BE2
	public void SetDeviceIndex(int index)
	{
		this.index = (SteamVR_TrackedObject.EIndex)index;
		this.modelOverride = "";
		if (base.enabled)
		{
			this.UpdateModel();
		}
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00058A04 File Offset: 0x00056C04
	private static void Sleep()
	{
		Thread.Sleep(1);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00058A0C File Offset: 0x00056C0C
	private RenderModel_t MarshalRenderModel(IntPtr pRenderModel)
	{
		if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
		{
			RenderModel_t_Packed renderModel_t_Packed = (RenderModel_t_Packed)Marshal.PtrToStructure(pRenderModel, typeof(RenderModel_t_Packed));
			RenderModel_t result = default(RenderModel_t);
			renderModel_t_Packed.Unpack(ref result);
			return result;
		}
		return (RenderModel_t)Marshal.PtrToStructure(pRenderModel, typeof(RenderModel_t));
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00058A74 File Offset: 0x00056C74
	private RenderModel_TextureMap_t MarshalRenderModel_TextureMap(IntPtr pRenderModel)
	{
		if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
		{
			RenderModel_TextureMap_t_Packed renderModel_TextureMap_t_Packed = (RenderModel_TextureMap_t_Packed)Marshal.PtrToStructure(pRenderModel, typeof(RenderModel_TextureMap_t_Packed));
			RenderModel_TextureMap_t result = default(RenderModel_TextureMap_t);
			renderModel_TextureMap_t_Packed.Unpack(ref result);
			return result;
		}
		return (RenderModel_TextureMap_t)Marshal.PtrToStructure(pRenderModel, typeof(RenderModel_TextureMap_t));
	}

	// Token: 0x04000E51 RID: 3665
	public SteamVR_TrackedObject.EIndex index = SteamVR_TrackedObject.EIndex.None;

	// Token: 0x04000E52 RID: 3666
	public const string modelOverrideWarning = "Model override is really only meant to be used in the scene view for lining things up; using it at runtime is discouraged.  Use tracked device index instead to ensure the correct model is displayed for all users.";

	// Token: 0x04000E53 RID: 3667
	[Tooltip("Model override is really only meant to be used in the scene view for lining things up; using it at runtime is discouraged.  Use tracked device index instead to ensure the correct model is displayed for all users.")]
	public string modelOverride;

	// Token: 0x04000E54 RID: 3668
	[Tooltip("Shader to apply to model.")]
	public Shader shader;

	// Token: 0x04000E55 RID: 3669
	[Tooltip("Enable to print out when render models are loaded.")]
	public bool verbose;

	// Token: 0x04000E56 RID: 3670
	[Tooltip("If available, break down into separate components instead of loading as a single mesh.")]
	public bool createComponents = true;

	// Token: 0x04000E57 RID: 3671
	[Tooltip("Update transforms of components at runtime to reflect user action.")]
	public bool updateDynamically = true;

	// Token: 0x04000E58 RID: 3672
	public RenderModel_ControllerMode_State_t controllerModeState;

	// Token: 0x04000E59 RID: 3673
	public const string k_localTransformName = "attach";

	// Token: 0x04000E5B RID: 3675
	public static Hashtable models = new Hashtable();

	// Token: 0x04000E5C RID: 3676
	public static Hashtable materials = new Hashtable();

	// Token: 0x04000E5D RID: 3677
	private SteamVR_Events.Action deviceConnectedAction;

	// Token: 0x04000E5E RID: 3678
	private SteamVR_Events.Action hideRenderModelsAction;

	// Token: 0x04000E5F RID: 3679
	private SteamVR_Events.Action modelSkinSettingsHaveChangedAction;

	// Token: 0x04000E60 RID: 3680
	private Dictionary<int, string> nameCache;

	// Token: 0x0200057A RID: 1402
	public class RenderModel
	{
		// Token: 0x06002890 RID: 10384 RVA: 0x000C42A4 File Offset: 0x000C24A4
		public RenderModel(Mesh mesh, Material material)
		{
			this.mesh = mesh;
			this.material = material;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x000C42BA File Offset: 0x000C24BA
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x000C42C2 File Offset: 0x000C24C2
		public Mesh mesh { get; private set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x000C42CB File Offset: 0x000C24CB
		// (set) Token: 0x06002894 RID: 10388 RVA: 0x000C42D3 File Offset: 0x000C24D3
		public Material material { get; private set; }
	}

	// Token: 0x0200057B RID: 1403
	public sealed class RenderModelInterfaceHolder : IDisposable
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000C42DC File Offset: 0x000C24DC
		public CVRRenderModels instance
		{
			get
			{
				if (this._instance == null && !this.failedLoadInterface)
				{
					if (!SteamVR.active && !SteamVR.usingNativeSupport)
					{
						EVRInitError evrinitError = EVRInitError.None;
						OpenVR.Init(ref evrinitError, EVRApplicationType.VRApplication_Utility);
						this.needsShutdown = true;
					}
					this._instance = OpenVR.RenderModels;
					if (this._instance == null)
					{
						Debug.LogError("Failed to load IVRRenderModels interface version IVRRenderModels_005");
						this.failedLoadInterface = true;
					}
				}
				return this._instance;
			}
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000C4343 File Offset: 0x000C2543
		public void Dispose()
		{
			if (this.needsShutdown)
			{
				OpenVR.Shutdown();
			}
		}

		// Token: 0x04002605 RID: 9733
		private bool needsShutdown;

		// Token: 0x04002606 RID: 9734
		private bool failedLoadInterface;

		// Token: 0x04002607 RID: 9735
		private CVRRenderModels _instance;
	}
}
