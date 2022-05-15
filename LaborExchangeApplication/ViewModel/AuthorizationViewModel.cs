using LaborExchangeApi.Models;
using LaborExchangeApplication.Command;
using LaborExchangeApplication.Core;
using LaborExchangeApplication.Model;
using LaborExchangeApplication.View.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaborExchangeApplication.ViewModel
{
    public class AuthorizationViewModel : MainViewModel
    {
        #region Constants

        private const string INCORRECT_LOGIN_OR_PASSWORD_EX = "Неправильный логин или пароль";
        private const string INTERNAL_SERVER_ERROR_EX = "Ошибка подключения к серверу";
        private const string LOGIN_OR_PASSWORD_IS_NULL_OR_WHITE_SPACE_EX = "Поля логина и пароля должны быть заполнены";
        private const string ROLE_NOT_FOUND_EX = "Роль пользователя не распознана";

        #endregion Constants

        #region Fields

        private string _login;
        private string _password;

        #endregion Fields

        #region Properties

        public string Login { get => _login; set => SetProperty(ref _login, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        public ICommand SignInCommand { get; private set; }
        public ICommand OpenRegistrationWindowCommand { get; private set; }

        #endregion Properties

        #region Methods

        public AuthorizationViewModel()
        {
            SignInCommand = new DelegateCommand(SignIn);
            OpenRegistrationWindowCommand = new DelegateCommand(OpenRegistrationWindow);
        }

        private async void SignIn(object obj)
        {
            try
            {
                if (!(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)))
                {
                    var response = await RequestHelper.GetUsersAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var user = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync())
                            ?.FirstOrDefault(u => u.Login.Equals(Login) && u.Password.Equals(Password));
                        if (user is not null)
                        {
                            LogginedUser.SetUser(user);
                            switch ((RoleEnum)user.RoleId)
                            {
                                case RoleEnum.Applicant: new ApplicantWindow().Show(); break;
                                case RoleEnum.Employer: new EmployerWindow().Show(); break;
                                default: throw new Exception(ROLE_NOT_FOUND_EX);
                            }

                            Application.Current?.Windows.Cast<Window>()?.FirstOrDefault(window => window is AuthorizationWindow)?.Close();
                        }
                        else throw new Exception(INCORRECT_LOGIN_OR_PASSWORD_EX);
                    }
                    else throw new Exception(INTERNAL_SERVER_ERROR_EX);
                }
                else throw new Exception(LOGIN_OR_PASSWORD_IS_NULL_OR_WHITE_SPACE_EX);
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
        }

        private void OpenRegistrationWindow(object obj)
        {
            new RegistrationWindow().Show();

            Application.Current?.Windows.Cast<Window>()?.FirstOrDefault(window => window is AuthorizationWindow)?.Close();
        }

        #endregion Methods
    }
}