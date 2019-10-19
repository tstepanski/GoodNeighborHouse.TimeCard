using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Web
{
    public class ApplyPolicyOrAuthorizeFilter : AuthorizeFilter
    {
        public ApplyPolicyOrAuthorizeFilter(AuthorizationPolicy policy) : base(policy) { }

        public ApplyPolicyOrAuthorizeFilter(IAuthorizationPolicyProvider policyProvider, IEnumerable<IAuthorizeData> authorizeData)
            : base(policyProvider, authorizeData) { }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorized = context
                .Filters
                .OfType<AuthorizeFilter>()
                .Any(authorizeFilter => !ReferenceEquals(authorizeFilter, this) && (authorizeFilter.AuthorizeData?.Any() ?? false));

            return authorized ? Task.CompletedTask : base.OnAuthorizationAsync(context);
        }
    }
}