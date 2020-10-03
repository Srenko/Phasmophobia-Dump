using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x020001DF RID: 479
[RequireComponent(typeof(Camera))]
public class SteamVR_Camera : MonoBehaviour
{
	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00053198 File Offset: 0x00051398
	public Transform head
	{
		get
		{
			return this._head;
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00053198 File Offset: 0x00051398
	public Transform offset
	{
		get
		{
			return this._head;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06000D3D RID: 3389 RVA: 0x000531A0 File Offset: 0x000513A0
	public Transform origin
	{
		get
		{
			return this._head.parent;
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000D3E RID: 3390 RVA: 0x000531AD File Offset: 0x000513AD
	// (set) Token: 0x06000D3F RID: 3391 RVA: 0x000531B5 File Offset: 0x000513B5
	public Camera camera { get; private set; }

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000D40 RID: 3392 RVA: 0x000531BE File Offset: 0x000513BE
	public Transform ears
	{
		get
		{
			return this._ears;
		}
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x000531C6 File Offset: 0x000513C6
	public Ray GetRay()
	{
		return new Ray(this._head.position, this._head.forward);
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000531E3 File Offset: 0x000513E3
	// (set) Token: 0x06000D43 RID: 3395 RVA: 0x000531EA File Offset: 0x000513EA
	public static float sceneResolutionScale
	{
		get
		{
			return XRSettings.eyeTextureResolutionScale;
		}
		set
		{
			XRSettings.eyeTextureResolutionScale = value;
		}
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x000531F2 File Offset: 0x000513F2
	private void OnDisable()
	{
		SteamVR_Render.Remove(this);
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x000531FC File Offset: 0x000513FC
	private void OnEnable()
	{
		if (SteamVR.instance == null)
		{
			if (this.head != null)
			{
				this.head.GetComponent<SteamVR_TrackedObject>().enabled = false;
			}
			base.enabled = false;
			return;
		}
		Transform transform = base.transform;
		if (this.head != transform)
		{
			this.Expand();
			transform.parent = this.origin;
			while (this.head.childCount > 0)
			{
				this.head.GetChild(0).parent = transform;
			}
			this.head.parent = transform;
			this.head.localPosition = Vector3.zero;
			this.head.localRotation = Quaternion.identity;
			this.head.localScale = Vector3.one;
			this.head.gameObject.SetActive(false);
			this._head = transform;
		}
		if (this.ears == null)
		{
			SteamVR_Ears componentInChildren = base.transform.GetComponentInChildren<SteamVR_Ears>();
			if (componentInChildren != null)
			{
				this._ears = componentInChildren.transform;
			}
		}
		if (this.ears != null)
		{
			this.ears.GetComponent<SteamVR_Ears>().vrcam = this;
		}
		SteamVR_Render.Add(this);
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0005332B File Offset: 0x0005152B
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
		this.ForceLast();
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x00053340 File Offset: 0x00051540
	public void ForceLast()
	{
		if (SteamVR_Camera.values != null)
		{
			foreach (object obj in SteamVR_Camera.values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				(dictionaryEntry.Key as FieldInfo).SetValue(this, dictionaryEntry.Value);
			}
			SteamVR_Camera.values = null;
			return;
		}
		Component[] components = base.GetComponents<Component>();
		for (int i = 0; i < components.Length; i++)
		{
			SteamVR_Camera steamVR_Camera = components[i] as SteamVR_Camera;
			if (steamVR_Camera != null && steamVR_Camera != this)
			{
				Object.DestroyImmediate(steamVR_Camera);
			}
		}
		components = base.GetComponents<Component>();
		if (this != components[components.Length - 1])
		{
			SteamVR_Camera.values = new Hashtable();
			foreach (FieldInfo fieldInfo in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (fieldInfo.IsPublic || fieldInfo.IsDefined(typeof(SerializeField), true))
				{
					SteamVR_Camera.values[fieldInfo] = fieldInfo.GetValue(this);
				}
			}
			GameObject gameObject = base.gameObject;
			Object.DestroyImmediate(this);
			gameObject.AddComponent<SteamVR_Camera>().ForceLast();
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00053488 File Offset: 0x00051688
	public string baseName
	{
		get
		{
			if (!base.name.EndsWith(" (eye)"))
			{
				return base.name;
			}
			return base.name.Substring(0, base.name.Length - " (eye)".Length);
		}
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x000534C8 File Offset: 0x000516C8
	public void Expand()
	{
		Transform transform = base.transform.parent;
		if (transform == null)
		{
			transform = new GameObject(base.name + " (origin)").transform;
			transform.localPosition = base.transform.localPosition;
			transform.localRotation = base.transform.localRotation;
			transform.localScale = base.transform.localScale;
		}
		if (this.head == null)
		{
			this._head = new GameObject(base.name + " (head)", new Type[]
			{
				typeof(SteamVR_TrackedObject)
			}).transform;
			this.head.parent = transform;
			this.head.position = base.transform.position;
			this.head.rotation = base.transform.rotation;
			this.head.localScale = Vector3.one;
			this.head.tag = base.tag;
		}
		if (base.transform.parent != this.head)
		{
			base.transform.parent = this.head;
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = Vector3.one;
			while (base.transform.childCount > 0)
			{
				base.transform.GetChild(0).parent = this.head;
			}
			AudioListener component = base.GetComponent<AudioListener>();
			if (component != null)
			{
				Object.DestroyImmediate(component);
				this._ears = new GameObject(base.name + " (ears)", new Type[]
				{
					typeof(SteamVR_Ears)
				}).transform;
				this.ears.parent = this._head;
				this.ears.localPosition = Vector3.zero;
				this.ears.localRotation = Quaternion.identity;
				this.ears.localScale = Vector3.one;
			}
		}
		if (!base.name.EndsWith(" (eye)"))
		{
			base.name += " (eye)";
		}
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0005370C File Offset: 0x0005190C
	public void Collapse()
	{
		base.transform.parent = null;
		while (this.head.childCount > 0)
		{
			this.head.GetChild(0).parent = base.transform;
		}
		if (this.ears != null)
		{
			while (this.ears.childCount > 0)
			{
				this.ears.GetChild(0).parent = base.transform;
			}
			Object.DestroyImmediate(this.ears.gameObject);
			this._ears = null;
			base.gameObject.AddComponent(typeof(AudioListener));
		}
		if (this.origin != null)
		{
			if (this.origin.name.EndsWith(" (origin)"))
			{
				Transform origin = this.origin;
				while (origin.childCount > 0)
				{
					origin.GetChild(0).parent = origin.parent;
				}
				Object.DestroyImmediate(origin.gameObject);
			}
			else
			{
				base.transform.parent = this.origin;
			}
		}
		Object.DestroyImmediate(this.head.gameObject);
		this._head = null;
		if (base.name.EndsWith(" (eye)"))
		{
			base.name = base.name.Substring(0, base.name.Length - " (eye)".Length);
		}
	}

	// Token: 0x04000DAE RID: 3502
	[SerializeField]
	private Transform _head;

	// Token: 0x04000DB0 RID: 3504
	[SerializeField]
	private Transform _ears;

	// Token: 0x04000DB1 RID: 3505
	public bool wireframe;

	// Token: 0x04000DB2 RID: 3506
	private static Hashtable values;

	// Token: 0x04000DB3 RID: 3507
	private const string eyeSuffix = " (eye)";

	// Token: 0x04000DB4 RID: 3508
	private const string earsSuffix = " (ears)";

	// Token: 0x04000DB5 RID: 3509
	private const string headSuffix = " (head)";

	// Token: 0x04000DB6 RID: 3510
	private const string originSuffix = " (origin)";
}
