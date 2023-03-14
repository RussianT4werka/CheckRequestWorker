using CheckRequestWorker.DB;
using CheckRequestWorker.Models;
using CheckRequestWorker.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckRequestWorker.ViewModels
{
    public class ModalWindowVM : BaseVM
    {
        private Status selectedStatus;
        private List<Status> status;
        private bool block = true;
        private bool blockRefusal = true;

        public bool Block
        {
            get => block;
            set
            {
                block = value;
                SignalChanged();
            }
        }

        public bool BlockRefusal 
        { 
            get => blockRefusal;
            set
            {
                blockRefusal = value;
                SignalChanged();
            }
        }

        public Command SaveEdit { get; set; }
        public Command Refusal { get; set; }

        public Request Request { get; set; }

        public Status SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                SignalChanged();
            }
        }

        public List<Status> Status
        {
            get => status;
            set
            {
                status = value;
                SignalChanged();
            }
        }

        public ModalWindowVM(Models.Request selectedRequest, Window wind)
        {
            var blacklist = user50_2Context.GetInstance().BlackLists.ToList();
            foreach (var visitorsRequest in selectedRequest.VisitorsRequests)
            {
                var darkVisitor = blacklist.FirstOrDefault(s => s.VisitorsId == visitorsRequest.VisitorsId);
                if (darkVisitor != null)
                {
                    MessageBox.Show("Один из посетителей находится в чёрном списке");
                    if (selectedRequest.StatusId != 3)
                    {
                        selectedRequest.StatusId = 3;
                        var oldRequest = user50_2Context.GetInstance().Requests.ToList().FirstOrDefault(s => s.Id == selectedRequest.Id);
                        user50_2Context.GetInstance().Entry(oldRequest).CurrentValues.SetValues(selectedRequest);
                        user50_2Context.GetInstance().SaveChanges();
                    }
                    Block = false;
                }
            }

            Status = user50_2Context.GetInstance().Statuses.ToList();
            Request = selectedRequest;

            SaveEdit = new Command(() =>
            {
                selectedRequest.StatusId = 2;
                var oldRequest = user50_2Context.GetInstance().Requests.ToList().FirstOrDefault(s => s.Id == selectedRequest.Id);
                user50_2Context.GetInstance().Entry(oldRequest).CurrentValues.SetValues(selectedRequest);
                user50_2Context.GetInstance().SaveChanges();
                wind.Close();
            });
            if (selectedRequest.StatusId == 3)
            {
                BlockRefusal = false;
            }
            if (selectedRequest.StatusId == 2)
            {
                Block = false;
            }
            Refusal = new Command(() =>
            {
                selectedRequest.StatusId = 3;
                var oldRequest = user50_2Context.GetInstance().Requests.ToList().FirstOrDefault(s => s.Id == selectedRequest.Id);
                user50_2Context.GetInstance().Entry(oldRequest).CurrentValues.SetValues(selectedRequest);
                user50_2Context.GetInstance().SaveChanges();
                wind.Close();
            });
        }
    }
}
