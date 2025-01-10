using Flunt.Notifications;
using LivrosAPI.Application.Contracts.Jwt;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.Identity.Commands;
using LivrosAPI.Application.Responses;
using LivrosAPI.Identity.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LivrosAPI.Identity.Services.Commands
{
    public class LoginService : IRequestHandler<LoginCommand, RetornoService>
    {
        private readonly IJwtService _jwtService;
        private readonly IApplicationUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        private RetornoService response;

        public LoginService(
            IJwtService jwtService,
            IApplicationUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IClientRepository clientRepository,
            UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _clientRepository = clientRepository;
            _userManager = userManager;
            response = new RetornoService();
        }

        public async Task<RetornoService> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Validate(request);
            response.Sucesso = true;

            if (response.TemMensagens)
            {
                response.Sucesso = false;
                return response;
            }

            ApplicationUser user = null;

            if (request.GrantType.Equals("password"))
            {
                user = await HandleUserAuthentication(request);
            }
            else if (request.GrantType.Equals("refresh_token"))
            {
                user = await HandleRefreshToken(request);
            }

            if (response.TemMensagens || user == null)
            {
                response.Sucesso = false;
                return response;
            }

            await HandleTokenJwt(user, request);

            return response;
        }

        private async Task HandleTokenJwt(ApplicationUser user, LoginCommand request)
        {
            var roles = await _userManager.GetRolesAsync(user);

            //pode puxar aqui o perfil e adicionar no claims

            int? idPerfil = null;

            var jwt = _jwtService.CreateJsonWebToken(user, roles, idPerfil);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            jwt.RefreshToken.IssuedUtc = DateTime.UtcNow;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            jwt.RefreshToken.ClientId = request.ClientId;

            await _refreshTokenRepository.Save(jwt.RefreshToken);

            response.AddValue(new
            {
                access_token = jwt.AccessToken,
                refresh_token = jwt.RefreshToken.Token,
                token_type = jwt.TokenType,
                expires_in = jwt.ExpiresIn,
                email = user.Email
            });
        }

        private async Task<ApplicationUser> HandleUserAuthentication(LoginCommand request)
        {

            var client = await _clientRepository.Get(request.ClientId);

            if (client == null)
            {
                response.AddNotification(new Notification("user", "O cliente não está registrado no sistema"));
            }

            var usuario = await _userRepository.Get(request.Email);

            if (usuario == null)
            {
                response.AddNotification(new Notification("user", "O usuário não foi encontrado"));
            }

            var userAtenticado = await _userManager.CheckPasswordAsync(usuario, request.Password);

            if (!userAtenticado)
            {
                usuario = null;
                response.AddNotification(new Notification("user", "A senha digitada está incorreta"));
            }

            return usuario;
        }

        private async Task<ApplicationUser> HandleRefreshToken(LoginCommand request)
        {
            var token = await _refreshTokenRepository.Get(request.RefreshToken);

            if (token == null)
            {
                response.AddNotification(new Notification(nameof(request.RefreshToken), "Refresh Token inválido"));
            }
            else if (token.ExpiresUtc < DateTime.Now)
            {
                response.AddNotification(new Notification(nameof(request.RefreshToken), "Refresh Token expirado"));
            }

            if (response.TemMensagens)
            {
                return null;
            }

            return await _userRepository.Get(token.UserName);
        }

        private void Validate(LoginCommand request)
        {
            if (string.IsNullOrEmpty(request.ClientId))
            {
                var notification = new Notification("clientId", "A permissão deve ser enviada");
                response.AddNotification(notification);
            }

            if (string.IsNullOrEmpty(request.GrantType))
            {
                var notification = new Notification("grantType", "O tipo de autenticação não pode ficar vazio");
                response.AddNotification(notification);
            }
            else
            {
                if (!request.GrantType.Equals("refresh_token") && !request.GrantType.Equals("password"))
                {
                    var notification = new Notification("grantType", "Tipo de autenticação inválido");
                    response.AddNotification(notification);
                }

                if (request.GrantType.Equals("refresh_token"))
                {
                    if (string.IsNullOrEmpty(request.RefreshToken))
                    {
                        var notification = new Notification("refresh_token", "O refresh token não pode ficar vazio");
                        response.AddNotification(notification);
                    }

                }
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                var notification = new Notification("password", "O campo de senha é obrigatório");
                response.AddNotification(notification);
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                var notification = new Notification("email", "O campo de email é obrigatório");
                response.AddNotification(notification);

                if (!response.EmailValido(request.Email))
                {
                    notification = new Notification("email", "O campo de email está inválido");
                    response.AddNotification(notification);
                }
            }

        }
    }
}
