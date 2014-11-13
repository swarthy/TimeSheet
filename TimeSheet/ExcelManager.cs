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
        const int templateRowCount = 2;
        static int lastRow = 31;
        static int rowCount = 0;
        static int personalCount = 0;
        static ExcelWorksheet worksheet;
        public static EventHandler OnProgress = null, OnSavingStart = null, OnExportEnd = null;
        public static void ExportContent(TimeSheetInstance timeSheet, string templatePath, string newFilePath)
        {
            WaitScreen waitScreen = new WaitScreen(true);
            FileInfo templateFile = new FileInfo(templatePath);
            FileInfo newFile = new FileInfo(newFilePath);
            lastRow = 31;
            rowCount = 0;
            personalCount = 0;

            using (ExcelPackage package = new ExcelPackage(newFile, templateFile))
            {
                worksheet = package.Workbook.Worksheets.First();
                #region Заполнение по маркерам
                worksheet.Workbook.Names["CommitDate1"].Value = worksheet.Workbook.Names["CommitDate2"].Value = new DateTime(timeSheet.TS_Year, timeSheet.TS_Month, timeSheet._DaysInMonth).ToString("dd MMMM yyyy");
                //worksheet.Workbook.Names["MainDoc_LPUName"].Value = "Главный врач " + timeSheet.Department.LPU.Name;
                worksheet.Workbook.Names["MainDoc"].Value = timeSheet.Department.LPU == null ? null : timeSheet.Department.LPU.MainDoc;
                worksheet.Workbook.Names["TimeSheetDate"].Value = string.Format("{0} г.", timeSheet._GetDate.ToString("MMMM yyyy"));
                //worksheet.Workbook.Names["TimeSheetMonth"].Value = timeSheet._GetDate.ToString("MMMM");
                //worksheet.Workbook.Names["TimeSheetYear"].Value = timeSheet._GetDate.ToString("yyyy");
                worksheet.Workbook.Names["LPU_Name"].Value = timeSheet.Department.LPU.Name;
                worksheet.Workbook.Names["DepartmentName"].Value = timeSheet.Department.Name;
                var workedDays = Calendar.WorkedDaysInMonth(timeSheet.TS_Year, timeSheet.TS_Month);
                worksheet.Workbook.Names["CalendarDaysCount"].Value = workedDays;
                //worksheet.Workbook.Names["CalendarDayString"].Value = Helper.MakeForCount(workedDays, "день", "дня", "дней");
                #endregion
                foreach (TimeSheet_Content content in timeSheet.Content)
                {
                    if (content.Days.Count == 0 && !content._IsPercentType)
                    {
                        var insertPos = lastRow;
                        AddRow();
                        worksheet.Cells[insertPos, 1].Value = ++personalCount;
                        worksheet.Cells[insertPos, 2].Value = content.Personal._FullName;//string.Format("{0} - {1}", content.Personal._ShortName, content.Post.Name);
                        //worksheet.Cells[insertPos, 3].Value = content.Personal.Table_Number;
                        //worksheet.Cells[insertPos, 10].Value = content.Rate;
                        worksheet.Cells[insertPos, 10].Value = content.Post.Name;
                        continue;
                    }
                    if (content._IsPercentType)
                    {
                        var insertPos = lastRow;
                        AddRow();
                        worksheet.Cells[insertPos, 1].Value = personalCount;
                        worksheet.Cells[insertPos, 2].Value = content.Personal._FullName;//string.Format("{0} - {1}", content.Personal._ShortName, content.Post.Name);
                        //worksheet.Cells[insertPos, 3].Value = content.Personal.Table_Number;
                        worksheet.Cells[insertPos, 10].Value = content.Post.Name;//content._IsPercentType ? string.Format("{0}%", content.Percent) : content.Rate.ToString();
                        worksheet.Cells[insertPos, 12].Value = string.Format("Отработано {0}%. {1} {2}.", content.Percent, content.PercentDays, Helper.MakeForCount(content.PercentDays, "рабочий день", "рабочих дня", "рабочих дней"));
                        worksheet.Cells[string.Format("M{0}:AQ{1}", insertPos, insertPos + templateRowCount - 1)].Merge = true;
                        continue;
                    }
                    var groups = content.Days.GroupBy(d => d.Item_Date);
                    var needRows = groups.Max(a => a.Count());

                    var daysCount = new int[needRows];
                    var totalHoursCount = new double[needRows];
                    var nightHoursCount = new double[needRows];
                    var holydayHoursCount = new double[needRows];

                    int rangeStart = lastRow;//начало

                    personalCount++;//п/п
                    for (int i = 0; i < needRows; i++)
                        AddRow();
                    #region Заполнение ПП, ФИО и Табельного номера
                    worksheet.Cells[rangeStart, 1].Value = personalCount;
                    worksheet.Cells[rangeStart, 2].Value = content.Personal._FullName;//string.Format("{0} - {1}", content.Personal._ShortName, content.Post.Name);
                    //worksheet.Cells[rangeStart, 3].Value = content.Personal.Table_Number;
                    worksheet.Cells[rangeStart, 10].Value = content.Post.Name;// content._IsPercentType ? string.Format("{0}%", content.Percent) : content.Rate.ToString();
                    #endregion
                    #region Заполнение дней
                    foreach (var gr in groups)
                    {
                        int col = gr.Key.Day + 12;
                        int i = 0;
                        foreach (var day in gr)
                        {
                            worksheet.Cells[rangeStart + i * templateRowCount, col].Value = day.Flag.Ru_Name;
                            if (day.Worked_Time.TotalHours == 0 && day.Flag.Name == "v")
                                worksheet.Cells[rangeStart + i * templateRowCount + 1, col].Value = day.Flag.Ru_Name;
                            else
                                worksheet.Cells[rangeStart + i * templateRowCount + 1, col].Value = Helper.FormatSpan(day.Worked_Time);
                            if (day.Flag.Name == "v")
                            {
                                worksheet.Cells[rangeStart + i * templateRowCount, col].Style.Font.Color.SetColor(Color.Red);
                                worksheet.Cells[rangeStart + i * templateRowCount + 1, col].Style.Font.Color.SetColor(Color.Red);
                            }
                            switch (day.Flag.Name)
                            {
                                case "ya":
                                    daysCount[i]++;
                                    totalHoursCount[i] += day.Worked_Time.TotalHours;
                                    break;
                                case "s":
                                    totalHoursCount[i] += day.Worked_Time.TotalHours;
                                    break;
                                case "n":
                                    nightHoursCount[i] += day.Worked_Time.TotalHours;
                                    totalHoursCount[i] += day.Worked_Time.TotalHours;
                                    break;
                                case "rp":
                                    holydayHoursCount[i] += day.Worked_Time.TotalHours;
                                    totalHoursCount[i] += day.Worked_Time.TotalHours;
                                    break;
                                case "v":
                                    holydayHoursCount[i] += day.Worked_Time.TotalHours;
                                    totalHoursCount[i] += day.Worked_Time.TotalHours;
                                    break;
                            }
                            i++;
                        }
                    }
                    #endregion

                    for (int i = 0; i < needRows; i++)
                        for (int j = timeSheet._DaysInMonth + 1; j <= 31; j++)
                        {
                            worksheet.Cells[rangeStart + i * templateRowCount, j + 12].Value = "X";
                            worksheet.Cells[rangeStart + i * templateRowCount + 1, j + 12].Value = "X";
                        }
                    //#endregion
                    //#endregion
                    #region Summary
                    for (int i = 0; i < needRows; i++)
                    {
                        worksheet.Cells[rangeStart + i * templateRowCount, 45].Value = daysCount[i];

                        worksheet.Cells[rangeStart + i * templateRowCount, 46].Value = totalHoursCount[i];

                        worksheet.Cells[rangeStart + i * templateRowCount, 47].Value = nightHoursCount[i];

                        worksheet.Cells[rangeStart + i * templateRowCount, 48].Value = holydayHoursCount[i];

                        worksheet.Cells[rangeStart + i * templateRowCount, 49].Value = content.Personal.Table_Number;

                    }
                    #endregion

                    if (OnProgress != null)
                        OnProgress(null, new ProgressEventArgs(personalCount));
                }
                //worksheet.Cells[lastRow + 1, 15].Value = timeSheet.Department.DepartmentManager == null ? null : timeSheet.Department.DepartmentManager._ShortName;
                worksheet.Cells[lastRow + 2, 5].Value = timeSheet.User.Profile.MainPost == null ? null : timeSheet.User.Profile.MainPost.Name;
                worksheet.Cells[lastRow + 2, 20].Value = timeSheet.User.Profile == null ? null : timeSheet.User.Profile._ShortName;
                worksheet.Cells[lastRow + 2, 33].Value = new DateTime(timeSheet.TS_Year, timeSheet.TS_Month, timeSheet._DaysInMonth).ToString("dd MMMM yyyy");
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
                return worksheet.Cells[string.Format("A{0}:AW{1}", templateStartRow + rowCount * templateRowCount, templateStartRow + rowCount * templateRowCount + templateRowCount - 1)];
            }
        }
        static void AddRow()
        {
            worksheet.InsertRow(lastRow, templateRowCount);
            rowCount++;
            template.Copy(worksheet.Cells[string.Format("A{0}:AW{1}", lastRow, lastRow + templateRowCount - 1)]);
            lastRow += templateRowCount;
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
