using System.Windows.Forms;

namespace Common.FileUpload
{
	public class FileDialog : IDialogBrowser
	{
		//Otvara se prozor za odabir csv fajla za import podataka
		//Kao rezultat vraca se putanja do fajla
		public string GetDialogPathName()
		{
			using (OpenFileDialog fileDialog = new OpenFileDialog())
			{
				fileDialog.Filter = "Excel files (*.csv)|*.csv";

				DialogResult result = fileDialog.ShowDialog();

				if (result == DialogResult.OK)
				{
					return fileDialog.InitialDirectory + fileDialog.FileName;
				}
			}

			return string.Empty;
		}
	}
}