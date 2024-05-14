using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Commond.Extension
{
    public static class StreamExtensions
    {
        public static void SerializeToJsonAndWrite<T>(this Stream stream, T inputDataObj)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            if (!stream.CanWrite) throw new NotSupportedException("Can't write to this stream.");

            using var streamWriter = new StreamWriter(stream, new UTF8Encoding(), 1024, true);

            using var jsonTextWriter = new JsonTextWriter(streamWriter);
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(jsonTextWriter, inputDataObj);
            jsonTextWriter.Flush();
        }
    }
}
