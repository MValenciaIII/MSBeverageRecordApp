
using System.IO;
using System.Windows.Controls;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Win32;
using static System.Net.WebRequestMethods;
using static MSBeverageRecordApp.Reports;


//order filter cases to match filter data
//grid printing offset
//change order didn't work in add/write row functions
//order works on print but not in save file, revert and test, databinding should handle 

namespace MSBeverageRecordApp {
    internal class SaveReport {
        public void ExportToPdf(RootObject obj, DataGrid dataGrid, string title, string filter) {
            // Prompt the user to select a file location for saving the PDF
            string filePath = GetSaveFilePath("PDF Files (*.pdf)|*.pdf|All files (*.*)|*.*");
            if (string.IsNullOrEmpty(filePath)) return;

            // Create a new PDF document
            using (var pdfWriter = new PdfWriter(filePath))
            using (var pdfDocument = new PdfDocument(pdfWriter))
            using (var document = new Document(pdfDocument)) {


                // Set up custom fonts
                PdfFont regularFont = PdfFontFactory.CreateFont();
                PdfFont boldFont = PdfFontFactory.CreateFont();

                // Add title to the top of the page with custom font and color
                Paragraph titleParagraph = new Paragraph(title)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFont(boldFont)
                    .SetFontSize(20)
                    .SetFontColor(DeviceGray.BLACK);
                    document.Add(titleParagraph);

                
                // Create a table to hold the data from the DataGrid
                var table = new Table(dataGrid.Columns.Count);

                // Add column headers to the table with formatting
                //add case to only add relavent headers
                //try changing to for to check
                foreach (var column in dataGrid.Columns) {
                Cell headerCell = new Cell().Add(new Paragraph((column as DataGridColumn).Header.ToString())
                    .SetFont(boldFont)
                    .SetFontSize(12))
                    .SetBackgroundColor(new DeviceRgb(192, 192, 192))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

                    // Add bottom border to the header cell
                    headerCell.SetBorder(new iText.Layout.Borders.SolidBorder(new DeviceRgb(0, 0, 0), 1f));

                    table.AddHeaderCell(headerCell);

                }//end for


                // Add rows to the table with formatting based on the filter

                AddDataRows(obj, table, regularFont, filter);

                // Add the table to the PDF document
                document.Add(table);
            }
        }

        public void ExportToCsv(RootObject obj, DataGrid dataGrid, string title, string filter) {
            // Prompt the user to select a file location for saving the CSV
            string filePath = GetSaveFilePath("CSV Files (*.csv)|*.csv|All files (*.*)|*.*");
            
            if (string.IsNullOrEmpty(filePath)) return;

            using (StreamWriter sw = new StreamWriter(filePath)) {
                // Write title to the CSV file
                sw.WriteLine(title);

                // Write column headers to the CSV file
                foreach (var column in dataGrid.Columns) {
                    sw.Write((column as DataGridColumn).Header.ToString() + ",");
                }
                sw.WriteLine();

                // Write data rows to the CSV file based on the filter

                        WriteDataRows(obj, sw, filter);
                      
            }
        }

        private string GetSaveFilePath(string filter) {
            // Prompt the user to select a file location for saving
            string filePath = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter;
            if (saveFileDialog.ShowDialog() == true) {
                filePath = saveFileDialog.FileName;
            }
            return filePath;
        }

        private void AddDataRows(RootObject obj, Table table, PdfFont font, string title) {

            switch (title) {
                case "allData":
                    foreach (var item in obj.Items) {
                        table.AddCell(CreateCell(item.record_id.ToString(), font));
                        table.AddCell(CreateCell(item.categoryName.ToString(), font));
                        table.AddCell(CreateCell(item.companyName.ToString(), font));
                        table.AddCell(CreateCell(item.model.ToString(), font));
                        table.AddCell(CreateCell(item.serial.ToString(), font));
                        table.AddCell(CreateCell(item.purchase_date.ToString(), font));
                        table.AddCell(CreateCell(item.cost.ToString(), font));
                        table.AddCell(CreateCell(item.locationName.ToString(), font));
                        table.AddCell(CreateCell(item.sub_location.ToString(), font));
                    }
                    break;
                case "category":
                    foreach (var item in obj.Items) {
                        //table.AddCell(CreateCell(item.record_id.ToString(), font));
                        table.AddCell(CreateCell(item.categoryName.ToString(), font));
                        table.AddCell(CreateCell(item.companyName.ToString(), font));
                        table.AddCell(CreateCell(item.model.ToString(), font));
                        table.AddCell(CreateCell(item.serial.ToString(), font));
                        //table.AddCell(CreateCell(item.purchase_date.ToString(), font));
                        //table.AddCell(CreateCell(item.cost.ToString(), font));
                        table.AddCell(CreateCell(item.locationName.ToString(), font));
                        table.AddCell(CreateCell(item.sub_location.ToString(), font));
                    }
                    break;
                case "manufacturer":
                    foreach (var item in obj.Items) {
                        //table.AddCell(CreateCell(item.record_id.ToString(), font));
                        //table.AddCell(CreateCell(item.categoryName.ToString(), font));
                        table.AddCell(CreateCell(item.companyName.ToString(), font));
                        table.AddCell(CreateCell(item.model.ToString(), font));
                        table.AddCell(CreateCell(item.serial.ToString(), font));
                        //table.AddCell(CreateCell(item.purchase_date.ToString(), font));
                        //table.AddCell(CreateCell(item.cost.ToString(), font));
                        table.AddCell(CreateCell(item.locationName.ToString(), font));
                        table.AddCell(CreateCell(item.sub_location.ToString(), font));
                    }
                    break;
                case "location"://
                    foreach (var item in obj.Items) {
                        //table.AddCell(CreateCell(item.record_id.ToString(), font));
                        //table.AddCell(CreateCell(item.categoryName.ToString(), font));
                        table.AddCell(CreateCell(item.locationName.ToString(), font));
                        table.AddCell(CreateCell(item.sub_location.ToString(), font));
                        table.AddCell(CreateCell(item.companyName.ToString(), font));
                        table.AddCell(CreateCell(item.model.ToString(), font));
                        table.AddCell(CreateCell(item.serial.ToString(), font));
                        //table.AddCell(CreateCell(item.purchase_date.ToString(), font));
                        //table.AddCell(CreateCell(item.cost.ToString(), font));

                    }
                    break;
                case "totalValue":
                    foreach (var item in obj.Items) {

                        //table.AddCell(CreateCell(item.record_id.ToString(), font));
                        table.AddCell(CreateCell(item.categoryName.ToString(), font));
                        //table.AddCell(CreateCell(item.companyName.ToString(), font));
                        //table.AddCell(CreateCell(item.model.ToString(), font));
                        //table.AddCell(CreateCell(item.serial.ToString(), font));
                        //table.AddCell(CreateCell(item.purchase_date.ToString(), font));
                        table.AddCell(CreateCell(item.cost.ToString(), font));
                        //table.AddCell(CreateCell(item.locationName.ToString(), font));
                        //table.AddCell(CreateCell(item.sub_location.ToString(), font));

                    }
                    break;

            }


        }



        private Cell CreateCell(string content, PdfFont font) {
            return new Cell().Add(new Paragraph(content)
                .SetFont(font)
                .SetFontSize(10))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new iText.Layout.Borders.SolidBorder(new DeviceRgb(0, 0, 0), 1f));
        }

        // Implement similar methods for other filters: AddCategoryRows, AddManufacturerRows, AddLocationRows, AddTotalValueRows

        private void WriteDataRows(RootObject obj, StreamWriter sw, string filter) {


            // Write data rows to the CSV file based on the filter
            switch (filter) {
                case "allData":
                    foreach (var item in obj.Items) {
                        sw.Write(GetStringValue(item.record_id) + ",");
                        sw.Write(GetStringValue(item.categoryName) + ",");
                        sw.Write(GetStringValue(item.companyName) + ",");
                        sw.Write(GetStringValue(item.model) + ",");
                        sw.Write(GetStringValue(item.serial) + ",");
                        sw.Write(GetStringValue(item.purchase_date) + ",");
                        sw.Write(GetStringValue(item.cost) + ",");
                        sw.Write(GetStringValue(item.locationName) + ",");
                        sw.WriteLine(GetStringValue(item.sub_location));
                    }
                    break;
                case "category":
                    foreach (var item in obj.Items) {
                        //sw.Write(GetStringValue(item.record_id) + ",");
                        sw.Write(GetStringValue(item.categoryName) + ",");
                        sw.Write(GetStringValue(item.companyName) + ",");
                        sw.Write(GetStringValue(item.model) + ",");
                        sw.Write(GetStringValue(item.serial) + ",");
                        //sw.Write(GetStringValue(item.purchase_date) + ",");
                        //sw.Write(GetStringValue(item.cost) + ",");
                        sw.Write(GetStringValue(item.locationName) + ",");
                        sw.WriteLine(GetStringValue(item.sub_location));
                    }
                    break;
                case "manufacturer":
                    foreach (var item in obj.Items) {
                        //sw.Write(GetStringValue(item.record_id) + ",");
                        //sw.Write(GetStringValue(item.categoryName) + ",");
                        sw.Write(GetStringValue(item.companyName) + ",");
                        sw.Write(GetStringValue(item.model) + ",");
                        sw.Write(GetStringValue(item.serial) + ",");
                        //sw.Write(GetStringValue(item.purchase_date) + ",");
                        //sw.Write(GetStringValue(item.cost) + ",");
                        sw.Write(GetStringValue(item.locationName) + ",");
                        sw.WriteLine(GetStringValue(item.sub_location));
                    }
                    break;
                case "location":
                    foreach (var item in obj.Items) {

                        //sw.Write(GetStringValue(item.record_id) + ",");
                        //sw.Write(GetStringValue(item.categoryName) + ",");
                        sw.Write(GetStringValue(item.locationName) + ",");
                        sw.Write(GetStringValue(item.sub_location) + ",");
                        sw.Write(GetStringValue(item.companyName) + ",");
                        sw.Write(GetStringValue(item.model) + ",");
                        sw.WriteLine(GetStringValue(item.serial));
                        //sw.Write(GetStringValue(item.purchase_date) + ",");
                        //sw.Write(GetStringValue(item.cost) + ",");
                    }
                    break;
                case "totalValue":
                    foreach (var item in obj.Items) {
                        //sw.Write(GetStringValue(item.record_id) + ",");
                        sw.Write(GetStringValue(item.categoryName) + ",");
                        sw.WriteLine(GetStringValue(item.cost) + ",");
                        //sw.Write(GetStringValue(item.companyName) + ",");
                        //sw.Write(GetStringValue(item.model) + ",");
                        //sw.Write(GetStringValue(item.serial) + ",");
                        //sw.Write(GetStringValue(item.purchase_date) + ",");
                        //sw.Write(GetStringValue(item.locationName) + ",");
                        //sw.WriteLine(GetStringValue(item.sub_location));
                    }
                    break;
            }


        }//em

        private string GetStringValue(object value) {
            return value != null ? value.ToString() : "";
        }

    }
}
