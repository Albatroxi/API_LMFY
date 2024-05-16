namespace API_LMFY.Helper.users
{
    public interface IEmails
    {
        bool SendMail(string email, string subject, string message );
    }
}
