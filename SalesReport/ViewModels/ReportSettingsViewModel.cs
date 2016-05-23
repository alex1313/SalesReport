namespace SalesReport.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReportSettingsViewModel
    {
        [Display(Name = "Start date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Recipient email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please, enter the recipient email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string RecipientEmail { get; set; }
    }
}