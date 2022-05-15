using LaborExchangeApi.Models;
using LaborExchangeApplication.Command;
using LaborExchangeApplication.Core;
using LaborExchangeApplication.Model;
using LaborExchangeApplication.View.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LaborExchangeApplication.ViewModel
{
    public class RegistrationViewModel : MainViewModel
    {
        #region Constants

        private const string LOGIN_DUPLICATE_EX = "Пользователь с таким логином уже существует";
        private const string INVALID_STATUS_CODE_EX = "Ошибка регистрации";
        private const string INVALID_FULLNAME_EX = "Введите ФИО через пробелы на русском языке";
        private const string INVALID_LOGIN_EX = "Логин должен содержать букву, быть не меньше 6 и не больше 50";
        private const string INVALID_PASSWORD_EX = "Пароль должен содержать букву, быть не меньше 6 и не больше 50";
        private const string INVALID_REPEAT_PASSWORD_EX = "Пароли не совпадают";
        private const string INVALID_BORNDATE_EX = "Вы не являетесь совершеннолетним";
        private const string INVALID_EMAIL_EX = "Неверный формат электронной почты";
        private const string INVALID_PHONE_NUMBER_EX = "Неверный формат номера телефона";
        private const string SUCCESS_REGISTRATION_MSG = "Регистрация прошла успешно";

        #endregion Constants

        #region Fields

        private string _fullname;
        private string _login;
        private string _password;
        private string _repeatPassword;
        private DateTime _bornDate;
        private Gender _selectedGender;
        private string _email;
        private string _phone;
        private Role _selectedRole;
        private City _selectedCity;
        private FamilyStatus _selectedFamilyStatus;
        private string _housingConditions;
        private string _personalInfo;
        private BitmapImage _avatar;
        private ObservableCollection<Gender> _genders;
        private ObservableCollection<Role> _roles;
        private ObservableCollection<City> _cities;
        private ObservableCollection<FamilyStatus> _familyStatuses;

        #endregion Fields

        #region Properties

        public string Fullname { get => _fullname; set => SetProperty(ref _fullname, value); }
        public string Login { get => _login; set => SetProperty(ref _login, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string RepeatPassword { get => _repeatPassword; set => SetProperty(ref _repeatPassword, value); }
        public DateTime BornDate { get => _bornDate; set => SetProperty(ref _bornDate, value); }
        public Gender SelectedGender { get => _selectedGender; set => SetProperty(ref _selectedGender, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
        public Role SelectedRole { get => _selectedRole; set => SetProperty(ref _selectedRole, value); }
        public City SelectedCity { get => _selectedCity; set => SetProperty(ref _selectedCity, value); }
        public FamilyStatus SelectedFamilyStatus { get => _selectedFamilyStatus; set => SetProperty(ref _selectedFamilyStatus, value); }
        public string HousingConditions { get => _housingConditions; set => SetProperty(ref _housingConditions, value); }
        public string PersonalInfo { get => _personalInfo; set => SetProperty(ref _personalInfo, value); }
        public BitmapImage Avatar { get => _avatar; set => SetProperty(ref _avatar, value); }
        public ObservableCollection<Gender> Genders { get => _genders; set => SetProperty(ref _genders, value); }
        public ObservableCollection<Role> Roles { get => _roles; set => SetProperty(ref _roles, value); }
        public ObservableCollection<City> Cities { get => _cities; set => SetProperty(ref _cities, value); }
        public ObservableCollection<FamilyStatus> FamilyStatuses { get => _familyStatuses; set => SetProperty(ref _familyStatuses, value); }

        public ICommand OpenAuthorizationWindowCommand { get; private set; }
        public ICommand RegisterUserCommand { get; private set; }
        public ICommand UploadImageCommand { get; private set; }

        #endregion Properties

        #region Methods

        public RegistrationViewModel() => FillProperties();

        private void OpenAuthorizationWindow(object obj)
        {
            new AuthorizationWindow().Show();

            Application.Current.Windows.Cast<Window>()?.FirstOrDefault(window => window is RegistrationWindow)?.Close();
        }

        private async void RegisterUser(object obj)
        {
            try
            {
                if (JsonConvert.DeserializeObject<List<User>>(await (await RequestHelper.GetUsersAsync()).Content.ReadAsStringAsync())
                    .Any(u => u.Login.Equals(Login)))
                    throw new Exception(LOGIN_DUPLICATE_EX);

                var errors = CheckRegistrationForErrors();
                if (errors.Length > 0)
                    throw new Exception(errors.ToString());

                var response = await RequestHelper.PostUserAsync(new StringContent(JsonConvert.SerializeObject(
                    new User()
                    {
                        Firstname = Fullname?.Split(' ')[0],
                        Middlename = Fullname?.Split(' ')[1],
                        Lastname = Fullname?.Split(' ')[2],
                        Login = Login,
                        Password = Password,
                        BornDate = BornDate,
                        GenderId = (SelectedGender is not null) ? SelectedGender.Id : 1,
                        Email = Email,
                        Phone = Phone,
                        RoleId = (SelectedRole is not null) ? SelectedRole.Id : 1,
                        CityId = (SelectedCity is not null) ? SelectedCity.Id : 1,
                        FamilyStatusId = (SelectedFamilyStatus is not null) ? SelectedFamilyStatus.Id : 1,
                        HousingConditions = HousingConditions,
                        Info = PersonalInfo,
                        Avatar = ImageConverter.BytesFromBitmapImage(Avatar)
                    }), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    new InformationBoxWindow("Информация", SUCCESS_REGISTRATION_MSG, InformationBoxImage.Information).ShowDialog();

                    new AuthorizationWindow().Show();

                    Application.Current.Windows.Cast<Window>()?.FirstOrDefault(window => window is RegistrationWindow)?.Close();
                }
                else throw new Exception(INVALID_STATUS_CODE_EX);
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private void UploadImage(object obj)
        {
            try
            {
                var ofdPicture = new OpenFileDialog
                {
                    Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*",
                    FilterIndex = 1
                };

                if (ofdPicture.ShowDialog() == true)
                    Avatar = new BitmapImage(new Uri(ofdPicture.FileName));
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private StringBuilder CheckRegistrationForErrors()
        {
            var errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Fullname) || !new Regex(@"\b([А-я]+\s+[А-я]+\s+[А-я]+\b)|\b([A-z]+\s+[A-z]+\s+[A-z]+\b)").IsMatch(Fullname.Trim()))
                errors.AppendLine(INVALID_FULLNAME_EX);
            if (string.IsNullOrWhiteSpace(Login) || Login.Trim().Length is not >= 6 and <= 50 || !Login.Trim().Any(s => char.IsLetter(s)))
                errors.AppendLine(INVALID_LOGIN_EX);
            if (string.IsNullOrWhiteSpace(Password) || Password.Trim().Length is not >= 6 and <= 50 || !Password.Trim().Any(s => char.IsLetter(s)))
                errors.AppendLine(INVALID_PASSWORD_EX);
            if (string.IsNullOrWhiteSpace(RepeatPassword) || !Password.Trim().Equals(RepeatPassword.Trim()))
                errors.AppendLine(INVALID_REPEAT_PASSWORD_EX);
            if (DateTime.Now.Year - BornDate.Year < 18)
                errors.AppendLine(INVALID_BORNDATE_EX);
            if (string.IsNullOrEmpty(Email) || !new MailAddress(Email.Trim()).Address.Equals(Email.Trim()))
                errors.AppendLine(INVALID_EMAIL_EX);
            if (string.IsNullOrWhiteSpace(Phone) || !new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").IsMatch(Phone.Trim()))
                errors.AppendLine(INVALID_PHONE_NUMBER_EX);

            return errors;
        }

        private void FillProperties()
        {
            try
            {
                OpenAuthorizationWindowCommand = new DelegateCommand(OpenAuthorizationWindow);
                RegisterUserCommand = new DelegateCommand(RegisterUser);
                UploadImageCommand = new DelegateCommand(UploadImage);

                _bornDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);

                FillGenders();
                FillRoles();
                FillCities();
                FillFamilyStatuses();
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async void FillGenders()
        {
            try
            {
                var response = await RequestHelper.GetGendersAsync();
                if (response.IsSuccessStatusCode)
                {
                    Genders = JsonConvert.DeserializeObject<ObservableCollection<Gender>>(await response.Content.ReadAsStringAsync());

                    SelectedGender = Genders[0];
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async void FillRoles()
        {
            try
            {
                var response = await RequestHelper.GetRolesAsync();
                if (response.IsSuccessStatusCode)
                {
                    Roles = JsonConvert.DeserializeObject<ObservableCollection<Role>>(await response.Content.ReadAsStringAsync());
                    Roles?.Remove(Roles?.FirstOrDefault(r => r.Value.Equals("Гость")));
                    Roles?.Remove(Roles?.FirstOrDefault(r => r.Value.Equals("Администратор")));

                    SelectedRole = Roles[0];
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async void FillCities()
        {
            try
            {
                var response = await RequestHelper.GetCitiesAsync();
                if (response.IsSuccessStatusCode)
                {
                    Cities = JsonConvert.DeserializeObject<ObservableCollection<City>>(await response.Content.ReadAsStringAsync());

                    SelectedCity = Cities[0];
                }
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private async void FillFamilyStatuses()
        {
            try
            {
                var response = await RequestHelper.GetFamilyStatusesAsync();
                if (response.IsSuccessStatusCode)
                {
                    FamilyStatuses = JsonConvert.DeserializeObject<ObservableCollection<FamilyStatus>>(await response.Content.ReadAsStringAsync());

                    SelectedFamilyStatus = FamilyStatuses[0];
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