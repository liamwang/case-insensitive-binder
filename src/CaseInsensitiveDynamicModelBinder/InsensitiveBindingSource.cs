using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CaseInsensitiveDynamicModelBinder
{
    /// <summary>
    /// A metadata object representing a source of data for model binding.
    /// </summary>
    public class InsensitiveBindingSource : BindingSource
    {
        public InsensitiveBindingSource(string id, string displayName, bool isGreedy, bool isFromRequest)
            : base(id, displayName, isGreedy, isFromRequest)
        {
        }

        /// <summary>
        /// A <see cref="BindingSource"/> for the request body.
        /// </summary>
        public static readonly BindingSource Insensitive = new BindingSource("Insensitive", "Insensitive", isGreedy: true, isFromRequest: true);
    }
}
