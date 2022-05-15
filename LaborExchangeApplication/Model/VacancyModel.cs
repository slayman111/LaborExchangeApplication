using LaborExchangeApi.Models;
using LaborExchangeApplication.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LaborExchangeApplication.Model
{
    public class VacancyModel
    {
        public string CompanyOwnerName { get; set; }
        public string CompanyOwnerEmail { get; set; }
        public string CompanyOwnerPhone { get; set; }
        public BitmapImage CompanyOwnerAvatar { get; set; }
        public string CompanyName { get; set; }
        public BitmapImage CompanyLogotype { get; set; }
        public string Profession { get; set; }
        public string EducationQualification { get; set; }
        public string EducationRank { get; set; }
        public string EducationProfession { get; set; }
        public string WorkDayRequirements { get; set; }
        public string Payment { get; set; }
        public string SpecialistRequirements { get; set; }
        public string Info { get; set; }

        public VacancyModel()
        {
            CompanyOwnerName = string.Empty;
            CompanyOwnerEmail = string.Empty;
            CompanyOwnerPhone = string.Empty;
            CompanyOwnerAvatar = new BitmapImage();
            CompanyName = string.Empty;
            CompanyLogotype = new BitmapImage();
            Profession = string.Empty;
            EducationQualification = string.Empty;
            EducationRank = string.Empty;
            EducationProfession = string.Empty;
            WorkDayRequirements = string.Empty;
            Payment = string.Empty;
            SpecialistRequirements = string.Empty;
            Info = string.Empty;
        }

        public static async Task<VacancyModel> GetModel(Vacancy context)
        {
            Company currentCompany = new Company();
            var response = await RequestHelper.GetCompaniesAsync();
            if (response.IsSuccessStatusCode)
                foreach (var company in JsonConvert.DeserializeObject<List<Company>>(await response.Content.ReadAsStringAsync()))
                    if (company.CompanyHasVacancies.Any(v => v.VacancyId == context.Id))
                        currentCompany = company;

            User companyOwner = new User();
            response = await RequestHelper.GetUsersAsync();
            if (response.IsSuccessStatusCode)
                foreach (var user in JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync()))
                    if (user.UserHasCompanies.Any(c => c.CompanyId.Equals(currentCompany.Id)))
                        companyOwner = user;

            return new()
            {
                CompanyOwnerName = $"{companyOwner.Firstname} {companyOwner.Middlename} {companyOwner.Lastname}",
                CompanyOwnerEmail = companyOwner.Email,
                CompanyOwnerPhone = companyOwner.Phone,
                CompanyOwnerAvatar = companyOwner.Avatar.SequenceEqual(Array.Empty<byte>())
                    ? new BitmapImage(new Uri("pack://application:,,,/Assets/Images/defaultAvatar.png"))
                    : ImageConverter.BitmapImageFromBytes(companyOwner.Avatar),
                CompanyName = currentCompany.Name,
                CompanyLogotype = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/noCompanyPhoto.png")),
                //CompanyLogotype = companyLogotype.SequenceEqual(Array.Empty<byte>())
                //    ? new BitmapImage(new Uri("pack://application:,,,/Assets/Images/noCompanyPhoto.png"))
                //    : ImageConverter.BitmapImageFromBytes(companyLogotype),
                Profession = context.Profession.Value,
                EducationQualification = context.Education.Qualification.Value,
                EducationRank = context.Education.Rank.Value,
                EducationProfession = context.Education.Profession.Value,
                WorkDayRequirements = context.WorkDayRequirements.Value,
                Payment = string.Format("{0:C}", context.Payment),
                SpecialistRequirements = context.SpecialistRequirements,
                Info = context.Info,
            };
        }
    }
}