using Microsoft.EntityFrameworkCore;

namespace BackendGastos.Repository.Models;

public partial class ProyectoGastosTestContext : DbContext
{
    public ProyectoGastosTestContext(DbContextOptions<ProyectoGastosTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthGroup> AuthGroups { get; set; }

    public virtual DbSet<AuthGroupPermission> AuthGroupPermissions { get; set; }

    public virtual DbSet<AuthPermission> AuthPermissions { get; set; }

    public virtual DbSet<AuthenticationPerfil> AuthenticationPerfils { get; set; }

    public virtual DbSet<AuthenticationUsuario> AuthenticationUsuarios { get; set; }

    public virtual DbSet<AuthenticationUsuarioGroup> AuthenticationUsuarioGroups { get; set; }

    public virtual DbSet<AuthenticationUsuarioUserPermission> AuthenticationUsuarioUserPermissions { get; set; }

    public virtual DbSet<AuthtokenToken> AuthtokenTokens { get; set; }

    public virtual DbSet<DjangoAdminLog> DjangoAdminLogs { get; set; }

    public virtual DbSet<DjangoContentType> DjangoContentTypes { get; set; }

    public virtual DbSet<DjangoMigration> DjangoMigrations { get; set; }

    public virtual DbSet<DjangoSession> DjangoSessions { get; set; }

    public virtual DbSet<GastosCategoriagasto> GastosCategoriagastos { get; set; }

    public virtual DbSet<GastosCategoriaigreso> GastosCategoriaigresos { get; set; }

    public virtual DbSet<GastosGasto> GastosGastos { get; set; }

    public virtual DbSet<GastosIngreso> GastosIngresos { get; set; }

    public virtual DbSet<GastosMonedum> GastosMoneda { get; set; }

    public virtual DbSet<GastosSubcategoriagasto> GastosSubcategoriagastos { get; set; }

    public virtual DbSet<GastosSubcategoriaingreso> GastosSubcategoriaingresos { get; set; }

    public virtual DbSet<TokenBlacklistBlacklistedtoken> TokenBlacklistBlacklistedtokens { get; set; }

    public virtual DbSet<TokenBlacklistOutstandingtoken> TokenBlacklistOutstandingtokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_group_pkey");

            entity.ToTable("auth_group");

            entity.HasIndex(e => e.Name, "auth_group_name_a6ea08ec_like").HasOperators(new[] { "varchar_pattern_ops" });

            entity.HasIndex(e => e.Name, "auth_group_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AuthGroupPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_group_permissions_pkey");

            entity.ToTable("auth_group_permissions");

            entity.HasIndex(e => e.GroupId, "auth_group_permissions_group_id_b120cbf9");

            entity.HasIndex(e => new { e.GroupId, e.PermissionId }, "auth_group_permissions_group_id_permission_id_0cd325b0_uniq").IsUnique();

            entity.HasIndex(e => e.PermissionId, "auth_group_permissions_permission_id_84c5c92e");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");

            entity.HasOne(d => d.Group).WithMany(p => p.AuthGroupPermissions)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_group_permissions_group_id_b120cbf9_fk_auth_group_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.AuthGroupPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_group_permissio_permission_id_84c5c92e_fk_auth_perm");
        });

        modelBuilder.Entity<AuthPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_permission_pkey");

            entity.ToTable("auth_permission");

            entity.HasIndex(e => e.ContentTypeId, "auth_permission_content_type_id_2f476e4b");

            entity.HasIndex(e => new { e.ContentTypeId, e.Codename }, "auth_permission_content_type_id_codename_01ab375a_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codename)
                .HasMaxLength(100)
                .HasColumnName("codename");
            entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.ContentType).WithMany(p => p.AuthPermissions)
                .HasForeignKey(d => d.ContentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_permission_content_type_id_2f476e4b_fk_django_co");
        });

        modelBuilder.Entity<AuthenticationPerfil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authentication_perfil_pkey");

            entity.ToTable("authentication_perfil");

            entity.HasIndex(e => e.UsuarioId, "authentication_perfil_usuario_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Imagen)
                .HasMaxLength(200)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithOne(p => p.AuthenticationPerfil)
                .HasForeignKey<AuthenticationPerfil>(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authentication_perfi_usuario_id_4a6ffbad_fk_authentic");
        });

        modelBuilder.Entity<AuthenticationUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authentication_usuario_pkey");

            entity.ToTable("authentication_usuario");

            entity.HasIndex(e => e.Email, "authentication_usuario_email_4395e031_like").HasOperators(new[] { "varchar_pattern_ops" });

            entity.HasIndex(e => e.Email, "authentication_usuario_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "authentication_usuario_username_2a253d7a_like").HasOperators(new[] { "varchar_pattern_ops" });

            entity.HasIndex(e => e.Username, "authentication_usuario_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.EmailConfirmado).HasColumnName("email_confirmado");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsStaff).HasColumnName("is_staff");
            entity.Property(e => e.IsSuperuser).HasColumnName("is_superuser");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AuthenticationUsuarioGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authentication_usuario_groups_pkey");

            entity.ToTable("authentication_usuario_groups");

            entity.HasIndex(e => e.GroupId, "authentication_usuario_groups_group_id_4f39557c");

            entity.HasIndex(e => e.UsuarioId, "authentication_usuario_groups_usuario_id_0da0c40a");

            entity.HasIndex(e => new { e.UsuarioId, e.GroupId }, "authentication_usuario_groups_usuario_id_group_id_785ce8d9_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Group).WithMany(p => p.AuthenticationUsuarioGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authentication_usuar_group_id_4f39557c_fk_auth_grou");

            entity.HasOne(d => d.Usuario).WithMany(p => p.AuthenticationUsuarioGroups)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authentication_usuar_usuario_id_0da0c40a_fk_authentic");
        });

        modelBuilder.Entity<AuthenticationUsuarioUserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authentication_usuario_user_permissions_pkey");

            entity.ToTable("authentication_usuario_user_permissions");

            entity.HasIndex(e => new { e.UsuarioId, e.PermissionId }, "authentication_usuario_u_usuario_id_permission_id_731b2cda_uniq").IsUnique();

            entity.HasIndex(e => e.PermissionId, "authentication_usuario_user_permissions_permission_id_0bfdaa25");

            entity.HasIndex(e => e.UsuarioId, "authentication_usuario_user_permissions_usuario_id_ee98284a");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.AuthenticationUsuarioUserPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authentication_usuar_permission_id_0bfdaa25_fk_auth_perm");

            entity.HasOne(d => d.Usuario).WithMany(p => p.AuthenticationUsuarioUserPermissions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authentication_usuar_usuario_id_ee98284a_fk_authentic");
        });

        modelBuilder.Entity<AuthtokenToken>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("authtoken_token_pkey");

            entity.ToTable("authtoken_token");

            entity.HasIndex(e => e.Key, "authtoken_token_key_10f0b77e_like").HasOperators(new[] { "varchar_pattern_ops" });

            entity.HasIndex(e => e.UserId, "authtoken_token_user_id_key").IsUnique();

            entity.Property(e => e.Key)
                .HasMaxLength(40)
                .HasColumnName("key");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.AuthtokenToken)
                .HasForeignKey<AuthtokenToken>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authtoken_token_user_id_35299eff_fk_authentication_usuario_id");
        });

        modelBuilder.Entity<DjangoAdminLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("django_admin_log_pkey");

            entity.ToTable("django_admin_log");

            entity.HasIndex(e => e.ContentTypeId, "django_admin_log_content_type_id_c4bce8eb");

            entity.HasIndex(e => e.UserId, "django_admin_log_user_id_c564eba6");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionFlag).HasColumnName("action_flag");
            entity.Property(e => e.ActionTime).HasColumnName("action_time");
            entity.Property(e => e.ChangeMessage).HasColumnName("change_message");
            entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.ObjectRepr)
                .HasMaxLength(200)
                .HasColumnName("object_repr");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ContentType).WithMany(p => p.DjangoAdminLogs)
                .HasForeignKey(d => d.ContentTypeId)
                .HasConstraintName("django_admin_log_content_type_id_c4bce8eb_fk_django_co");

            entity.HasOne(d => d.User).WithMany(p => p.DjangoAdminLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("django_admin_log_user_id_c564eba6_fk_authentication_usuario_id");
        });

        modelBuilder.Entity<DjangoContentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("django_content_type_pkey");

            entity.ToTable("django_content_type");

            entity.HasIndex(e => new { e.AppLabel, e.Model }, "django_content_type_app_label_model_76bd3d3b_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppLabel)
                .HasMaxLength(100)
                .HasColumnName("app_label");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
        });

        modelBuilder.Entity<DjangoMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("django_migrations_pkey");

            entity.ToTable("django_migrations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.App)
                .HasMaxLength(255)
                .HasColumnName("app");
            entity.Property(e => e.Applied).HasColumnName("applied");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DjangoSession>(entity =>
        {
            entity.HasKey(e => e.SessionKey).HasName("django_session_pkey");

            entity.ToTable("django_session");

            entity.HasIndex(e => e.ExpireDate, "django_session_expire_date_a5c62663");

            entity.HasIndex(e => e.SessionKey, "django_session_session_key_c0390e0f_like").HasOperators(new[] { "varchar_pattern_ops" });

            entity.Property(e => e.SessionKey)
                .HasMaxLength(40)
                .HasColumnName("session_key");
            entity.Property(e => e.ExpireDate).HasColumnName("expire_date");
            entity.Property(e => e.SessionData).HasColumnName("session_data");
        });

        modelBuilder.Entity<GastosCategoriagasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gastos_categoriagasto_pkey");

            entity.ToTable("gastos_categoriagasto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(64)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
        });

        modelBuilder.Entity<GastosCategoriaigreso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gastos_categoriaigreso_pkey");

            entity.ToTable("gastos_categoriaigreso");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(64)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
        });

        modelBuilder.Entity<GastosGasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gastos_gasto_pkey");

            entity.ToTable("gastos_gasto");

            entity.HasIndex(e => e.CategoriaGastoId, "gastos_gasto_categoria_gasto_id_b248e6a5");

            entity.HasIndex(e => e.MonedaId, "gastos_gasto_moneda_id_05130697");

            entity.HasIndex(e => e.SubcategoriaGastoId, "gastos_gasto_subcategoria_gasto_id_f1a744ce");

            entity.HasIndex(e => e.UsuarioId, "gastos_gasto_usuario_id_df3bc76c");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.CategoriaGastoId).HasColumnName("categoria_gasto_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(64)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.Importe)
                .HasPrecision(18, 2)
                .HasColumnName("importe");
            entity.Property(e => e.MonedaId).HasColumnName("moneda_id");
            entity.Property(e => e.SubcategoriaGastoId).HasColumnName("subcategoria_gasto_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.CategoriaGasto).WithMany(p => p.GastosGastos)
                .HasForeignKey(d => d.CategoriaGastoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_gasto_categoria_gasto_id_b248e6a5_fk_gastos_ca");

            entity.HasOne(d => d.Moneda).WithMany(p => p.GastosGastos)
                .HasForeignKey(d => d.MonedaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_gasto_moneda_id_05130697_fk_gastos_moneda_id");

            entity.HasOne(d => d.SubcategoriaGasto).WithMany(p => p.GastosGastos)
                .HasForeignKey(d => d.SubcategoriaGastoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_gasto_subcategoria_gasto_i_f1a744ce_fk_gastos_su");

            entity.HasOne(d => d.Usuario).WithMany(p => p.GastosGastos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_gasto_usuario_id_df3bc76c_fk_authentication_usuario_id");
        });

        modelBuilder.Entity<GastosIngreso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gastos_ingreso_pkey");

            entity.ToTable("gastos_ingreso");

            entity.HasIndex(e => e.CategoriaIngresoId, "gastos_ingreso_categoria_ingreso_id_502a786d");

            entity.HasIndex(e => e.MonedaId, "gastos_ingreso_moneda_id_201e93b0");

            entity.HasIndex(e => e.SubcategoriaIngresoId, "gastos_ingreso_subcategoria_ingreso_id_7f00afd8");

            entity.HasIndex(e => e.UsuarioId, "gastos_ingreso_usuario_id_7fda200f");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.CategoriaIngresoId).HasColumnName("categoria_ingreso_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(64)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.Importe)
                .HasPrecision(18, 2)
                .HasColumnName("importe");
            entity.Property(e => e.MonedaId).HasColumnName("moneda_id");
            entity.Property(e => e.SubcategoriaIngresoId).HasColumnName("subcategoria_ingreso_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.CategoriaIngreso).WithMany(p => p.GastosIngresos)
                .HasForeignKey(d => d.CategoriaIngresoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_ingreso_categoria_ingreso_id_502a786d_fk_gastos_ca");

            entity.HasOne(d => d.Moneda).WithMany(p => p.GastosIngresos)
                .HasForeignKey(d => d.MonedaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_ingreso_moneda_id_201e93b0_fk_gastos_moneda_id");

            entity.HasOne(d => d.SubcategoriaIngreso).WithMany(p => p.GastosIngresos)
                .HasForeignKey(d => d.SubcategoriaIngresoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_ingreso_subcategoria_ingreso_7f00afd8_fk_gastos_su");

            entity.HasOne(d => d.Usuario).WithMany(p => p.GastosIngresos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_ingreso_usuario_id_7fda200f_fk_authentication_usuario_id");
        });

        modelBuilder.Entity<GastosMonedum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gastos_moneda_pkey");

            entity.ToTable("gastos_moneda");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(28)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
        });

        modelBuilder.Entity<GastosSubcategoriagasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gastos_subcategoriagasto_pkey");

            entity.ToTable("gastos_subcategoriagasto");

            entity.HasIndex(e => e.CategoriaGastoId, "gastos_subcategoriagasto_categoria_gasto_id_b740a7f3");

            entity.HasIndex(e => e.UsuarioId, "gastos_subcategoriagasto_usuario_id_4be4aa70");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.CategoriaGastoId).HasColumnName("categoria_gasto_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(64)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.CategoriaGasto).WithMany(p => p.GastosSubcategoriagastos)
                .HasForeignKey(d => d.CategoriaGastoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_subcategoriag_categoria_gasto_id_b740a7f3_fk_gastos_ca");

            entity.HasOne(d => d.Usuario).WithMany(p => p.GastosSubcategoriagastos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_subcategoriag_usuario_id_4be4aa70_fk_authentic");
        });

        modelBuilder.Entity<GastosSubcategoriaingreso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gastos_subcategoriaingreso_pkey");

            entity.ToTable("gastos_subcategoriaingreso");

            entity.HasIndex(e => e.CategoriaIngresoId, "gastos_subcategoriaingreso_categoria_ingreso_id_c8a045c2");

            entity.HasIndex(e => e.UsuarioId, "gastos_subcategoriaingreso_usuario_id_a08d5632");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.CategoriaIngresoId).HasColumnName("categoria_ingreso_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(64)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.CategoriaIngreso).WithMany(p => p.GastosSubcategoriaingresos)
                .HasForeignKey(d => d.CategoriaIngresoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_subcategoriai_categoria_ingreso_id_c8a045c2_fk_gastos_ca");

            entity.HasOne(d => d.Usuario).WithMany(p => p.GastosSubcategoriaingresos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_subcategoriai_usuario_id_a08d5632_fk_authentic");
        });

        modelBuilder.Entity<TokenBlacklistBlacklistedtoken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("token_blacklist_blacklistedtoken_pkey");

            entity.ToTable("token_blacklist_blacklistedtoken");

            entity.HasIndex(e => e.TokenId, "token_blacklist_blacklistedtoken_token_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BlacklistedAt).HasColumnName("blacklisted_at");
            entity.Property(e => e.TokenId).HasColumnName("token_id");

            entity.HasOne(d => d.Token).WithOne(p => p.TokenBlacklistBlacklistedtoken)
                .HasForeignKey<TokenBlacklistBlacklistedtoken>(d => d.TokenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("token_blacklist_blacklistedtoken_token_id_3cc7fe56_fk");
        });

        modelBuilder.Entity<TokenBlacklistOutstandingtoken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("token_blacklist_outstandingtoken_pkey");

            entity.ToTable("token_blacklist_outstandingtoken");

            entity.HasIndex(e => e.Jti, "token_blacklist_outstandingtoken_jti_hex_d9bdf6f7_like").HasOperators(new[] { "varchar_pattern_ops" });

            entity.HasIndex(e => e.Jti, "token_blacklist_outstandingtoken_jti_hex_d9bdf6f7_uniq").IsUnique();

            entity.HasIndex(e => e.UserId, "token_blacklist_outstandingtoken_user_id_83bc629a");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.Jti)
                .HasMaxLength(255)
                .HasColumnName("jti");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TokenBlacklistOutstandingtokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("token_blacklist_outs_user_id_83bc629a_fk_authentic");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
