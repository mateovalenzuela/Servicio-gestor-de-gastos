namespace BackendGastos.Repository.Models;

public partial class TokenBlacklistOutstandingtoken
{
    public long Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public long? UserId { get; set; }

    public string Jti { get; set; } = null!;

    public virtual TokenBlacklistBlacklistedtoken? TokenBlacklistBlacklistedtoken { get; set; }

    public virtual AuthenticationUsuario? User { get; set; }
}
