using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1
{
    class Funciones
    {
        public SqlConnection conexion = new SqlConnection("integrated security = true; server=DESKTOP-F385B8I;database=dbo");
    }
}
