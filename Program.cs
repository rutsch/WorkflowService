using WorkflowService.Services;
using WorkflowService.Managers;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddSingleton<ActivitiesManager>();
builder.Services.AddSingleton<CommentsManager>();
builder.Services.AddSingleton<HierarchiesManager>();
builder.Services.AddSingleton<NotificationsManager>();
builder.Services.AddSingleton<ProjectsManager>();
builder.Services.AddSingleton<SectionsManager>();
builder.Services.AddSingleton<UsersManager>();

builder.Services.AddSingleton<LiteDB.LiteDatabase>(new LiteDatabase(builder.Configuration["LiteDbOptions:DatabaseLocation"]));
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "X-Grpc-Web", "User-Agent");
}));

var app = builder.Build();
app.UseCors();
app.UseGrpcWeb();

// Configure the HTTP request pipeline.
app.MapGrpcService<ActivityService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<CommentService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<HierarchyService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<ProjectService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<NotificationService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<SectionService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<UserService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
