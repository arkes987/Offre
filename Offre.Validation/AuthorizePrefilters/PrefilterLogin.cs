using System;
using System.Text.RegularExpressions;

namespace Offre.Validation.AuthorizePrefilters
{
    public class PrefilterLogin : IPrefilter
    {
        private const int MaxLoginLength = 128;
        private const short MinLoginLength = 8;

        private const string EmailRegex =
            "\r\n^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$\r\n1\r\n^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
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
            var regex = new Regex(EmailRegex);

            var match = regex.Match(_data);

            return match.Success;
        }
    }
}
