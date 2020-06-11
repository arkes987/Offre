using System.Text.RegularExpressions;

namespace Offre.Validation
{
    public static partial class OffreRegex
    {
        public static readonly Regex HasNumber = new Regex(@"[0-9]+");
    }
}
