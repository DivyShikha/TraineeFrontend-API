using TraineeFrontend.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register services
builder.Services.AddScoped<TraineeService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<EnrolmentService>();
builder.Services.AddScoped<AuthService>();

// Configure HttpClients for each service

// TraineeService ? points to /api/
builder.Services.AddHttpClient<TraineeService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/api/");
});

// CourseService ? also /api/
builder.Services.AddHttpClient<CourseService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/api/");
});

// EnrolmentService ? also /api/
builder.Services.AddHttpClient<EnrolmentService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/api/");
});

// AuthService ? root (because /register and /login are not under /api/)
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


//using TraineeFrontend.Service;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();
//builder.Services.AddScoped<TraineeService>();
//builder.Services.AddScoped<CourseService>();
//builder.Services.AddScoped<EnrolmentService>();
//builder.Services.AddScoped<AuthService>();
//builder.Services.AddHttpClient<AuthService>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7240/");
//});

//builder.Services.AddHttpClient("TraineeAPI", httpclient =>
//{
//    httpclient.BaseAddress = new Uri("https://localhost:7240/api/Trainee");
//});



//var app = builder.Build();
//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();
