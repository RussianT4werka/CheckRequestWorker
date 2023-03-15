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
        private int dateVisitMinutes;
        private int dateVisitHours;

        public int DateVisitHours
        {
            get => dateVisitHours;
            set
            {
                dateVisitHours = value;
                if (dateVisitHours > 23 || dateVisitHours < 0)
                    dateVisitHours = 0;
                SignalChanged();
            }
        }

        public int DateVisitMinutes
        {
            get => dateVisitMinutes;
            set
            {
                dateVisitMinutes = value;
                if(dateVisitMinutes > 59 || dateVisitHours < 0)
                    DateVisitMinutes = 0;
                SignalChanged();
            }

        }

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
            if (selectedRequest.DateVisit != null)
            {
                DateVisitHours = selectedRequest.DateVisit.Value.Hour;
                DateVisitMinutes = selectedRequest.DateVisit.Value.Minute;
            }
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
                if(selectedRequest.DateVisit < selectedRequest.StartDate || selectedRequest.DateVisit > selectedRequest.FinishDate)
                {
                    MessageBox.Show("Сотрудник дурак!");
                    return;
                }
                selectedRequest.DateVisit = new DateTime(selectedRequest.DateVisit.Value.Year, selectedRequest.DateVisit.Value.Month, selectedRequest.DateVisit.Value.Day, DateVisitHours, DateVisitMinutes, 0);
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
