namespace BackendGastos.Repository.Models;

public partial class TokenBlacklistBlacklistedtoken
{
    public long Id { get; set; }

    public DateTime BlacklistedAt { get; set; }

    public long TokenId { get; set; }

    public virtual TokenBlacklistOutstandingtoken Token { get; set; } = null!;
}
