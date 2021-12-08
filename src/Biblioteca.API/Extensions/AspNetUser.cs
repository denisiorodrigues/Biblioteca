
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Biblioteca.Business.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.API.Extensions
{
  public class AspNetUser : IUser
  {
	  private readonly IHttpContextAccessor _accessor;

	  public AspNetUser(IHttpContextAccessor accessor)
	  {
		  _accessor = accessor;
	  }

    public string Nome => _accessor.HttpContext.User.Identity.Name;

    public IEnumerable<Claim> GetClaimsIdentity()
    {
      return _accessor.HttpContext.User.Claims;
    }

    public string GetUserEmail()
    {
      return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
    }

    public Guid GetUserId()
    {
      return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.NewGuid();
    }

    public bool IsAuthenticated()
    {
      return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public bool IsInRole(string role)
    {
      return _accessor.HttpContext.User.IsInRole(role);
    }
  }

  public static class ClaimsPrincipalExtensions
  {
    public static string GetUserId(this ClaimsPrincipal principal)
    {
      	if(principal == null)
		{
        	throw new ArgumentNullException(nameof(principal));
      	}

      	var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
      	return  claim?.Value;
    }

	public static string GetUserEmail(this ClaimsPrincipal principal)
    {
      	if(principal == null)
		{
        	throw new ArgumentNullException(nameof(principal));
      	}

      	var claim = principal.FindFirst(ClaimTypes.Email);
      	return  claim?.Value;
    }
  }
}