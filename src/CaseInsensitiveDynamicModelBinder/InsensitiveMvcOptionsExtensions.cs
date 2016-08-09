using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using System.Buffers;

namespace CaseInsensitiveDynamicModelBinder
{
    public static class InsensitiveMvcOptionsExtensions
    {
        public static void InsertCaseInsensitiveDynamicModelBinder(this MvcOptions options)
        {
            var readerFactory = new MemoryPoolHttpRequestStreamReaderFactory(ArrayPool<byte>.Shared, ArrayPool<char>.Shared);
            options.ModelBinderProviders.Insert(0, new DynamicModelBinderProvider(options.InputFormatters, readerFactory));
        }
    }
}
