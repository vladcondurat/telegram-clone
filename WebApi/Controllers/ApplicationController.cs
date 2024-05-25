using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;
using Services.Features.Auth.Jwt;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        protected int? UserId
        {
            get
            {
                if (HttpContext is null || HttpContext.User is null)
                {
                    return null;
                }

                var currentUser = HttpContext.User;

                if (!currentUser.HasClaim(c => c.Type == JwtClaims.Id))
                {
                    return null;
                }

                var contextUserId = currentUser.Claims.FirstOrDefault(user => user.Type == JwtClaims.Id)!.Value;
                var isParsed = int.TryParse(contextUserId, out int userId);

                return isParsed ? userId : null;
            }
        }

        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                throw new BusinessException("Model is not valid");
            }
        }

        protected void ValidateUserId()
        {
            if (UserId is null)
            {
                throw new ArgumentNullException(nameof(UserId), "User Id not found");
            }
        }
    }
}