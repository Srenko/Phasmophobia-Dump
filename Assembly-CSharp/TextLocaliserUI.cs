using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000177 RID: 375
public class TextLocaliserUI : MonoBehaviour
{
	// Token: 0x060009EA RID: 2538 RVA: 0x0003D3B9 File Offset: 0x0003B5B9
	private void Awake()
	{
		this.textField = base.GetComponent<Text>();
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0003D3C7 File Offset: 0x0003B5C7
	private void Start()
	{
		this.LoadText();
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0003D3D0 File Offset: 0x0003B5D0
	private void OnEnable()
	{
		if (SceneManager.GetActiveScene().name == "Menu_New")
		{
			this.LoadText();
		}
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0003D3FC File Offset: 0x0003B5FC
	public void LoadText()
	{
		if (this.textField != null && this.textField.text != string.Empty)
		{
			this.textField.text = LocalisationSystem.GetLocalisedValue(this.key);
		}
	}

	// Token: 0x04000A0E RID: 2574
	public string key;

	// Token: 0x04000A0F RID: 2575
	private Text textField;
}
