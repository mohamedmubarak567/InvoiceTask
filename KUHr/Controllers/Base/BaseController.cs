using Microsoft.AspNetCore.Mvc;


namespace KUHr.WebAPI.Controllers.Base
{
        /// <inheritdoc />
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class BaseController : ControllerBase
        {

            /// <inheritdoc />
            public BaseController()
            {
            }
        }
        [Route("api/[controller]/[action]")]
        [Produces("application/json")]
        [ApiController]
        public class BaseControllerBasic : ControllerBase
        {

            /// <inheritdoc />
            public BaseControllerBasic()
            {
            }
        
    }
}
