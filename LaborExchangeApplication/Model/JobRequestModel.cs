using LaborExchangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaborExchangeApplication.Model
{
    public class JobRequestModel
    {
        public int Id { get; set; }
        public string ProfessionName { get; set; }
        public string SalaryRequirements { get; set; }
        public string WorkDayRequirementsName { get; set; }
        public string Info { get; set; }

        public JobRequestModel()
        {
            ProfessionName = string.Empty;
            SalaryRequirements = string.Empty;
            WorkDayRequirementsName = string.Empty;
            Info = string.Empty;
        }

        public static JobRequestModel GetModel(JobRequest context) =>
            new()
            {
                Id = context.Id,
                ProfessionName = context.Profession.Value,
                SalaryRequirements = string.Format("{0:C}", context.SalaryRequirements),
                WorkDayRequirementsName = context.WorkDayRequirements.Value,
                Info = context.Info
            };
    }
}