using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Reflection;
using Syncfusion.UI.Xaml.Grid;
using ChoETL;
using static MSBeverageRecordApp.CrudWindow;



public class PrintDG {
    public void printDG(RootObject obj, DataGrid dataGrid, string title) {

        PrintDialog printDialog = new PrintDialog();
        if (printDialog.ShowDialog() == true) {
            FlowDocument fd = new FlowDocument();

            Paragraph p = new Paragraph(new Run(title));
            p.FontStyle = dataGrid.FontStyle;
            p.FontFamily = dataGrid.FontFamily;
            p.FontSize = 18;
            fd.Blocks.Add(p);

            Table table = new Table();
            TableRowGroup tableRowGroup = new TableRowGroup();
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
            //for (int i = 0; i < dataGrid.Items.Count; i++) {



                //DataRowView row = (DataRowView)dataGrid.Items[i];



                table.BorderBrush = Brushes.Gray;
                table.BorderThickness = new Thickness(1, 1, 0, 0);
                table.FontStyle = dataGrid.FontStyle;
                table.FontFamily = dataGrid.FontFamily;
                table.FontSize = 13;
                tableRowGroup = new TableRowGroup();
                
                
                for (int j = 0; j < obj.Items.Count; j++) {
                    r = new TableRow();
                    int cellNumber = 0;
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

                table.RowGroups.Add(tableRowGroup);

           // }
            fd.Blocks.Add(table);

            printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");

        }
    }

}
