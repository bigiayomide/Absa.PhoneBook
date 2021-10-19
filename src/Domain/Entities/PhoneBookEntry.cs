using System.ComponentModel.DataAnnotations.Schema;
using Absa.Domain.Common;

namespace Absa.Domain.Entities
{
    public class PhoneBookEntry : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        [ForeignKey("PhoneBook")]
        public int PhoneBookId { get; set; }

        public PhoneBook PhoneBook { get; set; }
    }
}