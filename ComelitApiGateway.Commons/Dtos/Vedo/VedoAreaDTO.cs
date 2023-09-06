using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Dtos.Vedo
{
    /// <summary>
    /// This class describe an Area.
    /// One Area can be subdivided into multiple Zone
    /// </summary>
    public class VedoAreaDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
