using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Viewmodel.User
{
    public class AccountViewModel
    {
        [Display(Name = "name")]
        public string Employee_name { get; set; }
    }
}
