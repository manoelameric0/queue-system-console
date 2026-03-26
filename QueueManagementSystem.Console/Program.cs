using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Repositories;
using QueueManagementSystem.Console.Services;
using QueueManagementSystem.Console.UI;


var _repository = new InMemoryQueueRepository();
var _policy = new CallOrderPolicy();
var _service = new QueueService(_repository, _policy);
var _menu = new Menu(_service, _policy);

_menu.Executar();


