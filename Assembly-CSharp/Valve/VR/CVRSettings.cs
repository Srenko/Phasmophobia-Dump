using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x0200039B RID: 923
	public class CVRSettings
	{
		// Token: 0x06001FE3 RID: 8163 RVA: 0x0009E28E File Offset: 0x0009C48E
		internal CVRSettings(IntPtr pInterface)
		{
			this.FnTable = (IVRSettings)Marshal.PtrToStructure(pInterface, typeof(IVRSettings));
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0009E2B1 File Offset: 0x0009C4B1
		public string GetSettingsErrorNameFromEnum(EVRSettingsError eError)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetSettingsErrorNameFromEnum(eError));
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0009E2C9 File Offset: 0x0009C4C9
		public bool Sync(bool bForce, ref EVRSettingsError peError)
		{
			return this.FnTable.Sync(bForce, ref peError);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0009E2DD File Offset: 0x0009C4DD
		public void SetBool(string pchSection, string pchSettingsKey, bool bValue, ref EVRSettingsError peError)
		{
			this.FnTable.SetBool(pchSection, pchSettingsKey, bValue, ref peError);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0009E2F4 File Offset: 0x0009C4F4
		public void SetInt32(string pchSection, string pchSettingsKey, int nValue, ref EVRSettingsError peError)
		{
			this.FnTable.SetInt32(pchSection, pchSettingsKey, nValue, ref peError);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0009E30B File Offset: 0x0009C50B
		public void SetFloat(string pchSection, string pchSettingsKey, float flValue, ref EVRSettingsError peError)
		{
			this.FnTable.SetFloat(pchSection, pchSettingsKey, flValue, ref peError);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0009E322 File Offset: 0x0009C522
		public void SetString(string pchSection, string pchSettingsKey, string pchValue, ref EVRSettingsError peError)
		{
			this.FnTable.SetString(pchSection, pchSettingsKey, pchValue, ref peError);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0009E339 File Offset: 0x0009C539
		public bool GetBool(string pchSection, string pchSettingsKey, ref EVRSettingsError peError)
		{
			return this.FnTable.GetBool(pchSection, pchSettingsKey, ref peError);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0009E34E File Offset: 0x0009C54E
		public int GetInt32(string pchSection, string pchSettingsKey, ref EVRSettingsError peError)
		{
			return this.FnTable.GetInt32(pchSection, pchSettingsKey, ref peError);
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0009E363 File Offset: 0x0009C563
		public float GetFloat(string pchSection, string pchSettingsKey, ref EVRSettingsError peError)
		{
			return this.FnTable.GetFloat(pchSection, pchSettingsKey, ref peError);
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0009E378 File Offset: 0x0009C578
		public void GetString(string pchSection, string pchSettingsKey, StringBuilder pchValue, uint unValueLen, ref EVRSettingsError peError)
		{
			this.FnTable.GetString(pchSection, pchSettingsKey, pchValue, unValueLen, ref peError);
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0009E391 File Offset: 0x0009C591
		public void RemoveSection(string pchSection, ref EVRSettingsError peError)
		{
			this.FnTable.RemoveSection(pchSection, ref peError);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0009E3A5 File Offset: 0x0009C5A5
		public void RemoveKeyInSection(string pchSection, string pchSettingsKey, ref EVRSettingsError peError)
		{
			this.FnTable.RemoveKeyInSection(pchSection, pchSettingsKey, ref peError);
		}

		// Token: 0x04001934 RID: 6452
		private IVRSettings FnTable;
	}
}
