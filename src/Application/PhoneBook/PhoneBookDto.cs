using System.Collections.Generic;
using Absa.Application.Common.Mappings;
using Absa.Application.PhoneBookEntry;

namespace Absa.Application.PhoneBook
{
    public class PhoneBookVm
    {
        public IList<PhoneBookDto> PhoneBooks { get; set; }
    }
    public class PhoneBookDto : IMapFrom<Domain.Entities.PhoneBook>
    {
        public PhoneBookDto()
        {
            PhoneBookEntries = new List<PhoneBookEntryDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<PhoneBookEntryDto> PhoneBookEntries { get; set; }
    }
}