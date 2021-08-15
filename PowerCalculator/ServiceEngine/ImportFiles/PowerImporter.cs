using Common.FileUpload;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEngine.ImportFiles
{
	public class PowerImporter
	{
		private FileDialog fileDialog;

		public PowerImporter(FileDialog fileDialog)
		{
			this.fileDialog = fileDialog;
		}

		public void CollectData(string expetedPowerPath, string actualPowerPath)
		{
			List<PowerRecord> expetedPowerImporters = fileDialog.Load(expetedPowerPath);
			List<PowerRecord> actualPowerImporters = fileDialog.Load(actualPowerPath);
		}
	}
}
