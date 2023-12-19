using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace P7CreateRestApi.Domain
{
    public class TestUserManager : UserManager<IdentityUser>
    {
        public TestUserManager() : base(
            new Mock<IUserStore<IdentityUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<IdentityUser>>().Object,
            new IUserValidator<IdentityUser>[0],
            new IPasswordValidator<IdentityUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        {
        }
    }
}
    
