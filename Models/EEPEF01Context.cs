using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ERP6.Models
{
    public partial class EEPEF01Context : DbContext
    {
        public EEPEF01Context()
        {
        }

        public EEPEF01Context(DbContextOptions<EEPEF01Context> options)
            : base(options)
        {
        }



        public virtual DbSet<STOCK_TYPE> StockTypes { get; set; }
        public virtual DbSet<STO_AREA> STO_AREA { get; set; }
        public virtual DbSet<STO_ORDER> STO_ORDER { get; set; }
        public virtual DbSet<STO_ORDER_DET> STO_ORDER_DET { get; set; }
        public virtual DbSet<Acccv10> Acccv10 { get; set; }
        public virtual DbSet<Accgr10> Accgr10 { get; set; }
        public virtual DbSet<Accmo10> Accmo10 { get; set; }
        public virtual DbSet<Accmo20> Accmo20 { get; set; }
        public virtual DbSet<Accmp10> Accmp10 { get; set; }
        public virtual DbSet<Accmp20> Accmp20 { get; set; }
        public virtual DbSet<Accno10> Accno10 { get; set; }
        public virtual DbSet<Accph10> Accph10 { get; set; }
        public virtual DbSet<Accph20> Accph20 { get; set; }
        public virtual DbSet<Acctg10> Acctg10 { get; set; }
        public virtual DbSet<Acctg20> Acctg20 { get; set; }
        public virtual DbSet<Acctg30> Acctg30 { get; set; }
        public virtual DbSet<Accye10> Accye10 { get; set; }
        public virtual DbSet<Agentlog> Agentlog { get; set; }
        public virtual DbSet<Autonum> Autonum { get; set; }
        public virtual DbSet<Bank10> Bank10 { get; set; }
        public virtual DbSet<Bank20> Bank20 { get; set; }
        public virtual DbSet<Bou10> Bou10 { get; set; }
        public virtual DbSet<Bou20> Bou20 { get; set; }
        public virtual DbSet<Bplfilelist> Bplfilelist { get; set; }
        public virtual DbSet<Chstock10> Chstock10 { get; set; }
        public virtual DbSet<Chstock11> Chstock11 { get; set; }
        public virtual DbSet<Coldef> Coldef { get; set; }
        public virtual DbSet<Consts> Consts { get; set; }
        public virtual DbSet<Cstm10> Cstm10 { get; set; }
        public virtual DbSet<Fix10> Fix10 { get; set; }
        public virtual DbSet<Gcheck10> Gcheck10 { get; set; }
        public virtual DbSet<Gcheck30> Gcheck30 { get; set; }
        public virtual DbSet<Groupdisitems> Groupdisitems { get; set; }
        public virtual DbSet<Groupmenus> Groupmenus { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<IPay10> IPay10 { get; set; }
        public virtual DbSet<In10> In10 { get; set; }
        public virtual DbSet<In20> In20 { get; set; }
        public virtual DbSet<Inout10> Inout10 { get; set; }
        public virtual DbSet<Invo10> Invo10 { get; set; }
        public virtual DbSet<Invo20> Invo20 { get; set; }
        public virtual DbSet<InvoInout10> InvoInout10 { get; set; }
        public virtual DbSet<Is10> Is10 { get; set; }
        public virtual DbSet<Is20> Is20 { get; set; }
        public virtual DbSet<Jobparams> Jobparams { get; set; }
        public virtual DbSet<Jobtable> Jobtable { get; set; }
        public virtual DbSet<Mailaddress> Mailaddress { get; set; }
        public virtual DbSet<Mailgroups> Mailgroups { get; set; }
        public virtual DbSet<Maillog> Maillog { get; set; }
        public virtual DbSet<Mailusergroups> Mailusergroups { get; set; }
        public virtual DbSet<Memdbf> Memdbf { get; set; }
        public virtual DbSet<Memo10> Memo10 { get; set; }
        public virtual DbSet<Menucontrols> Menucontrols { get; set; }
        public virtual DbSet<Menuitemtype> Menuitemtype { get; set; }
        public virtual DbSet<Menutable> Menutable { get; set; }
        public virtual DbSet<Messagetable> Messagetable { get; set; }
        public virtual DbSet<Mymenu> Mymenu { get; set; }
        public virtual DbSet<OGet10> OGet10 { get; set; }
        public virtual DbSet<Order10> Order10 { get; set; }
        public virtual DbSet<Order20> Order20 { get; set; }
        public virtual DbSet<Out10> Out10 { get; set; }
        public virtual DbSet<Out20> Out20 { get; set; }
        public virtual DbSet<Out30> Out30 { get; set; }
        public virtual DbSet<Out40> Out40 { get; set; }
        public virtual DbSet<Outvo10> Outvo10 { get; set; }
        public virtual DbSet<Outvo20> Outvo20 { get; set; }
        public virtual DbSet<Paperno10> Paperno10 { get; set; }
        public virtual DbSet<Partdesc10> Partdesc10 { get; set; }
        public virtual DbSet<Partpress10> Partpress10 { get; set; }
        public virtual DbSet<Partprint10> Partprint10 { get; set; }
        public virtual DbSet<Partstock10> Partstock10 { get; set; }
        public virtual DbSet<Parttype10> Parttype10 { get; set; }
        public virtual DbSet<Pcheck10> Pcheck10 { get; set; }
        public virtual DbSet<Pcheck30> Pcheck30 { get; set; }
        public virtual DbSet<Pepo10> Pepo10 { get; set; }
        public virtual DbSet<Pepo20> Pepo20 { get; set; }
        public virtual DbSet<Pepo30> Pepo30 { get; set; }
        public virtual DbSet<Pepo40> Pepo40 { get; set; }
        public virtual DbSet<Phase10> Phase10 { get; set; }
        public virtual DbSet<Phase20> Phase20 { get; set; }
        public virtual DbSet<PRICE_TYPE> PriceType { get;set; }
        public virtual DbSet<Pur10> Pur10 { get; set; }
        public virtual DbSet<Pur20> Pur20 { get; set; }
        public virtual DbSet<Rate10> Rate10 { get; set; }
        public virtual DbSet<Rate20> Rate20 { get; set; }
        public virtual DbSet<Reportdefines> Reportdefines { get; set; }
        public virtual DbSet<Reportlayout> Reportlayout { get; set; }
        public virtual DbSet<Reportlimitations> Reportlimitations { get; set; }
        public virtual DbSet<Reportlog> Reportlog { get; set; }
        public virtual DbSet<Reportlogdetail> Reportlogdetail { get; set; }
        public virtual DbSet<Reportloguser> Reportloguser { get; set; }
        public virtual DbSet<Reportqbuilder> Reportqbuilder { get; set; }
        public virtual DbSet<Setagent> Setagent { get; set; }
        public virtual DbSet<Setgridwidth> Setgridwidth { get; set; }
        public virtual DbSet<Simplereportlog> Simplereportlog { get; set; }
        public virtual DbSet<Stock10> Stock10 { get; set; }
        public virtual DbSet<Stock10p> Stock10p { get; set; }
        public virtual DbSet<Stock11> Stock11 { get; set; }
        public virtual DbSet<Stock20> Stock20 { get; set; }
        public virtual DbSet<Stock21> Stock21 { get; set; }
        public virtual DbSet<Stock30> Stock30 { get; set; }
        public virtual DbSet<Stock31> Stock31 { get; set; }
        public virtual DbSet<SyncLog> SyncLog { get; set; }
        public virtual DbSet<SyncLogHistory> SyncLogHistory { get; set; }
        public virtual DbSet<SyncTableCon> SyncTableCon { get; set; }
        public virtual DbSet<SysParamValue> SysParamValue { get; set; }
        public virtual DbSet<SysParamdef> SysParamdef { get; set; }
        public virtual DbSet<SysParamsTable> SysParamsTable { get; set; }
        public virtual DbSet<Sysautonum> Sysautonum { get; set; }
        public virtual DbSet<Systemparams> Systemparams { get; set; }
        public virtual DbSet<Tablesynclog> Tablesynclog { get; set; }
        public virtual DbSet<Tableversion> Tableversion { get; set; }
        public virtual DbSet<TransLogD> TransLogD { get; set; }
        public virtual DbSet<TransactionLog> TransactionLog { get; set; }
        public virtual DbSet<TransactionLogD> TransactionLogD { get; set; }
        public virtual DbSet<Trsflowdef> Trsflowdef { get; set; }
        public virtual DbSet<Trsparams> Trsparams { get; set; }
        public virtual DbSet<Trstbl> Trstbl { get; set; }
        public virtual DbSet<Userdeftablelist> Userdeftablelist { get; set; }
        public virtual DbSet<Userdisitems> Userdisitems { get; set; }
        public virtual DbSet<Usergroups> Usergroups { get; set; }
        public virtual DbSet<Usermenus> Usermenus { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Userworksheet> Userworksheet { get; set; }
        public virtual DbSet<Vendor10> Vendor10 { get; set; }
        public virtual DbSet<Work10> Work10 { get; set; }
        public virtual DbSet<Work20> Work20 { get; set; }
        public virtual DbSet<Work30> Work30 { get; set; }
        public virtual DbSet<Work40> Work40 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.168.231;Database=EEPEF01;Trusted_Connection=True;User ID=sa;Password=22607188;Integrated Security = False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<STOCK_TYPE>(entity =>
            {
                entity.HasKey(e => e.TYPE_ID);

                entity.ToTable("STOCK_TYPE");

                entity.Property(e => e.TYPE_ID)
                    .HasColumnName("TYPE_ID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

            });
            modelBuilder.Entity<PRICE_TYPE>(entity =>
            {
                entity.HasKey(e => e.PT_ID);

                entity.ToTable("PRICE_TYPE");

                entity.Property(e => e.PT_ID)
                    .HasColumnName("PT_ID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

            });
            modelBuilder.Entity<STO_AREA>(entity =>
            {
                entity.HasKey(e => e.AREA_ID);

                entity.ToTable("STO_AREA");

                entity.Property(e => e.AREA_ID)
                    .HasColumnName("AREA_ID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

            });
            modelBuilder.Entity<STO_ORDER>(entity =>
            {
                entity.HasKey(e => e.ORD_ID);

                entity.ToTable("STO_ORDER");

                entity.Property(e => e.ORD_ID)
                    .HasColumnName("ORD_ID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

            });
            modelBuilder.Entity<STO_ORDER_DET>(entity =>
            {
                entity.HasKey(e => e.ORDD_ID);

                entity.ToTable("STO_ORDER_DET");

                entity.Property(e => e.ORDD_ID)
                    .HasColumnName("ORDD_ID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Acccv10>(entity =>
            {
                entity.HasKey(e => e.Subno);

                entity.ToTable("ACCCV10");

                entity.Property(e => e.Subno)
                    .HasColumnName("SUBNO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Subname)
                    .HasColumnName("SUBNAME")
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Accgr10>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCGR10");

                entity.Property(e => e.Accdate)
                    .HasColumnName("ACCDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Accid)
                    .HasColumnName("ACCID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnName("BALANCE");

                entity.Property(e => e.Cd)
                    .HasColumnName("CD")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Ctotal)
                    .HasColumnName("CTOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Dtotal)
                    .HasColumnName("DTOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Subaccno)
                    .HasColumnName("SUBACCNO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Taccno)
                    .HasColumnName("TACCNO")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Accmo10>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCMO10");

                entity.HasIndex(e => new { e.Company, e.Accmonth, e.Accno, e.Subaccno, e.Ntus })
                    .HasName("IX_ACCMO10")
                    .IsClustered();

                entity.Property(e => e.Accmonth)
                    .IsRequired()
                    .HasColumnName("ACCMONTH")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Accno)
                    .IsRequired()
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Acctype)
                    .HasColumnName("ACCTYPE")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasColumnName("COMPANY")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ctotal).HasColumnName("CTOTAL");

                entity.Property(e => e.Dtotal).HasColumnName("DTOTAL");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Printtype)
                    .HasColumnName("PRINTTYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Subaccno)
                    .HasColumnName("SUBACCNO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Subtotal).HasColumnName("SUBTOTAL");

                entity.Property(e => e.Taccno)
                    .HasColumnName("TACCNO")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Accmo20>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCMO20");

                entity.Property(e => e.Ctotal).HasColumnName("CTOTAL");

                entity.Property(e => e.Dtotal).HasColumnName("DTOTAL");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Taccno)
                    .HasColumnName("TACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Accmp10>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCMP10");

                entity.Property(e => e.Accmonth)
                    .HasColumnName("ACCMONTH")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Acctype)
                    .HasColumnName("ACCTYPE")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasColumnName("COMPANY")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ctotal).HasColumnName("CTOTAL");

                entity.Property(e => e.Dtotal).HasColumnName("DTOTAL");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Printtype)
                    .HasColumnName("PRINTTYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Subaccno)
                    .HasColumnName("SUBACCNO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Subtotal).HasColumnName("SUBTOTAL");

                entity.Property(e => e.Taccno)
                    .HasColumnName("TACCNO")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Accmp20>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCMP20");

                entity.Property(e => e.Accmonth)
                    .HasColumnName("ACCMONTH")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Accname)
                    .HasColumnName("ACCNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Accname1)
                    .HasColumnName("ACCNAME1")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Accno1)
                    .HasColumnName("ACCNO1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Acctype)
                    .HasColumnName("ACCTYPE")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Acctype1)
                    .HasColumnName("ACCTYPE1")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasColumnName("COMPANY")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Company1)
                    .HasColumnName("COMPANY1")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ctotal).HasColumnName("CTOTAL");

                entity.Property(e => e.Ctotal1).HasColumnName("CTOTAL1");

                entity.Property(e => e.Dtotal).HasColumnName("DTOTAL");

                entity.Property(e => e.Dtotal1).HasColumnName("DTOTAL1");

                entity.Property(e => e.Midtype)
                    .HasColumnName("MIDTYPE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Midtype1)
                    .HasColumnName("MIDTYPE1")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Printtype)
                    .HasColumnName("PRINTTYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Printtype1)
                    .HasColumnName("PRINTTYPE1")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Subaccno)
                    .HasColumnName("SUBACCNO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Subaccno1)
                    .HasColumnName("SUBACCNO1")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Subtotal).HasColumnName("SUBTOTAL");

                entity.Property(e => e.Subtotal1).HasColumnName("SUBTOTAL1");

                entity.Property(e => e.Taccno)
                    .HasColumnName("TACCNO")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Taccno1)
                    .HasColumnName("TACCNO1")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Accno10>(entity =>
            {
                entity.HasKey(e => e.Accno);

                entity.ToTable("ACCNO10");

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Accname)
                    .HasColumnName("ACCNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Acctype)
                    .HasColumnName("ACCTYPE")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Midtype)
                    .HasColumnName("MIDTYPE")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Accph10>(entity =>
            {
                entity.HasKey(e => e.Accno);

                entity.ToTable("ACCPH10");

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Accname)
                    .IsRequired()
                    .HasColumnName("ACCNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Accph20>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCPH20");

                entity.HasIndex(e => new { e.Accno, e.Serno })
                    .HasName("IX_ACCPH20")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Accno)
                    .IsRequired()
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phase)
                    .HasColumnName("PHASE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");
            });

            modelBuilder.Entity<Acctg10>(entity =>
            {
                entity.HasKey(e => e.Accid);

                entity.ToTable("ACCTG10");

                entity.Property(e => e.Accid)
                    .HasColumnName("ACCID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accdate)
                    .IsRequired()
                    .HasColumnName("ACCDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasColumnName("COMPANY")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ctotal)
                    .HasColumnName("CTOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Dtotal)
                    .HasColumnName("DTOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Ntus)
                    .IsRequired()
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Papertype)
                    .HasColumnName("PAPERTYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Passid)
                    .HasColumnName("PASSID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Passpaper)
                    .HasColumnName("PASSPAPER")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.YnPrint)
                    .HasColumnName("YN_PRINT")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Acctg20>(entity =>
            {
                entity.HasKey(e => new { e.Accid, e.Serno });

                entity.ToTable("ACCTG20");

                entity.Property(e => e.Accid)
                    .HasColumnName("ACCID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cmoney)
                    .HasColumnName("CMONEY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Dmoney)
                    .HasColumnName("DMONEY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Subaccno)
                    .HasColumnName("SUBACCNO")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Acctg30>(entity =>
            {
                entity.HasKey(e => new { e.PayNo, e.CheckNo });

                entity.ToTable("ACCTG30");

                entity.Property(e => e.PayNo)
                    .HasColumnName("PAY_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.CheckNo)
                    .HasColumnName("CHECK_NO")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Accid3)
                    .HasColumnName("ACCID3")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accid4)
                    .HasColumnName("ACCID4")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accno3)
                    .HasColumnName("ACCNO3")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Accno4)
                    .HasColumnName("ACCNO4")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CheckType)
                    .HasColumnName("CHECK_TYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.DLine)
                    .HasColumnName("D_LINE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.VendorNo)
                    .HasColumnName("VENDOR_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.YesGet).HasColumnName("YES_GET");
            });

            modelBuilder.Entity<Accye10>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCYE10");

                entity.HasIndex(e => new { e.Company, e.Accmonth, e.Accno, e.Subaccno, e.Ntus })
                    .HasName("IX_ACCYE10")
                    .IsClustered();

                entity.Property(e => e.Accmonth)
                    .HasColumnName("ACCMONTH")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Acctype)
                    .HasColumnName("ACCTYPE")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasColumnName("COMPANY")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ctotal).HasColumnName("CTOTAL");

                entity.Property(e => e.Dtotal).HasColumnName("DTOTAL");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Printtype)
                    .HasColumnName("PRINTTYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Subaccno)
                    .HasColumnName("SUBACCNO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Subtotal).HasColumnName("SUBTOTAL");

                entity.Property(e => e.Taccno)
                    .HasColumnName("TACCNO")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Agentlog>(entity =>
            {
                entity.HasKey(e => e.Agentid);

                entity.ToTable("AGENTLOG");

                entity.Property(e => e.Agentid)
                    .HasColumnName("AGENTID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Agentuserid)
                    .IsRequired()
                    .HasColumnName("AGENTUSERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Execdate)
                    .HasColumnName("EXECDATE")
                    .HasMaxLength(19)
                    .IsUnicode(false);

                entity.Property(e => e.Exitdate)
                    .HasColumnName("EXITDATE")
                    .HasMaxLength(19)
                    .IsUnicode(false);

                entity.Property(e => e.Menuid)
                    .IsRequired()
                    .HasColumnName("MENUID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Autonum>(entity =>
            {
                entity.HasKey(e => new { e.Numbername, e.Currentprefix });

                entity.ToTable("AUTONUM");

                entity.Property(e => e.Numbername)
                    .HasColumnName("NUMBERNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Currentprefix)
                    .HasColumnName("CURRENTPREFIX")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Currentdigits).HasColumnName("CURRENTDIGITS");

                entity.Property(e => e.Digitswidth).HasColumnName("DIGITSWIDTH");

                entity.Property(e => e.Maxvalue).HasColumnName("MAXVALUE");

                entity.Property(e => e.Minvalue).HasColumnName("MINVALUE");

                entity.Property(e => e.Valueinterval).HasColumnName("VALUEINTERVAL");
            });

            modelBuilder.Entity<Bank10>(entity =>
            {
                entity.HasKey(e => e.BankNo);

                entity.ToTable("BANK10");

                entity.Property(e => e.BankNo)
                    .HasColumnName("BANK_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasColumnName("BANK_NAME")
                    .HasMaxLength(26)
                    .IsUnicode(false);

                entity.Property(e => e.Initamount)
                    .HasColumnName("INITAMOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Initdate)
                    .HasColumnName("INITDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bank20>(entity =>
            {
                entity.HasKey(e => e.IoNo);

                entity.ToTable("BANK20");

                entity.Property(e => e.IoNo)
                    .HasColumnName("IO_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accid)
                    .HasColumnName("ACCID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.CrMemo)
                    .HasColumnName("CR_MEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CrNo)
                    .HasColumnName("CR_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeMemo)
                    .HasColumnName("DE_MEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DeNo)
                    .HasColumnName("DE_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InBankno)
                    .HasColumnName("IN_BANKNO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InMoney).HasColumnName("IN_MONEY");

                entity.Property(e => e.IoDate)
                    .HasColumnName("IO_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OutBankno)
                    .HasColumnName("OUT_BANKNO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OutMoney).HasColumnName("OUT_MONEY");
            });

            modelBuilder.Entity<Bou10>(entity =>
            {
                entity.HasKey(e => e.QuNo);

                entity.ToTable("BOU10");

                entity.Property(e => e.QuNo)
                    .HasColumnName("QU_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Atte)
                    .HasColumnName("ATTE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Corp)
                    .HasColumnName("CORP")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PaperTail)
                    .HasColumnName("PAPER_TAIL")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Payment)
                    .HasColumnName("PAYMENT")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.QuDate)
                    .IsRequired()
                    .HasColumnName("QU_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Sales)
                    .HasColumnName("SALES")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.SendAddr)
                    .HasColumnName("SEND_ADDR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendDate)
                    .HasColumnName("SEND_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasColumnName("TEL")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.Property(e => e.Total0).HasColumnName("TOTAL0");

                entity.Property(e => e.Total1).HasColumnName("TOTAL1");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.YnPass)
                    .HasColumnName("YN_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bou20>(entity =>
            {
                entity.HasKey(e => new { e.QuNo, e.Serno, e.PartNo });

                entity.ToTable("BOU20");

                entity.Property(e => e.QuNo)
                    .HasColumnName("QU_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Profit)
                    .HasColumnName("PROFIT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.SPrice).HasColumnName("S_PRICE");

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bplfilelist>(entity =>
            {
                entity.HasKey(e => e.Filename);

                entity.ToTable("BPLFILELIST");

                entity.Property(e => e.Filename)
                    .HasColumnName("FILENAME")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("CONTENT")
                    .HasColumnType("image");

                entity.Property(e => e.Createdate)
                    .IsRequired()
                    .HasColumnName("CREATEDATE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Createtime)
                    .IsRequired()
                    .HasColumnName("CREATETIME")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Filepath)
                    .HasColumnName("FILEPATH")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Filesize).HasColumnName("FILESIZE");

                entity.Property(e => e.Filetime).HasColumnName("FILETIME");

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("OWNER")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Updatemode)
                    .HasColumnName("UPDATEMODE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Chstock10>(entity =>
            {
                entity.HasKey(e => e.ChNo);

                entity.ToTable("CHSTOCK10");

                entity.Property(e => e.ChNo)
                    .HasColumnName("CH_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ChDate)
                    .HasColumnName("CH_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ChPe)
                    .HasColumnName("CH_PE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.InitDate)
                    .HasColumnName("INIT_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.StockPass)
                    .HasColumnName("STOCK_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Chstock11>(entity =>
            {
                entity.HasKey(e => new { e.ChNo, e.PartNo });

                entity.ToTable("CHSTOCK11");

                entity.Property(e => e.ChNo)
                    .HasColumnName("CH_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CheckQty).HasColumnName("CHECK_QTY");

                entity.Property(e => e.InitDate)
                    .HasColumnName("INIT_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SubQty).HasColumnName("SUB_QTY");
            });

            modelBuilder.Entity<Coldef>(entity =>
            {
                entity.HasKey(e => new { e.TableName, e.FieldName });

                entity.ToTable("COLDEF");

                entity.Property(e => e.TableName)
                    .HasColumnName("TABLE_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .HasColumnName("FIELD_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Canreport)
                    .HasColumnName("CANREPORT")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Caption)
                    .HasColumnName("CAPTION")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DdName)
                    .HasColumnName("DD_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Editmask)
                    .HasColumnName("EDITMASK")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ExtMenuid)
                    .HasColumnName("EXT_MENUID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FieldLength)
                    .HasColumnName("FIELD_LENGTH")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.FieldScale)
                    .HasColumnName("FIELD_SCALE")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.FieldType)
                    .HasColumnName("FIELD_TYPE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsKey)
                    .IsRequired()
                    .HasColumnName("IS_KEY")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Lastupdatedatetime)
                    .HasColumnName("LASTUPDATEDATETIME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Needbox)
                    .HasColumnName("NEEDBOX")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Seq)
                    .HasColumnName("SEQ")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Sysflag)
                    .HasColumnName("SYSFLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("UPDATE_TIME")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("UPDATE_USER")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Consts>(entity =>
            {
                entity.HasKey(e => new { e.Consttype, e.Constid, e.Constvalue });

                entity.ToTable("CONSTS");

                entity.Property(e => e.Consttype)
                    .HasColumnName("CONSTTYPE")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Constid)
                    .HasColumnName("CONSTID")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Constvalue)
                    .HasColumnName("CONSTVALUE")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cstm10>(entity =>
            {
                entity.HasKey(e => e.CoNo)
                    .IsClustered(false);

                entity.ToTable("CSTM10");

                entity.HasIndex(e => e.CoNo)
                    .HasName("IX_CSTM10")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.AreaNo)
                    .HasColumnName("AREA_NO")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Boss)
                    .HasColumnName("BOSS")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ChehkDay)
                    .HasColumnName("CHEHK_DAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Compaddr)
                    .HasColumnName("COMPADDR")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasColumnName("COMPANY")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Coname)
                    .HasColumnName("CONAME")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CusType)
                    .HasColumnName("CUS_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.Dlien)
                    .HasColumnName("DLIEN")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DriveNo)
                    .HasColumnName("DRIVE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Invocomp)
                    .HasColumnName("INVOCOMP")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasColumnName("MOBILE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NotGet)
                    .HasColumnName("NOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Payaccount)
                    .HasColumnName("PAYACCOUNT")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Paybank)
                    .HasColumnName("PAYBANK")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Payment)
                    .HasColumnName("PAYMENT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Prenotget).HasColumnName("PRENOTGET");

                entity.Property(e => e.PriceType)
                    .HasColumnName("PRICE_TYPE")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PrintPrice)
                    .HasColumnName("PRINT_PRICE")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasColumnName("PRODUCT")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sales)
                    .HasColumnName("SALES")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sendaddr)
                    .HasColumnName("SENDADDR")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.SubTot)
                    .HasColumnName("SUB_TOT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Tax).HasColumnName("TAX");

                entity.Property(e => e.TaxType)
                    .HasColumnName("TAX_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Taxrate)
                    .HasColumnName("TAXRATE")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Tel1)
                    .HasColumnName("TEL1")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Tel2)
                    .HasColumnName("TEL2")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Total1).HasColumnName("TOTAL1");

                entity.Property(e => e.Total2).HasColumnName("TOTAL2");

                entity.Property(e => e.Uniform)
                    .HasColumnName("UNIFORM")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Www)
                    .HasColumnName("WWW")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YesGet)
                    .HasColumnName("YES_GET")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<Fix10>(entity =>
            {
                entity.HasKey(e => e.FixNo);

                entity.ToTable("FIX10");

                entity.Property(e => e.FixNo)
                    .HasColumnName("FIX_NO")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FixName)
                    .HasColumnName("FIX_NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FixType)
                    .HasColumnName("FIX_TYPE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.HoldAmt).HasColumnName("HOLD_AMT");

                entity.Property(e => e.InAmt).HasColumnName("IN_AMT");

                entity.Property(e => e.InDate)
                    .HasColumnName("IN_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.NotAmt).HasColumnName("NOT_AMT");

                entity.Property(e => e.Ntus)
                    .IsRequired()
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Remark)
                    .HasColumnName("REMARK")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RepairAmt).HasColumnName("REPAIR_AMT");

                entity.Property(e => e.RepairDate)
                    .HasColumnName("REPAIR_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SetPlace)
                    .HasColumnName("SET_PLACE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SourceName)
                    .HasColumnName("SOURCE_NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ThisAmt).HasColumnName("THIS_AMT");

                entity.Property(e => e.ThisDate)
                    .HasColumnName("THIS_DATE")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmt).HasColumnName("TOTAL_AMT");

                entity.Property(e => e.TypeNo)
                    .HasColumnName("TYPE_NO")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Useyear).HasColumnName("USEYEAR");

                entity.Property(e => e.YnCont)
                    .HasColumnName("YN_CONT")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.YnHoldAmt).HasColumnName("YN_HOLD_AMT");

                entity.Property(e => e.YnUseyear).HasColumnName("YN_USEYEAR");
            });

            modelBuilder.Entity<Gcheck10>(entity =>
            {
                entity.HasKey(e => e.PayNo);

                entity.ToTable("GCHECK10");

                entity.Property(e => e.PayNo)
                    .HasColumnName("PAY_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accid1)
                    .HasColumnName("ACCID1")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accid2)
                    .HasColumnName("ACCID2")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accid3)
                    .HasColumnName("ACCID3")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accmemo)
                    .HasColumnName("ACCMEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Accno3)
                    .HasColumnName("ACCNO3")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankNo)
                    .HasColumnName("BANK_NO")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.BankSerno)
                    .HasColumnName("BANK_SERNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CheckNo)
                    .HasColumnName("CHECK_NO")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CheckType)
                    .HasColumnName("CHECK_TYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.DLine)
                    .HasColumnName("D_LINE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.GetType)
                    .HasColumnName("GET_TYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.OutNo)
                    .HasColumnName("OUT_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PayDate)
                    .HasColumnName("PAY_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Payaccount)
                    .HasColumnName("PAYACCOUNT")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Paybank)
                    .HasColumnName("PAYBANK")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sendbankdate)
                    .HasColumnName("SENDBANKDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Sub1Tot).HasColumnName("SUB1_TOT");

                entity.Property(e => e.SubTot).HasColumnName("SUB_TOT");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.YesGet).HasColumnName("YES_GET");

                entity.Property(e => e.YnPass)
                    .HasColumnName("YN_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gcheck30>(entity =>
            {
                entity.HasKey(e => new { e.PayNo, e.Serno });

                entity.ToTable("GCHECK30");

                entity.Property(e => e.PayNo)
                    .HasColumnName("PAY_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Maccno)
                    .HasColumnName("MACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Mmoney)
                    .HasColumnName("MMONEY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Paccno)
                    .HasColumnName("PACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Pmoney)
                    .HasColumnName("PMONEY")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<Groupdisitems>(entity =>
            {
                entity.HasKey(e => new { e.Groupid, e.Menuid, e.Controlclass });

                entity.ToTable("GROUPDISITEMS");

                entity.Property(e => e.Groupid)
                    .HasColumnName("GROUPID")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Controlclass)
                    .HasColumnName("CONTROLCLASS")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Caption)
                    .HasColumnName("CAPTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Groupmenus>(entity =>
            {
                entity.HasKey(e => new { e.Groupid, e.Menuid });

                entity.ToTable("GROUPMENUS");

                entity.Property(e => e.Groupid)
                    .HasColumnName("GROUPID")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.Groupid);

                entity.ToTable("GROUPS");

                entity.Property(e => e.Groupid)
                    .HasColumnName("GROUPID")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Groupname)
                    .HasColumnName("GROUPNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IPay10>(entity =>
            {
                entity.HasKey(e => new { e.Paymonth, e.VendorNo, e.Ntus });

                entity.ToTable("I_PAY10");

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.VendorNo)
                    .HasColumnName("VENDOR_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Accid)
                    .HasColumnName("ACCID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.CashDiscount)
                    .HasColumnName("CASH_DISCOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.LnotGet)
                    .HasColumnName("LNOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NotGet)
                    .HasColumnName("NOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.RetPercent)
                    .HasColumnName("RET_PERCENT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.RetTotal)
                    .HasColumnName("RET_TOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SubTot)
                    .HasColumnName("SUB_TOT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Tax)
                    .HasColumnName("TAX")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.TnotGet)
                    .HasColumnName("TNOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total)
                    .HasColumnName("TOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total0)
                    .HasColumnName("TOTAL0")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total1)
                    .HasColumnName("TOTAL1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total2)
                    .HasColumnName("TOTAL2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.YesGet)
                    .HasColumnName("YES_GET")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<In10>(entity =>
            {
                entity.HasKey(e => e.InNo);

                entity.ToTable("IN10");

                entity.Property(e => e.InNo)
                    .HasColumnName("IN_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CashDis).HasColumnName("CASH_DIS");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.InDate)
                    .HasColumnName("IN_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.InType)
                    .HasColumnName("IN_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceNo)
                    .HasColumnName("INVOICE_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NotGet)
                    .HasColumnName("NOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.StockPass)
                    .HasColumnName("STOCK_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SubTot).HasColumnName("SUB_TOT");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.Property(e => e.Total0).HasColumnName("TOTAL0");

                entity.Property(e => e.Total1).HasColumnName("TOTAL1");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.VendorNo)
                    .HasColumnName("VENDOR_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.YesGet).HasColumnName("YES_GET");

                entity.Property(e => e.YnClose)
                    .HasColumnName("YN_CLOSE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<In20>(entity =>
            {
                entity.HasKey(e => new { e.InNo, e.Serno, e.PartNo });

                entity.ToTable("IN20");

                entity.Property(e => e.InNo)
                    .HasColumnName("IN_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.PurNo)
                    .HasColumnName("PUR_NO")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inout10>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("INOUT10");

                entity.Property(e => e.BalAmount).HasColumnName("BAL_AMOUNT");

                entity.Property(e => e.BalPrice).HasColumnName("BAL_PRICE");

                entity.Property(e => e.BalkgQty)
                    .HasColumnName("BALKG_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.BalpQty)
                    .HasColumnName("BALP_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.BalstQty)
                    .HasColumnName("BALST_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.InAmount).HasColumnName("IN_AMOUNT");

                entity.Property(e => e.InPrice).HasColumnName("IN_PRICE");

                entity.Property(e => e.Indexno).HasColumnName("INDEXNO");

                entity.Property(e => e.InkgQty)
                    .HasColumnName("INKG_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.InpQty)
                    .HasColumnName("INP_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.InstQty)
                    .HasColumnName("INST_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IoDate)
                    .IsRequired()
                    .HasColumnName("IO_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.KgQty)
                    .HasColumnName("KG_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.OutAmount).HasColumnName("OUT_AMOUNT");

                entity.Property(e => e.OutNo)
                    .HasColumnName("OUT_NO")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OutPrice).HasColumnName("OUT_PRICE");

                entity.Property(e => e.OutkgQty)
                    .HasColumnName("OUTKG_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.OutpQty)
                    .HasColumnName("OUTP_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.OutstQty)
                    .HasColumnName("OUTST_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.PAmount).HasColumnName("P_AMOUNT");

                entity.Property(e => e.PPrice).HasColumnName("P_PRICE");

                entity.Property(e => e.PQty)
                    .HasColumnName("P_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasColumnName("PART_NO")
                    .HasMaxLength(37)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.StQty)
                    .HasColumnName("ST_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invo10>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo);

                entity.ToTable("INVO10");

                entity.Property(e => e.InvoiceNo)
                    .HasColumnName("INVOICE_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InvoDate)
                    .HasColumnName("INVO_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.StockPass)
                    .HasColumnName("STOCK_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Tax).HasColumnName("TAX");

                entity.Property(e => e.TaxType)
                    .HasColumnName("TAX_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Total1).HasColumnName("TOTAL1");

                entity.Property(e => e.Total2).HasColumnName("TOTAL2");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.VendorNo)
                    .HasColumnName("VENDOR_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Ynclose)
                    .HasColumnName("YNCLOSE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invo20>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceNo, e.Serno })
                    .IsClustered(false);

                entity.ToTable("INVO20");

                entity.HasIndex(e => new { e.InvoiceNo, e.Serno })
                    .HasName("IX_INVO20")
                    .IsClustered();

                entity.Property(e => e.InvoiceNo)
                    .HasColumnName("INVOICE_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvoInout10>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("INVO_INOUT10");

                entity.Property(e => e.BalAmount)
                    .HasColumnName("BAL_AMOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.BalPrice)
                    .HasColumnName("BAL_PRICE")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.BalQty)
                    .HasColumnName("BAL_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.InAmount)
                    .HasColumnName("IN_AMOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.InPrice)
                    .HasColumnName("IN_PRICE")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.InQty)
                    .HasColumnName("IN_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Indexno).HasColumnName("INDEXNO");

                entity.Property(e => e.InitAmount)
                    .HasColumnName("INIT_AMOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.InitPrice)
                    .HasColumnName("INIT_PRICE")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.InitQty)
                    .HasColumnName("INIT_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IoDate)
                    .IsRequired()
                    .HasColumnName("IO_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OutAmount)
                    .HasColumnName("OUT_AMOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.OutPrice)
                    .HasColumnName("OUT_PRICE")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.OutQty)
                    .HasColumnName("OUT_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasColumnName("PART_NO")
                    .HasMaxLength(37)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.StockNo)
                    .HasColumnName("STOCK_NO")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Is10>(entity =>
            {
                entity.HasKey(e => e.IsNo);

                entity.ToTable("IS10");

                entity.Property(e => e.IsNo)
                    .HasColumnName("IS_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsDate)
                    .HasColumnName("IS_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IsType)
                    .HasColumnName("IS_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IspeNo)
                    .HasColumnName("ISPE_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StockPass)
                    .HasColumnName("STOCK_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Is20>(entity =>
            {
                entity.HasKey(e => new { e.IsNo, e.Serno });

                entity.ToTable("IS20");

                entity.Property(e => e.IsNo)
                    .HasColumnName("IS_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Jobparams>(entity =>
            {
                entity.HasKey(e => new { e.Jobid, e.Paramname });

                entity.ToTable("JOBPARAMS");

                entity.Property(e => e.Jobid)
                    .HasColumnName("JOBID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Paramname)
                    .HasColumnName("PARAMNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Paramtype)
                    .IsRequired()
                    .HasColumnName("PARAMTYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Paramvalue)
                    .HasColumnName("PARAMVALUE")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Jobtable>(entity =>
            {
                entity.HasKey(e => e.Jobid);

                entity.ToTable("JOBTABLE");

                entity.Property(e => e.Jobid)
                    .HasColumnName("JOBID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Createdate)
                    .HasColumnName("CREATEDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Createtime)
                    .HasColumnName("CREATETIME")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Execflag)
                    .HasColumnName("EXECFLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Jobdescrip)
                    .HasColumnName("JOBDESCRIP")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lasttimedate)
                    .HasColumnName("LASTTIMEDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Lasttimetime)
                    .HasColumnName("LASTTIMETIME")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Methodname)
                    .HasColumnName("METHODNAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notificationdata)
                    .HasColumnName("NOTIFICATIONDATA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasColumnName("OWNER")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Packagename)
                    .HasColumnName("PACKAGENAME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Scheduler)
                    .HasColumnName("SCHEDULER")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Scheduletype)
                    .HasColumnName("SCHEDULETYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Selectedmonthdays).HasColumnName("SELECTEDMONTHDAYS");

                entity.Property(e => e.Selectedweekdays)
                    .HasColumnName("SELECTEDWEEKDAYS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Startdate)
                    .HasColumnName("STARTDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Starttime)
                    .HasColumnName("STARTTIME")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Timestep).HasColumnName("TIMESTEP");

                entity.Property(e => e.Unittype)
                    .HasColumnName("UNITTYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mailaddress>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Mailaddr, e.Owner });

                entity.ToTable("MAILADDRESS");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Mailaddr)
                    .HasColumnName("MAILADDR")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasColumnName("OWNER")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mailgroups>(entity =>
            {
                entity.HasKey(e => new { e.Groupname, e.Owner });

                entity.ToTable("MAILGROUPS");

                entity.Property(e => e.Groupname)
                    .HasColumnName("GROUPNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasColumnName("OWNER")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Maillog>(entity =>
            {
                entity.HasKey(e => new { e.Createdate, e.Createtime, e.Owner, e.Seqno });

                entity.ToTable("MAILLOG");

                entity.Property(e => e.Createdate)
                    .HasColumnName("CREATEDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Createtime)
                    .HasColumnName("CREATETIME")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasColumnName("OWNER")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Seqno)
                    .HasColumnName("SEQNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Bcc)
                    .HasColumnName("BCC")
                    .HasMaxLength(232)
                    .IsUnicode(false);

                entity.Property(e => e.Cc)
                    .HasColumnName("CC")
                    .HasMaxLength(232)
                    .IsUnicode(false);

                entity.Property(e => e.Executive)
                    .IsRequired()
                    .HasColumnName("EXECUTIVE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mailfrom)
                    .HasColumnName("MAILFROM")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mailto)
                    .HasColumnName("MAILTO")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnName("RESULT")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasColumnName("SUBJECT")
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mailusergroups>(entity =>
            {
                entity.HasKey(e => new { e.Groupname, e.Username, e.Mailaddr, e.Owner });

                entity.ToTable("MAILUSERGROUPS");

                entity.Property(e => e.Groupname)
                    .HasColumnName("GROUPNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Mailaddr)
                    .HasColumnName("MAILADDR")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasColumnName("OWNER")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Memdbf>(entity =>
            {
                entity.HasKey(e => e.Progno)
                    .IsClustered(false);

                entity.ToTable("MEMDBF");

                entity.HasIndex(e => e.Progno)
                    .HasName("IX_MEMDBF")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Progno)
                    .HasColumnName("PROGNO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("CONTENT")
                    .HasColumnType("text");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Memo10>(entity =>
            {
                entity.HasKey(e => e.Progno);

                entity.ToTable("MEMO10");

                entity.Property(e => e.Progno)
                    .HasColumnName("PROGNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasColumnType("text");

                entity.Property(e => e.Whereuse)
                    .HasColumnName("WHEREUSE")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menucontrols>(entity =>
            {
                entity.HasKey(e => new { e.Menuid, e.Controlclass });

                entity.ToTable("MENUCONTROLS");

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Controlclass)
                    .HasColumnName("CONTROLCLASS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Caption)
                    .HasColumnName("CAPTION")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menuitemtype>(entity =>
            {
                entity.HasKey(e => e.Itemtype);

                entity.ToTable("MENUITEMTYPE");

                entity.Property(e => e.Itemtype)
                    .HasColumnName("ITEMTYPE")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Itemname)
                    .HasColumnName("ITEMNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menutable>(entity =>
            {
                entity.HasKey(e => e.Menuid);

                entity.ToTable("MENUTABLE");

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Caption)
                    .IsRequired()
                    .HasColumnName("CAPTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Form)
                    .HasColumnName("FORM")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Functions)
                    .HasColumnName("FUNCTIONS")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Isshowmodal)
                    .HasColumnName("ISSHOWMODAL")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Itemparam)
                    .HasColumnName("ITEMPARAM")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Itemtype)
                    .HasColumnName("ITEMTYPE")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Package)
                    .HasColumnName("PACKAGE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Packagecla)
                    .HasColumnName("PACKAGECLA")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Parent)
                    .HasColumnName("PARENT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SeqNo)
                    .HasColumnName("SEQ_NO")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Messagetable>(entity =>
            {
                entity.HasKey(e => new { e.Msgdate, e.Msgtime, e.Seqno });

                entity.ToTable("MESSAGETABLE");

                entity.Property(e => e.Msgdate)
                    .HasColumnName("MSGDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Msgtime)
                    .HasColumnName("MSGTIME")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Seqno).HasColumnName("SEQNO");

                entity.Property(e => e.Data)
                    .HasColumnName("DATA")
                    .HasColumnType("image");

                entity.Property(e => e.Flag).HasColumnName("FLAG");

                entity.Property(e => e.Messageid).HasColumnName("MESSAGEID");

                entity.Property(e => e.Methodid).HasColumnName("METHODID");

                entity.Property(e => e.Optdata)
                    .HasColumnName("OPTDATA")
                    .HasColumnType("image");

                entity.Property(e => e.Ownerid)
                    .HasColumnName("OWNERID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Trycount).HasColumnName("TRYCOUNT");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("USERID")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mymenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MYMENU");

                entity.Property(e => e.Caption)
                    .HasColumnName("CAPTION")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Form)
                    .HasColumnName("FORM")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Itemtype)
                    .HasColumnName("ITEMTYPE")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Package)
                    .HasColumnName("PACKAGE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OGet10>(entity =>
            {
                entity.HasKey(e => new { e.Paymonth, e.CoNo, e.Ntus });

                entity.ToTable("O_GET10");

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Accid)
                    .HasColumnName("ACCID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.CashDiscount)
                    .HasColumnName("CASH_DISCOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.LnotGet)
                    .HasColumnName("LNOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NotGet)
                    .HasColumnName("NOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.RetPercent)
                    .HasColumnName("RET_PERCENT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.RetTotal)
                    .HasColumnName("RET_TOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SubTot)
                    .HasColumnName("SUB_TOT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Tax)
                    .HasColumnName("TAX")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.TnotGet)
                    .HasColumnName("TNOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total)
                    .HasColumnName("TOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total0)
                    .HasColumnName("TOTAL0")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total1)
                    .HasColumnName("TOTAL1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total2)
                    .HasColumnName("TOTAL2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.YesGet)
                    .HasColumnName("YES_GET")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<Order10>(entity =>
            {
                entity.HasKey(e => e.OrderNo)
                    .HasName("PK_訂單資料");

                entity.ToTable("ORDER10");

                entity.Property(e => e.OrderNo)
                    .HasColumnName("ORDER_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("ORDER_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OrderType)
                    .HasColumnName("ORDER_TYPE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Outsource01)
                    .HasColumnName("OUTSOURCE01")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Outsource02)
                    .HasColumnName("OUTSOURCE02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Outsource03)
                    .HasColumnName("OUTSOURCE03")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Proc01)
                    .HasColumnName("PROC01")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc02)
                    .HasColumnName("PROC02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc03)
                    .HasColumnName("PROC03")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc04)
                    .HasColumnName("PROC04")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc05)
                    .HasColumnName("PROC05")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc06)
                    .HasColumnName("PROC06")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc07)
                    .HasColumnName("PROC07")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc08)
                    .HasColumnName("PROC08")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.YnPur)
                    .HasColumnName("YN_PUR")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.YnWork)
                    .HasColumnName("YN_WORK")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order20>(entity =>
            {
                entity.HasKey(e => new { e.OrderNo, e.Serno });

                entity.ToTable("ORDER20");

                entity.Property(e => e.OrderNo)
                    .HasColumnName("ORDER_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Barcode)
                    .HasColumnName("BARCODE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cno)
                    .HasColumnName("CNO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ctn)
                    .HasColumnName("CTN")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Ctnsize)
                    .HasColumnName("CTNSIZE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cuft1)
                    .HasColumnName("CUFT1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Cuft2)
                    .HasColumnName("CUFT2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.GoodNo)
                    .HasColumnName("GOOD_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gw1)
                    .HasColumnName("GW1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Gw2)
                    .HasColumnName("GW2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Nw1)
                    .HasColumnName("NW1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Nw2)
                    .HasColumnName("NW2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.OutNo)
                    .HasColumnName("OUT_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.OutQty)
                    .HasColumnName("OUT_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Packing)
                    .HasColumnName("PACKING")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Pcs)
                    .HasColumnName("PCS")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Pcsctn)
                    .HasColumnName("PCSCTN")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Price)
                    .HasColumnName("PRICE")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Qty)
                    .HasColumnName("QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SendDate)
                    .HasColumnName("SEND_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.WorkOk)
                    .HasColumnName("WORK_OK")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.WorkQty)
                    .HasColumnName("WORK_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.YnClose)
                    .HasColumnName("YN_CLOSE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.YnWork)
                    .HasColumnName("YN_WORK")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Out10>(entity =>
            {
                entity.HasKey(e => e.OutNo);

                entity.ToTable("OUT10");

                entity.Property(e => e.OutNo)
                    .HasColumnName("OUT_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CashDis).HasColumnName("CASH_DIS");

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.DriveNo)
                    .HasColumnName("DRIVE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.KeyinDate)
                    .HasColumnName("KEYIN_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Kg).HasColumnName("KG");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NotGet)
                    .HasColumnName("NOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.OutDate)
                    .HasColumnName("OUT_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OutType)
                    .HasColumnName("OUT_TYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.StockPass)
                    .HasColumnName("STOCK_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SubTot).HasColumnName("SUB_TOT");

                entity.Property(e => e.Tax).HasColumnName("TAX");

                entity.Property(e => e.Total)
                    .HasColumnName("TOTAL")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total0)
                    .HasColumnName("TOTAL0")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total1)
                    .HasColumnName("TOTAL1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.YesGet).HasColumnName("YES_GET");

                entity.Property(e => e.YnClose)
                    .HasColumnName("YN_CLOSE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Out20>(entity =>
            {
                entity.HasKey(e => new { e.OutNo, e.Serno, e.PartNo });

                entity.ToTable("OUT20");

                entity.Property(e => e.OutNo)
                    .HasColumnName("OUT_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PPrice).HasColumnName("P_PRICE");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.SPrice).HasColumnName("S_PRICE");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Out30>(entity =>
            {
                entity.HasKey(e => new { e.CoNo, e.Paymonth });

                entity.ToTable("OUT30");

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CashDis).HasColumnName("CASH_DIS");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NotGet)
                    .HasColumnName("NOT_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.StockPass)
                    .HasColumnName("STOCK_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SubTot).HasColumnName("SUB_TOT");

                entity.Property(e => e.Tax)
                    .HasColumnName("TAX")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.TaxType)
                    .HasColumnName("TAX_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Total0)
                    .HasColumnName("TOTAL0")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total1)
                    .HasColumnName("TOTAL1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Total2)
                    .HasColumnName("TOTAL2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Out40>(entity =>
            {
                entity.HasKey(e => new { e.CoNo, e.Paymonth, e.Barcode, e.PartNo, e.Price, e.Unit });

                entity.ToTable("OUT40");

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Barcode)
                    .HasColumnName("BARCODE")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Discount)
                    .HasColumnName("DISCOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.InQty).HasColumnName("IN_QTY");

                entity.Property(e => e.InretQty).HasColumnName("INRET_QTY");

                entity.Property(e => e.LQty).HasColumnName("L_QTY");

                entity.Property(e => e.OutQty).HasColumnName("OUT_QTY");

                entity.Property(e => e.StQty).HasColumnName("ST_QTY");
            });

            modelBuilder.Entity<Outvo10>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo);

                entity.ToTable("OUTVO10");

                entity.Property(e => e.InvoiceNo)
                    .HasColumnName("INVOICE_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BookNo)
                    .HasColumnName("BOOK_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.InvoDate)
                    .HasColumnName("INVO_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OutNo)
                    .HasColumnName("OUT_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.StockPass)
                    .HasColumnName("STOCK_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Tax).HasColumnName("TAX");

                entity.Property(e => e.TaxType)
                    .HasColumnName("TAX_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Total1).HasColumnName("TOTAL1");

                entity.Property(e => e.Total2).HasColumnName("TOTAL2");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Uniform)
                    .HasColumnName("UNIFORM")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Ynclose)
                    .HasColumnName("YNCLOSE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Outvo20>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceNo, e.Serno })
                    .IsClustered(false);

                entity.ToTable("OUTVO20");

                entity.HasIndex(e => new { e.InvoiceNo, e.Serno })
                    .HasName("IX_OUTVO20")
                    .IsClustered();

                entity.Property(e => e.InvoiceNo)
                    .HasColumnName("INVOICE_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Paperno10>(entity =>
            {
                entity.HasKey(e => e.PaperNo);

                entity.ToTable("PAPERNO10");

                entity.Property(e => e.PaperNo)
                    .HasColumnName("PAPER_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Bdate)
                    .HasColumnName("BDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Bno)
                    .HasColumnName("BNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Counter).HasColumnName("COUNTER");

                entity.Property(e => e.Edate)
                    .HasColumnName("EDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Eno)
                    .HasColumnName("ENO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NoLength).HasColumnName("NO_LENGTH");

                entity.Property(e => e.NowActive)
                    .HasColumnName("NOW_ACTIVE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PaperType)
                    .HasColumnName("PAPER_TYPE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Prefix)
                    .HasColumnName("PREFIX")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Partdesc10>(entity =>
            {
                entity.HasKey(e => e.Descno)
                    .IsClustered(false);

                entity.ToTable("PARTDESC10");

                entity.HasIndex(e => e.Descno)
                    .HasName("IX_PARTDESC10")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Descno)
                    .HasColumnName("DESCNO")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Descname)
                    .HasColumnName("DESCNAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Partpress10>(entity =>
            {
                entity.HasKey(e => e.Pressno)
                    .HasName("PK_PARTPRESS110")
                    .IsClustered(false);

                entity.ToTable("PARTPRESS10");

                entity.HasIndex(e => e.Pressno)
                    .HasName("IX_PARTPRESS110")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Pressno)
                    .HasColumnName("PRESSNO")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Pressname)
                    .HasColumnName("PRESSNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Partprint10>(entity =>
            {
                entity.HasKey(e => e.Printno)
                    .IsClustered(false);

                entity.ToTable("PARTPRINT10");

                entity.HasIndex(e => e.Printno)
                    .HasName("IX_PARTPRINT10")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Printno)
                    .HasColumnName("PRINTNO")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Printname)
                    .HasColumnName("PRINTNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Partstock10>(entity =>
            {
                entity.HasKey(e => e.Stockno)
                    .IsClustered(false);

                entity.ToTable("PARTSTOCK10");

                entity.HasIndex(e => e.Stockno)
                    .HasName("IX_PARTSTOCK10")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Stockno)
                    .HasColumnName("STOCKNO")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Stockname)
                    .HasColumnName("STOCKNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parttype10>(entity =>
            {
                entity.HasKey(e => e.Typeno)
                    .IsClustered(false);

                entity.ToTable("PARTTYPE10");

                entity.HasIndex(e => e.Typeno)
                    .HasName("IX_PARTTYPE10")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Typeno)
                    .HasColumnName("TYPENO")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TypeDesc)
                    .HasColumnName("TYPE_DESC")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Typepart)
                    .HasColumnName("TYPEPART")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Typeuse)
                    .HasColumnName("TYPEUSE")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.YnCount)
                    .HasColumnName("YN_COUNT")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.YnDrive)
                    .HasColumnName("YN_DRIVE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pcheck10>(entity =>
            {
                entity.HasKey(e => e.PayNo);

                entity.ToTable("PCHECK10");

                entity.Property(e => e.PayNo)
                    .HasColumnName("PAY_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accid1)
                    .HasColumnName("ACCID1")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accid2)
                    .HasColumnName("ACCID2")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accid3)
                    .HasColumnName("ACCID3")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Accmemo)
                    .HasColumnName("ACCMEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Accno)
                    .HasColumnName("ACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Accno3)
                    .HasColumnName("ACCNO3")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankNo)
                    .HasColumnName("BANK_NO")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.CheckNo)
                    .HasColumnName("CHECK_NO")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CheckType)
                    .HasColumnName("CHECK_TYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DLine)
                    .HasColumnName("D_LINE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.GetType)
                    .HasColumnName("GET_TYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.InNo)
                    .HasColumnName("IN_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PayDate)
                    .HasColumnName("PAY_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Sub1Tot).HasColumnName("SUB1_TOT");

                entity.Property(e => e.SubTot).HasColumnName("SUB_TOT");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.VendorNo)
                    .HasColumnName("VENDOR_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.YesGet).HasColumnName("YES_GET");

                entity.Property(e => e.YnPass)
                    .HasColumnName("YN_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pcheck30>(entity =>
            {
                entity.HasKey(e => new { e.PayNo, e.Serno });

                entity.ToTable("PCHECK30");

                entity.Property(e => e.PayNo)
                    .HasColumnName("PAY_NO")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Maccno)
                    .HasColumnName("MACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Mmoney)
                    .HasColumnName("MMONEY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Paccno)
                    .HasColumnName("PACCNO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Pmoney)
                    .HasColumnName("PMONEY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.VendorNo)
                    .HasColumnName("VENDOR_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pepo10>(entity =>
            {
                entity.HasKey(e => e.PeNo);

                entity.ToTable("PEPO10");

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Address1)
                    .HasColumnName("ADDRESS1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("ADDRESS2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AllPay)
                    .HasColumnName("ALL_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Bankname)
                    .HasColumnName("BANKNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Bankno)
                    .HasColumnName("BANKNO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Birthday)
                    .HasColumnName("BIRTHDAY")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("COUNTRY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DaiGet).HasColumnName("DAI_GET");

                entity.Property(e => e.Dep)
                    .HasColumnName("DEP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EatPay)
                    .HasColumnName("EAT_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Eduction)
                    .HasColumnName("EDUCTION")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FuliGet)
                    .HasColumnName("FULI_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.HourPay)
                    .HasColumnName("HOUR_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IgetGet1)
                    .HasColumnName("IGET_GET1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IgetGet2)
                    .HasColumnName("IGET_GET2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IgetName1)
                    .HasColumnName("IGET_NAME1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IgetName2)
                    .HasColumnName("IGET_NAME2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Indate)
                    .HasColumnName("INDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Insudate)
                    .HasColumnName("INSUDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IpayName1)
                    .HasColumnName("IPAY_NAME1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IpayName2)
                    .HasColumnName("IPAY_NAME2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IpayPay1)
                    .HasColumnName("IPAY_PAY1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IpayPay2)
                    .HasColumnName("IPAY_PAY2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.JenGet)
                    .HasColumnName("JEN_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.LateGet)
                    .HasColumnName("LATE_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Linkperson)
                    .HasColumnName("LINKPERSON")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LocationPay).HasColumnName("LOCATION_PAY");

                entity.Property(e => e.LouGet)
                    .HasColumnName("LOU_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.MasterPay)
                    .HasColumnName("MASTER_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MonthPay)
                    .HasColumnName("MONTH_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OilPay)
                    .HasColumnName("OIL_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Outdate)
                    .HasColumnName("OUTDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Outsudate)
                    .HasColumnName("OUTSUDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Paytype)
                    .HasColumnName("PAYTYPE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Posi)
                    .HasColumnName("POSI")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PosiPay)
                    .HasColumnName("POSI_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.ProPay)
                    .HasColumnName("PRO_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.PromiseGet).HasColumnName("PROMISE_GET");

                entity.Property(e => e.School)
                    .HasColumnName("SCHOOL")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceGet)
                    .HasColumnName("SERVICE_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Sex)
                    .HasColumnName("SEX")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Study)
                    .HasColumnName("STUDY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TaxGet)
                    .HasColumnName("TAX_GET")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Tel1)
                    .HasColumnName("TEL1")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Tel2)
                    .HasColumnName("TEL2")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TraPay)
                    .HasColumnName("TRA_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Uniform)
                    .HasColumnName("UNIFORM")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ynmarry)
                    .HasColumnName("YNMARRY")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pepo20>(entity =>
            {
                entity.HasKey(e => new { e.PeNo, e.Paymonth });

                entity.ToTable("PEPO20");

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Paymonth)
                    .HasColumnName("PAYMONTH")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.AddHour).HasColumnName("ADD_HOUR");

                entity.Property(e => e.AddHour133).HasColumnName("ADD_HOUR133");

                entity.Property(e => e.AddHour166).HasColumnName("ADD_HOUR166");

                entity.Property(e => e.AddPay).HasColumnName("ADD_PAY");

                entity.Property(e => e.AllPay).HasColumnName("ALL_PAY");

                entity.Property(e => e.DaiGet).HasColumnName("DAI_GET");

                entity.Property(e => e.EatDay).HasColumnName("EAT_DAY");

                entity.Property(e => e.EatPay).HasColumnName("EAT_PAY");

                entity.Property(e => e.FuliGet).HasColumnName("FULI_GET");

                entity.Property(e => e.HoliDay1)
                    .HasColumnName("HOLI_DAY1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.HoliDay2)
                    .HasColumnName("HOLI_DAY2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.HoliName1)
                    .HasColumnName("HOLI_NAME1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoliName2)
                    .HasColumnName("HOLI_NAME2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IgetGet1)
                    .HasColumnName("IGET_GET1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IgetGet2)
                    .HasColumnName("IGET_GET2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IgetName1)
                    .HasColumnName("IGET_NAME1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IgetName2)
                    .HasColumnName("IGET_NAME2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IpayName1)
                    .HasColumnName("IPAY_NAME1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IpayName2)
                    .HasColumnName("IPAY_NAME2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IpayPay1)
                    .HasColumnName("IPAY_PAY1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IpayPay2)
                    .HasColumnName("IPAY_PAY2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.JenGet).HasColumnName("JEN_GET");

                entity.Property(e => e.LateGet).HasColumnName("LATE_GET");

                entity.Property(e => e.LateMin)
                    .HasColumnName("LATE_MIN")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.LendGet).HasColumnName("LEND_GET");

                entity.Property(e => e.LocationPay).HasColumnName("LOCATION_PAY");

                entity.Property(e => e.LouGet).HasColumnName("LOU_GET");

                entity.Property(e => e.MasterPay).HasColumnName("MASTER_PAY");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MonthPay).HasColumnName("MONTH_PAY");

                entity.Property(e => e.NewcusPay).HasColumnName("NEWCUS_PAY");

                entity.Property(e => e.OilPay).HasColumnName("OIL_PAY");

                entity.Property(e => e.Paytype)
                    .HasColumnName("PAYTYPE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PosiPay).HasColumnName("POSI_PAY");

                entity.Property(e => e.ProPay).HasColumnName("PRO_PAY");

                entity.Property(e => e.ProfitPay).HasColumnName("PROFIT_PAY");

                entity.Property(e => e.PromiseGet).HasColumnName("PROMISE_GET");

                entity.Property(e => e.PuhoPay).HasColumnName("PUHO_PAY");

                entity.Property(e => e.PuniGet).HasColumnName("PUNI_GET");

                entity.Property(e => e.RealPay).HasColumnName("REAL_PAY");

                entity.Property(e => e.ReturnPay).HasColumnName("RETURN_PAY");

                entity.Property(e => e.SalesPay).HasColumnName("SALES_PAY");

                entity.Property(e => e.ServiceGet).HasColumnName("SERVICE_GET");

                entity.Property(e => e.ShouldGet).HasColumnName("SHOULD_GET");

                entity.Property(e => e.ShouldPay).HasColumnName("SHOULD_PAY");

                entity.Property(e => e.SickDay).HasColumnName("SICK_DAY");

                entity.Property(e => e.SickGet).HasColumnName("SICK_GET");

                entity.Property(e => e.TaxGet).HasColumnName("TAX_GET");

                entity.Property(e => e.ThingDay).HasColumnName("THING_DAY");

                entity.Property(e => e.ThingGet).HasColumnName("THING_GET");

                entity.Property(e => e.TonluPay).HasColumnName("TONLU_PAY");

                entity.Property(e => e.TraPay).HasColumnName("TRA_PAY");

                entity.Property(e => e.WorkHour).HasColumnName("WORK_HOUR");

                entity.Property(e => e.WorkPay).HasColumnName("WORK_PAY");
            });

            modelBuilder.Entity<Pepo30>(entity =>
            {
                entity.HasKey(e => new { e.PeUpdate, e.PeNo });

                entity.ToTable("PEPO30");

                entity.Property(e => e.PeUpdate)
                    .HasColumnName("PE_UPDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Minus1)
                    .HasColumnName("MINUS1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Minus2)
                    .HasColumnName("MINUS2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Minus3)
                    .HasColumnName("MINUS3")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Plus1)
                    .HasColumnName("PLUS1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Plus2)
                    .HasColumnName("PLUS2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Plus3)
                    .HasColumnName("PLUS3")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<Pepo40>(entity =>
            {
                entity.HasKey(e => e.UpNo);

                entity.ToTable("PEPO40");

                entity.Property(e => e.UpNo)
                    .HasColumnName("UP_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AAmount)
                    .HasColumnName("A_AMOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.BAmount)
                    .HasColumnName("B_AMOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.UpDate)
                    .HasColumnName("UP_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.UpItem)
                    .HasColumnName("UP_ITEM")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpPercent)
                    .HasColumnName("UP_PERCENT")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<Phase10>(entity =>
            {
                entity.HasKey(e => e.Whereuse);

                entity.ToTable("PHASE10");

                entity.Property(e => e.Whereuse)
                    .HasColumnName("WHEREUSE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Serno)
                    .IsRequired()
                    .HasColumnName("SERNO")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Phase20>(entity =>
            {
                entity.HasKey(e => new { e.Whereuse, e.Serno });

                entity.ToTable("PHASE20");

                entity.Property(e => e.Whereuse)
                    .HasColumnName("WHEREUSE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Serno)
                    .HasColumnName("SERNO")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Passbystk)
                    .HasColumnName("PASSBYSTK")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Phase)
                    .HasColumnName("PHASE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ynselect)
                    .HasColumnName("YNSELECT")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pur10>(entity =>
            {
                entity.HasKey(e => e.PurNo);

                entity.ToTable("PUR10");

                entity.Property(e => e.PurNo)
                    .HasColumnName("PUR_NO")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.InNo)
                    .HasColumnName("IN_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PaperTail)
                    .HasColumnName("PAPER_TAIL")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PurDate)
                    .HasColumnName("PUR_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SendDate)
                    .HasColumnName("SEND_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.Property(e => e.Total0).HasColumnName("TOTAL0");

                entity.Property(e => e.Total1).HasColumnName("TOTAL1");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.VendorNo)
                    .HasColumnName("VENDOR_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.YnPur)
                    .HasColumnName("YN_PUR")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pur20>(entity =>
            {
                entity.HasKey(e => new { e.PurNo, e.Serno });

                entity.ToTable("PUR20");

                entity.Property(e => e.PurNo)
                    .HasColumnName("PUR_NO")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.InQty).HasColumnName("IN_QTY");

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rate10>(entity =>
            {
                entity.HasKey(e => e.Ntus);

                entity.ToTable("RATE10");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnName("RATE");
            });

            modelBuilder.Entity<Rate20>(entity =>
            {
                entity.HasKey(e => new { e.Ntus, e.Ntmonth });

                entity.ToTable("RATE20");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Ntmonth)
                    .HasColumnName("NTMONTH")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnName("RATE");
            });

            modelBuilder.Entity<Reportdefines>(entity =>
            {
                entity.HasKey(e => e.Reportname);

                entity.ToTable("REPORTDEFINES");

                entity.Property(e => e.Reportname)
                    .HasColumnName("REPORTNAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Reporttitle)
                    .HasColumnName("REPORTTITLE")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reportlayout>(entity =>
            {
                entity.HasKey(e => new { e.LayoutType, e.Owner, e.GroupNa, e.LayoutNa });

                entity.ToTable("REPORTLAYOUT");

                entity.Property(e => e.LayoutType)
                    .HasColumnName("LAYOUT_TYPE")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasColumnName("OWNER")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.GroupNa)
                    .HasColumnName("GROUP_NA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LayoutNa)
                    .HasColumnName("LAYOUT_NA")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultFlag)
                    .HasColumnName("DEFAULT_FLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Infocomp)
                    .HasColumnName("INFOCOMP")
                    .HasColumnType("image");
            });

            modelBuilder.Entity<Reportlimitations>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("REPORTLIMITATIONS");

                entity.Property(e => e.Printername)
                    .IsRequired()
                    .HasColumnName("PRINTERNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reportname)
                    .IsRequired()
                    .HasColumnName("REPORTNAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reportlog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("REPORTLOG");

                entity.Property(e => e.Condition)
                    .HasColumnName("CONDITION")
                    .HasColumnType("image");

                entity.Property(e => e.Createdate)
                    .IsRequired()
                    .HasColumnName("CREATEDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Createtime)
                    .IsRequired()
                    .HasColumnName("CREATETIME")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Jobid)
                    .IsRequired()
                    .HasColumnName("JOBID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasColumnName("MODULE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Needshowdialog)
                    .IsRequired()
                    .HasColumnName("NEEDSHOWDIALOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("OWNER")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Pagecount)
                    .IsRequired()
                    .HasColumnName("PAGECOUNT")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Printername)
                    .IsRequired()
                    .HasColumnName("PRINTERNAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Report)
                    .IsRequired()
                    .HasColumnName("REPORT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sysflag)
                    .HasColumnName("SYSFLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sysver)
                    .HasColumnName("SYSVER")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reportlogdetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("REPORTLOGDETAIL");

                entity.Property(e => e.Jobid)
                    .IsRequired()
                    .HasColumnName("JOBID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Jobpageid)
                    .IsRequired()
                    .HasColumnName("JOBPAGEID")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Pagecontent)
                    .HasColumnName("PAGECONTENT")
                    .HasColumnType("image");

                entity.Property(e => e.Sysflag)
                    .HasColumnName("SYSFLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sysver)
                    .HasColumnName("SYSVER")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reportloguser>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("REPORTLOGUSER");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reportqbuilder>(entity =>
            {
                entity.HasKey(e => e.Reportname);

                entity.ToTable("REPORTQBUILDER");

                entity.Property(e => e.Reportname)
                    .HasColumnName("REPORTNAME")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Contents)
                    .HasColumnName("CONTENTS")
                    .HasColumnType("image");
            });

            modelBuilder.Entity<Setagent>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Menuid, e.Agentuserid });

                entity.ToTable("SETAGENT");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Agentuserid)
                    .HasColumnName("AGENTUSERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Begindate)
                    .HasColumnName("BEGINDATE")
                    .HasMaxLength(19)
                    .IsUnicode(false);

                entity.Property(e => e.Enddate)
                    .HasColumnName("ENDDATE")
                    .HasMaxLength(19)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Setgridwidth>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SETGRIDWIDTH");

                entity.HasIndex(e => new { e.Formname, e.Userid, e.Gridname })
                    .HasName("IX_SETGRIDWIDTH")
                    .IsClustered();

                entity.Property(e => e.Fieldname)
                    .HasColumnName("FIELDNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Formname)
                    .HasColumnName("FORMNAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gridname)
                    .HasColumnName("GRIDNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Width).HasColumnName("WIDTH");
            });

            modelBuilder.Entity<Simplereportlog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SIMPLEREPORTLOG");

                entity.Property(e => e.Bplname)
                    .HasColumnName("BPLNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Createdate)
                    .HasColumnName("CREATEDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Createtime)
                    .HasColumnName("CREATETIME")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Machine)
                    .HasColumnName("MACHINE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasColumnName("OWNER")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Reportname)
                    .HasColumnName("REPORTNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sysflag)
                    .HasColumnName("SYSFLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sysver)
                    .HasColumnName("SYSVER")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock10>(entity =>
            {
                entity.HasKey(e => e.PartNo);

                entity.ToTable("STOCK10");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Atti)
                    .HasColumnName("ATTI")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Barcode)
                    .HasColumnName("BARCODE")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.CompCost).HasColumnName("COMP_COST");

                entity.Property(e => e.Cost1)
                    .HasColumnName("COST1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Cost2)
                    .HasColumnName("COST2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Cost3)
                    .HasColumnName("COST3")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Cuft).HasColumnName("CUFT");

                entity.Property(e => e.InitCost1).HasColumnName("INIT_COST1");

                entity.Property(e => e.InitCost2).HasColumnName("INIT_COST2");

                entity.Property(e => e.InitCost3).HasColumnName("INIT_COST3");

                entity.Property(e => e.InitCost4).HasColumnName("INIT_COST4");

                entity.Property(e => e.InitCost5).HasColumnName("INIT_COST5");

                entity.Property(e => e.InitQty1).HasColumnName("INIT_QTY1");

                entity.Property(e => e.InitQty2).HasColumnName("INIT_QTY2");

                entity.Property(e => e.LastCost).HasColumnName("LAST_COST");

                entity.Property(e => e.LastDiscount)
                    .HasColumnName("LAST_DISCOUNT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.LastIn)
                    .HasColumnName("LAST_IN")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LastModidate)
                    .HasColumnName("LAST_MODIDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LastModiuser)
                    .HasColumnName("LAST_MODIUSER")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LastOut)
                    .HasColumnName("LAST_OUT")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PackQty).HasColumnName("PACK_QTY");

                entity.Property(e => e.Price1)
                    .HasColumnName("PRICE1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Price2)
                    .HasColumnName("PRICE2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Price3)
                    .HasColumnName("PRICE3")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SPrice1)
                    .HasColumnName("S_PRICE1")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SPrice2)
                    .HasColumnName("S_PRICE2")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SPrice3)
                    .HasColumnName("S_PRICE3")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SafeQty).HasColumnName("SAFE_QTY");

                entity.Property(e => e.SalesCost)
                    .HasColumnName("SALES_COST")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StQty).HasColumnName("ST_QTY");

                entity.Property(e => e.TUnit1)
                    .HasColumnName("T_UNIT1")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TUnit2)
                    .HasColumnName("T_UNIT2")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TUnit3)
                    .HasColumnName("T_UNIT3")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TaxType)
                    .HasColumnName("TAX_TYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TranPara1).HasColumnName("TRAN_PARA1");

                entity.Property(e => e.TranPara2).HasColumnName("TRAN_PARA2");

                entity.Property(e => e.TranPara3).HasColumnName("TRAN_PARA3");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.YnCount)
                    .HasColumnName("YN_COUNT")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock10p>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("STOCK10P");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock11>(entity =>
            {
                entity.HasKey(e => new { e.PartNo, e.InitDate });

                entity.ToTable("STOCK11");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.InitDate)
                    .HasColumnName("INIT_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.BalQty)
                    .HasColumnName("BAL_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.CheckQty)
                    .HasColumnName("CHECK_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.InQty)
                    .HasColumnName("IN_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IoQty)
                    .HasColumnName("IO_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.IsQty)
                    .HasColumnName("IS_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.LastQty)
                    .HasColumnName("LAST_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.OutQty)
                    .HasColumnName("OUT_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.StQty)
                    .HasColumnName("ST_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SubQty)
                    .HasColumnName("SUB_QTY")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<Stock20>(entity =>
            {
                entity.HasKey(e => e.SpNo);

                entity.ToTable("STOCK20");

                entity.Property(e => e.SpNo)
                    .HasColumnName("SP_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Bdate)
                    .HasColumnName("BDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CoNo)
                    .HasColumnName("CO_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Edate)
                    .HasColumnName("EDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock21>(entity =>
            {
                entity.HasKey(e => new { e.SpNo, e.Serno, e.PartNo });

                entity.ToTable("STOCK21");

                entity.Property(e => e.SpNo)
                    .HasColumnName("SP_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Newprice).HasColumnName("NEWPRICE");

                entity.Property(e => e.Newsprice).HasColumnName("NEWSPRICE");

                entity.Property(e => e.Oldprice).HasColumnName("OLDPRICE");

                entity.Property(e => e.Oldsprice).HasColumnName("OLDSPRICE");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock30>(entity =>
            {
                entity.HasKey(e => e.PartNo);

                entity.ToTable("STOCK30");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cost)
                    .HasColumnName("COST")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StQty)
                    .HasColumnName("ST_QTY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.TaxType)
                    .HasColumnName("TAX_TYPE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.YnUpdate)
                    .HasColumnName("YN_UPDATE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock31>(entity =>
            {
                entity.HasKey(e => new { e.PartNo, e.InitDate });

                entity.ToTable("STOCK31");

                entity.Property(e => e.PartNo)
                    .HasColumnName("PART_NO")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.InitDate)
                    .HasColumnName("INIT_DATE")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Cost)
                    .HasColumnName("COST")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.StQty)
                    .HasColumnName("ST_QTY")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<SyncLog>(entity =>
            {
                entity.HasKey(e => e.ApSrvId);

                entity.ToTable("SYNC_LOG");

                entity.Property(e => e.ApSrvId)
                    .HasColumnName("ApSrv_ID")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.AliasName)
                    .IsRequired()
                    .HasColumnName("ALIAS_NAME")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Deptname)
                    .IsRequired()
                    .HasColumnName("DEPTNAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Lastexportdatetime)
                    .IsRequired()
                    .HasColumnName("LASTEXPORTDATETIME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Lastimportdatetime)
                    .IsRequired()
                    .HasColumnName("LASTIMPORTDATETIME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.SyncType)
                    .IsRequired()
                    .HasColumnName("SYNC_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SyncLogHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SYNC_LOG_HISTORY");

                entity.Property(e => e.ApsrvId)
                    .IsRequired()
                    .HasColumnName("APSRV_ID")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Lastexportdatetime)
                    .IsRequired()
                    .HasColumnName("LASTEXPORTDATETIME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Lastimportdatetime)
                    .IsRequired()
                    .HasColumnName("LASTIMPORTDATETIME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.SyncType)
                    .IsRequired()
                    .HasColumnName("SYNC_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SyncTableCon>(entity =>
            {
                entity.HasKey(e => new { e.TableName, e.ApsrvId });

                entity.ToTable("SYNC_TABLE_CON");

                entity.Property(e => e.TableName)
                    .HasColumnName("TABLE_NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ApsrvId)
                    .HasColumnName("APSRV_ID")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasColumnName("CONDITION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldsList)
                    .HasColumnName("FIELDS_LIST")
                    .HasColumnType("decimal(16, 0)");

                entity.Property(e => e.KeyField)
                    .IsRequired()
                    .HasColumnName("KEY_FIELD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNo)
                    .HasColumnName("ORDER_NO")
                    .HasColumnType("decimal(16, 0)");
            });

            modelBuilder.Entity<SysParamValue>(entity =>
            {
                entity.HasKey(e => new { e.ParamId, e.ValueId });

                entity.ToTable("SYS_PARAM_VALUE");

                entity.Property(e => e.ParamId)
                    .HasColumnName("PARAM_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ValueId)
                    .HasColumnName("VALUE_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ValueCont)
                    .IsRequired()
                    .HasColumnName("VALUE_CONT")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysParamdef>(entity =>
            {
                entity.HasKey(e => e.ParamId);

                entity.ToTable("SYS_PARAMDEF");

                entity.Property(e => e.ParamId)
                    .HasColumnName("PARAM_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ParamName)
                    .HasColumnName("PARAM_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysParamsTable>(entity =>
            {
                entity.HasKey(e => new { e.ParamKind, e.UnitId, e.Itemtype, e.Menuid, e.ParamId, e.ValueId });

                entity.ToTable("SYS_PARAMS_TABLE");

                entity.Property(e => e.ParamKind)
                    .HasColumnName("PARAM_KIND")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.UnitId)
                    .HasColumnName("UNIT_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Itemtype)
                    .HasColumnName("ITEMTYPE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ParamId)
                    .HasColumnName("PARAM_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ValueId)
                    .HasColumnName("VALUE_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ValueStr)
                    .HasColumnName("VALUE_STR")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sysautonum>(entity =>
            {
                entity.HasKey(e => e.Numbername);

                entity.ToTable("SYSAUTONUM");

                entity.Property(e => e.Numbername)
                    .HasColumnName("NUMBERNAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Currentdigits).HasColumnName("CURRENTDIGITS");

                entity.Property(e => e.Currentprefix)
                    .IsRequired()
                    .HasColumnName("CURRENTPREFIX")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Digitswidth).HasColumnName("DIGITSWIDTH");

                entity.Property(e => e.Minvalue).HasColumnName("MINVALUE");

                entity.Property(e => e.Valueinterval).HasColumnName("VALUEINTERVAL");
            });

            modelBuilder.Entity<Systemparams>(entity =>
            {
                entity.HasKey(e => e.Paramid);

                entity.ToTable("SYSTEMPARAMS");

                entity.Property(e => e.Paramid)
                    .HasColumnName("PARAMID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Paramtype)
                    .HasColumnName("PARAMTYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Paramvalue)
                    .HasColumnName("PARAMVALUE")
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tablesynclog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TABLESYNCLOG");

                entity.Property(e => e.Descr)
                    .HasColumnName("DESCR")
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.Lastupdatetime)
                    .IsRequired()
                    .HasColumnName("LASTUPDATETIME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Tablename)
                    .IsRequired()
                    .HasColumnName("TABLENAME")
                    .HasMaxLength(48)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tableversion>(entity =>
            {
                entity.HasKey(e => e.Tablename);

                entity.ToTable("TABLEVERSION");

                entity.Property(e => e.Tablename)
                    .HasColumnName("TABLENAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Version).HasColumnName("VERSION");
            });

            modelBuilder.Entity<TransLogD>(entity =>
            {
                entity.HasKey(e => new { e.TrnDate, e.TrnTime, e.TrnSeq, e.TrnUserid, e.TrnDSeq });

                entity.ToTable("TRANS_LOG_D");

                entity.Property(e => e.TrnDate)
                    .HasColumnName("TRN_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnTime)
                    .HasColumnName("TRN_TIME")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnSeq)
                    .HasColumnName("TRN_SEQ")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnUserid)
                    .HasColumnName("TRN_USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrnDSeq)
                    .HasColumnName("TRN_D_SEQ")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorCode)
                    .HasColumnName("ERROR_CODE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("ERROR_MESSAGE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProgName)
                    .HasColumnName("PROG_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransactionLog>(entity =>
            {
                entity.HasKey(e => new { e.TrnDate, e.TrnTime, e.TrnSeq, e.TrnUserid });

                entity.ToTable("TRANSACTION_LOG");

                entity.Property(e => e.TrnDate)
                    .HasColumnName("TRN_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnTime)
                    .HasColumnName("TRN_TIME")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnSeq)
                    .HasColumnName("TRN_SEQ")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnUserid)
                    .HasColumnName("TRN_USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrnECount)
                    .HasColumnName("TRN_E_COUNT")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.TrnErrorFlag)
                    .HasColumnName("TRN_ERROR_FLAG")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TrnIndex1)
                    .HasColumnName("TRN_INDEX_1")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TrnIndex2)
                    .HasColumnName("TRN_INDEX_2")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TrnMethod)
                    .HasColumnName("TRN_METHOD")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TrnName)
                    .HasColumnName("TRN_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrnPackage)
                    .HasColumnName("TRN_PACKAGE")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TrnSCount)
                    .HasColumnName("TRN_S_COUNT")
                    .HasColumnType("decimal(12, 0)");
            });

            modelBuilder.Entity<TransactionLogD>(entity =>
            {
                entity.HasKey(e => new { e.TrnDate, e.TrnTime, e.TrnSeq, e.TrnUserid, e.TrnDSeq });

                entity.ToTable("TRANSACTION_LOG_D");

                entity.Property(e => e.TrnDate)
                    .HasColumnName("TRN_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnTime)
                    .HasColumnName("TRN_TIME")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnSeq)
                    .HasColumnName("TRN_SEQ")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TrnUserid)
                    .HasColumnName("TRN_USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrnDSeq)
                    .HasColumnName("TRN_D_SEQ")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorCode)
                    .HasColumnName("ERROR_CODE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("ERROR_MESSAGE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProgName)
                    .HasColumnName("PROG_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trsflowdef>(entity =>
            {
                entity.HasKey(e => e.TfId);

                entity.ToTable("TRSFLOWDEF");

                entity.Property(e => e.TfId)
                    .HasColumnName("TF_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AuditorId)
                    .HasColumnName("AUDITOR_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CanExec)
                    .HasColumnName("CAN_EXEC")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PackageName)
                    .HasColumnName("PACKAGE_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.SPackageName)
                    .HasColumnName("S_PACKAGE_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.STableName)
                    .HasColumnName("S_TABLE_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.SyncType)
                    .HasColumnName("SYNC_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TfFlag)
                    .HasColumnName("TF_FLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TfName)
                    .HasColumnName("TF_NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trsparams>(entity =>
            {
                entity.HasKey(e => e.ParamId);

                entity.ToTable("TRSPARAMS");

                entity.Property(e => e.ParamId)
                    .HasColumnName("PARAM_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ParamCont)
                    .HasColumnName("PARAM_CONT")
                    .HasColumnType("image");

                entity.Property(e => e.Presentation)
                    .HasColumnName("PRESENTATION")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trstbl>(entity =>
            {
                entity.HasKey(e => new { e.TrsId, e.ParamId });

                entity.ToTable("TRSTBL");

                entity.Property(e => e.TrsId)
                    .HasColumnName("TRS_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParamId)
                    .HasColumnName("PARAM_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorMsg)
                    .HasColumnName("ERROR_MSG")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrsFlag)
                    .HasColumnName("TRS_FLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TrsOwner)
                    .HasColumnName("TRS_OWNER")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TrsType)
                    .HasColumnName("TRS_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Userdeftablelist>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("USERDEFTABLELIST");

                entity.Property(e => e.Tabledescription)
                    .HasColumnName("TABLEDESCRIPTION")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Tableid)
                    .HasColumnName("TABLEID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Tablename)
                    .HasColumnName("TABLENAME")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Userdisitems>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Menuid, e.Controlclass });

                entity.ToTable("USERDISITEMS");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Controlclass)
                    .HasColumnName("CONTROLCLASS")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Caption)
                    .HasColumnName("CAPTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Enabled)
                    .HasColumnName("ENABLED")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usergroups>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Groupid });

                entity.ToTable("USERGROUPS");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Groupid)
                    .HasColumnName("GROUPID")
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usermenus>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Menuid });

                entity.ToTable("USERMENUS");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Menuid)
                    .HasColumnName("MENUID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Enabled)
                    .HasColumnName("ENABLED")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sysflag)
                    .HasColumnName("SYSFLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("USERS");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Agent)
                    .HasColumnName("AGENT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Createdate)
                    .HasColumnName("CREATEDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Creater)
                    .HasColumnName("CREATER")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modifier)
                    .HasColumnName("MODIFIER")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Modifydate)
                    .HasColumnName("MODIFYDATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Pwd)
                    .HasColumnName("PWD")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sysflag)
                    .HasColumnName("SYSFLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Userworksheet>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("USERWORKSHEET");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Worksheetbplname)
                    .HasColumnName("WORKSHEETBPLNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Worksheetdefs)
                    .HasColumnName("WORKSHEETDEFS")
                    .HasColumnType("image");

                entity.Property(e => e.Worksheetdescription)
                    .HasColumnName("WORKSHEETDESCRIPTION")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Worksheetname)
                    .HasColumnName("WORKSHEETNAME")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Worksheetreportname)
                    .HasColumnName("WORKSHEETREPORTNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Worksheetserverend)
                    .HasColumnName("WORKSHEETSERVEREND")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vendor10>(entity =>
            {
                entity.HasKey(e => e.VendorNo)
                    .IsClustered(false);

                entity.ToTable("VENDOR10");

                entity.HasIndex(e => e.VendorNo)
                    .HasName("IX_VENDOR10")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.VendorNo)
                    .HasColumnName("VENDOR_NO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Accno1)
                    .HasColumnName("ACCNO1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Boss)
                    .HasColumnName("BOSS")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ChehkDay)
                    .HasColumnName("CHEHK_DAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Company)
                    .HasColumnName("COMPANY")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.Dlien)
                    .HasColumnName("DLIEN")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Factaddr)
                    .HasColumnName("FACTADDR")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Invoaddr)
                    .HasColumnName("INVOADDR")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Invocomp)
                    .HasColumnName("INVOCOMP")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasColumnName("MEMO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasColumnName("MOBILE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NotPay)
                    .HasColumnName("NOT_PAY")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Ntus)
                    .HasColumnName("NTUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Payaccount)
                    .HasColumnName("PAYACCOUNT")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Paybank)
                    .HasColumnName("PAYBANK")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Payment)
                    .HasColumnName("PAYMENT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Prenotpay).HasColumnName("PRENOTPAY");

                entity.Property(e => e.PriceType)
                    .HasColumnName("PRICE_TYPE")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasColumnName("PRODUCT")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sales)
                    .HasColumnName("SALES")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SubTot)
                    .HasColumnName("SUB_TOT")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Tax).HasColumnName("TAX");

                entity.Property(e => e.TaxType)
                    .HasColumnName("TAX_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Taxrate)
                    .HasColumnName("TAXRATE")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Tel1)
                    .HasColumnName("TEL1")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Tel2)
                    .HasColumnName("TEL2")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Total1).HasColumnName("TOTAL1");

                entity.Property(e => e.Total2).HasColumnName("TOTAL2");

                entity.Property(e => e.Uniform)
                    .HasColumnName("UNIFORM")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Vename)
                    .HasColumnName("VENAME")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Www)
                    .HasColumnName("WWW")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YesPay)
                    .HasColumnName("YES_PAY")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<Work10>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WORK10");

                entity.Property(e => e.Ctn).HasColumnName("CTN");

                entity.Property(e => e.Dep)
                    .HasColumnName("DEP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Kg).HasColumnName("KG");

                entity.Property(e => e.Mark001)
                    .HasColumnName("MARK001")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark002)
                    .HasColumnName("MARK002")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark003)
                    .HasColumnName("MARK003")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark004)
                    .HasColumnName("MARK004")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark005)
                    .HasColumnName("MARK005")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark006)
                    .HasColumnName("MARK006")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark007)
                    .HasColumnName("MARK007")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark008)
                    .HasColumnName("MARK008")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark009)
                    .HasColumnName("MARK009")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark010)
                    .HasColumnName("MARK010")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark011)
                    .HasColumnName("MARK011")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark012)
                    .HasColumnName("MARK012")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark101)
                    .HasColumnName("MARK101")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark102)
                    .HasColumnName("MARK102")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark103)
                    .HasColumnName("MARK103")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark104)
                    .HasColumnName("MARK104")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark105)
                    .HasColumnName("MARK105")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark106)
                    .HasColumnName("MARK106")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark107)
                    .HasColumnName("MARK107")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark108)
                    .HasColumnName("MARK108")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark109)
                    .HasColumnName("MARK109")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark110)
                    .HasColumnName("MARK110")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark111)
                    .HasColumnName("MARK111")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Mark112)
                    .HasColumnName("MARK112")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.OkDate)
                    .HasColumnName("OK_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNo)
                    .HasColumnName("ORDER_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PQty).HasColumnName("P_QTY");

                entity.Property(e => e.Proc01)
                    .HasColumnName("PROC01")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc02)
                    .HasColumnName("PROC02")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc03)
                    .HasColumnName("PROC03")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc04)
                    .HasColumnName("PROC04")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc05)
                    .HasColumnName("PROC05")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc06)
                    .HasColumnName("PROC06")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc07)
                    .HasColumnName("PROC07")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Proc08)
                    .HasColumnName("PROC08")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Remark1)
                    .HasColumnName("REMARK1")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark2)
                    .HasColumnName("REMARK2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark3)
                    .HasColumnName("REMARK3")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark4)
                    .HasColumnName("REMARK4")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark5)
                    .HasColumnName("REMARK5")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark6)
                    .HasColumnName("REMARK6")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark7)
                    .HasColumnName("REMARK7")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark8)
                    .HasColumnName("REMARK8")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.WorkDate)
                    .HasColumnName("WORK_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.WorkNo)
                    .HasColumnName("WORK_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Work20>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WORK20");

                entity.Property(e => e.Color)
                    .HasColumnName("COLOR")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Ctn).HasColumnName("CTN");

                entity.Property(e => e.GoodNo)
                    .HasColumnName("GOOD_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OOrderNo)
                    .HasColumnName("O_ORDER_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OSerno).HasColumnName("O_SERNO");

                entity.Property(e => e.OkQty).HasColumnName("OK_QTY");

                entity.Property(e => e.OkWork)
                    .HasColumnName("OK_WORK")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PQty).HasColumnName("P_QTY");

                entity.Property(e => e.PartName)
                    .HasColumnName("PART_NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Pctnqty).HasColumnName("PCTNQTY");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Totweight).HasColumnName("TOTWEIGHT");

                entity.Property(e => e.Weight).HasColumnName("WEIGHT");

                entity.Property(e => e.WorkNo)
                    .HasColumnName("WORK_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Work30>(entity =>
            {
                entity.HasKey(e => e.DailyNo);

                entity.ToTable("WORK30");

                entity.Property(e => e.DailyNo)
                    .HasColumnName("DAILY_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DailyDate)
                    .HasColumnName("DAILY_DATE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Dep)
                    .HasColumnName("DEP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNo)
                    .HasColumnName("ORDER_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PeNo)
                    .HasColumnName("PE_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Reamrk)
                    .HasColumnName("REAMRK")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StockPass)
                    .HasColumnName("STOCK_PASS")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkNo)
                    .HasColumnName("WORK_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Work40>(entity =>
            {
                entity.HasKey(e => new { e.DailyNo, e.Serno });

                entity.ToTable("WORK40");

                entity.Property(e => e.DailyNo)
                    .HasColumnName("DAILY_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Serno).HasColumnName("SERNO");

                entity.Property(e => e.Btime)
                    .HasColumnName("BTIME")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Etime)
                    .HasColumnName("ETIME")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.GoodNo)
                    .HasColumnName("GOOD_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OkWork)
                    .HasColumnName("OK_WORK")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNo)
                    .HasColumnName("ORDER_NO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PartName)
                    .HasColumnName("PART_NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Peponum).HasColumnName("PEPONUM");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TotHour).HasColumnName("TOT_HOUR");

                entity.Property(e => e.TotMin).HasColumnName("TOT_MIN");

                entity.Property(e => e.WorkNo)
                    .HasColumnName("WORK_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Workvalue)
                    .HasColumnName("WORKVALUE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.累計生產)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.預計生產)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
