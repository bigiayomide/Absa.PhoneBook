using Absa.Application.Common.Interfaces;
using System;

namespace Absa.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
