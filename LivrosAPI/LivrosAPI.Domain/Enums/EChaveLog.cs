using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EChaveLog
    {
        TEMPO_EXECUCAO,
        EXCEPTION_NAO_TRATADA
    }
}
