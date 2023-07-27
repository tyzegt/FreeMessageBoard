using FMB.Core.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.APITests
{
    internal class FakeUserManager : UserManager<AppUser>
    {

        public FakeUserManager(IUserStore<AppUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<AppUser> passwordHasher, IEnumerable<IUserValidator<AppUser>> userValidators, IEnumerable<IPasswordValidator<AppUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<AppUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public override async Task<AppUser> FindByNameAsync(string userName)
        {
            return new AppUser()
            {
                UserName = "testUser",
                Id = -1
            };
        }

        public static FakeUserManager GetInstance()
        {
            return new FakeUserManager(
                Mock.Of<IUserStore<AppUser>>(), 
                Mock.Of< IOptions<IdentityOptions>>(), 
                null, null, null, null, null, null, null);
        }
    }
}
