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

        protected void ManageException(Exception ex)
        {
            Console.WriteLine("********************************");
            Console.WriteLine(ex);
            Console.WriteLine("********************************");
        }

        
    }
}
