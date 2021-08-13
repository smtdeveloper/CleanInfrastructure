using System.Text;

namespace Business.Validators.FluentValidation.CustomValidates
{
    /// <summary>
    /// Sadece sayı kontrolü yapan validator classı.
    /// </summary>
    internal static class OnlyNumberValidate
    {
        /// <summary>
        /// Sayı kontrolü yapar.
        /// </summary>
        /// <param name="text">Sayı kontrolü yapılacak yazı.</param>
        /// <returns>Sadece sayı var ise true, sayıdan başka karakterler bulunursa false döner.</returns>
        internal static bool Check(string text)
        {
            bool identityNumberValid = true;

            byte[] asciiCodes = Encoding.UTF8.GetBytes(text.ToCharArray());
            foreach (var item in asciiCodes)
            {
                if (!(item >= 48 && item <= 57)) // 0 between 9 number ascii.
                {
                    identityNumberValid = false;
                    break;
                }
            }

            return identityNumberValid;
        }
    }
}
