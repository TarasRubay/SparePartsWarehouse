using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsWarehouse
{
    internal class ExcelReader
    {
        private string excelFile { get; set; }

        public ExcelReader(string Path)
        {
            excelFile = Path;
        }
        /// <summary>
        /// Index of table
        /// 0 - Двигуни 
        /// 1 - Датчики, перетворювачі, нагрівачі
        /// 2 - Частотники та блоки живлення
        /// 3 - Пневматика (фітінги) Для замовлення
        /// 4 - Модулі
        /// </summary>
        /// <param name="numberOfPage"></param>
        /// <returns></returns>
        public List<List<object>> read(int numberOfPage)
        {
            
            List<List<object>> allRowsList = new();
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = System.IO.File.Open(excelFile, FileMode.Open, FileAccess.Read))
                {
                    //var passConfig = new ExcelReaderConfiguration { Password = "1111" };
                    //IExcelDataReader excelDataReader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream,passConfig);
                    IExcelDataReader excelDataReader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);
                    var conf = new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = a => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };
                    DataSet dataSet = excelDataReader.AsDataSet(conf);
                        try
                        {
                            DataRowCollection row = dataSet.Tables[numberOfPage].Rows;
                        int indexColum = 0;
                        foreach (var item in dataSet.Tables[numberOfPage].Columns)
                        {
                            if (item.ToString().ToLower().Contains("column"))
                            {
                                break;
                            }
                            indexColum++;
                        }  
                        foreach (DataRow item in row)
                            {
                                if (item is not null)
                                {
                                    List<object> rowDataList = new();
                                    for (int i = 0; i < indexColum; i++)
                                    {
                                        rowDataList.Add(item.ItemArray[i]); //list of each rows
                                    }
                                        allRowsList.Add(rowDataList); //adding the above list of each row to another list
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);

                            //BagReport.SetBagAndSave($"{e.Message} read penunlimate", Program.pathBagJSON);
                        }
                    }
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //BagReport.SetBagAndSave($"{e.Message}  readX", Program.pathBagJSON);
            }
            return allRowsList;
        }
    }
}
