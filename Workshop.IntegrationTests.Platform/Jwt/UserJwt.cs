using System;

namespace Workshop.IntegrationTests.Platform.Jwt
{
    public class UserJwt
    {
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public Guid UserId { get; set; }
    }
}