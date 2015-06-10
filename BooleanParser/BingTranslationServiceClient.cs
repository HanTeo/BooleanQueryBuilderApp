using System;
using System.Linq;
using System.Net;
using Microsoft;

namespace BooleanParser
{
	public class BingTranslationServiceClient
	{
		private const string _appIdKey = "";
		private Uri _serviceRootUri = new Uri("https://api.datamarket.azure.com/Bing/MicrosoftTranslator/");
		private TranslatorContainer _translatorContainer;

		public BingTranslationServiceClient()
		{
			_translatorContainer = new TranslatorContainer(_serviceRootUri) { Credentials = new NetworkCredential(_appIdKey, _appIdKey) };
		}

		public string Translate(string text, string from = "", string to = "en")
		{
			var lang = _translatorContainer.Detect(text);
			var detected = lang.FirstOrDefault();

			var result = _translatorContainer.Translate(text, to, detected.Code);

			foreach (var translation in result)
			{
				return translation.Text;
			}

			return "";
		}
	}
}
