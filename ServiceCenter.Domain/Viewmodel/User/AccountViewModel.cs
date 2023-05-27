using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Domain.Viewmodel.User
{
    public class AccountViewModel
    {
        [Display(Name = "Наименование")]
        public string Employee_name { get; set; }
    }
}
