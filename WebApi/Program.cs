// using Microsoft.EntityFrameworkCore;
// using RedditAPI.Data.Infrastructure.Context;
// using RedditAPI.Data.Infrastructure.UnitOfWork;
// using RedditAPI.Data.Repositories.CommentRepository;
// using RedditAPI.Data.Repositories.LikeRepository;
// using RedditAPI.Data.Repositories.PostRepository;
// using RedditAPI.Data.Repositories.UserRepository;
// using RedditAPI.Services.Features.Comments;
// using RedditAPI.Services.Features.Likes;
// using RedditAPI.Services.Features.Posts;
// using RedditAPI.Services.Features.Users;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<AppDbContext>(o =>
//     o.UseSqlServer(
//         "Server=localhost,1433;Database=reddit-db;User Id=SA;Password=Vladcondurat2003;MultipleActiveResultSets=true;TrustServerCertificate=true"));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddScoped<IAppDbContext, AppDbContext>();
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<ICommentRepository, CommentRepository>();
// builder.Services.AddScoped<IPostRepository, PostRepository>();
// builder.Services.AddScoped<ILikeRepository, LikeRepository>();
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IPostService, PostService>();
// builder.Services.AddScoped<ICommentService, CommentService>();
// builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.MapControllers();
app.Run();