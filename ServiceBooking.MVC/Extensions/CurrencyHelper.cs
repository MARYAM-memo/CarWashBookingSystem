namespace ServiceBooking.MVC.Extensions;

public static class CurrencyHelper
{
      public static string ToEGP(this decimal amount)
      {
            return amount.ToString("C", new System.Globalization.CultureInfo("ar-EG"));
      }
}
