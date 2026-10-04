var builder = WebApplication.CreateBuilder(args);

// ── Services ──────────────────────────────────────────────────

builder.Services.AddRazorPages();

// Cookie-based authentication (ready for wiring up login logic)
builder.Services.AddAuthentication("CSASAuth")
    .AddCookie("CSASAuth", options =>
    {
        options.LoginPath        = "/Login";
        options.AccessDeniedPath = "/Login";
        options.ExpireTimeSpan   = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// ── Pipeline ──────────────────────────────────────────────────

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
