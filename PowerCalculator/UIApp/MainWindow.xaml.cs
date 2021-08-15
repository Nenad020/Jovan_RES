using Common.FileUpload;
using Common.Logging;
using ServiceEngine.ImportFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string message;
		private List<string> regions = new List<string>();

		private FileDialog fileDialog;
		private PowerImporter powerImporter;

		public MainWindow()
		{
			InitializeComponent();

			fileDialog = new FileDialog();
			powerImporter = new PowerImporter(fileDialog);
		}

		//Otvara se prozor za odabir csv fajla za prognoziranu potrosnju
		private void browseExpetedPower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			expetedPowerTextBlock.Text = fileDialog.GetDialogPathName();
		}

		//Otvara se prozor za odabir csv fajla za ostvarenu potrosnju
		private void browseActualPower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			actualPowerTextBlock.Text = fileDialog.GetDialogPathName();
		}

		//Validacija i primenjivanje podataka
		private void importButton_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			LogHelper.Log(Operation.Info, "Validating input .csv files");

			if (!ValidateImportFiles())
			{
				return;
			}

			powerImporter.CollectData(expetedPowerTextBlock.Text, actualPowerTextBlock.Text);
		}

		//Izracunava potrosnju i ispisuje na ekran
		private void calculatePower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
		}

		//Izracunati podaci se exportuju u XML
		private void exportToXML_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
		}

		//Proveravamo da li su uneti import fajlovi
		private bool ValidateImportFiles()
		{
			if (expetedPowerTextBlock.Text == string.Empty || actualPowerTextBlock.Text == string.Empty)
			{
				message = "Please select excel files first!";
				errorTextBlock.Text = message;
				LogHelper.Log(Operation.Error, message);

				return false;
			}

			return true;
		}

		//Resetujemo tekst za greske
		private void ResetErrorText()
		{
			errorTextBlock.Text = string.Empty;
		}
	}
}
