using System.Collections.Generic;
using System.Threading.Tasks;
using Absa.Application.Common.Security;
using Absa.Application.PhoneBook;
using Absa.Application.PhoneBook.Commands.CreatePhoneBook;
using Application.PhoneBook.Commands.DeletePhoneBook;
using Application.PhoneBook.Commands.UpdatePhoneBook;
using Application.PhoneBook.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Absa.WebUI.Controllers
{
    [Authorize]
    public class PhoneBookController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PhoneBookVm>> Get()
        {
            return await Mediator.Send(new GetPhoneBookQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePhoneBookCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePhoneBookCommand command)
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
            await Mediator.Send(new DeletePhoneBookCommand { Id = id });

            return NoContent();
        }
    }
}