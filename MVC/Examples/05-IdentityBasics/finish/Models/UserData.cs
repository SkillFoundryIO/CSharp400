using System.Security.Claims;

namespace IdentityBasics.Models
{
    public class UserData
    {
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
