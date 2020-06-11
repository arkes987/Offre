using System;
using System.Net.Mail;

namespace Offre.Validation.AuthorizePrefilters
{
    public class PrefilterLogin : IPrefilter
    {
        private const int MaxLoginLength = 128;
        private const short MinLoginLength = 8;
        private readonly string _data;
        public PrefilterLogin(string data)
        {
            _data = data;
        }
        public bool MatchPrefilter()
        {
            if (!MatchNotEmpty())
                throw new ArgumentException("Login cannot be empty.");

            if (!MatchMaxLength())
                throw new ArgumentException("Login too long.");

            if (!MatchMinLength())
                throw new ArgumentException("Login too short.");

            if (!MatchEmail())
                throw new ArgumentException("Login is not valid email.");

            return true;
        }
        private bool MatchMaxLength()
        {
            return _data.Length <= MaxLoginLength;
        }
        private bool MatchMinLength()
        {
            return _data.Length > MinLoginLength;
        }
        private bool MatchNotEmpty()
        {
            return !string.IsNullOrWhiteSpace(_data);
        }

        private bool MatchEmail()
        {
            try
            {
                var m = new MailAddress(_data);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
