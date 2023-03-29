using ExceptionHandlingInMiddleware.Exceptions;
using Microsoft.AspNetCore.Mvc;
using NotImplementedException = ExceptionHandlingInMiddleware.Exceptions.NotImplementedException;

namespace ExceptionHandlingInMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }
        [HttpGet]
        [Route("TestExceptions")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status501NotImplemented, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult TestExceptions(int number)
        {
            CheckTheNumber(number);
            return Ok();
        }

        private void CheckTheNumber(int number)
        {
            if (number == 1)
            {
                throw new BadRequestException("Number = 1 is the bad request exception");
            }
            else if (number == 2)
            {
                throw new NotFoundException("Number = 2 is the Not found exception");
            }
            else if (number == 3)
            {
                throw new NotImplementedException("Number = 3 is the Not implemented exception");
            }
            else if (number == 4)
            {
                throw new UnauthorizedException("Number = 4 is the unauthorized exception");
            }
        }

    }
}
