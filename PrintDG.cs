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
using iText.Kernel.Font;

public class PrintDG {

    //globals
    TableRowGroup tableRowGroup = new TableRowGroup();
    decimal totalCost = 0.0m;
    public void printDG(RootObject obj, DataGrid dataGrid, string title, string filter) {
        //set title in call from reports class
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



            table.BorderBrush = Brushes.Gray;
            table.BorderThickness = new Thickness(1, 1, 0, 0);
            table.FontStyle = dataGrid.FontStyle;
            table.FontFamily = dataGrid.FontFamily;
            table.FontSize = 13;
            

            int cellNumber = 0;
            //check filter arg to choose which grid to print
            //set title in reports.cs

            AddHeaders(table, dataGrid);
            AddDataRows(obj, r, cellNumber, filter);

            table.RowGroups.Add(tableRowGroup);
            AddTotalCostRow(table, CalculateTotalCost(obj, filter));
            fd.Blocks.Add(table);


            printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");
        }
    }//em


    private void AddHeaders(System.Windows.Documents.Table table, DataGrid dataGrid) {
        TableRowGroup tableRowGroup = new TableRowGroup();
        TableRow headerRow = new TableRow();

        foreach (DataGridColumn column in dataGrid.Columns) {
            TableCell cell = new TableCell(new Paragraph(new Run(column.Header.ToString())));
            cell.ColumnSpan = 4;
            cell.Padding = new Thickness(4);
            cell.BorderBrush = Brushes.Black;
            cell.FontWeight = FontWeights.Bold;
            cell.Background = Brushes.DarkGray;
            cell.Foreground = Brushes.White;
            cell.BorderThickness = new Thickness(1, 1, 1, 1);
            headerRow.Cells.Add(cell);
        }

        tableRowGroup.Rows.Add(headerRow);
        table.RowGroups.Add(tableRowGroup);
    }


    //print on filter tabs, cut columns depending on filter
    public void AddDataRows(RootObject obj, TableRow r, int cellNumber, string filter) {
        

        switch (filter) {
            case "allData":
                for (int j = 0; j < obj.Items.Count; j++) {
                    if (obj.Items[j].sub_location == null) {
                        obj.Items[j].sub_location = "";
                    }

                    if (obj.Items[j].is_deleted == 0) {
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
                        r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].purchase_date.ToString("d")))));
                        r.Cells[cellNumber].ColumnSpan = 4;
                        r.Cells[cellNumber].Padding = new Thickness(4);

                        r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                        r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                        cellNumber++;
                        r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].cost.ToString("C")))));
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
                }



                break;
            case "category":
                for (int j = 0; j < obj.Items.Count; j++) {

                    if (obj.Items[j].sub_location == null) {
                        obj.Items[j].sub_location = "";
                    }

                    if (obj.Items[j].is_deleted == 0) {
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
                }

                break;
            case "manufacturer":

                for (int j = 0; j < obj.Items.Count; j++) {

                    if (obj.Items[j].sub_location == null) {
                        obj.Items[j].sub_location = "";
                    }

                    if (obj.Items[j].is_deleted == 0) {
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
                }

                break;
            case "location":

                for (int j = 0; j < obj.Items.Count; j++) {

                    if (obj.Items[j].sub_location == null) {
                        obj.Items[j].sub_location = "";
                    }

                    if (obj.Items[j].is_deleted == 0) {
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
                }

                break;
            case "totalValue":

                for (int j = 0; j < obj.Items.Count; j++) {

                    if (obj.Items[j].sub_location == null) {
                        obj.Items[j].sub_location = "";
                    }

                    if (obj.Items[j].is_deleted == 0) {
                        totalCost += obj.Items[j].cost;
                        r = new TableRow();
                        cellNumber = 0;

                        r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].categoryName.ToString()))));
                        r.Cells[cellNumber].ColumnSpan = 4;
                        r.Cells[cellNumber].Padding = new Thickness(4);
                        r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                        r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                        cellNumber++;


                        r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].cost.ToString("C")))));
                        r.Cells[cellNumber].ColumnSpan = 4;
                        r.Cells[cellNumber].Padding = new Thickness(4);
                        r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
                        r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
                        cellNumber++;

                        tableRowGroup.Rows.Add(r); 
                    }
                }

                break;
        }


    }//ef

    private decimal CalculateTotalCost(RootObject obj, string title) {
        decimal totalCost = 0;
        //only add not deleted items
        //check if necessary
        //switch (title) {
        //    case "allData":
        //    case "category":
        //    case "manufacturer":
        //    case "location":
        //        foreach (var item in obj.Items) {
        //            totalCost += item.cost;
        //        }
        //        break;
        //    case "totalValue":
        //        foreach (var item in obj.Items) {
        //            totalCost += item.cost;
        //        }
        //        break;
        //}
        //test on each
        foreach (var item in obj.Items) {
            if (item.is_deleted == 0) {
                totalCost += item.cost; 
            }
        }

        return totalCost;
    }


    private void AddTotalCostRow(System.Windows.Documents.Table table, decimal totalCost) {
        TableRow totalRow = new TableRow();
        TableCell totalCell = new TableCell(new Paragraph(new Run("Total Cost: " + totalCost.ToString("C")))) {
            ColumnSpan = tableRowGroup.Rows[0].Cells.Count, // Span across all columns
            Padding = new Thickness(4),
            BorderBrush = Brushes.DarkGray,
            BorderThickness = new Thickness(0, 0, 1, 1)
        };
        totalRow.Cells.Add(totalCell);
        tableRowGroup.Rows.Add(totalRow);
    }



}//ec


