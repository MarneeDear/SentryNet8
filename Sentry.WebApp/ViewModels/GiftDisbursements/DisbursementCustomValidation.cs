using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels.GiftDisbursements
{
    public class CustomDebitAccountAllDigitsValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var debitAccount = (string)value;
            var test = debitAccount.Replace("-", "");
            foreach (char c in test)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
        
}
