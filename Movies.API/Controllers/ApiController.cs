using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Movies.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/{version}/controller")]
    [ApiController]
    public class ApiController:ControllerBase
    {

    }
}
