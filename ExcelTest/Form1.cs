using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace ExcelTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateExcelFile(@"C:\Users\Administrator\Desktop\myExcel.xlsx");
        }

        private string getExcelFilePath(string fileName) 
        {
            return @System.Windows.Forms.Application.StartupPath+@"\"+fileName;
        }

        private bool isFileExists(string filePath) 
        {
            if (File.Exists(filePath))
                return true;
            else
                return false;
        }

        private void CreateExcelFile(string ExcelFilePath)
        {
            object Nothing = System.Reflection.Missing.Value;
            var app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = false;
            Microsoft.Office.Tools.Excel.Workbook workBook = (Microsoft.Office.Tools.Excel.Workbook)app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Tools.Excel.Worksheet worksheet = (Microsoft.Office.Tools.Excel.Worksheet)workBook.Sheets[1];
            //headline  
            worksheet.Cells[1, 1] = "时间";
            worksheet.Cells[1, 2] = "命令类型";
            worksheet.Cells[1, 3] = "测试对象";
            worksheet.Cells[1, 3] = "已发生对象";
            worksheet.Cells[1, 3] = "用时";
            worksheet.SaveAs(ExcelFilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing);
            workBook.Close(true, Type.Missing, Type.Missing);
            app.Quit();
        }

        private void WriteToExcel(string excelFilePath, string time, string cmdType, string testObject,string occurObject,string lastTime)
        {
            //open  
            object Nothing = System.Reflection.Missing.Value;
            var app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = false;
            Microsoft.Office.Interop.Excel.Workbook mybook = app.Workbooks.Open(excelFilePath, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);
            Microsoft.Office.Interop.Excel.Worksheet mysheet = (Microsoft.Office.Interop.Excel.Worksheet)mybook.Worksheets[1];
            //get activate sheet max row count  
            int maxrow = mysheet.UsedRange.Rows.Count + 1;
            mysheet.Cells[maxrow, 1] = time;
            mysheet.Cells[maxrow, 2] = cmdType;
            mysheet.Cells[maxrow, 3] = testObject;
            mysheet.Cells[maxrow, 4] = occurObject;
            mysheet.Cells[maxrow, 5] = lastTime;
            mybook.Save();
            mybook.Close(true, Type.Missing, Type.Missing);
            mybook = null;
            app.Quit();
        }  
    }
}
