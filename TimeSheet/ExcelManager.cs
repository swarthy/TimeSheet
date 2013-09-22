using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Drawing;
using System.Drawing;
using System.Windows.Forms;

namespace TimeSheetManager
{
    public static class ExcelManager
    {
        const int templateStartRow = 35;
        static int lastRow = 30;
        static int rowCount = 0;
        static int personalCount = 0;        
        static ExcelWorksheet worksheet;
        public static EventHandler OnProgress = null, OnSavingStart = null, OnExportEnd = null;
        public static void ExportContent(TimeSheetInstance timeSheet, string templatePath, string newFilePath)
        {
            WaitScreen waitScreen = new WaitScreen(true);
            FileInfo templateFile = new FileInfo(templatePath);
            FileInfo newFile = new FileInfo(newFilePath);
            lastRow = 30;
            rowCount = 0;
            personalCount = 0;            
            using (ExcelPackage package = new ExcelPackage(newFile, templateFile))
            {
                worksheet = package.Workbook.Worksheets.First();                
                #region Заполнение по маркерам
                worksheet.Workbook.Names["CommitDate"].Value = new DateTime(timeSheet.TS_Year, timeSheet.TS_Month, timeSheet._DaysInMonth).ToString("dd MMMM yyyy");
                worksheet.Workbook.Names["MainDoc_LPUName"].Value = "Главный врач " + timeSheet.Department.LPU.Name;
                worksheet.Workbook.Names["MainDoc"].Value = timeSheet.Department.LPU == null ? null : timeSheet.Department.LPU.MainDoc;
                worksheet.Workbook.Names["TimeSheetMonth"].Value = timeSheet._GetDate.ToString("MMMM");
                worksheet.Workbook.Names["TimeSheetYear"].Value = timeSheet._GetDate.ToString("yyyy") + "г.";
                worksheet.Workbook.Names["LPU_Name"].Value = timeSheet.Department.LPU.Name;
                worksheet.Workbook.Names["DepartmentName"].Value = timeSheet.Department.Name;
                var workedDays = Calendar.WorkedDaysInMonth(timeSheet.TS_Year, timeSheet.TS_Month);
                worksheet.Workbook.Names["CalendarDaysCount"].Value = workedDays;
                worksheet.Workbook.Names["CalendarDayString"].Value = Helper.MakeForCount(workedDays, "день", "дня", "дней");
                #endregion
                foreach (TimeSheet_Content content in timeSheet.Content)
                {
                    if (content.Days.Count == 0 && !content._IsPercentType)
                    {
                        var insertPos = lastRow;
                        AddRow();
                        worksheet.Cells[insertPos, 1].Value = ++personalCount;
                        worksheet.Cells[insertPos, 2].Value = string.Format("{0} - {1}", content.Personal._ShortName, content.Post.Name);
                        worksheet.Cells[insertPos, 3].Value = content.Personal.Table_Number;
                        worksheet.Cells[insertPos, 4].Value = content.Rate;
                        continue;
                    }
                    if (content._IsPercentType)
                    {
                        var insertPos = lastRow;
                        AddRow();
                        worksheet.Cells[insertPos, 1].Value = personalCount;
                        worksheet.Cells[insertPos, 2].Value = string.Format("{0} - {1}", content.Personal._ShortName, content.Post.Name);
                        worksheet.Cells[insertPos, 3].Value = content.Personal.Table_Number;
                        worksheet.Cells[insertPos, 4].Value = content._IsPercentType ? string.Format("{0}%", content.Percent) : content.Rate.ToString();
                        worksheet.Cells[insertPos, 5].Value = string.Format("Отработано {0}%. {1} {2}.", content.Percent, content.PercentDays, Helper.MakeForCount(content.PercentDays, "рабочий день", "рабочих дня", "рабочих дней"));
                        worksheet.Cells[string.Format("E{0}:S{1}", insertPos, insertPos + 1)].Merge = true;
                        continue;
                    }
                    var groups = content.Days.GroupBy(d => d.Item_Date);
                    var needRows = groups.Max(a => a.Count());

                    var topRowDays = groups.Where(ig => ig.Key.Day >= 1 && ig.Key.Day <= 15);
                    var bottomRowDays = groups.Where(ig => ig.Key.Day >= 16 && ig.Key.Day <= 31);

                    DayCounter[] daysCount = new DayCounter[needRows];
                    HoursCounter[] totalHoursCount = new HoursCounter[needRows];
                    HoursCounter[] nightHoursCount = new HoursCounter[needRows];
                    HoursCounter[] holydayHoursCount = new HoursCounter[needRows];

                    int rangeStart = lastRow;//начало

                    personalCount++;//п/п
                    for (int i = 0; i < needRows; i++)
                        AddRow();
                    #region Заполнение ПП, ФИО и Табельного номера
                    worksheet.Cells[rangeStart, 1].Value = personalCount;
                    worksheet.Cells[rangeStart, 2].Value = string.Format("{0} - {1}", content.Personal._ShortName, content.Post.Name);
                    worksheet.Cells[rangeStart, 3].Value = content.Personal.Table_Number;
                    worksheet.Cells[rangeStart, 4].Value = content._IsPercentType ? string.Format("{0}%", content.Percent) : content.Rate.ToString();
                    #endregion                    
                    #region Заполнение дней
                    #region Верхняя строка
                    foreach (var gr in topRowDays)
                    {
                        int col = gr.Key.Day + 4;
                        int i = 0;
                        foreach (var day in gr)
                        {
                            worksheet.Cells[rangeStart + i * 4, col].Value = day.Flag.Ru_Name;
                            if (day.Worked_Time.TotalHours == 0 && day.Flag.Name == "v")
                                worksheet.Cells[rangeStart + i * 4 + 1, col].Value = day.Flag.Ru_Name;
                            else
                                worksheet.Cells[rangeStart + i * 4 + 1, col].Value = Helper.FormatSpan(day.Worked_Time);
                            if (day.Flag.Name == "v")
                            {
                                worksheet.Cells[rangeStart + i * 4, col].Style.Font.Color.SetColor(Color.Red);
                                worksheet.Cells[rangeStart + i * 4 + 1, col].Style.Font.Color.SetColor(Color.Red);
                            }
                            switch (day.Flag.Name)
                            {
                                case "ya":
                                    daysCount[i].TopRow++;
                                    totalHoursCount[i].TopRow += day.Worked_Time.TotalHours;
                                    break;
                                case "s":
                                    totalHoursCount[i].TopRow += day.Worked_Time.TotalHours;
                                    break;
                                case "n":
                                    nightHoursCount[i].TopRow += day.Worked_Time.TotalHours;
                                    totalHoursCount[i].TopRow += day.Worked_Time.TotalHours;
                                    break;
                                case "rp":
                                    holydayHoursCount[i].TopRow += day.Worked_Time.TotalHours;
                                    totalHoursCount[i].TopRow += day.Worked_Time.TotalHours;
                                    break;
                                case "v":
                                    holydayHoursCount[i].TopRow += day.Worked_Time.TotalHours;
                                    totalHoursCount[i].TopRow += day.Worked_Time.TotalHours;
                                    break;
                            }
                            i++;
                        }
                    }
                    #endregion
                    #region Нижняя строка
                    foreach (var gr in bottomRowDays)
                    {
                        int col = gr.Key.Day - 15 + 4;
                        int i = 0;
                        foreach (var day in gr)
                        {
                            worksheet.Cells[rangeStart + i * 4 + 2, col].Value = day.Flag.Ru_Name;
                            if (day.Worked_Time.TotalHours == 0 && day.Flag.Name == "v")
                                worksheet.Cells[rangeStart + i * 4 + 3, col].Value = day.Flag.Ru_Name;
                            else
                                worksheet.Cells[rangeStart + i * 4 + 3, col].Value = Helper.FormatSpan(day.Worked_Time);
                            if (day.Flag.Name == "v")
                            {
                                worksheet.Cells[rangeStart + i * 4 + 2, col].Style.Font.Color.SetColor(Color.Red);
                                worksheet.Cells[rangeStart + i * 4 + 3, col].Style.Font.Color.SetColor(Color.Red);
                            }

                            switch (day.Flag.Name)
                            {
                                case "ya":
                                    daysCount[i].BottomRow++;
                                    totalHoursCount[i].BottomRow += day.Worked_Time.TotalHours;
                                    break;
                                case "s":
                                    totalHoursCount[i].BottomRow += day.Worked_Time.TotalHours;
                                    break;
                                case "n":
                                    nightHoursCount[i].BottomRow += day.Worked_Time.TotalHours;
                                    totalHoursCount[i].BottomRow += day.Worked_Time.TotalHours;
                                    break;
                                case "rp":
                                    holydayHoursCount[i].BottomRow += day.Worked_Time.TotalHours;
                                    totalHoursCount[i].BottomRow += day.Worked_Time.TotalHours;
                                    break;
                            }
                            i++;
                        }
                    }
                    for (int i = 0; i < needRows; i++)
                        for (int j = timeSheet._DaysInMonth + 1; j <= 31; j++)
                        {
                            worksheet.Cells[rangeStart + i * 4 + 3, j - 15 + 4].Value = "X";
                            worksheet.Cells[rangeStart + i * 4 + 4, j - 15 + 4].Value = "X";
                        }
                    #endregion
                    #endregion
                    #region Summary
                    for (int i = 0; i < needRows; i++)
                    {
                        worksheet.Cells[rangeStart + i * 4, 21].Value = daysCount[i].AllSum;
                        worksheet.Cells[rangeStart + i * 4 + 1, 21].Value = daysCount[i].TopRow;
                        worksheet.Cells[rangeStart + i * 4 + 3, 21].Value = daysCount[i].BottomRow;

                        worksheet.Cells[rangeStart + i * 4, 22].Value = totalHoursCount[i].AllSum;
                        worksheet.Cells[rangeStart + i * 4 + 1, 22].Value = totalHoursCount[i].TopRow;
                        worksheet.Cells[rangeStart + i * 4 + 3, 22].Value = totalHoursCount[i].BottomRow;

                        worksheet.Cells[rangeStart + i * 4, 23].Value = nightHoursCount[i].AllSum;
                        worksheet.Cells[rangeStart + i * 4 + 1, 23].Value = nightHoursCount[i].TopRow;
                        worksheet.Cells[rangeStart + i * 4 + 3, 23].Value = nightHoursCount[i].BottomRow;

                        worksheet.Cells[rangeStart + i * 4, 24].Value = holydayHoursCount[i].AllSum;
                        worksheet.Cells[rangeStart + i * 4 + 1, 24].Value = holydayHoursCount[i].TopRow;
                        worksheet.Cells[rangeStart + i * 4 + 3, 24].Value = holydayHoursCount[i].BottomRow;
                    }
                    #endregion
                    
                    if (OnProgress != null)
                        OnProgress(null, new ProgressEventArgs(personalCount));
                }
                worksheet.Cells[lastRow + 1, 15].Value = timeSheet.Department.DepartmentManager == null ? null : timeSheet.Department.DepartmentManager._ShortName;
                worksheet.Cells[lastRow + 3, 15].Value = timeSheet.User.Profile == null ? null : timeSheet.User.Profile._ShortName;
                template.Value = "";
                template.StyleName = "Normal";
                if (OnSavingStart != null)
                    OnSavingStart(null, EventArgs.Empty);
                package.Save();                
                if (OnExportEnd != null)
                    OnExportEnd(null, EventArgs.Empty);
            }
            waitScreen.Close();
        }
        static ExcelRange template
        {
            get
            {
                return worksheet.Cells[string.Format("A{0}:X{1}", templateStartRow + rowCount * 4, templateStartRow + rowCount * 4 + 3)];
            }
        }
        static void AddRow()
        {
            worksheet.InsertRow(lastRow, 4);
            rowCount++;
            template.Copy(worksheet.Cells[string.Format("A{0}:X{1}", lastRow, lastRow + 3)]);
            lastRow += 4;
        }
        struct DayCounter
        {
            public int TopRow;
            public int BottomRow;
            public int AllSum
            {
                get
                {
                    return TopRow + BottomRow;
                }
            }
        }
        struct HoursCounter
        {
            public double TopRow;
            public double BottomRow;
            public double AllSum
            {
                get
                {
                    return TopRow + BottomRow;
                }
            }
        }
    }
    public class ProgressEventArgs : EventArgs
    {
        public int Value { get; private set; }
        public ProgressEventArgs(int val)
            : base()
        {
            Value = val;
        }
    }
}
