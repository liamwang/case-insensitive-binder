using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CaseInsensitiveDynamicModelBinder
{
    public class DynamicModelBinder : IModelBinder
    {
        private readonly IList<IInputFormatter> _formatters;
        private readonly Func<Stream, Encoding, TextReader> _readerFactory;

        public DynamicModelBinder(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
        {
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));
            if (readerFactory == null) throw new ArgumentNullException(nameof(readerFactory));

            _formatters = formatters;
            _readerFactory = readerFactory.CreateReader;
        }


        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var modelBindingKey = bindingContext.BinderModelName ?? string.Empty;

            var httpContext = bindingContext.HttpContext;

            var formatterContext = new InputFormatterContext(
                httpContext,
                modelBindingKey,
                bindingContext.ModelState,
                bindingContext.ModelMetadata,
                _readerFactory);

            var formatter = (IInputFormatter)null;
            for (var i = 0; i < _formatters.Count; i++)
            {
                if (_formatters[i].CanRead(formatterContext))
                {
                    formatter = _formatters[i];
                    break;
                }
            }

            if (formatter == null)
            {
                var exception = new UnsupportedContentTypeException($"Unsupported content type '{httpContext.Request.ContentType}'.");
                bindingContext.ModelState.AddModelError(modelBindingKey, exception, bindingContext.ModelMetadata);
                return;
            }

            try
            {
                var result = await formatter.ReadAsync(formatterContext);
                if (result.HasError)
                {
                    // Formatter encountered an error. Do not use the model it returned.
                    return;
                }

                var jObject = result.Model as JObject;
                var model = new InsensitiveDynamicDictionary();
                foreach (var m in jObject)
                {
                    model.SetMember(m.Key, m.Value);
                }

                bindingContext.Result = ModelBindingResult.Success(model);
                return;
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(modelBindingKey, ex, bindingContext.ModelMetadata);
                return;
            }
        }
    }
}
