using System.Text.RegularExpressions;

namespace Offre.Validation
{
    public static partial class OffreRegex
    {
        public static readonly Regex HasSpecialChar = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
    }
}
