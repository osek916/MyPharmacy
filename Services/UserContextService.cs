﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MyPharmacy.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
        string Role { get;  }
        public int? PharmacyId { get; }
    }
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User; 
        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public string Role => User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Role).Value.ToString();
        public int? PharmacyId => User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == "PharmacyId").Value);
        
    }
}
