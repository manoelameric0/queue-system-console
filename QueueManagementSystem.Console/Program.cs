using QueueManagementSystem.Console.Services;
using QueueManagementSystem.Console.UI;

IQueueService _service = new QueueService();
Menu _menu = new Menu();

_menu.Executar(_service);
