using System;
using System.Collections.Generic;
using Capa_Modelos;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Capa_Datos;

public partial class BaseContext : DbContext
{
    public BaseContext()
    {
    }

    public BaseContext(DbContextOptions<BaseContext> options)
        : base(options)
    {
    }

    public  DbSet<AccesoUsuario> AccesoUsuarios { get; set; }

    public virtual DbSet<ArqueoCaja> ArqueoCajas { get; set; }

    public virtual DbSet<Canton> Cantons { get; set; }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ConciliacionDiarium> ConciliacionDiaria { get; set; }

    public virtual DbSet<CondicionProducto> CondicionProductos { get; set; }

    public virtual DbSet<ControlStock> ControlStocks { get; set; }

    public virtual DbSet<Correo> Correos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EmpleadoRol> EmpleadoRols { get; set; }

    public virtual DbSet<EstadoCliente> EstadoClientes { get; set; }

    public virtual DbSet<EstadoFactura> EstadoFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<MaestroCompra> MaestroCompras { get; set; }

    public virtual DbSet<MaestroVentum> MaestroVenta { get; set; }

    public virtual DbSet<Monedum> Moneda { get; set; }

    public virtual DbSet<Nacionalidad> Nacionalidads { get; set; }

    public virtual DbSet<PagoFactura> PagoFacturas { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<PersonaFisica> PersonaFisicas { get; set; }

    public virtual DbSet<PersonaFisicaCorreo> PersonaFisicaCorreos { get; set; }

    public virtual DbSet<PersonaJuridica> PersonaJuridicas { get; set; }

    public virtual DbSet<PersonaJuriducaCorreo> PersonaJuriducaCorreos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<ProveedorProducto> ProveedorProductos { get; set; }

    public virtual DbSet<Provincium> Provincia { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolAcceso> RolAccesos { get; set; }

    public virtual DbSet<Salario> Salarios { get; set; }

    public virtual DbSet<SexoPersona> SexoPersonas { get; set; }

    public virtual DbSet<SubcategoriaCliente> SubcategoriaClientes { get; set; }

   

    public virtual DbSet<Telefono> Telefonos { get; set; }

    public virtual DbSet<TipoCliente> TipoClientes { get; set; }

    public virtual DbSet<TipoPagoFactura> TipoPagoFacturas { get; set; }

    public virtual DbSet<TipoPagoPlanilla> TipoPagoPlanillas { get; set; }

    public virtual DbSet<TipoPersonaFisica> TipoPersonaFisicas { get; set; }

    public virtual DbSet<TipoSociedad> TipoSociedads { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioCorreo> UsuarioCorreos { get; set; }

    public virtual DbSet<UsuarioEmpleado> UsuarioEmpleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseMySql("server=winsvr-pruebas;user=gjhidalgo;password=Abc123456;database=base", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.46-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AccesoUsuario>(entity =>
        {
            entity.HasKey(e => new { e.RolAccesoIdRolAcceso, e.UsuarioPersonaFisicaId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("acceso_usuario");

            entity.HasIndex(e => e.UsuarioPersonaFisicaId, "fk_Acceso_Usuario_usuario1_idx");

            entity.Property(e => e.RolAccesoIdRolAcceso).HasColumnName("Rol_Acceso_id_Rol_Acceso");
            entity.Property(e => e.UsuarioPersonaFisicaId).HasColumnName("usuario_persona_fisica_ID");
            entity.Property(e => e.Estado).HasColumnType("bit(1)");

            entity.HasOne(d => d.RolAccesoIdRolAccesoNavigation).WithMany(p => p.AccesoUsuarios)
                .HasForeignKey(d => d.RolAccesoIdRolAcceso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Acceso_Usuario_Rol_Acceso1");

            entity.HasOne(d => d.UsuarioPersonaFisica).WithMany(p => p.AccesoUsuarios)
                .HasForeignKey(d => d.UsuarioPersonaFisicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Acceso_Usuario_usuario1");
        });

        modelBuilder.Entity<ArqueoCaja>(entity =>
        {
            entity.HasKey(e => e.IdArqueo).HasName("PRIMARY");

            entity.ToTable("arqueo_caja");

            entity.HasIndex(e => e.EmpleadoId, "FK_Arqueo_Caja_Empleado");

            entity.HasIndex(e => e.Moneda, "FK_Arqueo_Caja_Moneda");

            entity.Property(e => e.IdArqueo)
                .ValueGeneratedNever()
                .HasColumnName("ID_Arqueo");
            entity.Property(e => e.Diferencia).HasPrecision(18, 2);
            entity.Property(e => e.EmpleadoId).HasColumnName("Empleado_Id");
            entity.Property(e => e.FechaArqueo)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Arqueo");
            entity.Property(e => e.FechaRegistro)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Registro");
            entity.Property(e => e.HoraCierre)
                .HasMaxLength(6)
                .HasColumnName("Hora_Cierre");
            entity.Property(e => e.HoraInicio)
                .HasMaxLength(6)
                .HasColumnName("Hora_Inicio");
            entity.Property(e => e.MontoElectrónico)
                .HasPrecision(18, 2)
                .HasColumnName("Monto_Electrónico");
            entity.Property(e => e.MontoFisico)
                .HasPrecision(18, 2)
                .HasColumnName("Monto_Fisico");
            entity.Property(e => e.MontoInicial)
                .HasPrecision(18, 2)
                .HasColumnName("Monto_Inicial");
            entity.Property(e => e.MontoRegistrado)
                .HasPrecision(18, 2)
                .HasColumnName("Monto_Registrado");
            entity.Property(e => e.Observaciones).HasMaxLength(100);

            entity.HasOne(d => d.Empleado).WithMany(p => p.ArqueoCajas)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arqueo_Caja_Empleado");

            entity.HasOne(d => d.MonedaNavigation).WithMany(p => p.ArqueoCajas)
                .HasForeignKey(d => d.Moneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arqueo_Caja_Moneda");
        });

        modelBuilder.Entity<Canton>(entity =>
        {
            entity.HasKey(e => e.IdCantón).HasName("PRIMARY");

            entity.ToTable("canton");

            entity.Property(e => e.IdCantón)
                .ValueGeneratedNever()
                .HasColumnName("Id_Cantón");
            entity.Property(e => e.DescripciónCanton)
                .HasMaxLength(100)
                .HasColumnName("Descripción_Canton");
            entity.Property(e => e.Estado).HasMaxLength(100);
            entity.Property(e => e.IdPersonaCanton).HasColumnName("ID_Persona_Canton");
        });

        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categoria_producto");

            entity.Property(e => e.IdCategoria)
                .ValueGeneratedNever()
                .HasColumnName("ID_Categoria");
            entity.Property(e => e.DescripciónCategoria)
                .HasMaxLength(100)
                .HasColumnName("Descripción_Categoria");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.Estado, "FK_Cliente_Estado_Cliente");

            entity.HasIndex(e => e.IdPersona, "FK_Cliente_Persona_Fisica");

            entity.HasIndex(e => e.TipoCliente, "FK_Cliente_SubCategoria_Cliente");

            entity.Property(e => e.IdCliente)
                .ValueGeneratedNever()
                .HasColumnName("Id_Cliente");
            entity.Property(e => e.CategoriaCliente).HasColumnName("Categoria_Cliente");
            entity.Property(e => e.DescripcionCliente)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Cliente");
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.Observacion).HasMaxLength(100);
            entity.Property(e => e.TipoCliente).HasColumnName("Tipo_Cliente");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Estado_Cliente");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Persona_Fisica");

            entity.HasOne(d => d.TipoClienteNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_SubCategoria_Cliente");

            entity.HasOne(d => d.TipoCliente1).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Tipo_Cliente");
        });

        modelBuilder.Entity<ConciliacionDiarium>(entity =>
        {
            entity.HasKey(e => e.ConciliacionId).HasName("PRIMARY");

            entity.ToTable("conciliacion_diaria");

            entity.HasIndex(e => e.ArqueoId, "FK_Conciliacion_Diaria_Arqueo_Caja");

            entity.Property(e => e.ConciliacionId)
                .ValueGeneratedNever()
                .HasColumnName("Conciliacion_id");
            entity.Property(e => e.ArqueoId).HasColumnName("Arqueo_ID");
            entity.Property(e => e.Diferencia).HasPrecision(18, 2);
            entity.Property(e => e.Egresos).HasPrecision(18, 2);
            entity.Property(e => e.FechaConciliacion)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Conciliacion");
            entity.Property(e => e.Ingresos).HasPrecision(18, 2);
            entity.Property(e => e.SaldoFinal)
                .HasPrecision(18, 2)
                .HasColumnName("Saldo_Final");

            entity.HasOne(d => d.Arqueo).WithMany(p => p.ConciliacionDiaria)
                .HasForeignKey(d => d.ArqueoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conciliacion_Diaria_Arqueo_Caja");
        });

        modelBuilder.Entity<CondicionProducto>(entity =>
        {
            entity.HasKey(e => e.IdCondicion).HasName("PRIMARY");

            entity.ToTable("condicion_producto");

            entity.Property(e => e.IdCondicion)
                .ValueGeneratedNever()
                .HasColumnName("ID_Condicion");
            entity.Property(e => e.DescripcionCondicion)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Condicion");
        });

        modelBuilder.Entity<ControlStock>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PRIMARY");

            entity.ToTable("control_stock");

            entity.HasIndex(e => e.IdProductoStock, "FK_Control_Stock_Producto");

            entity.Property(e => e.IdInventario)
                .ValueGeneratedNever()
                .HasColumnName("ID_inventario");
            entity.Property(e => e.CantidadActual).HasColumnName("Cantidad_Actual");
            entity.Property(e => e.FechaActualizacion).HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.IdProductoStock).HasColumnName("ID_producto_stock");

            entity.HasOne(d => d.IdProductoStockNavigation).WithMany(p => p.ControlStocks)
                .HasForeignKey(d => d.IdProductoStock)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Control_Stock_Producto");
        });

        modelBuilder.Entity<Correo>(entity =>
        {
            entity.HasKey(e => e.IdCorreo).HasName("PRIMARY");

            entity.ToTable("correo");

            entity.Property(e => e.IdCorreo)
             .ValueGeneratedOnAdd()
                .HasColumnName("ID_Correo");
            entity.Property(e => e.DescripcionCorreoPersona)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Correo_Persona");

            entity.HasMany(d => d.ProveedorIdProveedors).WithMany(p => p.CorreoIdCorreos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProveedorCorreo",
                    r => r.HasOne<Proveedor>().WithMany()
                        .HasForeignKey("ProveedorIdProveedor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Proveedor-Correo_proveedor1"),
                    l => l.HasOne<Correo>().WithMany()
                        .HasForeignKey("CorreoIdCorreo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Proveedor-Correo_correo1"),
                    j =>
                    {
                        j.HasKey("CorreoIdCorreo", "ProveedorIdProveedor")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("proveedor-correo");
                        j.HasIndex(new[] { "ProveedorIdProveedor" }, "fk_Proveedor-Correo_proveedor1_idx");
                        j.IndexerProperty<int>("CorreoIdCorreo").HasColumnName("correo_ID_Correo");
                        j.IndexerProperty<long>("ProveedorIdProveedor").HasColumnName("proveedor_Id_Proveedor");
                    });
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PRIMARY");

            entity.ToTable("departamento");

            entity.Property(e => e.IdDepartamento)
                .ValueGeneratedNever()
                .HasColumnName("Id_Departamento");
            entity.Property(e => e.DescripcionDepartamento)
                .HasMaxLength(50)
                .HasColumnName("Descripcion_Departamento");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PRIMARY");

            entity.ToTable("detalle_compra");

            entity.HasIndex(e => e.IdCompra, "FK_Detalle_Compra_Maestro_Compra");

            entity.HasIndex(e => e.IdProducto, "FK_Detalle_Compra_Producto");

            entity.Property(e => e.IdDetalle)
                .ValueGeneratedNever()
                .HasColumnName("ID_Detalle");
            entity.Property(e => e.IdCompra).HasColumnName("ID_Compra");
            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.Precio).HasPrecision(15, 2);

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Compra_Maestro_Compra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Compra_Producto");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PRIMARY");

            entity.ToTable("detalle_venta");

            entity.HasIndex(e => e.IdVenta, "FK_Detalle_Venta_Maestro_Venta");

            entity.HasIndex(e => e.IdProducto, "FK_Detalle_Venta_Producto");

            entity.Property(e => e.IdDetalle)
                .ValueGeneratedNever()
                .HasColumnName("ID_Detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Precio)
                .HasPrecision(15, 2)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Venta_Producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Venta_Maestro_Venta");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PRIMARY");

            entity.ToTable("direccion");

            entity.HasIndex(e => e.Canton, "FK_Direccion_Canton");

            entity.HasIndex(e => e.Distrito, "FK_Direccion_Distrito");

            entity.HasIndex(e => e.Pais, "FK_Direccion_Pais");

            entity.HasIndex(e => e.IdPersonaDireccion, "FK_Direccion_Persona_Juridica");

            entity.HasIndex(e => e.Provincia, "FK_Direccion_Provincia");

            entity.Property(e => e.IdDireccion)
                .ValueGeneratedNever()
                .HasColumnName("ID_Direccion");
            entity.Property(e => e.IdPersonaDireccion).HasColumnName("ID_Persona_Direccion");

            entity.HasOne(d => d.CantonNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.Canton)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion_Canton");

            entity.HasOne(d => d.DistritoNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.Distrito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion_Distrito");

            entity.HasOne(d => d.IdPersonaDireccionNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdPersonaDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion_Persona_Juridica");

            entity.HasOne(d => d.PaisNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.Pais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion_Pais");

            entity.HasOne(d => d.ProvinciaNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.Provincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion_Provincia");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.IdDistrito).HasName("PRIMARY");

            entity.ToTable("distrito");

            entity.Property(e => e.IdDistrito)
                .ValueGeneratedNever()
                .HasColumnName("ID_Distrito");
            entity.Property(e => e.DescripcionDistrito)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Distrito");
            entity.Property(e => e.IdPersonaDistrito)
                .HasMaxLength(100)
                .HasColumnName("ID_Persona_Distrito");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdColaborador).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.IdTurno, "FK_Empleado_Turno");

            entity.Property(e => e.IdColaborador)
                .ValueGeneratedNever()
                .HasColumnName("ID_Colaborador");
            entity.Property(e => e.IdEmpleado).HasColumnName("ID_Empleado");
            entity.Property(e => e.IdPuesto).HasColumnName("id_puesto");
            entity.Property(e => e.IdSalarios).HasColumnName("id_salarios");
            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.RolEmpleado).HasColumnName("Rol_Empleado");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Turno");
        });

        modelBuilder.Entity<EmpleadoRol>(entity =>
        {
            entity.HasKey(e => new { e.IdColaborador, e.IdRol })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("empleado_rol");

            entity.HasIndex(e => e.IdRol, "FK_Empleado_rol_Rol");

            entity.Property(e => e.IdColaborador).HasColumnName("ID_Colaborador");
            entity.Property(e => e.IdRol).HasColumnName("ID_rol");
            entity.Property(e => e.FechaAsignacion).HasColumnName("Fecha_Asignacion");
            entity.Property(e => e.UsuarioResponsable).HasColumnName("Usuario_responsable");

            entity.HasOne(d => d.IdColaboradorNavigation).WithMany(p => p.EmpleadoRols)
                .HasForeignKey(d => d.IdColaborador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_rol_Empleado");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.EmpleadoRols)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_rol_Rol");
        });

        modelBuilder.Entity<EstadoCliente>(entity =>
        {
            entity.HasKey(e => e.IdEstadoCliente).HasName("PRIMARY");

            entity.ToTable("estado_cliente");

            entity.Property(e => e.IdEstadoCliente)
                .ValueGeneratedNever()
                .HasColumnName("Id_Estado_Cliente");
            entity.Property(e => e.DescripciónEstadoCliente)
                .HasMaxLength(100)
                .HasColumnName("Descripción_Estado_Cliente");
        });

        modelBuilder.Entity<EstadoFactura>(entity =>
        {
            entity.HasKey(e => e.IdEstadoFactura).HasName("PRIMARY");

            entity.ToTable("estado_factura");

            entity.Property(e => e.IdEstadoFactura)
                .ValueGeneratedNever()
                .HasColumnName("id_Estado_Factura");
            entity.Property(e => e.DescripcionEstadoFactura)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Estado_Factura");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.FacturaId).HasName("PRIMARY");

            entity.ToTable("factura");

            entity.HasIndex(e => e.IdCliente, "FK_Factura_Cliente");

            entity.HasIndex(e => e.Estado, "FK_Factura_Estado_Factura");

            entity.HasIndex(e => e.Moneda, "FK_Factura_Moneda");

            entity.Property(e => e.FacturaId)
                .ValueGeneratedNever()
                .HasColumnName("Factura_ID");
            entity.Property(e => e.ClaveFiscal)
                .HasMaxLength(100)
                .HasColumnName("Clave_Fiscal");
            entity.Property(e => e.FechaEmision)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Emision");
            entity.Property(e => e.FechaRegistro)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Registro");
            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.Impuesto).HasPrecision(18, 2);
            entity.Property(e => e.MontoTotal)
                .HasPrecision(18, 2)
                .HasColumnName("Monto_Total");
            entity.Property(e => e.NumeroFactura).HasColumnName("Numero_Factura");
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Xml).HasColumnName("XML");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Estado_Factura");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Cliente");

            entity.HasOne(d => d.MonedaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Moneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Moneda");
        });

        modelBuilder.Entity<MaestroCompra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PRIMARY");

            entity.ToTable("maestro_compra");

            entity.Property(e => e.IdCompra)
                .ValueGeneratedNever()
                .HasColumnName("ID_Compra");
            entity.Property(e => e.IdProveedor).HasColumnName("ID_Proveedor");
            entity.Property(e => e.Subtotal).HasPrecision(15, 2);
            entity.Property(e => e.Total).HasPrecision(15, 2);
        });

        modelBuilder.Entity<MaestroVentum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PRIMARY");

            entity.ToTable("maestro_venta");

            entity.Property(e => e.IdVenta)
                .ValueGeneratedNever()
                .HasColumnName("Id_Venta");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Total).HasPrecision(15, 2);
        });

        modelBuilder.Entity<Monedum>(entity =>
        {
            entity.HasKey(e => e.IdMoneda).HasName("PRIMARY");

            entity.ToTable("moneda");

            entity.Property(e => e.IdMoneda)
                .ValueGeneratedNever()
                .HasColumnName("id_moneda");
            entity.Property(e => e.DescripcionMoneda)
                .HasMaxLength(50)
                .HasColumnName("Descripcion_moneda");
        });

        modelBuilder.Entity<Nacionalidad>(entity =>
        {
            entity.HasKey(e => e.IdNacionalidad).HasName("PRIMARY");

            entity.ToTable("nacionalidad");

            entity.Property(e => e.IdNacionalidad)
                .ValueGeneratedNever()
                .HasColumnName("ID_Nacionalidad");
            entity.Property(e => e.DescripcionNacionalidad)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Nacionalidad]");
        });

        modelBuilder.Entity<PagoFactura>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.ToTable("pago_factura");

            entity.HasIndex(e => e.UsuarioId, "FK_Pago_Factura_Empleado");

            entity.HasIndex(e => e.Estado, "FK_Pago_Factura_Estado_Factura");

            entity.HasIndex(e => e.FacturaId, "FK_Pago_Factura_Factura");

            entity.HasIndex(e => e.MedioPago, "FK_Pago_Factura_Tipo_Pago_Factura");

            entity.Property(e => e.IdPago)
                .ValueGeneratedNever()
                .HasColumnName("id_pago");
            entity.Property(e => e.FacturaId).HasColumnName("Factura_ID");
            entity.Property(e => e.FechaPago)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Pago");
            entity.Property(e => e.MedioPago).HasColumnName("Medio_Pago");
            entity.Property(e => e.Monto).HasPrecision(18, 2);
            entity.Property(e => e.Referencia).HasMaxLength(50);
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_id");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.PagoFacturas)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Factura_Estado_Factura");

            entity.HasOne(d => d.Factura).WithMany(p => p.PagoFacturas)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Factura_Factura");

            entity.HasOne(d => d.MedioPagoNavigation).WithMany(p => p.PagoFacturas)
                .HasForeignKey(d => d.MedioPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Factura_Tipo_Pago_Factura");

            entity.HasOne(d => d.Usuario).WithMany(p => p.PagoFacturas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Factura_Empleado");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PRIMARY");

            entity.ToTable("pais");

            entity.Property(e => e.IdPais)
                .ValueGeneratedNever()
                .HasColumnName("ID_Pais");
            entity.Property(e => e.DescripcionPais)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Pais");
        });

        modelBuilder.Entity<PersonaFisica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("persona_fisica");

            entity.HasIndex(e => e.Nacionalidad, "FK_Persona Fisica_Nacionalidad");

            entity.HasIndex(e => e.Sexo, "FK_Persona Fisica_Sexo_Persona");

            entity.HasIndex(e => e.Tipo, "FK_Persona Fisica_Tipo_Persona_Fisica");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Apellido1).HasMaxLength(100);
            entity.Property(e => e.Apellido2).HasMaxLength(100);
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.NacionalidadNavigation).WithMany(p => p.PersonaFisicas)
                .HasForeignKey(d => d.Nacionalidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona Fisica_Nacionalidad");

            entity.HasOne(d => d.SexoNavigation).WithMany(p => p.PersonaFisicas)
                .HasForeignKey(d => d.Sexo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona Fisica_Sexo_Persona");

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.PersonaFisicas)
                .HasForeignKey(d => d.Tipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona Fisica_Tipo_Persona_Fisica");
        });

        modelBuilder.Entity<PersonaFisicaCorreo>(entity =>
        {
            entity.HasKey(e => new { e.PersonaFisicaId, e.CorreoIdCorreo });

            entity.ToTable("persona_fisica-correo");

            entity.HasIndex(e => e.PersonaFisicaId, "fk_Persona-Correo_persona_fisica1_idx");

            entity.Property(e => e.CorreoIdCorreo)
                .ValueGeneratedNever()
                .HasColumnName("correo_ID_Correo");
            entity.Property(e => e.PersonaFisicaId).HasColumnName("persona_fisica_ID");

            entity.HasOne(d => d.CorreoIdCorreoNavigation).WithOne(p => p.PersonaFisicaCorreo)
                .HasForeignKey<PersonaFisicaCorreo>(d => d.CorreoIdCorreo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Persona-Correo_correo1");

            entity.HasOne(d => d.PersonaFisica).WithMany(p => p.PersonaFisicaCorreos)
                .HasForeignKey(d => d.PersonaFisicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Persona-Correo_persona_fisica1");
        });

        modelBuilder.Entity<PersonaJuridica>(entity =>
        {
            entity.HasKey(e => e.CedulaJuridica).HasName("PRIMARY");

            entity.ToTable("persona_juridica");

            entity.HasIndex(e => e.PaisConstitucion, "FK_Persona_Juridica_Pais");

            entity.HasIndex(e => e.TipoSociedad, "FK_Persona_Juridica_Tipo_Sociedad");

            entity.Property(e => e.CedulaJuridica)
                .ValueGeneratedNever()
                .HasColumnName("Cedula_Juridica");
            entity.Property(e => e.FechaConstitucion).HasColumnName("Fecha_Constitucion");
            entity.Property(e => e.IdPersonaJuridica).HasColumnName("ID_Persona_Juridica");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(200)
                .HasColumnName("Nombre_Comercial");
            entity.Property(e => e.PaisConstitucion).HasColumnName("Pais_Constitucion");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .HasColumnName("Razon_Social");
            entity.Property(e => e.TipoSociedad).HasColumnName("Tipo_Sociedad");

            entity.HasOne(d => d.CedulaJuridicaNavigation).WithOne(p => p.PersonaJuridica)
                .HasForeignKey<PersonaJuridica>(d => d.CedulaJuridica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona_Juridica_Proveedor");

            entity.HasOne(d => d.PaisConstitucionNavigation).WithMany(p => p.PersonaJuridicas)
                .HasForeignKey(d => d.PaisConstitucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona_Juridica_Pais");

            entity.HasOne(d => d.TipoSociedadNavigation).WithMany(p => p.PersonaJuridicas)
                .HasForeignKey(d => d.TipoSociedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona_Juridica_Tipo_Sociedad");
        });

        modelBuilder.Entity<PersonaJuriducaCorreo>(entity =>
        {
            entity.HasKey(e => e.CorreoIdCorreo).HasName("PRIMARY");

            entity.ToTable("persona_juriduca-correo");

            entity.HasIndex(e => e.PersonaJuridicaCedulaJuridica, "fk_Persona_Juriduca-Correo_persona_juridica1_idx");

            entity.Property(e => e.CorreoIdCorreo)
                .ValueGeneratedNever()
                .HasColumnName("correo_ID_Correo");
            entity.Property(e => e.PersonaJuridicaCedulaJuridica).HasColumnName("persona_juridica_Cedula_Juridica");

            entity.HasOne(d => d.CorreoIdCorreoNavigation).WithOne(p => p.PersonaJuriducaCorreo)
                .HasForeignKey<PersonaJuriducaCorreo>(d => d.CorreoIdCorreo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Persona_Juriduca-Correo_correo1");

            entity.HasOne(d => d.PersonaJuridicaCedulaJuridicaNavigation).WithMany(p => p.PersonaJuriducaCorreos)
                .HasForeignKey(d => d.PersonaJuridicaCedulaJuridica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Persona_Juriduca-Correo_persona_juridica1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.IdCategoria, "FK_Producto_Categoria_Producto");

            entity.HasIndex(e => e.Condicion, "FK_Producto_Condicion_Producto");

            entity.HasIndex(e => e.UnidadMedida, "FK_Producto_Unidad_Medida");

            entity.Property(e => e.IdProducto)
                .ValueGeneratedNever()
                .HasColumnName("ID_Producto");
            entity.Property(e => e.DescripciónProducto)
                .HasMaxLength(100)
                .HasColumnName("Descripción_Producto");
            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrecioCompra)
                .HasPrecision(10, 2)
                .HasColumnName("Precio_Compra");
            entity.Property(e => e.PrecioVenta)
                .HasPrecision(10, 2)
                .HasColumnName("Precio_Venta");
            entity.Property(e => e.UnidadMedida).HasColumnName("Unidad_Medida");

            entity.HasOne(d => d.CondicionNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Condicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Condicion_Producto");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria_Producto");

            entity.HasOne(d => d.UnidadMedidaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.UnidadMedida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Unidad_Medida");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PRIMARY");

            entity.ToTable("proveedor");

            entity.HasIndex(e => e.IdTelefono, "FK_Proveedor_Telefono");

            entity.Property(e => e.IdProveedor)
                .ValueGeneratedNever()
                .HasColumnName("Id_Proveedor");
            entity.Property(e => e.IdTelefono).HasColumnName("Id_Telefono");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(100)
                .HasColumnName("Nombre_Proveedor");
            entity.Property(e => e.TipoInsumo).HasColumnName("Tipo_Insumo");

            entity.HasOne(d => d.IdTelefonoNavigation).WithMany(p => p.Proveedors)
                .HasForeignKey(d => d.IdTelefono)
                .HasConstraintName("FK_Proveedor_Telefono");
        });

        modelBuilder.Entity<ProveedorProducto>(entity =>
        {
            entity.HasKey(e => new { e.IdProveedor, e.IdProducto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("proveedor-producto");

            entity.HasIndex(e => e.IdProducto, "FK_Proveedor-Producto_Producto");

            entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.FechaUltimaCompra)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Ultima_Compra");
            entity.Property(e => e.PrecioCompra)
                .HasPrecision(18, 2)
                .HasColumnName("Precio_Compra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProveedorProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedor-Producto_Producto");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProveedorProductos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedor-Producto_Proveedor");
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PRIMARY");

            entity.ToTable("provincia");

            entity.Property(e => e.IdProvincia)
                .ValueGeneratedNever()
                .HasColumnName("ID_Provincia");
            entity.Property(e => e.DescripcionProvincia)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Provincia");
            entity.Property(e => e.IdPersonaProvincia).HasColumnName("ID_Persona_Provincia");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuesto).HasName("PRIMARY");

            entity.ToTable("puesto");

            entity.HasIndex(e => e.IdEmpleado, "FK_Puesto_Empleado");

            entity.Property(e => e.IdPuesto)
                .ValueGeneratedNever()
                .HasColumnName("id_puesto");
            entity.Property(e => e.DescripcionPuesto)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Puesto");
            entity.Property(e => e.FechaCreacion)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Creacion");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.NivelJerarquicos)
                .HasMaxLength(50)
                .HasColumnName("Nivel_Jerarquicos");
            entity.Property(e => e.NombrePuesto)
                .HasMaxLength(50)
                .HasColumnName("Nombre_Puesto");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Puestos)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Puesto_Empleado");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("Id_Rol");
            entity.Property(e => e.DescripciónRol)
                .HasMaxLength(100)
                .HasColumnName("Descripción_Rol");
        });

        modelBuilder.Entity<RolAcceso>(entity =>
        {
            entity.HasKey(e => e.IdRolAcceso).HasName("PRIMARY");

            entity.ToTable("rol_acceso");

            entity.Property(e => e.IdRolAcceso)
                .ValueGeneratedNever()
                .HasColumnName("id_Rol_Acceso");
            entity.Property(e => e.Descripcion).HasMaxLength(45);
            entity.Property(e => e.Estado).HasColumnType("bit(1)");
        });

        modelBuilder.Entity<Salario>(entity =>
        {
            entity.HasKey(e => e.IdSalario).HasName("PRIMARY");

            entity.ToTable("salario");

            entity.HasIndex(e => e.IdEmpleado, "FK_Salario_Empleado");

            entity.HasIndex(e => e.Moneda, "FK_Salario_Moneda");

            entity.HasIndex(e => e.IdPuesto, "FK_Salario_Puesto");

            entity.HasIndex(e => e.TipoPago, "FK_Salario_Tipo_Pago_Planilla");

            entity.HasIndex(e => e.UsuarioRegistro, "FK_Salario_Usuario");

            entity.Property(e => e.IdSalario)
                .ValueGeneratedNever()
                .HasColumnName("id_Salario");
            entity.Property(e => e.DescripcionSalario)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_salario");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdPuesto).HasColumnName("id_puesto");
            entity.Property(e => e.SalarioBruto)
                .HasPrecision(18, 2)
                .HasColumnName("Salario_Bruto");
            entity.Property(e => e.TipoPago).HasColumnName("Tipo_Pago");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("Usuario_Registro");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Salarios)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salario_Empleado");

            entity.HasOne(d => d.IdPuestoNavigation).WithMany(p => p.Salarios)
                .HasForeignKey(d => d.IdPuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salario_Puesto");

            entity.HasOne(d => d.MonedaNavigation).WithMany(p => p.Salarios)
                .HasForeignKey(d => d.Moneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salario_Moneda");

            entity.HasOne(d => d.TipoPagoNavigation).WithMany(p => p.Salarios)
                .HasForeignKey(d => d.TipoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salario_Tipo_Pago_Planilla");
        });

        modelBuilder.Entity<SexoPersona>(entity =>
        {
            entity.HasKey(e => e.IdSexoPersona).HasName("PRIMARY");

            entity.ToTable("sexo_persona");

            entity.Property(e => e.IdSexoPersona)
                .ValueGeneratedNever()
                .HasColumnName("Id_Sexo_Persona");
            entity.Property(e => e.DescripcionSexoPersona)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Sexo_Persona");
        });

        modelBuilder.Entity<SubcategoriaCliente>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaCliente).HasName("PRIMARY");

            entity.ToTable("subcategoria_cliente");

            entity.Property(e => e.IdCategoriaCliente)
                .ValueGeneratedNever()
                .HasColumnName("Id_Categoria_Cliente");
            entity.Property(e => e.DescripcionCategoriaCliente)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Categoria_Cliente");
        });

        

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => e.IdTelefono).HasName("PRIMARY");

            entity.ToTable("telefono");

            entity.HasIndex(e => e.IdPersona, "FK_Telefono_Persona_Juridica");

            entity.Property(e => e.IdTelefono)
                .ValueGeneratedNever()
                .HasColumnName("ID_Telefono");
            entity.Property(e => e.DescripcionTelefono)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Telefono");
            entity.Property(e => e.IdPersona).HasColumnName("ID_persona");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Telefono_Persona_Juridica");

            entity.HasOne(d => d.IdPersona1).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Telefono_Persona Fisica");
        });

        modelBuilder.Entity<TipoCliente>(entity =>
        {
            entity.HasKey(e => e.IdTipoCliente).HasName("PRIMARY");

            entity.ToTable("tipo_cliente");

            entity.Property(e => e.IdTipoCliente)
                .ValueGeneratedNever()
                .HasColumnName("ID_TipoCliente");
            entity.Property(e => e.DescripcionTipoCliente)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Tipo_Cliente");
        });

        modelBuilder.Entity<TipoPagoFactura>(entity =>
        {
            entity.HasKey(e => e.IdTipoPagoFactura).HasName("PRIMARY");

            entity.ToTable("tipo_pago_factura");

            entity.Property(e => e.IdTipoPagoFactura)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_pago_factura");
            entity.Property(e => e.DescripcionTipoPagoFactura)
                .HasMaxLength(50)
                .HasColumnName("Descripcion_tipo_pago_factura");
        });

        modelBuilder.Entity<TipoPagoPlanilla>(entity =>
        {
            entity.HasKey(e => e.IdTipoPagoPlanilla).HasName("PRIMARY");

            entity.ToTable("tipo_pago_planilla");

            entity.Property(e => e.IdTipoPagoPlanilla)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_pago_planilla");
            entity.Property(e => e.DescripcionTipoPagoPlanilla)
                .HasMaxLength(50)
                .HasColumnName("Descripcion_tipo_pago_planilla");
        });

        modelBuilder.Entity<TipoPersonaFisica>(entity =>
        {
            entity.HasKey(e => e.IdTipoPersona).HasName("PRIMARY");

            entity.ToTable("tipo_persona_fisica");

            entity.Property(e => e.IdTipoPersona)
                .ValueGeneratedNever()
                .HasColumnName("ID_tipo_persona");
            entity.Property(e => e.DescripcionTipoPersona)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Tipo_Persona");
        });

        modelBuilder.Entity<TipoSociedad>(entity =>
        {
            entity.HasKey(e => e.IdTipoSociedad).HasName("PRIMARY");

            entity.ToTable("tipo_sociedad");

            entity.Property(e => e.IdTipoSociedad)
                .ValueGeneratedNever()
                .HasColumnName("ID_tipo_Sociedad");
            entity.Property(e => e.DescripcionTipoSociedad)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Tipo_Sociedad");
            entity.Property(e => e.Estado).HasMaxLength(100);
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PRIMARY");

            entity.ToTable("turno");

            entity.Property(e => e.IdTurno)
                .ValueGeneratedNever()
                .HasColumnName("Id_turno");
            entity.Property(e => e.DescripcionTurno)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Turno");
            entity.Property(e => e.HoraRoja).HasColumnName("Hora_Roja");
            entity.Property(e => e.Horario).HasMaxLength(350);
        });

        modelBuilder.Entity<UnidadMedidum>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PRIMARY");

            entity.ToTable("unidad_medida");

            entity.Property(e => e.IdUnidad)
                .ValueGeneratedNever()
                .HasColumnName("ID_Unidad");
            entity.Property(e => e.DescripcionUnidad)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_Unidad");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.PersonaFisicaId).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.EmpleadoIdColaborador, "fk_usuario_empleado1_idx");

            entity.Property(e => e.PersonaFisicaId)
                .ValueGeneratedNever()
                .HasColumnName("persona_fisica_ID");
            entity.Property(e => e.Contraseña).HasMaxLength(50);
            entity.Property(e => e.EmpleadoIdColaborador).HasColumnName("empleado_ID_Colaborador");
            entity.Property(e => e.FechaCreacion)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Creacion");
            entity.Property(e => e.IntentosFallidos).HasColumnName("Intentos_Fallidos");
            entity.Property(e => e.UltimoAcceso)
                .HasMaxLength(6)
                .HasColumnName("Ultimo_Acceso");
            entity.Property(e => e.Usuario_Logueo)
                .HasMaxLength(50)
                .HasColumnName("Usuario");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .HasColumnName("Usuario_Registro");

            entity.HasOne(d => d.EmpleadoIdColaboradorNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpleadoIdColaborador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_empleado1");

            entity.HasOne(d => d.PersonaFisica).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.PersonaFisicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_persona_fisica1");
        });

        modelBuilder.Entity<UsuarioCorreo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("usuario-correo");

            entity.HasIndex(e => e.CorreoIdCorreo, "fk_Usuario-Correo_correo1_idx");

            entity.HasIndex(e => e.UsuarioPersonaFisicaId, "fk_Usuario-Correo_usuario1_idx");

            entity.Property(e => e.CorreoIdCorreo).HasColumnName("correo_ID_Correo");
            entity.Property(e => e.UsuarioPersonaFisicaId).HasColumnName("usuario_persona_fisica_ID");

            entity.HasOne(d => d.CorreoIdCorreoNavigation).WithMany()
                .HasForeignKey(d => d.CorreoIdCorreo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Usuario-Correo_correo1");

            entity.HasOne(d => d.UsuarioPersonaFisica).WithMany()
                .HasForeignKey(d => d.UsuarioPersonaFisicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Usuario-Correo_usuario1");
        });

        modelBuilder.Entity<UsuarioEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PRIMARY");

            entity.ToTable("usuario_empleado");

            entity.Property(e => e.IdEmpleado)
                .ValueGeneratedNever()
                .HasColumnName("id_empleado");
            entity.Property(e => e.FechaModificacion)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Modificacion");
            entity.Property(e => e.FechaRegistro)
                .HasMaxLength(6)
                .HasColumnName("Fecha_Registro");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithOne(p => p.UsuarioEmpleado)
                .HasForeignKey<UsuarioEmpleado>(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Empleado_Empleado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
