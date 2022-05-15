using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LaborExchangeApplication.Core
{
    public class RequestHelper
    {
        private const string DEFAULT_CONNECTION = "https://localhost:44305/api/";

        private static readonly HttpClient _httpClient;

        static RequestHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region GET

        public static async Task<HttpResponseMessage> GetUsersAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "Users");

        public static async Task<HttpResponseMessage> GetGendersAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "Genders");

        public static async Task<HttpResponseMessage> GetCitiesAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "Cities");

        public static async Task<HttpResponseMessage> GetFamilyStatusesAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "FamilyStatuses");

        public static async Task<HttpResponseMessage> GetRolesAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "Roles");

        public static async Task<HttpResponseMessage> GetVacanciesAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "Vacancies");

        public static async Task<HttpResponseMessage> GetCompaniesAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "Companies");

        public static async Task<HttpResponseMessage> GetWorkDayRequirementsAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "WorkDayRequirements");

        public static async Task<HttpResponseMessage> GetProfessionsAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "Professions");

        public static async Task<HttpResponseMessage> GetJobRequestsAsync() =>
            await _httpClient.GetAsync(DEFAULT_CONNECTION + "JobRequests");

        #endregion GET

        #region POST

        public static async Task<HttpResponseMessage> PostUserAsync(HttpContent content) =>
            await _httpClient.PostAsync(DEFAULT_CONNECTION + "Users", content);

        public static async Task<HttpResponseMessage> PostJobRequestAsync(HttpContent content) =>
            await _httpClient.PostAsync(DEFAULT_CONNECTION + "JobRequests", content);

        public static async Task<HttpResponseMessage> PostUserHasJobRequestAsync(HttpContent content) =>
            await _httpClient.PostAsync(DEFAULT_CONNECTION + "UserHasJobRequests", content);

        #endregion POST

        #region PUT

        public static async Task<HttpResponseMessage> PutUserAsync(HttpContent content, int id) =>
            await _httpClient.PutAsync(DEFAULT_CONNECTION + $"Users/{id}", content);

        #endregion PUT

        #region DELETE

        public static async Task<HttpResponseMessage> DeleteJobRequestAsync(int id) =>
            await _httpClient.DeleteAsync(DEFAULT_CONNECTION + $"JobRequests/{id}");

        public static async Task<HttpResponseMessage> DeleteUserHasJobRequestAsync(int userId, int jobRequestId) =>
            await _httpClient.DeleteAsync(DEFAULT_CONNECTION + $"UserHasJobRequests/{userId}/{jobRequestId}");

        #endregion DELETE
    }
}