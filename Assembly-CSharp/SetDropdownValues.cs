using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000176 RID: 374
[RequireComponent(typeof(Dropdown))]
public class SetDropdownValues : MonoBehaviour
{
	// Token: 0x060009E7 RID: 2535 RVA: 0x0003D32C File Offset: 0x0003B52C
	private void Awake()
	{
		this.dropdown = base.GetComponent<Dropdown>();
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0003D33C File Offset: 0x0003B53C
	private void Start()
	{
		for (int i = 0; i < this.dropdown.options.Count; i++)
		{
			this.values.Add(LocalisationSystem.GetLocalisedValue(this.dropdown.options[i].text));
		}
		this.dropdown.ClearOptions();
		this.dropdown.AddOptions(this.values);
	}

	// Token: 0x04000A0C RID: 2572
	private Dropdown dropdown;

	// Token: 0x04000A0D RID: 2573
	private List<string> values = new List<string>();
}
