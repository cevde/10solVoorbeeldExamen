using Microsoft.AspNetCore.Mvc.Filters;
using System;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Filters
{
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class KlantFilter:  ActionFilterAttribute
    {
        private readonly IKlantRepository _klantRepository;

        public KlantFilter(IKlantRepository klantRepository)
        {
            _klantRepository = klantRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["klant"] = context.HttpContext.User.Identity.IsAuthenticated? _klantRepository.GetByEmail(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
    }
}

