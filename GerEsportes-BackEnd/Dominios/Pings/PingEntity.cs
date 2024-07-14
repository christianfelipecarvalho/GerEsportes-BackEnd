using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerEsportes_BackEnd.Dominios.Pings
{
    public class PingEntity
    {
        public int Id { get; set; }
        public string Reponse { get; set; }
    }
}
