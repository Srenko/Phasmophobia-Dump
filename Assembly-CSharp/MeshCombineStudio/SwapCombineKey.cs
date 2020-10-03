using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004A3 RID: 1187
	public class SwapCombineKey : MonoBehaviour
	{
		// Token: 0x06002512 RID: 9490 RVA: 0x000B7344 File Offset: 0x000B5544
		private void Awake()
		{
			SwapCombineKey.instance = this;
			this.meshCombiner = base.GetComponent<MeshCombiner>();
			this.meshCombinerList.Add(this.meshCombiner);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000B7369 File Offset: 0x000B5569
		private void OnDestroy()
		{
			SwapCombineKey.instance = null;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000B7371 File Offset: 0x000B5571
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				QualitySettings.vSyncCount = 0;
				this.meshCombiner.SwapCombine();
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000B7390 File Offset: 0x000B5590
		private void OnGUI()
		{
			if (this.textStyle == null)
			{
				this.textStyle = new GUIStyle("label");
				this.textStyle.fontStyle = FontStyle.Bold;
				this.textStyle.fontSize = 16;
			}
			this.textStyle.normal.textColor = (this.meshCombiner.combinedActive ? Color.green : Color.red);
			GUI.Label(new Rect(10f, (float)(45 + this.meshCombinerList.Count * 22), 200f, 30f), "Toggle with 'Tab' key.", this.textStyle);
			for (int i = 0; i < this.meshCombinerList.Count; i++)
			{
				MeshCombiner meshCombiner = this.meshCombinerList[i];
				if (meshCombiner.combinedActive)
				{
					GUI.Label(new Rect(10f, (float)(30 + i * 22), 300f, 30f), meshCombiner.gameObject.name + " is Enabled.", this.textStyle);
				}
				else
				{
					GUI.Label(new Rect(10f, (float)(30 + i * 22), 300f, 30f), meshCombiner.gameObject.name + " is Disabled.", this.textStyle);
				}
			}
		}

		// Token: 0x04002271 RID: 8817
		public static SwapCombineKey instance;

		// Token: 0x04002272 RID: 8818
		public List<MeshCombiner> meshCombinerList = new List<MeshCombiner>();

		// Token: 0x04002273 RID: 8819
		private MeshCombiner meshCombiner;

		// Token: 0x04002274 RID: 8820
		private GUIStyle textStyle;
	}
}
