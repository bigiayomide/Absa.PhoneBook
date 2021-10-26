using System;
using System.Collections.Generic;
using System.Linq;
using Absa.Application.Common.Mappings;
using Absa.Application.PhoneBookEntry;
using AutoMapper;

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

        public long PhoneBookEntriesCount { get; set; }

        public IList<PhoneBookEntryDto> PhoneBookEntries { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.PhoneBook, PhoneBookDto>();

            profile.CreateMap<PhoneBookDto, Domain.Entities.PhoneBook>();
        }
    }
}