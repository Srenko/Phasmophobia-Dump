using System;

namespace Photon.Chat
{
	// Token: 0x02000462 RID: 1122
	public class AuthenticationValues
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x000AC057 File Offset: 0x000AA257
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x000AC05F File Offset: 0x000AA25F
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

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000AC068 File Offset: 0x000AA268
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x000AC070 File Offset: 0x000AA270
		public string AuthGetParameters { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x000AC079 File Offset: 0x000AA279
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x000AC081 File Offset: 0x000AA281
		public object AuthPostData { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x000AC08A File Offset: 0x000AA28A
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x000AC092 File Offset: 0x000AA292
		public string Token { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x000AC09B File Offset: 0x000AA29B
		// (set) Token: 0x060022DA RID: 8922 RVA: 0x000AC0A3 File Offset: 0x000AA2A3
		public string UserId { get; set; }

		// Token: 0x060022DB RID: 8923 RVA: 0x000AC0AC File Offset: 0x000AA2AC
		public AuthenticationValues()
		{
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000AC0BF File Offset: 0x000AA2BF
		public AuthenticationValues(string userId)
		{
			this.UserId = userId;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000AC0D9 File Offset: 0x000AA2D9
		public virtual void SetAuthPostData(string stringData)
		{
			this.AuthPostData = (string.IsNullOrEmpty(stringData) ? null : stringData);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000AC0ED File Offset: 0x000AA2ED
		public virtual void SetAuthPostData(byte[] byteData)
		{
			this.AuthPostData = byteData;
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000AC0F8 File Offset: 0x000AA2F8
		public virtual void AddAuthParameter(string key, string value)
		{
			string text = string.IsNullOrEmpty(this.AuthGetParameters) ? string.Empty : "&";
			this.AuthGetParameters = string.Format("{0}{1}{2}={3}", new object[]
			{
				this.AuthGetParameters,
				text,
				Uri.EscapeDataString(key),
				Uri.EscapeDataString(value)
			});
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000AC154 File Offset: 0x000AA354
		public override string ToString()
		{
			return string.Format("AuthenticationValues UserId: {0}, GetParameters: {1} Token available: {2}", this.UserId, this.AuthGetParameters, this.Token != null);
		}

		// Token: 0x0400204F RID: 8271
		private CustomAuthenticationType authType = CustomAuthenticationType.None;
	}
}
