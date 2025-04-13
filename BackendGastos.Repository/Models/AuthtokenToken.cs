namespace BackendGastos.Repository.Models;

public partial class AuthtokenToken
{
    public string Key { get; set; } = null!;

    public DateTime Created { get; set; }

    public long UserId { get; set; }

    public virtual AuthenticationUsuario User { get; set; } = null!;
}
