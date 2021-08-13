using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class ValidationMessages
    {
        public static string NotEmpty => "{PropertyName} alanı boş olamaz.";
        public static string MaxLength => "{PropertyName} alanına en fazla {MaxLength} karakter girebilirsiniz.";
        public static string MinLength => "{PropertyName} alanına en az {MinLength} karakter girebilirsiniz.";
        public static string OnlyNumber => "{PropertyName} alanına sadece sayı girmelisiniz.";
    }
}
