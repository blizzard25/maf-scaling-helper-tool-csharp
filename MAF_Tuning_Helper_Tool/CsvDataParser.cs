using System;
using System.Collections.Generic;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace MAF_Tuning_Helper_Tool
{
    public class CsvDataParser
    {
        private string filePath;
        public static List<double> mafVoltages = new List<double>{ .08, .16, .23, .31, .39, .47, .55, .63, .70, .78, .86, .94, 1.02, 1.09, 1.17, 1.25, 1.33, 1.41, 1.48, 1.56, 1.64, 1.72, 1.80, 1.88, 1.95, 2.03, 2.11, 2.19, 2.27, 2.34, 2.42, 2.50 };

        public CsvDataParser(string filePath)
        {
            this.filePath = filePath;
            ParseCsvFile();
        }

        private void ParseCsvFile()
        {
            var relevantHeaders = new List<string> { "MAS A/F -B1 (V)", "MAS A/F -B2 (V)", "A/F CORR-B1 (%)", "A/F CORR-B2 (%)" };
            var mafB1Volts = new List<double>();
            var mafB2Volts = new List<double>();
            var afCorrB1 = new List<double>();
            var afCorrB2 = new List<double>();
            using(CsvReader csvReader = new CsvReader(new StreamReader(filePath), true))
            {
                var headers = csvReader.GetFieldHeaders().ToList();
                var matchedHeaders = headers.Where(i => relevantHeaders.Contains(i)).Select(j => j).ToList();
                if(!matchedHeaders.Contains("MAS A/F -B1 (V)") && matchedHeaders.Contains("A/F CORR-B1 (%)")) matchedHeaders.Remove("A/F CORR-B1 (%)");
                else if (matchedHeaders.Contains("MAS A/F -B1 (V)") && !matchedHeaders.Contains("A/F CORR-B1 (%)")) matchedHeaders.Remove("MAS A/F -B1 (V)");
                else if (!matchedHeaders.Contains("MAS A/F -B2 (V)") && matchedHeaders.Contains("A/F CORR-B2 (%)")) matchedHeaders.Remove("A/F CORR-B2 (%)");
                else if (matchedHeaders.Contains("MAS A/F -B2 (V)") && !matchedHeaders.Contains("A/F CORR-B2 (%)")) matchedHeaders.Remove("MAS A/F -B2 (V)");
                var indeces = matchedHeaders.Select(j => headers.IndexOf(j)).ToList();
                while (csvReader.ReadNextRecord())
                {
                    foreach(int i in indeces)
                    {
                        switch (headers[i])
                        {
                            case "MAS A/F -B1 (V)":
                                mafB1Volts.Add(double.Parse(csvReader[i]));
                                break;
                            case "MAS A/F -B2 (V)":
                                mafB2Volts.Add(double.Parse(csvReader[i]));
                                break;
                            case "A/F CORR-B1 (%)":
                                afCorrB1.Add(double.Parse(csvReader[i]));
                                break;
                            case "A/F CORR-B2 (%)":
                                afCorrB2.Add(double.Parse(csvReader[i]));
                                break;
                        }
                    }
                }
                var bank1Data = new List<Tuple<double, double>>();
                var bank2Data = new List<Tuple<double, double>>();
                int index = 0;
                if (mafB1Volts.Count() > 0 && afCorrB1.Count() > 0)
                {
                    for(; index < mafB1Volts.Count(); index++) bank1Data.Add(new Tuple<double, double>(mafB1Volts[index], afCorrB1[index]));
                }
                index = 0;
                if(mafB2Volts.Count() > 0 && afCorrB2.Count() > 0)
                {
                    for(; index < mafB2Volts.Count(); index++) bank2Data.Add(new Tuple<double, double>(mafB2Volts[index], afCorrB2[index]));
                }
                if (bank1Data.Count() > 0)
                {
                    AddPlotDataSet(bank1Data);
                    SortDataToBinRanges(bank1Data);
                }
                if (bank2Data.Count() > 0)
                {
                    AddPlotDataSet(bank2Data);
                    SortDataToBinRanges(bank2Data);
                }
            }
            ShowDataDisplayForm();
        }

        private void SortDataToBinRanges(List<Tuple<double, double>> bankData)
        {
            bankData.Sort((a, b) => b.Item1.CompareTo(a.Item1));
        }

        private void AddPlotDataSet(List<Tuple<double, double>> bankData)
        {
            DataDisplayForm.plotDataSets.Add(bankData);
        }

        private void ShowDataDisplayForm()
        {
            DataDisplayForm ddf = new DataDisplayForm();
            ddf.Text = "Calculated Output";
            ddf.Show();
        }

    }
}
