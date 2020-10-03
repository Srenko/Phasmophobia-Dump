using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

// Token: 0x020000AB RID: 171
public class WebRpcResponse
{
	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060003FB RID: 1019 RVA: 0x00019EBE File Offset: 0x000180BE
	// (set) Token: 0x060003FC RID: 1020 RVA: 0x00019EC6 File Offset: 0x000180C6
	public string Name { get; private set; }

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060003FD RID: 1021 RVA: 0x00019ECF File Offset: 0x000180CF
	// (set) Token: 0x060003FE RID: 1022 RVA: 0x00019ED7 File Offset: 0x000180D7
	public int ReturnCode { get; private set; }

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060003FF RID: 1023 RVA: 0x00019EE0 File Offset: 0x000180E0
	// (set) Token: 0x06000400 RID: 1024 RVA: 0x00019EE8 File Offset: 0x000180E8
	public string DebugMessage { get; private set; }

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000401 RID: 1025 RVA: 0x00019EF1 File Offset: 0x000180F1
	// (set) Token: 0x06000402 RID: 1026 RVA: 0x00019EF9 File Offset: 0x000180F9
	public Dictionary<string, object> Parameters { get; private set; }

	// Token: 0x06000403 RID: 1027 RVA: 0x00019F04 File Offset: 0x00018104
	public WebRpcResponse(OperationResponse response)
	{
		object obj;
		response.Parameters.TryGetValue(209, out obj);
		this.Name = (obj as string);
		response.Parameters.TryGetValue(207, out obj);
		this.ReturnCode = ((obj != null) ? ((int)((byte)obj)) : -1);
		response.Parameters.TryGetValue(208, out obj);
		this.Parameters = (obj as Dictionary<string, object>);
		response.Parameters.TryGetValue(206, out obj);
		this.DebugMessage = (obj as string);
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x00019F99 File Offset: 0x00018199
	public string ToStringFull()
	{
		return string.Format("{0}={2}: {1} \"{3}\"", new object[]
		{
			this.Name,
			SupportClass.DictionaryToString(this.Parameters),
			this.ReturnCode,
			this.DebugMessage
		});
	}
}
