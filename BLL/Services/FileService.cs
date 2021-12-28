using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
using BLL.Models;
using DAL.Interfaces;
using System.Diagnostics;
using System.IO;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Collections.ObjectModel;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace BLL.Services
{
    public class FileService :IFileService
    {
        IDbRepository dataBase;
        public FileService(IDbRepository dbRepository)
        {
            dataBase = dbRepository;
        }

        public void PrintExceptions(Exception ex)
        {
            string writePath = @"Exception.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(ex.Message);
                    sw.WriteLine(ex.StackTrace);
                    sw.WriteLine(ex.InnerException);
                    sw.WriteLine(ex.Source + "\n");
                }
            }
            catch (Exception e)
            {
                PrintExceptions(e);
            }
        }

        public void PrintCheque(int UserId)
        {
            try
            {
                string file = @"Check.pdf";
                Order order = dataBase.Orders.GetList().Where(i => i.Order_Client == UserId).Where(i => i.Order_Status == 1).FirstOrDefault();
                var client = dataBase.Users.GetItem(UserId);
                var lines = dataBase.OrderStrings.GetList().Where(i => i.OrderString_Order == order.Order_Id).ToList();
                

                FileStream fs = new FileStream(file, FileMode.Create);
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                BaseFont baseFont = BaseFont.CreateFont("arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font1 = new Font(baseFont, 14, Font.BOLD);
                Font font = new Font(baseFont, 12);

                document.Open();

                Paragraph header = new Paragraph("Пиццерия", font1);
                Paragraph subHeader = new Paragraph("Кассовый чек", font);
                header.Alignment = Element.ALIGN_CENTER;
                subHeader.Alignment = Element.ALIGN_CENTER;
                Paragraph separator = new Paragraph("***********************************************************************************", font1);
                separator.Alignment = Element.ALIGN_CENTER;
                Paragraph productstart = new Paragraph("--------------------------- Список товаров в заказе ---------------------------", font1);
                productstart.Alignment = Element.ALIGN_CENTER;
                Paragraph productend = new Paragraph("------------------------------------------------------------------------------------", font1);
                productend.Alignment = Element.ALIGN_CENTER;
                Paragraph date = new Paragraph($"Дата оформления заказа: {((DateTime)order.Order_CreationTime).ToShortDateString()}", font);
                Paragraph name = new Paragraph($"Заказчик: {client.User_FullName}", font);
                Paragraph ord = new Paragraph($"Статус: {dataBase.Statuses.GetItem((int)order.Order_Status).Status_Name}", font);
                Paragraph id = new Paragraph($"ID заказа: {order.Order_Id}", font);

                document.Add(header);
                document.Add(subHeader);
                document.Add(separator);
                document.Add(new Paragraph("\n"));
                document.Add(date);
                document.Add(name);
                document.Add(ord);
                document.Add(id);
                document.Add(new Paragraph("\n"));

                document.Add(productstart);
                document.Add(new Paragraph("\n"));
                foreach (var i in lines)
                {
                    Chunk glue = new Chunk(new VerticalPositionMark());
                    Paragraph p = new Paragraph($"{i.Pizza.Pizza_Name}: {i.OrderString_Count} шт.", font);
                    p.Add(new Chunk(glue));
                    p.Add($"{i.Pizza.Pizza_Price*i.OrderString_Count:0.#} руб.");
                    document.Add(p);
                }
                document.Add(new Paragraph("\n"));
                document.Add(productend);

                document.Add(new Paragraph("\n"));
                
                document.Add(new Paragraph($"Итого: {order.Order_Price:0.##} руб.", font));
                document.Add(new Paragraph("\n"));
                document.Add(separator);

                document.Close();
                writer.Close();
                fs.Close();

                Process iStartProcess = new Process();
                iStartProcess.StartInfo.FileName = file;
                iStartProcess.Start();
            }
            catch (Exception ex)
            {
                PrintExceptions(ex);
            }
        }

        public void PrintReport(int SelectedStatus, DateTime DateStart, DateTime DateEnd)
        {
            try
            {
                string file = @"Report.pdf";

                FileStream fs = new FileStream(file, FileMode.Create);
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                BaseFont baseFont = BaseFont.CreateFont("arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font1 = new Font(baseFont, 14, Font.BOLD);
                Font font = new Font(baseFont, 12);

                Paragraph header = new Paragraph("Пиццерия", font1);
                Paragraph subHeader = new Paragraph($"Отчёт за период: с {DateStart.ToShortDateString()} по {DateEnd.ToShortDateString()}", font);
                header.Alignment = Element.ALIGN_CENTER;
                subHeader.Alignment = Element.ALIGN_CENTER;
                Paragraph separator = new Paragraph("***********************************************************************************", font1);
                separator.Alignment = Element.ALIGN_CENTER;
                Paragraph orderStart = new Paragraph("--------------------------- Список заказов ---------------------------", font1);
                orderStart.Alignment = Element.ALIGN_CENTER;
                Paragraph orderEnd = new Paragraph("----------------------------------------------------------------------------------", font1);
                orderEnd.Alignment = Element.ALIGN_CENTER;

                document.Open();

                var orders = dataBase.Orders.GetList();
                var orderLines = dataBase.OrderStrings.GetList();
                

                //Paragraph name = new Paragraph($": {Orders.Name}", font);

                decimal allSales = 0, Sum = 0, canceled = 0, notready = 0;
                List<OrderModel> orderModels = orders.Where(i => DateStart <= i.Order_CreationTime && DateEnd >= i.Order_CreationTime && ((SelectedStatus == -1 && (i.Order_Status == 1 || i.Order_Status == 2 || i.Order_Status == 3 || i.Order_Status == 4 || i.Order_Status == 5 || i.Order_Status == 6)) | SelectedStatus == 0 | SelectedStatus == i.Order_Status)).Select(i => new OrderModel { OrderLines = new ObservableCollection<OrderStringModel>(), Order_Id = i.Order_Id, Order_CreationTime = (DateTime)i.Order_CreationTime, Order_Status = i.Order_Status, Order_Price = i.Order_Price, Order_Address = i.Order_Address, Order_PhoneNumber = i.Order_PhoneNumber }).ToList();

                document.Add(header);
                document.Add(subHeader);
                document.Add(separator);
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(orderStart);
                document.Add(new Paragraph("\n"));
               

                foreach (var ord in orderModels)
                {
                    var order = dataBase.Orders.GetItem(ord.Order_Id);
                    

                    Paragraph date = new Paragraph($"Дата оформления заказа: {((DateTime)order.Order_CreationTime).ToShortDateString()}", font);
                    Paragraph Client = new Paragraph($"Покупатель: {order.User.User_FullName}", font);
                    Paragraph stat = new Paragraph($"Статус: {order.Status.Status_Name}", font);
                    Paragraph id = new Paragraph($"ID заказа: {order.Order_Id}", font);

                    document.Add(date);
                    document.Add(Client);
                    document.Add(stat);
                    document.Add(id);

                    Paragraph Worker;
                    Paragraph Courier;

                    if (order.User2 != null)
                    {
                        Worker = new Paragraph($"Работник ресторана: {order.User2.User_FullName}", font);
                        document.Add(Worker);
                    }
                    if (order.User1 != null)
                    {
                        Courier = new Paragraph($"Курьер: {order.User1.User_FullName}", font);
                        document.Add(Courier);
                    }

                    document.Add(new Paragraph($"Итого: {order.Order_Price:0.##} руб.", font));
                    document.Add(new Paragraph("\n"));
                    if (order.Order_Status == 5)
                    {
                        Sum += (decimal)order.Order_Price;
                        allSales++;
                    }
                    if (order.Order_Status == 6)
                    {
                        
                        canceled++;
                    }
                    if (order.Order_Status <5)
                    {

                        notready++;
                    }
                }
                document.Add(orderEnd);
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph($"Всего суммы с заказов за период: {Sum:0.##} руб.", font));
                document.Add(new Paragraph($"Общая кол-во завершённых заказов за период: {allSales:0.##} шт.", font));
                document.Add(new Paragraph($"Общая кол-во отменённых заказов за период: {canceled:0.##} шт.", font));
                document.Add(new Paragraph($"Общая кол-во незавершённых заказов за период: {notready:0.##} шт.", font));

                document.Add(new Paragraph("\n"));
                document.Add(separator);

                document.Close();
                writer.Close();
                fs.Close();

                Process iStartProcess = new Process();
                iStartProcess.StartInfo.FileName = file;
                iStartProcess.Start();
            }
            catch (Exception e)
            {
                PrintExceptions(e);
            }
        }
    }
}
