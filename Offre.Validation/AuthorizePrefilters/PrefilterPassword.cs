using System;
using System.Text.RegularExpressions;

namespace Offre.Validation.AuthorizePrefilters
{
    public class PrefilterPassword : IPrefilter
    {
        private const int MaxPasswordLength = 128;
        private const short MinPasswordLength = 8;

        private const string PasswordRegex =
            "/(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}/ ";
        private readonly string _data;
        public PrefilterPassword(string data)
        {
            _data = data;
        }
        public bool MatchPrefilter()
        {
            if (!MatchNotEmpty())
                throw new ArgumentException("Password cannot be empty.");

            if (!MatchMaxLength())
                throw new ArgumentException("Password too long.");

            if (!MatchMinLength())
                throw new ArgumentException("Password too short.");

            if (!MatchPassword())
                throw new ArgumentException("Password does not match rules.");

            return true;
        }
        private bool MatchMaxLength()
        {
            return _data.Length <= MaxPasswordLength;
        }
        private bool MatchMinLength()
        {
            return _data.Length > MinPasswordLength;
        }
        private bool MatchNotEmpty()
        {
            return !string.IsNullOrWhiteSpace(_data);
        }

        private bool MatchPassword()
        {
            var regex = new Regex(PasswordRegex);

            var match = regex.Match(_data);

            return match.Success;
        }
    }
}
