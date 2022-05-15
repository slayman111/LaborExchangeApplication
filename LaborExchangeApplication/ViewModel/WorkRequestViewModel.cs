using LaborExchangeApi.Models;
using LaborExchangeApplication.Command;
using LaborExchangeApplication.Core;
using LaborExchangeApplication.Model;
using LaborExchangeApplication.View.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaborExchangeApplication.ViewModel
{
    public class WorkRequestViewModel : MainViewModel
    {
        #region Contants

        private const string INVALID_STATUS_CODE_EX = "Ошибка при добавлении заявки";
        private const string INVALID_SALARY_REQUIREMENTS_EX = "Требования зарплаты не должно быть пустым и должно содержать вещественное число";
        private const string SUCCESS_JOB_REQUEST_POST_MSG = "Ваша заявка успешно добавлена";
        private const string SUCCESS_JOB_REQUEST_DELETE_MSG = "Ваша заявка успешно удалена";

        #endregion Contants

        #region Fields

        private Profession _selectedProfession;
        private decimal _salaryRequirements;
        private WorkDayRequirement _selectedWorkDayRequirement;
        private string _info;
        private JobRequestModel _selectedJobRequest;
        private ObservableCollection<Profession> _professions;
        private ObservableCollection<WorkDayRequirement> _workDayRequirements;
        private ObservableCollection<JobRequestModel> _jobRequests;

        #endregion Fields

        #region Properties

        public Profession SelectedProfession { get => _selectedProfession; set => SetProperty(ref _selectedProfession, value); }
        public decimal SalaryRequirements { get => _salaryRequirements; set => SetProperty(ref _salaryRequirements, value); }
        public WorkDayRequirement SelectedWorkDayRequirement { get => _selectedWorkDayRequirement; set => SetProperty(ref _selectedWorkDayRequirement, value); }
        public string Info { get => _info; set => SetProperty(ref _info, value); }
        public JobRequestModel SelectedJobRequest { get => _selectedJobRequest; set => SetProperty(ref _selectedJobRequest, value); }
        public ObservableCollection<Profession> Professions { get => _professions; set => SetProperty(ref _professions, value); }
        public ObservableCollection<WorkDayRequirement> WorkDayRequirements { get => _workDayRequirements; set => SetProperty(ref _workDayRequirements, value); }
        public ObservableCollection<JobRequestModel> JobRequests { get => _jobRequests; set => SetProperty(ref _jobRequests, value); }

        public ICommand AddJobRequestCommand { get; private set; }
        public ICommand DeleteJobRequestCommand { get; private set; }
        public ICommand OpenApplicantWindowCommand { get; private set; }
        public ICommand SignOutCommand { get; private set; }

        #endregion Properties

        #region Methods

        public WorkRequestViewModel() => FillProperties();

        public void FillProperties()
        {
            AddJobRequestCommand = new DelegateCommand(AddJobRequest);
            DeleteJobRequestCommand = new DelegateCommand(DeleteJobRequest);
            OpenApplicantWindowCommand = new DelegateCommand(OpenApplicantWindow);
            SignOutCommand = new DelegateCommand(SignOut);

            FillProfessions();
            FillWorkDayRequirements();
            FillJobRequests();
        }

        private async void AddJobRequest(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SalaryRequirements.ToString()) || !Regex.IsMatch(SalaryRequirements.ToString(), @"^[0-9]+(\.[0-9]{1,2})?$"))
                    throw new Exception(INVALID_SALARY_REQUIREMENTS_EX);

                var response = await RequestHelper.PostJobRequestAsync(new StringContent(JsonConvert.SerializeObject(
                        new JobRequest()
                        {
                            ProfessionId = SelectedProfession.Id,
                            SalaryRequirements = SalaryRequirements,
                            WorkDayRequirementsId = SelectedWorkDayRequirement.Id,
                            Info = Info
                        }), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    response = await RequestHelper.GetJobRequestsAsync();
                    var jobRequestId = JsonConvert.DeserializeObject<ObservableCollection<JobRequest>>
                        (await response.Content.ReadAsStringAsync())?.Last().Id;

                    var userHasJobRequest = new UserHasJobRequest()
                    {
                        UserId = LogginedUser.GetUser().Id,
                        JobRequestId = jobRequestId ?? 0,
                    };
                    response = await RequestHelper.PostUserHasJobRequestAsync(new StringContent(JsonConvert.SerializeObject(
                        userHasJobRequest), Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        new InformationBoxWindow("Информация", SUCCESS_JOB_REQUEST_POST_MSG, InformationBoxImage.Information).ShowDialog();

                        var updatedUser = LogginedUser.GetUser();
                        updatedUser.UserHasJobRequests.Add(userHasJobRequest);
                        LogginedUser.SetUser(updatedUser);
                        await FillJobRequests();
                    }
                    else throw new Exception(INVALID_STATUS_CODE_EX);
                }
                else throw new Exception(INVALID_STATUS_CODE_EX);
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async void DeleteJobRequest(object obj)
        {
            try
            {
                var response = await RequestHelper.DeleteJobRequestAsync(SelectedJobRequest.Id);

                if (response.IsSuccessStatusCode)
                {
                    response = await RequestHelper.DeleteUserHasJobRequestAsync(LogginedUser.GetUser().Id, SelectedJobRequest.Id);

                    if (response.IsSuccessStatusCode)
                    {
                        new InformationBoxWindow("Информация", SUCCESS_JOB_REQUEST_DELETE_MSG, InformationBoxImage.Information).ShowDialog();

                        await FillJobRequests();
                    }
                    else throw new Exception(INVALID_STATUS_CODE_EX);
                }
                else throw new Exception(INVALID_STATUS_CODE_EX);
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private void OpenApplicantWindow(object obj)
        {
            new ApplicantWindow().Show();

            Application.Current.Windows.Cast<Window>()?.FirstOrDefault(window => window is WorkRequestWindow)?.Close();
        }

        private void SignOut(object obj)
        {
            new AuthorizationWindow().Show();

            Application.Current.Windows.Cast<Window>()?.FirstOrDefault(window => window is WorkRequestWindow)?.Close();
        }

        private async Task FillProfessions()
        {
            try
            {
                var response = await RequestHelper.GetProfessionsAsync();
                if (response.IsSuccessStatusCode)
                {
                    Professions = new ObservableCollection<Profession>();

                    foreach (var profession in JsonConvert.DeserializeObject<ObservableCollection<Profession>>
                        (await response.Content.ReadAsStringAsync()))
                        Professions.Add(profession);

                    SelectedProfession = Professions[0];
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async Task FillWorkDayRequirements()
        {
            try
            {
                var response = await RequestHelper.GetWorkDayRequirementsAsync();
                if (response.IsSuccessStatusCode)
                {
                    WorkDayRequirements = new ObservableCollection<WorkDayRequirement>();

                    foreach (var workDayRequirement in JsonConvert.DeserializeObject<ObservableCollection<WorkDayRequirement>>
                        (await response.Content.ReadAsStringAsync()))
                        WorkDayRequirements.Add(workDayRequirement);

                    SelectedWorkDayRequirement = WorkDayRequirements[0];
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async Task FillJobRequests()
        {
            try
            {
                var response = await RequestHelper.GetJobRequestsAsync();
                if (response.IsSuccessStatusCode)
                {
                    JobRequests = new ObservableCollection<JobRequestModel>();

                    foreach (var jobRequest in JsonConvert.DeserializeObject<ObservableCollection<JobRequest>>
                        (await response.Content.ReadAsStringAsync()))
                        foreach (var userRequest in LogginedUser.GetUser().UserHasJobRequests)
                            if (userRequest.JobRequestId.Equals(jobRequest.Id)) JobRequests.Add(JobRequestModel.GetModel(jobRequest));
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        #endregion Methods
    }
}