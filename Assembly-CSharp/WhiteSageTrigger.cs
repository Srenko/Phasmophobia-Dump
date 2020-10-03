using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class WhiteSageTrigger : MonoBehaviour
{
	// Token: 0x060009D0 RID: 2512 RVA: 0x0003C554 File Offset: 0x0003A754
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Lighter>() != null)
		{
			if (other.GetComponent<Lighter>().isOn)
			{
				this.whiteSage.Use();
				return;
			}
		}
		else if (other.GetComponent<Candle>() != null && other.GetComponent<Candle>().isOn)
		{
			this.whiteSage.Use();
		}
	}

	// Token: 0x040009E7 RID: 2535
	[SerializeField]
	private WhiteSage whiteSage;
}
