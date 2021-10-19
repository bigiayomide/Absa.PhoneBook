using Absa.Application.Common.Mappings;

namespace Absa.Application.PhoneBookEntry
{
    public class PhoneBookEntryDto : IMapFrom<Absa.Domain.Entities.PhoneBookEntry>
    {
        public int Id { get; set; }

        public int PhoneBookId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }
    }
}