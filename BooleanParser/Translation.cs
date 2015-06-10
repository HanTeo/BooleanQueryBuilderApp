using System;

namespace Microsoft
{
	public partial class Translation
	{

		private String _Text;

		public String Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				this._Text = value;
			}
		}
	}
}