using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace businessEntities
{
    public class CPE_BAJA
    {
        public string NRO_DOCUMENTO_EMPRESA { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string CODIGO { get; set; }
        public string SERIE { get; set; }
        public string SECUENCIA { get; set; }
        public string FECHA_REFERENCIA { get; set; }
        public string FECHA_BAJA { get; set; }
        public Nullable<int> TIPO_PROCESO { get; set; }
        public string TICKET { get; set; }
        public string USUARIO_SOL_EMPRESA { get; set; }
        public string PASS_SOL_EMPRESA { get; set; }
        public string CONTRA_FIRMA { get; set; }

        public string COD_RESPUESTA { get; set; }
        public string UBIGEO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }
        public string DIRECCION { get; set; }

        public string DESCRIPCION_RESPUESTA { get; set; }
        public string HASH_CPE { get; set; }
        public string HASH_CDR { get; set; }
        public string FIRMA_EMP { get; set; }
        
        /////////detalle////////
        public List<CPE_BAJA_DETALLE> detalle = new List<CPE_BAJA_DETALLE>();
    }
}
