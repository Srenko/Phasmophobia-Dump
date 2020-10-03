using System;

// Token: 0x02000099 RID: 153
public class TypedLobby
{
	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000322 RID: 802 RVA: 0x00013925 File Offset: 0x00011B25
	public bool IsDefault
	{
		get
		{
			return this.Type == LobbyType.Default && string.IsNullOrEmpty(this.Name);
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0001393C File Offset: 0x00011B3C
	public TypedLobby()
	{
		this.Name = string.Empty;
		this.Type = LobbyType.Default;
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00013956 File Offset: 0x00011B56
	public TypedLobby(string name, LobbyType type)
	{
		this.Name = name;
		this.Type = type;
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0001396C File Offset: 0x00011B6C
	public override string ToString()
	{
		return string.Format("lobby '{0}'[{1}]", this.Name, this.Type);
	}

	// Token: 0x04000431 RID: 1073
	public string Name;

	// Token: 0x04000432 RID: 1074
	public LobbyType Type;

	// Token: 0x04000433 RID: 1075
	public static readonly TypedLobby Default = new TypedLobby();
}
