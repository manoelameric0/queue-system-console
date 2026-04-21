using QueueManagementSystem.Core.Policies;
using QueueManagementSystem.Core.Repositories;
using QueueManagementSystem.Core.Services;
using QueueManagementSystem.Console.UI;
using QueueManagementSystem.Core;


var _repository = new InMemoryQueueRepository();
var _policy = new CallOrderPolicy();
var _service = new QueueService(_repository, _policy);
var _menu = new Menu(_service, _policy);

_menu.Executar();


