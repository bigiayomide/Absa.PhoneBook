using Absa.Application.Common.Mappings;
using Absa.Domain.Entities;

namespace Absa.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
