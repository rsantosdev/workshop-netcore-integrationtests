using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Workshop.IntegrationTests.Platform.Jwt
{
    public static class JwtSettings
    {
        static SecurityKey _key;
        static SigningCredentials _credentials;
        static string _audience;
        static string _issuer;
        static bool _isInitialized;

        public static SecurityKey Key
        {
            get
            {
                EnsureInitialized();
                return _key;
            }
        }

        public static SigningCredentials Credentials
        {
            get
            {
                EnsureInitialized();
                return _credentials;
            }
        }

        public static string Issuer
        {
            get
            {
                EnsureInitialized();
                return _issuer;
            }
        }

        public static string Audience
        {
            get
            {
                EnsureInitialized();
                return _audience;
            }
        }

        public static void Init()
        {
            if (_isInitialized)
            {
                return;
            }

            var secretKey = GetEnvironmentVariable("WORKSHOP_AUTH_SECRET", "oRxbUHE}uR9xZaKN");

            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            _credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            _issuer = GetEnvironmentVariable("WORKSHOP_AUTH_ISSUER", "localhost");
            _audience = GetEnvironmentVariable("WORKSHOP_AUTH_AUDIENCE", "localhost");

            _isInitialized = true;
        }

        static void EnsureInitialized()
        {
            if (_isInitialized == false)
            {
                throw new InvalidOperationException("JWT settings not initialized yet.");
            }
        }

        static string GetEnvironmentVariable(string key, string defaultValue)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return value;
        }
    }
}
