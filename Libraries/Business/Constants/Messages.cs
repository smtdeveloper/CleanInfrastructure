using Core.Entities.Concrete;
using System.Runtime.Serialization;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AuthorizationDenied = "Erişim için yetkiniz yok.";
        public static string UserRegistered = "Kullanıcı Kayıtlı.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre yanlış.";
        public static string SuccessfulLogin = "Giriş başarılı.";
        public static string MailAlreadyExists = "E-Posta zaten kayıtlı.";
        public static string AccessTokenCreated = "Erişim Tokeni oluşturuldu.";

        public static string PersonAdded = "Kişi oluşturuldu.";
        public static string PersonNotAdded = "Kişi oluşturulamadı.";
        public static string PersonsNotFound = "Kayıtlı kişi/ler bulunamadı.";
        public static string PersonsListed = "Kişiler listelendi.";

        public static string UserNotAdded = "Kullanıcı kayıt edilemedi.";
        public static string UserAdded = "Kullanıcı kayıt edildi.";
        public static string UserBrought = "Kullanıcı getirildi.";

        public static string OperationClaimsNotFoundForUser = "Kullanıcının yetkileri bulunamadı.";
        public static string OperationClaimsListedForUser = "Kullanıcının yetkileri listelendi.";

       
        public static string PasswordsDoNotMatch = "Şifreler eşleşmedi.";
        public static string PasswordsMatched = "Şifreler eşleşti.";
        public static string UserOperationClaimNotAdded = "Kullanıcı Yetkisi kayıt edilemedi.";
        public static string UserOperationClaimAdded = "Kullanıcı Yetkisi kayıt edildi.";
        public static string OperationClaimAdded = "Yetki kayıt edildi.";
        public static string OperationClaimNotAdded = "Yetki kayıt edilemedi.";
        public static string DefaultOperationClaimNotFound = "Varsayılan Yetki bulunamadı.";
        public static string DefaultOperationClaimListed = "Varsayılan Yetki listelendi.";
        public static string OperationClaimDefaultValueAlreadyExist = "Sistemde kayıtlı Varsayılan bir Yetki zaten var.";
        public static string OperationClaimDefaultValueNotFound = "Varsayılan bir Yetki bulunamadı.";
        public static string OperationClaimNameNotFound = "Yetki Adı bulunamadı.";
        public static string OperationClaimNameAlreadyExist = "Yetki Adı zaten kayıtlı.";
        public static string UserOperationClaimAlreadyUsedByUser = "Yetki Kullanıcıda zaten var.";
        public static string UserOperationClaimNotFoundByUser = "Yetki Kullanıcıda yok.";
        public static string UserEmailAlreadyUsed = "Kullanıcı Mail Adresi zaten kayıtlı.";
        public static string UserEmailNotExist = "Kullanıcı Mail Adresi bulunamadı.";
        public static string UsersNotFoundInSystem = "Kullanıcılar sistemde bulunamadı.";
        public static string UsersListed = "Kullanıcılar listelendi.";
        public static string YouAreNotAllowedToUseSystem = "Sistemi kullanma izniniz yok.";
        public static string YouAreAllowedToUseSystem = "Sistemi kullanma izniniz var.";
    }
}
