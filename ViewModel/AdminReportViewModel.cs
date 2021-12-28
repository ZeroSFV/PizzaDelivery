using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using PizzaDelivery.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PizzaDelivery.View;
using Microsoft.Toolkit.Uwp.Notifications;

namespace PizzaDelivery.ViewModel
{
    public class AdminReportViewModel: INotifyPropertyChanged, IWindowPage
    {
        IDbCrud crud;
        IPizza ipizz;
        IFileService file;

        public AdminReportViewModel(IDbCrud dbCrud, IPizza _ipizz, int userId, IFileService _file)
        {
            ipizz = _ipizz;
            crud = dbCrud;
            logUser = userId;
            file = _file;
            string date = "12/31/2020";
            DateStart = DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture);
            DateEnd = DateTime.Now;

            Statuses = new ObservableCollection<StatusModel>();
            var stat = crud.GetAllStatuses();
            SelectedStatus = 0;
            Statuses.Add(new StatusModel { Status_Id = 0, Status_Name = "Любой" });
            foreach (var i in stat)
            {
                Statuses.Add(i);
            }

            Orders = new ObservableCollection<OrderModel>();
            Update(0);
           
        }

        public void Update(int nullable)
        {
            Orders.Clear();
            var orders = crud.GetOrdersByStatusAnddAte(SelectedStatus, DateStart, DateEnd);
            foreach (var i in orders)
            {
                Orders.Add(i);
            }
            if (Orders.Count == 0)
            {
                Vision = true;
            }
            else
            {
                Vision = false;
            }
        }

        private ICommand _PDF;
        public ICommand PDF
        {
            get
            {
                if (_PDF == null)
                    _PDF = new RelayCommand(args => ExportToPDF(args));
                return _PDF;
            }
        }
        private void ExportToPDF(object args)
        {
            file.PrintReport(SelectedStatus, DateStart, DateEnd);
        }

        private ICommand _sort;
        public ICommand Sort
        {
            get
            {
                if (_sort == null)
                    _sort = new RelayCommand(args => SortOrders(args));
                return _sort;
            }
        }
        private void SortOrders(object args)
        {
            Update(0);
        }

        private ObservableCollection<OrderModel> _Orders;
        public ObservableCollection<OrderModel> Orders
        {
            get
            {
                return _Orders;
            }
            set
            {
                _Orders = value;
                OnPropertyChanged("Orders");
            }
        }

        public ObservableCollection<StatusModel> Statuses { get; set; }

        private int _SelectedStatus;
        public int SelectedStatus
        {
            get
            {
                return _SelectedStatus;
            }
            set
            {
                _SelectedStatus = value;
                OnPropertyChanged("SelectedValue");
            }
        }

        private DateTime _DateStart;
        public DateTime DateStart
        {
            get
            {
                return _DateStart;
            }
            set
            {
                _DateStart = value;
                OnPropertyChanged("DateStart");
            }
        }

        private DateTime _DateEnd;
        public DateTime DateEnd
        {
            get
            {
                return _DateEnd;
            }
            set
            {
                _DateEnd = value;
                OnPropertyChanged("DateEnd");
            }
        }

        private bool vision;
        public bool Vision
        {
            get { return vision; }
            set { vision = value; ; OnPropertyChanged("Vision"); }
        }

        TypeWindow IWindowPage.GetWindowType()
        {
            return TypeWindow.AdminReportUserControl;
        }

        private int logUser;
        public bool WentIn { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public decimal Pricer
        {
            get
            {
                return _Pricer;
            }
            set
            {
                _Pricer = value;
                OnPropertyChanged("Pricer");
            }
        }
        private decimal _Pricer;
    }
}
