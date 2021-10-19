using System.Collections.Generic;
using Absa.Domain.Common;

namespace Absa.Domain.Entities
{
    public class PhoneBook : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<PhoneBookEntry> PhoneBookEntries { get; private set; } = new List<PhoneBookEntry>();
    }
}