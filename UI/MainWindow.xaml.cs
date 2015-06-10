using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BooleanParser;

namespace UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private BooleanParser.BingTranslationServiceClient _client;
		private const string _placeHolder = " Translating \'{0}\'...";

		public MainWindow()
		{
			InitializeComponent();
			_client = new BingTranslationServiceClient();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var text = InputBox.Text;
			Original.Items.Add(new ListBoxItem {Content = text});
			InputBox.Clear();
			var placeholder = string.Format(_placeHolder, text);
			Translated.Items.Add(placeholder);
			Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(
				() =>
					{
						var translated = _client.Translate(text);
						Translated.Items.Remove(placeholder);
						Translated.Items.Add(translated);
					})
			);
		}
	}
}
