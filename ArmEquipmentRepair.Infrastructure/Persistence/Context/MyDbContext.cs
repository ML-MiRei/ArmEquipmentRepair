using ArmEquipmentRepair.Infrastructure.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace ArmEquipmentRepair.Infrastructure.Persistence.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<TypesFault> TypesFaults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-E86S7QI;Database=ArmEquipmentRepairDB;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC07620BC4BB");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastModify)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.SecondName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModify)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Text)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Comments__Employ__4CA06362");

            entity.HasOne(d => d.Request).WithMany()
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Comments__Reques__4D94879B");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07BC44EB8C");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastModify)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.SecondName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employees__RoleI__398D8EEE");
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC075AEFE297");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07E540E04E");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Text)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Notificat__Emplo__52593CB8");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requests__3214EC071FCCC32A");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Equipment)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModify)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Requests__Client__47DBAE45");

            entity.HasOne(d => d.Employee).WithMany(p => p.Requests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Requests__Employ__48CFD27E");

            entity.HasOne(d => d.Status).WithMany(p => p.Requests)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Requests__Status__46E78A0C");

            entity.HasOne(d => d.Type).WithMany(p => p.Requests)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Requests__TypeId__45F365D3");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestS__3214EC073A03621E");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TypesFault>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypesFau__3214EC07024A15F1");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
