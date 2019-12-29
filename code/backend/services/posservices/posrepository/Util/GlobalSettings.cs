using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace posrepository
{
    public enum CSTATUS : int
    {
        ACTIVO = 1,
        INACTIVO = 2,
        ELIMINADO = 3,
    }

    public class GlobalSettings
    {
        
    }

    public class PosUtil 
    {
        public static long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            // epoch epoca
            return epoch;
        }
    }
}
