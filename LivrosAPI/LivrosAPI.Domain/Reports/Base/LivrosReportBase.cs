using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Reports.Base
{
    public abstract class LivrosReportBase
    {
        public abstract DataTable GetDadosLivro();
    }
}
