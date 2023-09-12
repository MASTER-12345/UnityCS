
//注意 文件打开的时候，无法读取 
public static void InputExcel()
 {
     string filePath = "C:\\Users\\Admin\\Desktop\\MiniExcel\\data.xlsx";
     ///获取Excel文件的信息
     FileInfo fileInfo = new FileInfo(filePath);
     ///通过Excel文件信息，打开表格
     using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))//using是用来强行资源释放（前括号是打开，后括号是关闭）
     {
         //取得Excel文件中的第N张表
         ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
         for (int i = 1; i < worksheet.Dimension.End.Row; i++)//End.Row获得当前表格的最大行数
         {
             Debug.Log("目标名称：" + worksheet.Cells[i, 3].Value.ToString());
         }
     }
 }
//写入文件，会覆盖掉
 public void WriteExcel()
 {
     string filePath = "C:\\Users\\Admin\\Desktop\\MiniExcel\\data.xlsx";
     ///获取Excel文件的信息
     FileInfo fileInfo = new FileInfo(filePath);
     ///通过Excel文件信息，打开表格
     using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))//using是用来强行资源释放（前括号是打开，后括号是关闭）
     {
         //取得Excel文件中的第N张表
         ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];
         for (int i = 2; i <= 12; i++)
         {
             //直接向每个表格赋值则是写入
             worksheet.Cells[i, 1].Value = "AAA";
             worksheet.Cells[i, 2].Value = "BBB";
         }
         excelPackage.Save();//写入后保存表格
     }
 }
