using CheckRequestWorker.DB;
using CheckRequestWorker.Models;
using CheckRequestWorker.Tools;
using CheckRequestWorker.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckRequestWorker.ViewModels
{
    public class ListRequestPageModel : BaseVM
    {
        private ObservableCollection<Request> requests;
        private Request selectedRequest;
        private List<TypeRequest> typeRequests;
        private TypeRequest selectedTypeRequest;
        private Status selectedStatus;
        private List<Status> statuses;
        private SubDivision selectedSubDivision;
        private List<SubDivision> subDivisions;
        


        public TypeRequest SelectedTypeRequest
        {
            get => selectedTypeRequest;
            set
            {
                selectedTypeRequest = value;
                DoSearch();
            }

        }

        public List<TypeRequest> TypeRequests
        {
            get => typeRequests;
            set
            {
                typeRequests = value;
                SignalChanged();
            }

        }

        public Status SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                DoSearch();
            }

        }

        public List<Status> Statuses
        {
            get => statuses;
            set
            {
                statuses = value;
                SignalChanged();
            }

        }

        public SubDivision SelectedSubDivision
        {
            get => selectedSubDivision;
            set
            {
                selectedSubDivision = value;
                DoSearch();
            }
        }

        public List<SubDivision> SubDivisions
        {
            get => subDivisions;
            set
            {
                subDivisions = value;
                SignalChanged();
            }

        }

        public ObservableCollection<Request> Requests
        {
            get => requests;
            set
            {
                requests = value;
                SignalChanged();
            }
        }

        public Request SelectedRequest
        {
            get => selectedRequest; set
            {
                selectedRequest = value;
                SignalChanged();
            }
        }

        public ListRequestPageModel()
        {
            Requests = new ObservableCollection<Request>(user50_2Context.GetInstance().Requests.Include(s => s.Worker).ThenInclude(s => s.SubDivision).Include(s => s.VisitorsRequests).ThenInclude(s => s.Visitors).Include(s => s.Users));

            TypeRequests = new List<TypeRequest> { new TypeRequest { Id = 0, Name = "Все" } };
            TypeRequests.AddRange(user50_2Context.GetInstance().TypeRequests);
            SelectedTypeRequest = TypeRequests[0];

            Statuses = new List<Status> { new Status { Id = 0, Name = "Все" } };
            Statuses.AddRange(user50_2Context.GetInstance().Statuses);
            SelectedStatus = Statuses[0];

            SubDivisions = new List<SubDivision> { new SubDivision { Id = 0, Name = "Все" } };
            SubDivisions.AddRange(user50_2Context.GetInstance().SubDivisions);
            SelectedSubDivision = SubDivisions[0];
        }

        public void OpenModalWindow()
        {
            var window = new ModalWindow(SelectedRequest);
            window.ShowDialog();
        }

        private void DoSearch()
        {
            IQueryable<Request> searchRequest = GetRequests();
            if (SelectedTypeRequest != null)
                searchRequest = searchRequest.Where(s => s.TypeRequestId == SelectedTypeRequest.Id || SelectedTypeRequest.Id == 0);
            if (SelectedStatus != null)
                searchRequest = searchRequest.Where(s => s.StatusId == SelectedStatus.Id || SelectedStatus.Id == 0);
            if (SelectedSubDivision != null)
                searchRequest = searchRequest.Where(s => s.Worker.SubDivisionId == SelectedSubDivision.Id || SelectedSubDivision.Id == 0);
            Requests = new ObservableCollection<Request>(searchRequest.ToList());

        }

        private static IQueryable<Request> GetRequests()
        {
            return user50_2Context.GetInstance().Requests.Include(s => s.TypeRequest).Include(s => s.Status).Include(s => s.Worker).ThenInclude(s => s.SubDivision);
        }

    }
}