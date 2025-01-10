namespace LivrosAPI.Application.Models
{
    public class MySettings
    {
        public string? PortalUrl { get; set; }
        public string? PortalUrlResetPassword { get; set; }
        public string? PortalUrlResetedPassword { get; set; }
        public string? PortalUrlConviteEmail { get; set; }
        public string? PortalUrlEsqueciSenha { get; set; }
        public int? MemoryCacheTempoExpiracaoMinutos { get; set; }
    }
}
