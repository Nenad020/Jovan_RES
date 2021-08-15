﻿using System;
using System.IO;

namespace Common.Logging
{
	/// <summary>
	/// Klasa koja nasledjuje LogBase i sluzi za logovanje
	/// </summary>
	public class Logger : LogBase
	{
		/// <summary>
		/// Metoda za upisavanje linije loga u neki fajl
		/// </summary>
		public override void Log(Operation operation, string msg)
		{
			//Relativna putanja fajla u kome se upisuje promene
			string path = $"../../../../Logs/log.txt";

			lock (lockObj)
			{
				//Koristimo StreamWriter za upisivanje poruke u fajl
				//Parametri su putanja i bool koji je true, taj bool je da li hocemo da upisemo poruku na kraj reda ili
				//da pravimo novi fajl tj brise se sve i upisuje samo ova poruka
				using (StreamWriter streamWriter = new StreamWriter(path, true))
				{
					//Na postojecu poruku samo dodajemo vreme i to upisujemo
					string modifiedMessage = $"Type: {operation}, Time: {DateTime.Now}, Message: {msg}";
					streamWriter.WriteLine(modifiedMessage);
					streamWriter.Close();
				}
			}
		}
	}
}