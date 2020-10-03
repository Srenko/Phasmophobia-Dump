using System;
using System.Collections.Generic;

// Token: 0x0200009D RID: 157
public class AuthenticationValues
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000329 RID: 809 RVA: 0x000139F0 File Offset: 0x00011BF0
	// (set) Token: 0x0600032A RID: 810 RVA: 0x000139F8 File Offset: 0x00011BF8
	public CustomAuthenticationType AuthType
	{
		get
		{
			return this.authType;
		}
		set
		{
			this.authType = value;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600032B RID: 811 RVA: 0x00013A01 File Offset: 0x00011C01
	// (set) Token: 0x0600032C RID: 812 RVA: 0x00013A09 File Offset: 0x00011C09
	public string AuthGetParameters { get; set; }

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600032D RID: 813 RVA: 0x00013A12 File Offset: 0x00011C12
	// (set) Token: 0x0600032E RID: 814 RVA: 0x00013A1A File Offset: 0x00011C1A
	public object AuthPostData { get; private set; }

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600032F RID: 815 RVA: 0x00013A23 File Offset: 0x00011C23
	// (set) Token: 0x06000330 RID: 816 RVA: 0x00013A2B File Offset: 0x00011C2B
	public string Token { get; set; }

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06000331 RID: 817 RVA: 0x00013A34 File Offset: 0x00011C34
	// (set) Token: 0x06000332 RID: 818 RVA: 0x00013A3C File Offset: 0x00011C3C
	public string UserId { get; set; }

	// Token: 0x06000333 RID: 819 RVA: 0x00013A45 File Offset: 0x00011C45
	public AuthenticationValues()
	{
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00013A58 File Offset: 0x00011C58
	public AuthenticationValues(string userId)
	{
		this.UserId = userId;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00013A72 File Offset: 0x00011C72
	public virtual void SetAuthPostData(string stringData)
	{
		this.AuthPostData = (string.IsNullOrEmpty(stringData) ? null : stringData);
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00013A86 File Offset: 0x00011C86
	public virtual void SetAuthPostData(byte[] byteData)
	{
		this.AuthPostData = byteData;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00013A86 File Offset: 0x00011C86
	public virtual void SetAuthPostData(Dictionary<string, object> dictData)
	{
		this.AuthPostData = dictData;
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00013A90 File Offset: 0x00011C90
	public virtual void AddAuthParameter(string key, string value)
	{
		string text = string.IsNullOrEmpty(this.AuthGetParameters) ? "" : "&";
		this.AuthGetParameters = string.Format("{0}{1}{2}={3}", new object[]
		{
			this.AuthGetParameters,
			text,
			Uri.EscapeDataString(key),
			Uri.EscapeDataString(value)
		});
	}

	// Token: 0x06000339 RID: 825 RVA: 0x00013AEC File Offset: 0x00011CEC
	public override string ToString()
	{
		return string.Format("AuthenticationValues UserId: {0}, GetParameters: {1} Token available: {2}", this.UserId, this.AuthGetParameters, this.Token != null);
	}

	// Token: 0x04000444 RID: 1092
	private CustomAuthenticationType authType = CustomAuthenticationType.None;
}
