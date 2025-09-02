using TraineeFrontend.Service;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages
builder.Services.AddRazorPages();

// ? Enable distributed cache + Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ? Shared CookieContainer for all HttpClients
var cookieContainer = new System.Net.CookieContainer();

// Register services
builder.Services.AddScoped<TraineeService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<EnrolmentService>();
builder.Services.AddScoped<AuthService>();

// ? Configure HttpClients (all use the same CookieContainer)

// TraineeService ? /api/
builder.Services.AddHttpClient<TraineeService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/api/");
})
.ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = cookieContainer
    });

// CourseService ? /api/
builder.Services.AddHttpClient<CourseService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/api/");
})
.ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = cookieContainer
    });

// EnrolmentService ? /api/
builder.Services.AddHttpClient<EnrolmentService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/api/");
})
.ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = cookieContainer
    });

// AuthService ? root (auth endpoints)
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/");
})
.ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = cookieContainer
    });

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ? Enable session before endpoints
app.UseSession();

app.UseAuthorization();

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == 401) // Unauthorized
    {
        response.Redirect("/Error?msg=?? Please login to access this page.");
    }
    else if (response.StatusCode == 403) // Forbidden
    {
        response.Redirect("/Error?msg=?? You do not have permission to access this page.");
    }
    else if (response.StatusCode == 404) // Not Found
    {
        response.Redirect("/Error?msg=?? The page you requested was not found.");
    }
});


app.MapRazorPages();

app.Run();
