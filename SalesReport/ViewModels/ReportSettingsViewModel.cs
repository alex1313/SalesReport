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

        [Required]
        [Display(Name = "Recipient email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please, enter the recipient email")]
        public string RecipientEmail { get; set; }
    }
}