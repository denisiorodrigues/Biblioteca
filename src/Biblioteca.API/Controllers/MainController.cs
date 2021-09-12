using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        //private readonly ILogger<MainController> _logger;

        // public MainController(ILogger<MainController> logger)
        // {
        //     _logger = logger;
        // }

        public MainController()
        {
            
        }
    }
}