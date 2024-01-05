using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string? UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        
    }
}
