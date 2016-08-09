using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace CaseInsensitiveDynamicModelBinder
{
    /// <summary>
    /// Specifies that properties should be case-insensitive and bound 
    /// using the <see cref="InsensitiveDynamicDictionary"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InsensitiveAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource
        {
            get
            {
                return InsensitiveBindingSource.Insensitive;
            }
        }
    }
    
}
