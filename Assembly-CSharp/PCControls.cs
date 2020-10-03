using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

// Token: 0x020001AC RID: 428
public class PCControls : MonoBehaviour
{
	// Token: 0x06000B9C RID: 2972 RVA: 0x00047EB9 File Offset: 0x000460B9
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00047EC7 File Offset: 0x000460C7
	private void Start()
	{
		if ((this.view.isMine || !PhotonNetwork.inRoom) && MainManager.instance)
		{
			this.LoadControlOverrides();
			MainManager.instance.controlsManager.LoadControls();
		}
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00047F00 File Offset: 0x00046100
	public void StoreControlOverrides()
	{
		PCControls.BindingWrapperClass bindingWrapperClass = new PCControls.BindingWrapperClass();
		foreach (InputActionMap inputActionMap in this.control.actionMaps)
		{
			foreach (InputBinding inputBinding in inputActionMap.bindings)
			{
				if (!string.IsNullOrEmpty(inputBinding.overridePath))
				{
					bindingWrapperClass.bindingList.Add(new PCControls.BindingSerializable(inputBinding.id.ToString(), inputBinding.overridePath));
				}
			}
		}
		PlayerPrefs.SetString("ControlOverrides", JsonUtility.ToJson(bindingWrapperClass));
		PlayerPrefs.Save();
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x00047FDC File Offset: 0x000461DC
	public void LoadControlOverrides()
	{
		if (PlayerPrefs.HasKey("ControlOverrides"))
		{
			PCControls.BindingWrapperClass bindingWrapperClass = JsonUtility.FromJson(PlayerPrefs.GetString("ControlOverrides"), typeof(PCControls.BindingWrapperClass)) as PCControls.BindingWrapperClass;
			Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
			foreach (PCControls.BindingSerializable bindingSerializable in bindingWrapperClass.bindingList)
			{
				dictionary.Add(new Guid(bindingSerializable.id), bindingSerializable.path);
			}
			foreach (InputActionMap inputActionMap in this.control.actionMaps)
			{
				ReadOnlyArray<InputBinding> bindings = inputActionMap.bindings;
				for (int i = 0; i < bindings.Count; i++)
				{
					string overridePath;
					if (dictionary.TryGetValue(bindings[i].id, out overridePath))
					{
						inputActionMap.ApplyBindingOverride(i, new InputBinding
						{
							overridePath = overridePath
						});
					}
				}
			}
		}
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00048108 File Offset: 0x00046308
	public void ResetKeybindings()
	{
		foreach (InputActionMap actionMap in this.controlAsset.actionMaps)
		{
			actionMap.RemoveAllBindingOverrides();
		}
		PlayerPrefs.DeleteKey("ControlOverrides");
	}

	// Token: 0x04000BF3 RID: 3059
	public InputActionAsset control;

	// Token: 0x04000BF4 RID: 3060
	private PhotonView view;

	// Token: 0x04000BF5 RID: 3061
	[SerializeField]
	private InputActionAsset controlAsset;

	// Token: 0x02000553 RID: 1363
	[Serializable]
	private class BindingWrapperClass
	{
		// Token: 0x04002575 RID: 9589
		public List<PCControls.BindingSerializable> bindingList = new List<PCControls.BindingSerializable>();
	}

	// Token: 0x02000554 RID: 1364
	[Serializable]
	private struct BindingSerializable
	{
		// Token: 0x060027DE RID: 10206 RVA: 0x000C2324 File Offset: 0x000C0524
		public BindingSerializable(string bindingId, string bindingPath)
		{
			this.id = bindingId;
			this.path = bindingPath;
		}

		// Token: 0x04002576 RID: 9590
		public string id;

		// Token: 0x04002577 RID: 9591
		public string path;
	}
}
