using Absa.Application.Common.Mappings;

namespace Absa.Application.PhoneBookEntry
{
    public class PhoneBookEntryDto : IMapFrom<Absa.Domain.Entities.PhoneBookEntry>
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }
    }
}