using Common.FileUpload;
using Common.Helper;
using Common.Logging;
using DatabaseAccess;
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
		private HashSet<string> regions = new HashSet<string>();

		private DataTypeParser dataTypeParser;
		private SqliteDataAccess sqliteDataAccess;
		private FileDialog fileDialog;
		private PowerImporter powerImporter;

		public MainWindow()
		{
			InitializeComponent();

			dataTypeParser = new DataTypeParser();
			sqliteDataAccess = new SqliteDataAccess();
			fileDialog = new FileDialog(dataTypeParser);
			powerImporter = new PowerImporter(fileDialog, sqliteDataAccess);
		}

		//Otvara se prozor za odabir csv fajla za prognoziranu potrosnju
		private void browseExpetedPower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();

			LogHelper.Log(Operation.Info, "Browsing for expeted power consumption .csv file");
			expetedPowerTextBlock.Text = fileDialog.GetDialogPathName();
		}

		//Otvara se prozor za odabir csv fajla za ostvarenu potrosnju
		private void browseActualPower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();

			LogHelper.Log(Operation.Info, "Browsing for actual power consumption .csv file");
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

			try
			{
				powerImporter.CollectData(expetedPowerTextBlock.Text, actualPowerTextBlock.Text);
			}
			catch (Exception ex)
			{
				errorTextBlock.Text = ex.Message;
				LogHelper.Log(Operation.Error, ex.Message);
				return;
			}

			message = "You have successfully imported files in db!";
			LogHelper.Log(Operation.Info, message);
			WriteSuccessfullyMessage(message);
		}

		//Izracunava potrosnju i ispisuje na ekran
		private void calculatePower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
		
			//TODO
		}

		//Izracunati podaci se exportuju u XML
		private void exportToXML_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();

			//TODO
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

		//Ispis poruke na popup ekran
		private void WriteSuccessfullyMessage(string message)
		{
			MessageBox.Show(message);
		}
	}
}