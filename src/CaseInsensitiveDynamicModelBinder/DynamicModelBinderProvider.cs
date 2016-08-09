using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;

namespace CaseInsensitiveDynamicModelBinder
{
    public class DynamicModelBinderProvider : IModelBinderProvider
    {
        private readonly IList<IInputFormatter> _formatters;
        private readonly IHttpRequestStreamReaderFactory _readerFactory;

        /// <summary>
        /// Creates a new <see cref="DynamicModelBinderProvider"/>.
        /// </summary>
        /// <param name="formatters">The list of <see cref="IInputFormatter"/>.</param>
        /// <param name="readerFactory">The <see cref="IHttpRequestStreamReaderFactory"/>.</param>
        public DynamicModelBinderProvider(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
        {
            if (formatters == null)
            {
                throw new ArgumentNullException(nameof(formatters));
            }

            if (readerFactory == null)
            {
                throw new ArgumentNullException(nameof(readerFactory));
            }

            _formatters = formatters;
            _readerFactory = readerFactory;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.BindingInfo.BindingSource != null &&
                context.BindingInfo.BindingSource.Id == InsensitiveBindingSource.Insensitive.Id)
            {
                if (_formatters.Count == 0)
                {
                    throw new InvalidOperationException($"'{typeof(MvcOptions).FullName}.{nameof(MvcOptions.InputFormatters)}' must not be empty.");
                }
                return new DynamicModelBinder(_formatters, _readerFactory);
            }

            return null;
        }
    }
}
