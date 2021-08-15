using Common.FileUpload;
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
		IDialogBrowser fileBrowser = new FileDialog();

		public MainWindow()
		{
			InitializeComponent();
		}

		//Otvara se prozor za odabir csv fajla za prognoziranu potrosnju
		private void browseExpetedPower_Click(object sender, RoutedEventArgs e)
		{
			expetedPowerTextBlock.Text = fileBrowser.GetDialogPathName();
		}

		//Otvara se prozor za odabir csv fajla za ostvarenu potrosnju
		private void browseActualPower_Click(object sender, RoutedEventArgs e)
		{
			actualPowerTextBlock.Text = fileBrowser.GetDialogPathName();
		}

		//Validacija i primenjivanje podataka
		private void importButton_Click(object sender, RoutedEventArgs e)
		{

		}

		//Izracunati podaci se exportuju u XML
		private void exportToXML_Click(object sender, RoutedEventArgs e)
		{

		}

		//Izracunava potrosnju i ispisuje na ekran
		private void calculatePower_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
