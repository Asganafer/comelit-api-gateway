using Microsoft.AspNetCore.Mvc;

namespace ComelitApiGateway.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IConfiguration _config;
        

        public BaseController(IConfiguration config)
        {
            _config = config;
        }

        

        
    }
}
