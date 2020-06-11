using System.Text.RegularExpressions;

namespace Offre.Validation
{
    public static partial class OffreRegex
    {
        public static readonly Regex HasUpperChar = new Regex(@"[A-Z]+");
    }
}
