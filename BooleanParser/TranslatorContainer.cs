using System;
using System.Data.Services.Client;

namespace Microsoft
{
	public partial class TranslatorContainer : System.Data.Services.Client.DataServiceContext
	{

		public TranslatorContainer(Uri serviceRoot) :
			base(serviceRoot)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="Text">the text to translate Sample Values : hello</param>
		/// <param name="To">the language code to translate the text into Sample Values : nl</param>
		/// <param name="From">the language code of the translation text Sample Values : en</param>
		public DataServiceQuery<Translation> Translate(String Text, String To, String From)
		{
			if ((Text == null))
			{
				throw new System.ArgumentNullException("Text", "Text value cannot be null");
			}
			if ((To == null))
			{
				throw new System.ArgumentNullException("To", "To value cannot be null");
			}
			DataServiceQuery<Translation> query;
			query = base.CreateQuery<Translation>("Translate");
			if ((Text != null))
			{
				query = query.AddQueryOption("Text", string.Concat("\'", System.Uri.EscapeDataString(Text), "\'"));
			}
			if ((To != null))
			{
				query = query.AddQueryOption("To", string.Concat("\'", System.Uri.EscapeDataString(To), "\'"));
			}
			if ((From != null))
			{
				query = query.AddQueryOption("From", string.Concat("\'", System.Uri.EscapeDataString(From), "\'"));
			}
			return query;
		}

		/// <summary>
		/// </summary>
		public DataServiceQuery<Language> GetLanguagesForTranslation()
		{
			DataServiceQuery<Language> query;
			query = base.CreateQuery<Language>("GetLanguagesForTranslation");
			return query;
		}

		/// <summary>
		/// </summary>
		/// <param name="Text">the text whose language is to be identified Sample Values : hello</param>
		public DataServiceQuery<DetectedLanguage> Detect(String Text)
		{
			if ((Text == null))
			{
				throw new System.ArgumentNullException("Text", "Text value cannot be null");
			}
			DataServiceQuery<DetectedLanguage> query;
			query = base.CreateQuery<DetectedLanguage>("Detect");
			if ((Text != null))
			{
				query = query.AddQueryOption("Text", string.Concat("\'", System.Uri.EscapeDataString(Text), "\'"));
			}
			return query;
		}
	}
}