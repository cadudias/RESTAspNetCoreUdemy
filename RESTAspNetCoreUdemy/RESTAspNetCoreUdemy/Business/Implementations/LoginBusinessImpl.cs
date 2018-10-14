using Microsoft.IdentityModel.Tokens;
using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Security.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace RESTAspNetCoreUdemy.Business.Implementattions
{
    public class LoginBusinessImpl : ILoginBusiness
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfiguration _tokenConfigurations;

        public LoginBusinessImpl(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfiguration;
        }

        public object FindByLogin(UserVO user)
        {
            bool credentialsIsValid = false;
            if (user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = _repository.FindByLogin(user.Login);
                credentialsIsValid = (baseUser != null && user.Login == baseUser.Login && user.AccessKey == baseUser.AccessKey);
            }
            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                        new[]
                        {
                            /// <summary>
                            /// "jti" (JWT ID) Claim (default ID is a GUID)
                            /// </summary>
                            /// <remarks>The "jti" (JWT ID) claim provides a unique identifier for the JWT.
                            ///   The identifier value MUST be assigned in a manner that ensures that
                            ///   there is a negligible probability that the same value will be
                            ///   accidentally assigned to a different data object; if the application
                            ///   uses multiple issuers, collisions MUST be prevented among values
                            ///   produced by different issuers as well.  The "jti" claim can be used
                            ///   to prevent the JWT from being replayed.  The "jti" value is a case-
                            ///   sensitive string.  Use of this claim is OPTIONAL.</remarks>
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                        }
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                // JwtSecurityTokenHandler manipula a segurança do token
                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            // configuração do token
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                // não pode ser menor que a data de criação
                NotBefore = createDate,
                Expires = expirationDate
            });

            // armazena o token e devoolve um hash da informação pra comparar com o que vem na requisição
            // e dizer se esta autenticado ou não
            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to autheticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}