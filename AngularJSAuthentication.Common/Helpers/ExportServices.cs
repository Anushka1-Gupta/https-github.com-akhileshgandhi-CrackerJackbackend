using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace AngularJSAuthentication.Common.Helpers
{
    public class ExportServices
    {

        public static void DataSet_To_Excel(DataSet dataSet, string pFilePath)
        {
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                IWorkbook workbook = null;
                ISheet worksheet = null;
                string Ext = System.IO.Path.GetExtension(pFilePath); //<-Extension del archivo
                switch (Ext.ToLower())
                {
                    case ".xls":
                        HSSFWorkbook workbookH = new HSSFWorkbook();
                        NPOI.HPSF.DocumentSummaryInformation dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                        dsi.Company = "ERP"; dsi.Manager = "IT Department";
                        workbookH.DocumentSummaryInformation = dsi;
                        workbook = workbookH;
                        break;

                    case ".xlsx": workbook = new XSSFWorkbook(); break;
                }
                using (FileStream stream = new FileStream(pFilePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    foreach (DataTable pDatos in dataSet.Tables)
                    {
                        try
                        {
                            if (pDatos != null && pDatos.Rows.Count > 0)
                            {



                                worksheet = workbook.CreateSheet(pDatos.TableName); //<-Usa el nombre de la tabla como nombre de la Hoja


                                if (pDatos.Columns.Count > 0)
                                {
                                    int index = 0;
                                    foreach (var col in pDatos.Columns)
                                    {
                                        worksheet.SetColumnWidth(index++, (int)((22 + 0.72) * 256));
                                    }
                                }


                                //CREAR EN LA PRIMERA FILA LOS TITULOS DE LAS COLUMNAS
                                int iRow = 0;
                                if (pDatos.Columns.Count > 0)
                                {
                                    int iCol = 0;
                                    IRow fila = worksheet.CreateRow(iRow);
                                    foreach (DataColumn columna in pDatos.Columns)
                                    {
                                        ICell cell = fila.CreateCell(iCol, CellType.String);
                                        cell.SetCellValue(columna.ColumnName);
                                        iCol++;
                                    }
                                    iRow++;
                                }

                                //FORMATOS PARA CIERTOS TIPOS DE DATOS
                                ICellStyle _doubleCellStyle = workbook.CreateCellStyle();
                                _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.###");

                                ICellStyle _intCellStyle = workbook.CreateCellStyle();
                                _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0");

                                ICellStyle _boolCellStyle = workbook.CreateCellStyle();
                                _boolCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("BOOLEAN");

                                ICellStyle _dateCellStyle = workbook.CreateCellStyle();
                                _dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-MM-yyyy");

                                ICellStyle _dateTimeCellStyle = workbook.CreateCellStyle();
                                _dateTimeCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-MM-yyyy HH:mm:ss");

                                //AHORA CREAR UNA FILA POR CADA REGISTRO DE LA TABLA
                                foreach (DataRow row in pDatos.Rows)
                                {
                                    IRow fila = worksheet.CreateRow(iRow);
                                    int iCol = 0;
                                    foreach (DataColumn column in pDatos.Columns)
                                    {
                                        ICell cell = null; //<-Representa la celda actual                               
                                        object cellValue = row[iCol]; //<- El valor actual de la celda

                                        switch (column.DataType.ToString())
                                        {
                                            case "System.Boolean":
                                                if (cellValue != DBNull.Value)
                                                {
                                                    cell = fila.CreateCell(iCol, CellType.Boolean);

                                                    if (Convert.ToBoolean(cellValue)) { cell.SetCellFormula("TRUE()"); }
                                                    else { cell.SetCellFormula("FALSE()"); }

                                                    cell.CellStyle = _boolCellStyle;
                                                }
                                                break;

                                            case "System.String":
                                                if (cellValue != DBNull.Value)
                                                {
                                                    cell = fila.CreateCell(iCol, CellType.String);
                                                    cell.SetCellValue(Convert.ToString(cellValue));
                                                }
                                                break;

                                            case "System.Int32":
                                                if (cellValue != DBNull.Value)
                                                {
                                                    cell = fila.CreateCell(iCol, CellType.Numeric);
                                                    cell.SetCellValue(Convert.ToInt32(cellValue));
                                                    cell.CellStyle = _intCellStyle;
                                                }
                                                break;
                                            case "System.Int64":
                                                if (cellValue != DBNull.Value)
                                                {
                                                    cell = fila.CreateCell(iCol, CellType.Numeric);
                                                    cell.SetCellValue(Convert.ToInt64(cellValue));
                                                    cell.CellStyle = _intCellStyle;
                                                }
                                                break;
                                            case "System.Decimal":
                                                if (cellValue != DBNull.Value)
                                                {
                                                    cell = fila.CreateCell(iCol, CellType.Numeric);
                                                    cell.SetCellValue(Convert.ToDouble(cellValue));
                                                    cell.CellStyle = _doubleCellStyle;
                                                }
                                                break;
                                            case "System.Double":
                                                if (cellValue != DBNull.Value)
                                                {
                                                    cell = fila.CreateCell(iCol, CellType.Numeric);
                                                    cell.SetCellValue(Convert.ToDouble(cellValue));
                                                    cell.CellStyle = _doubleCellStyle;
                                                }
                                                break;

                                            case "System.DateTime":
                                                if (cellValue != DBNull.Value)
                                                {
                                                    cell = fila.CreateCell(iCol, CellType.Numeric);
                                                    cell.SetCellValue(Convert.ToDateTime(cellValue));

                                                    //Si No tiene valor de Hora, usar formato dd-MM-yyyy
                                                    DateTime cDate = Convert.ToDateTime(cellValue);
                                                    if (cDate != null && cDate.Hour > 0) { cell.CellStyle = _dateTimeCellStyle; }
                                                    else { cell.CellStyle = _dateCellStyle; }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                        iCol++;
                                    }
                                    iRow++;
                                }



                            }
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }


                    workbook.Write(stream);
                    stream.Close();
                }
            }
        }
       
    }

    public class ListtoDataTableConverter
    {

        public static DataTable ToDataTable<T>(List<T> items)

        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)

            {

                //Setting column names as Property names

                dataTable.Columns.Add(prop.Name);

            }

            foreach (T item in items)

            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)

                {

                    //inserting property values to datatable rows

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

            //put a breakpoint here and check datatable

            return dataTable;

        }

        public static string CamelCase(string str)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
            str = cultInfo.ToTitleCase(str);
            str = str.Replace(" ", "");
            return str;
        }

    }
}
