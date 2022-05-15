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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LaborExchangeApplication.ViewModel
{
    public class ApplicantViewModel : MainViewModel
    {
        #region Constants

        private const string NOT_IMPORTANT_FILTER = "Неважно";

        #endregion Constants

        #region Fields

        private User _user;
        private BitmapImage _avatar;
        private string _fullName;
        private string _professionFilter;
        private VacancyModel _selectedVacancy;
        private WorkDayRequirement _workDayRequirementFilter;
        private ObservableCollection<WorkDayRequirement> _workDayRequirements;
        private ObservableCollection<VacancyModel> _vacancies;

        #endregion Fields

        #region Properties

        public User User { get => _user; set => SetProperty(ref _user, value); }
        public BitmapImage Avatar { get => _avatar; set => SetProperty(ref _avatar, value); }
        public string FullName { get => _fullName; set => SetProperty(ref _fullName, value); }
        public string ProfessionFilter { get => _professionFilter; set => SetProperty(ref _professionFilter, value); }
        public VacancyModel SelectedVacancy { get => _selectedVacancy; set => SetProperty(ref _selectedVacancy, value); }
        public WorkDayRequirement WorkDayRequirementFilter { get => _workDayRequirementFilter; set => SetProperty(ref _workDayRequirementFilter, value); }
        public ObservableCollection<WorkDayRequirement> WorkDayRequirements { get => _workDayRequirements; set => SetProperty(ref _workDayRequirements, value); }
        public ObservableCollection<VacancyModel> Vacancies { get => _vacancies; set => SetProperty(ref _vacancies, value); }

        public ICommand VacanciesFilterCommand { get; private set; }
        public ICommand ClearVacanciesFilterCommand { get; private set; }
        public ICommand OpenWorkRequestWindowCommand { get; private set; }
        public ICommand SignOutCommand { get; private set; }

        #endregion Properties

        #region Methods

        public ApplicantViewModel() => FillProperties();

        private async void VacanciesFilter(object obj)
        {
            try
            {
                Vacancies.Clear();

                var response = await RequestHelper.GetVacanciesAsync();

                if (response.IsSuccessStatusCode)
                {
                    Vacancies = new ObservableCollection<VacancyModel>();

                    if (WorkDayRequirementFilter.Value.Equals(NOT_IMPORTANT_FILTER) && string.IsNullOrWhiteSpace(ProfessionFilter)) FillVacancies();
                    else foreach (var vacancy in JsonConvert.DeserializeObject<ObservableCollection<Vacancy>>(await response.Content.ReadAsStringAsync()))
                            if (WorkDayRequirementFilter.Value.Equals(NOT_IMPORTANT_FILTER))
                            {
                                if (vacancy.Profession.Value.Contains(ProfessionFilter))
                                    Vacancies.Add(await VacancyModel.GetModel(vacancy));
                            }
                            else if (string.IsNullOrWhiteSpace(ProfessionFilter))
                            {
                                if (vacancy.WorkDayRequirements.Value.Equals(WorkDayRequirementFilter.Value))
                                    Vacancies.Add(await VacancyModel.GetModel(vacancy));
                            }
                            else if (vacancy.Profession.Value.Contains(ProfessionFilter) &&
                                vacancy.WorkDayRequirements.Value.Equals(WorkDayRequirementFilter.Value))
                                Vacancies.Add(await VacancyModel.GetModel(vacancy));
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private void ClearVacanciesFilter(object obj) => FillVacancies();

        private void SignOut(object obj)
        {
            LogginedUser.ResetUser();

            new AuthorizationWindow().Show();

            Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is ApplicantWindow)?.Close();
        }

        private void FillProperties()
        {
            try
            {
                VacanciesFilterCommand = new DelegateCommand(VacanciesFilter);
                ClearVacanciesFilterCommand = new DelegateCommand(ClearVacanciesFilter);
                OpenWorkRequestWindowCommand = new DelegateCommand(OpenWorkRequestWindow);
                SignOutCommand = new DelegateCommand(SignOut);

                User = LogginedUser.GetUser();
                Avatar = User.Avatar.SequenceEqual(Array.Empty<byte>()) ? new BitmapImage(new Uri("pack://application:,,,/Assets/Images/defaultAvatar.png"))
                    : ImageConverter.BitmapImageFromBytes(User.Avatar);
                FullName = $"{User.Firstname} {User.Middlename} {User.Lastname}";

                FillVacancies();
                FillWorkDayRequirements();
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async void FillVacancies()
        {
            try
            {
                var response = await RequestHelper.GetVacanciesAsync();
                if (response.IsSuccessStatusCode)
                {
                    Vacancies = new ObservableCollection<VacancyModel>();

                    foreach (var vacancy in JsonConvert.DeserializeObject<ObservableCollection<Vacancy>>
                        (await response.Content.ReadAsStringAsync()))
                        Vacancies.Add(await VacancyModel.GetModel(vacancy));
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async void FillWorkDayRequirements()
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

                    WorkDayRequirements.Add(new WorkDayRequirement() { Value = NOT_IMPORTANT_FILTER });
                    WorkDayRequirementFilter = WorkDayRequirements[0];
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private void OpenWorkRequestWindow(object obj)
        {
            new WorkRequestWindow().Show();

            Application.Current.Windows.Cast<Window>()?.FirstOrDefault(window => window is ApplicantWindow)?.Close();
        }

        #endregion Methods
    }
}