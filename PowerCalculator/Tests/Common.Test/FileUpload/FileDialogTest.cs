using Common.FileUpload;
using Common.Helper.Interfaces;
using Common.Model;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Common.Test.FileUpload
{
	public class FileDialogTest
	{
		private IDataTypeParser dataTypeParser;

		private FileDialog fileDialog;

		[SetUp]
		public void SetUp()
		{
			dataTypeParser = Substitute.For<IDataTypeParser>();

			fileDialog = new FileDialog(dataTypeParser);
		}

		[Test]
		public void Load_TryToParseDataFromFile_ReturnsException()
		{
			Exception ex = Assert.Throws<Exception>(() => fileDialog.Load("18al32../"));

			Assert.That(ex.Message == "Csv files aren't loaded successfully");
		}

		[Test]
		public void Load_TryToParseDataFromFile_CallsTheMethod()
		{
			string path = @"D:\Projekti\RES\Jovan_RES\ImportFiles\ostv_2020_05_07.csv";

			List<PowerRecord> actual = fileDialog.Load(path);

			dataTypeParser.
				Received().
				GetDateFromFileName(path);
		}

		[Test]
		public void SaveToXML_TryToSaveDataToXML_ReturnsException()
		{
			Exception ex = Assert.Throws<Exception>(() => fileDialog.SaveToXML(null, ""));

			Assert.That(ex.Message == "Exporting result to xml has failed!");
		}

		[Test]
		public void SaveToXML_TryToSaveDataToXML_ReturnsVoid()
		{
			string path = @"D:\Projekti\RES\Jovan_RES\output.xml";

			fileDialog.SaveToXML(new OutputModel(), path);
		}
	}
}