using System.Threading.Tasks;
using Absa.Application.Common.Models;
using Absa.Application.Common.Security;
using Absa.Application.PhoneBook.Queries;
using Absa.Application.PhoneBookEntry;
using Absa.Application.PhoneBookEntry.Commands.UpdatePhoneBookEntry;
using Absa.Application.TodoItems.Commands.CreatePhoneBookEntry;
using Absa.Application.TodoItems.Commands.DeletePhoneBookEntry;
using Microsoft.AspNetCore.Mvc;

namespace Absa.WebUI.Controllers
{
    [Authorize]
    public class PhoneBookEntryController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<PhoneBookEntryDto>>> GetTodoItemsWithPagination([FromQuery] GetPhoneBookEntryWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePhoneBookEntryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePhoneBookEntryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePhoneBookEntryCommand { Id = id });

            return NoContent();
        }
    }
}
