using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorUI.Services
{
    internal class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();
        internal const string c_LocalStorageKey = "bearerToken";

        //This is where the magic happens

        public AppAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var savedtoken = await localStorage.GetItemAsync<string>(c_LocalStorageKey);

                if(string.IsNullOrWhiteSpace(savedtoken))
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                var token = _tokenHandler.ReadJwtToken(savedtoken);
                
                if(token.ValidTo < DateTime.UtcNow)
                {
                    await localStorage.RemoveItemAsync(c_LocalStorageKey);  
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var claims = ParseClaims(token);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

                return new AuthenticationState(user);

            } catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        private IList<Claim> ParseClaims(JwtSecurityToken token)
        {
            IList<Claim> claims = token.Claims.ToList();
            
            //Subject is the username/email
            claims.Add(new Claim(ClaimTypes.Name, token.Subject));

            return claims;
        }

        internal async Task SignIn()
        {
            var savedtoken = await localStorage.GetItemAsync<string>(c_LocalStorageKey);
            var securityToken = _tokenHandler.ReadJwtToken(savedtoken);

            var claims = ParseClaims(securityToken);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        internal void SignOut()
        {
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
