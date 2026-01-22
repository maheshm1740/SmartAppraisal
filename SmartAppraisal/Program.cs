using BLSmartAppraisal;
using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using DLSmartAppraisal.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ULSmartAppraisal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromMinutes(30);
    Options.Cookie.HttpOnly = true;
    Options.Cookie.IsEssential = true;
    Options.Cookie.SameSite = SameSiteMode.Lax;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<RoleContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DesignationContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<CompetencyContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<QuestionContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AssessmentContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserManagementRepositoryAdmin, UserManagementRepository>();
builder.Services.AddScoped<IRoleRepository, RolesRepository>();
builder.Services.AddScoped<IDesignation, DesignationRepository>();
builder.Services.AddScoped<ICompetency, CompetencyRepo>();
builder.Services.AddScoped<IQuestion, QuestionRepository>();
builder.Services.AddScoped<IAssessment, AssessmentRepository>();

builder.Services.AddScoped<BLlogin>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<BLUserManagement>();
builder.Services.AddScoped<BLRoleManagement>();
builder.Services.AddScoped<DesigManagement>();
builder.Services.AddScoped<BlCompetency>();
builder.Services.AddScoped<BLQuestion>();
builder.Services.AddScoped<BLAssessment>();
builder.Services.AddScoped <EmailService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Options =>
    {
        Options.LoginPath = "/Login/Index";
        Options.AccessDeniedPath= "/Login/Index";
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
