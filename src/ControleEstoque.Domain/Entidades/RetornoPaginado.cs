using Controle_Estoque.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades
{
    [DebuggerStepThrough]
    public class RetornoPaginado<TReturn> where TReturn : class
    {
        public long draw { get; set; } = 1;
        public long recordsTotal { get; set; } = default!;
        public long recordsFiltered { get; set; } = default!;

        public string JsonTypes { get; set; } = default!;
        public IEnumerable<TReturn?> data { get; set; } = default!;

        public RetornoPaginado<TReturn> RetornoVazio(int draw)
        {
            return new RetornoPaginado<TReturn>
            {
                draw = draw,
                recordsTotal = 0,
                recordsFiltered = 0,
                data = new List<TReturn>(),
                JsonTypes = IResponseController.ResponseJsonTypes.Success.ToString().ToLower(culture: CultureInfo.CurrentCulture)
            };
        }
    }
}
