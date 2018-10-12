using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAspNetCoreUdemy.Model.Base
{
    // Contrato entre atributos
    // e a estrutura da tabela
    // [DataContract]
    //[DataContract]
    public class BaseEntity
    {
        [Column("id")]
        public long? Id
        {
            get; set;
        }
    }
}