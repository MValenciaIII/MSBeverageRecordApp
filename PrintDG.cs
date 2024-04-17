using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Reflection;
using Syncfusion.UI.Xaml.Grid;
using ChoETL;
using static MSBeverageRecordApp.Reports;
using System.Linq.Expressions;


public class PrintDG {
    //globals
    TableRowGroup tableRowGroup = new TableRowGroup();
    public void printDG(RootObject obj, DataGrid dataGrid, string title, string filter) {
        obj.Items = (List<Records>)dataGrid.ItemsSource;
        PrintDialog printDialog = new PrintDialog();
        if (printDialog.ShowDialog() == true) {
            FlowDocument fd = new FlowDocument();

            Paragraph p = new Paragraph(new Run(title));
            p.FontStyle = dataGrid.FontStyle;
            p.FontFamily = dataGrid.FontFamily;
            p.FontSize = 18;
            fd.Blocks.Add(p);

            Table table = new Table();
            TableRow r = new TableRow();
            fd.PageWidth = printDialog.PrintableAreaWidth;
            fd.PageHeight = printDialog.PrintableAreaHeight;
            fd.BringIntoView();

            fd.TextAlignment = TextAlignment.Center;
            fd.ColumnWidth = 500;
            table.CellSpacing = 0;

            var headerList = dataGrid.Columns.Select(e => e.Header.ToString()).ToList();


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

            //filter names:
            //
            //allData
            //category
            //manufacturer
            //totalValue

            switch (filter) {
                case "allData":
                    //call 
                    AddAllDataRows(obj, r, cellNumber);
                    break;
                case "category":
                    //call 
                    AddCategoryRows(obj, r, cellNumber);
                    break;
                case "manufacturer":
                    //call
                    AddManuFacturerRows(obj, r, cellNumber);
                    break;
                case "totalValue":
                    //call
                    AddTotalValueRows(obj, r, cellNumber);
                    break;
            }

            //how to call on specific filters
            //make this a function to work for each category on filters

            table.RowGroups.Add(tableRowGroup);
            fd.Blocks.Add(table);
            printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");
        }
    }//em
    public void AddAllDataRows(RootObject obj, TableRow r, int cellNumber){
        for (int j = 0; j < obj.Items.Count; j++) {
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
    }//ef

    private void AddCategoryRows(RootObject obj, TableRow r, int cellNumber) {
        for (int j = 0; j < obj.Items.Count; j++) {
            r = new TableRow();
            cellNumber = 0;

            //r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].record_id.ToString()))));
            //r.Cells[cellNumber].ColumnSpan = 4;
            //r.Cells[cellNumber].Padding = new Thickness(4);
            //r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
            //r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
            //cellNumber++;

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

            //r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].model.ToString()))));
            //r.Cells[cellNumber].ColumnSpan = 4;
            //r.Cells[cellNumber].Padding = new Thickness(4);
            //r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
            //r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
            //cellNumber++;

            r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].serial.ToString()))));
            r.Cells[cellNumber].ColumnSpan = 4;
            r.Cells[cellNumber].Padding = new Thickness(4);
            r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
            r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
            cellNumber++;

            //r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].purchase_date.ToString()))));
            //r.Cells[cellNumber].ColumnSpan = 4;
            //r.Cells[cellNumber].Padding = new Thickness(4);
            //r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
            //r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
            //cellNumber++;

            //r.Cells.Add(new TableCell(new Paragraph(new Run(obj.Items[j].cost.ToString()))));
            //r.Cells[cellNumber].ColumnSpan = 4;
            //r.Cells[cellNumber].Padding = new Thickness(4);
            //r.Cells[cellNumber].BorderBrush = Brushes.DarkGray;
            //r.Cells[cellNumber].BorderThickness = new Thickness(0, 0, 1, 1);
            //cellNumber++;

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
    private void AddManuFacturerRows(RootObject obj, TableRow r, int cellNumber) {
        throw new NotImplementedException();
    }
    private void AddTotalValueRows(RootObject obj, TableRow r, int cellNumber) {
        throw new NotImplementedException();
    }

}//ec


