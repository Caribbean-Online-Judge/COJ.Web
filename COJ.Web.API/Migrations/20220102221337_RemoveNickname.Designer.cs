// <auto-generated />
using System;
using COJ.Web.Infrestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace COJ.Web.API.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20220102221337_RemoveNickname")]
    partial class RemoveNickname
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("COJ.Web.Domain.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("InstitutionId")
                        .HasColumnType("integer");

                    b.Property<int?>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastConnectionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastIpAddress")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("LocaleId")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SettingsId")
                        .HasColumnType("integer");

                    b.Property<int>("Sex")
                        .HasColumnType("integer");

                    b.Property<int>("StatisticsId")
                        .HasColumnType("integer");

                    b.Property<string>("Tags")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("InstitutionId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LocaleId");

                    b.HasIndex("SettingsId");

                    b.HasIndex("StatisticsId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.AccountPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountPermissions");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.AccountSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("ContestNotifications")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("EnabledAdvancedEditor")
                        .HasColumnType("boolean");

                    b.Property<bool>("EntriesNotifications")
                        .HasColumnType("boolean");

                    b.Property<bool>("SeeSolutions")
                        .HasColumnType("boolean");

                    b.Property<bool>("ShowBirthday")
                        .HasColumnType("boolean");

                    b.Property<bool>("ShowEmail")
                        .HasColumnType("boolean");

                    b.Property<bool>("SubmitionNotification")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("AccountSettings");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.AccountStatistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Accepted")
                        .HasColumnType("integer");

                    b.Property<int>("CompilationError")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FLE")
                        .HasColumnType("integer");

                    b.Property<int>("Ivf")
                        .HasColumnType("integer");

                    b.Property<int>("MemoryLimit")
                        .HasColumnType("integer");

                    b.Property<int>("Ole")
                        .HasColumnType("integer");

                    b.Property<int>("OutputLimit")
                        .HasColumnType("integer");

                    b.Property<int>("PresentationError")
                        .HasColumnType("integer");

                    b.Property<int>("RuntimeError")
                        .HasColumnType("integer");

                    b.Property<int>("SV")
                        .HasColumnType("integer");

                    b.Property<int>("Shipping")
                        .HasColumnType("integer");

                    b.Property<int>("TimeLimit")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Uq")
                        .HasColumnType("integer");

                    b.Property<int>("WrongAnswer")
                        .HasColumnType("integer");

                    b.Property<int>("accu")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AccountStatistics");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.AccountToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountTokens");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Legent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("ISOCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Institution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Aid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Prority")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Locale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Locales");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CaseTimeLimit")
                        .HasColumnType("integer");

                    b.Property<int>("ClassificationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<int>("MemoryLimit")
                        .HasColumnType("integer");

                    b.Property<bool>("Multidata")
                        .HasColumnType("boolean");

                    b.Property<int>("OutputLimit")
                        .HasColumnType("integer");

                    b.Property<double>("Points")
                        .HasColumnType("double precision");

                    b.Property<int>("ProblemStatisticsId")
                        .HasColumnType("integer");

                    b.Property<bool>("Published")
                        .HasColumnType("boolean");

                    b.Property<int>("SourceCodeLengthLimit")
                        .HasColumnType("integer");

                    b.Property<bool>("SpecialJudge")
                        .HasColumnType("boolean");

                    b.Property<int>("TotalTimeLimit")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClassificationId");

                    b.HasIndex("ProblemStatisticsId");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemClassification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AchievementId")
                        .HasColumnType("integer");

                    b.Property<double>("Complexity")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EstimatedCodeLenght")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AchievementId");

                    b.ToTable("ProblemClassifications");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemDataSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Output")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProblemId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.ToTable("ProblemDataSets");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemStatistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Accepted")
                        .HasColumnType("integer");

                    b.Property<int>("CompilationError")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FLE")
                        .HasColumnType("integer");

                    b.Property<int>("Ivf")
                        .HasColumnType("integer");

                    b.Property<int>("MemoryLimit")
                        .HasColumnType("integer");

                    b.Property<int>("Ole")
                        .HasColumnType("integer");

                    b.Property<int>("OutputLimit")
                        .HasColumnType("integer");

                    b.Property<int>("PresentationError")
                        .HasColumnType("integer");

                    b.Property<int>("RuntimeError")
                        .HasColumnType("integer");

                    b.Property<int>("SV")
                        .HasColumnType("integer");

                    b.Property<int>("Shipping")
                        .HasColumnType("integer");

                    b.Property<int>("TimeLimit")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Uq")
                        .HasColumnType("integer");

                    b.Property<int>("WrongAnswer")
                        .HasColumnType("integer");

                    b.Property<int>("accu")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ProblemStatistics");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<int>("AcceptedCases")
                        .HasColumnType("integer");

                    b.Property<int>("AcceptedTests")
                        .HasColumnType("integer");

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("AverageCase")
                        .HasColumnType("integer");

                    b.Property<int>("CpuTime")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastJudgingDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Lock")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxTestCase")
                        .HasColumnType("integer");

                    b.Property<long>("Memory")
                        .HasColumnType("bigint");

                    b.Property<int>("MinTestCase")
                        .HasColumnType("integer");

                    b.Property<int>("ProblemId")
                        .HasColumnType("integer");

                    b.Property<string>("SourceCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TestCase")
                        .HasColumnType("integer");

                    b.Property<double>("Time")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Verdict")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ProblemId");

                    b.ToTable("ProblemSubmissions");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LocaleId")
                        .HasColumnType("integer");

                    b.Property<int?>("ProblemId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LocaleId");

                    b.HasIndex("ProblemId");

                    b.ToTable("ProblemTranslation");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Account", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("COJ.Web.Domain.Entities.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId");

                    b.HasOne("COJ.Web.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("COJ.Web.Domain.Entities.Locale", "Locale")
                        .WithMany()
                        .HasForeignKey("LocaleId");

                    b.HasOne("COJ.Web.Domain.Entities.AccountSettings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId");

                    b.HasOne("COJ.Web.Domain.Entities.AccountStatistic", "Statistics")
                        .WithMany()
                        .HasForeignKey("StatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Institution");

                    b.Navigation("Language");

                    b.Navigation("Locale");

                    b.Navigation("Settings");

                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.AccountPermission", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Account", null)
                        .WithMany("Permissions")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.AccountToken", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Institution", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Problem", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.ProblemClassification", "Classification")
                        .WithMany()
                        .HasForeignKey("ClassificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COJ.Web.Domain.Entities.ProblemStatistic", "ProblemStatistics")
                        .WithMany()
                        .HasForeignKey("ProblemStatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classification");

                    b.Navigation("ProblemStatistics");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemClassification", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemDataSet", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Problem", null)
                        .WithMany("DataSets")
                        .HasForeignKey("ProblemId");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemSubmission", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COJ.Web.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COJ.Web.Domain.Entities.Problem", "Problem")
                        .WithMany()
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Language");

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.ProblemTranslation", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Locale", "Locale")
                        .WithMany()
                        .HasForeignKey("LocaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COJ.Web.Domain.Entities.Problem", null)
                        .WithMany("Translations")
                        .HasForeignKey("ProblemId");

                    b.Navigation("Locale");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("COJ.Web.Domain.Entities.Account", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Account", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("COJ.Web.Domain.Entities.Problem", b =>
                {
                    b.Navigation("DataSets");

                    b.Navigation("Translations");
                });
#pragma warning restore 612, 618
        }
    }
}
