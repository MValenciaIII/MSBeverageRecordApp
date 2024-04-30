using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Reflection;
using ChoETL;
using static MSBeverageRecordApp.Reports;
using System.Linq.Expressions;

public class PrintDG {

    //globals
    TableRowGroup tableRowGroup = new TableRowGroup();
    decimal totalCost = 0.0m;
    public void printDG(RootObject obj, DataGrid dataGrid, string title, string filter) {

        PrintDialog printDialog = new PrintDialog();

        if (printDialog.ShowDialog() == true) {
            FlowDocument fd = new FlowDocument();

            Paragraph p = new Paragraph(new Run(title));
            p.FontStyle = dataGrid.FontStyle;
            p.FontFamily = dataGrid.FontFamily;
            p.FontSize = 12;
            p.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(p);

            System.Windows.Documents.Table table = new Table();
            TableRow r = new TableRow();
            fd.PageWidth = printDialog.PrintableAreaWidth;
            fd.PageHeight = printDialog.PrintableAreaHeight;
            fd.BringIntoView();

            fd.TextAlignment = TextAlignment.Center;
            fd.ColumnWidth = 500;
            table.CellSpacing = 0;

            var headerList = dataGrid.Columns.Select(e => e.Header.ToString()).ToList();

            //fix header names
            for (int j = 0; j < headerList.Count; j++) {

                r.Cells.Add(new TableCell(new Paragraph(new Run(headerList[j]))));
                r.Cells[j].ColumnSpan = 4;
                r.Cells[j].Padding = new Thickness(4);

                r.Cells[j].BorderBrush = Brushes.Black;
                r.Cells[j].FontWeight = FontWeights.Bold;
                r.Cells[j].Background = Brushes.DarkGray;
                r.Cells[j].Foreground = Brushes.White;
                r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);
            }
            tableRowGroup.Rows.Add(r);
            table.RowGroups.Add(tableRowGroup);
            Records rep = new Records();

            table.BorderBrush = Brushes.Gray;
            table.BorderThickness = new Thickness(1, 1, 0, 0);
            table.FontStyle = dataGrid.FontStyle;
            table.FontFamily = dataGrid.FontFamily;
            table.FontSize = 13;
            tableRowGroup = new TableRowGroup();

            int cellNumber = 0;

            //check filter arg to choose which grid to print
            //set title in reports.cs

            AddDataRows(obj, r, cellNumber, filter);


            table.RowGroups.Add(tableRowGroup);
            fd.Blocks.Add(table);


            printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");
        }
    }//em

    //print on filter tabs, cut columns depending on filter
    public void AddDataRows(RootObject obj, TableRow r, int cellNumber, string filter) {


        switch (filter) {
            case "allData":
                for (int j = 0; j < obj.Items.Count; j++) {

                    totalCost += obj.Items[j].cost;
                    r = new TableRow();
                    cellNumber = 0;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].record_id.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].categoryName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].companyName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;
                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].model.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;
                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].serial.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;
                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].purchase_date.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;
                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].cost.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;
                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].locationName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;
                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].sub_location.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);

                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;
                    tableRowGroup.Rows.Add(r);
                }



                break;
            case "category":
                for (int j = 0; j < obj.Items.Count; j++) {

                    totalCost += obj.Items[j].cost;
                    r = new TableRow();
                    cellNumber = 0;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].categoryName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].companyName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].model.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].serial.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].locationName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].sub_location.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    tableRowGroup.Rows.Add(r);
                }

                break;
            case "manufacturer":

                for (int j = 0; j < obj.Items.Count; j++) {

                    totalCost += obj.Items[j].cost;
                    r = new TableRow();
                    cellNumber = 0;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].companyName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].model.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].serial.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].locationName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].sub_location.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    tableRowGroup.Rows.Add(r);
                }

                break;
            case "location":

                for (int j = 0; j < obj.Items.Count; j++) {

                    totalCost += obj.Items[j].cost;
                    r = new TableRow();
                    cellNumber = 0;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].locationName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].sub_location.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].companyName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].model.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].serial.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    tableRowGroup.Rows.Add(r);
                }

                break;
            case "totalValue":

                for (int j = 0; j < obj.Items.Count; j++) {

                    totalCost += obj.Items[j].cost;
                    r = new TableRow();
                    cellNumber = 0;

                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].categoryName.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;


                    r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].cost.ToString()))));
                    r.Cells[cellNumber].ColumnSpan = 4;
                    r.Cells[cellNumber].Padding = new Thickness(4);
                    r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                    r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                    cellNumber++;

                    tableRowGroup.Rows.Add(r);
                }

                break;
        }



    }//ef




}//ec


