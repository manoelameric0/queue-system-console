using QueueManagementSystem.Console.Repositories;
using QueueManagementSystem.Console.Services;
using QueueManagementSystem.Console.UI;


var _repository = new InMemoryQueueRepository();
var _service = new QueueService(_repository);
Menu _menu = new Menu();

_menu.Executar(_service);
