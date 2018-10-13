using System.Collections.Generic;

namespace RESTAspNetCoreUdemy.Data.Converter
{
    // na entrada o origin pode ser um VO e o destino uma entidade e vice-versa
    public interface IParser<Origin, Destiny>
    {
        Destiny Parse(Origin origin);

        List<Destiny> ParseList(List<Origin> originList);
    }
}