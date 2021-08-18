using Common.FileUpload;
using Common.Helper;
using Common.Logging;
using Common.Model;
using DatabaseAccess;
using ServiceEngine;
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

		private OutputModel outputModel = new OutputModel();
		private List<string> regions = new List<string>();

		private DataTypeParser dataTypeParser;
		private SqliteDataAccess sqliteDataAccess;
		private FileDialog fileDialog;
		private PowerImporter powerImporter;
		private PowerConsumptionCalculator powerConsumptionCalculator;

		public MainWindow()
		{
			InitializeComponent();

			dataTypeParser = new DataTypeParser();
			sqliteDataAccess = new SqliteDataAccess();
			fileDialog = new FileDialog(dataTypeParser);
			powerImporter = new PowerImporter(fileDialog, sqliteDataAccess);
			powerConsumptionCalculator = new PowerConsumptionCalculator(sqliteDataAccess, dataTypeParser);
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
			LogHelper.Log(Operation.Info, "Calculating power consumption started!");

			if (!ValidatePowerConsumptionInputs())
			{
				return;
			}

			DateTime fromDate = dataTypeParser.ConvertDateTimeFromString(fromDatePicker.Text);
			DateTime toDate = dataTypeParser.ConvertDateTimeFromString(toDatePicker.Text);

			if (!CheckDatesValues(fromDate, toDate))
			{
				return;
			}

			try
			{
				outputModel = powerConsumptionCalculator.CalculatePower(fromDate, toDate, regionComboBox.Text);
			}
			catch (Exception ex)
			{
				errorTextBlock.Text = ex.Message;
				LogHelper.Log(Operation.Error, ex.Message);
				return;
			}

			WriteOutputModelOnScreen();

			message = "Calculating power consumption ended!";
			LogHelper.Log(Operation.Info, message);
			WriteSuccessfullyMessage(message);
		}

		//Izracunati podaci se exportuju u XML
		private void exportToXML_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			LogHelper.Log(Operation.Info, "Exporting result to XML!");

			if (!ValidateOutputTextBlock())
			{
				return;
			}

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

		//Proveravamo da li su uneti podaci za izracunavanje potrosnje snage (vreme i region)
		private bool ValidatePowerConsumptionInputs()
		{
			if (fromDatePicker.Text == string.Empty || toDatePicker.Text == string.Empty || regionComboBox.Text == string.Empty)
			{
				message = "Please select necessary inputs first!";
				errorTextBlock.Text = message;
				LogHelper.Log(Operation.Error, message);

				return false;
			}

			return true;
		}

		//Proveravamo da li je pocetno vreme manje od krajnjeg
		private bool CheckDatesValues(DateTime from, DateTime to)
		{
			if (from > to)
			{
				message = "Starting date needs to be lower then finished date!";
				errorTextBlock.Text = message;
				LogHelper.Log(Operation.Error, message);

				return false;
			}

			return true;
		}

		//Proveravamo da li postoji nesto na ekranu za ispis
		private bool ValidateOutputTextBlock()
		{
			if (outputTextBlock.Text == string.Empty)
			{
				message = "Output screen is empty, please calculate first power consumption then export it to XML!";
				errorTextBlock.Text = message;
				LogHelper.Log(Operation.Error, message);

				return false;
			}

			return true;
		}

		//Iz baze izvlacimo sve regione i osvezujemo listu na UI
		private void refreshRegions_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			LogHelper.Log(Operation.Info, "Refreshing regions combobox started!");

			regions.Clear();
			regions = sqliteDataAccess.LoadRegions("ExpetedConsumption");

			regionComboBox.ItemsSource = regions;
			LogHelper.Log(Operation.Info, "Refreshing regions combobox ended!");
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

		private void WriteOutputModelOnScreen()
		{
			outputTextBlock.Text = string.Empty;

			outputTextBlock.Text += "Result:\n";
			outputTextBlock.Text += $"Mean deviation is: {outputModel.MeanDeviation}\n";
			outputTextBlock.Text += $"Square deviation is: {outputModel.SquareDeviation}\n";

			foreach (var key in outputModel.CalculatedPowers)
			{
				outputTextBlock.Text += $"Calculated absolute average power consumption for date: {key.Key.ToString("dd/MM/yyyy")}\n";
				foreach (var value in key.Value)
				{
					outputTextBlock.Text += $"Hour {value.Hour}, value: {value.AbsoluteValue}\n";
				}
			}
		}
	}
}