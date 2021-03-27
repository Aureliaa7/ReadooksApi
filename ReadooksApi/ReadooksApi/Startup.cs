using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Readooks.BusinessLogicLayer.MappingConfigurations;
using Readooks.BusinessLogicLayer.Services;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.Services.PasswordEncryption;
using Readooks.DataAccessLayer.DatabaseContext;
using Readooks.DataAccessLayer.Repositories;
using Readooks.DataAccessLayer.Repositories.Interfaces;
using Readooks.DataAccessLayer.UnitOfWork;
using Readooks.BusinessLogicLayer.MappingConfigurations.Users;
using Readooks.BusinessLogicLayer.MappingConfigurations.ReadingSessions;

namespace ReadooksApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly($"Readooks.DataAccessLayer"))
           );

            //
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IReadingSessionService, ReadingSessionService>();

            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(UserRegistrationProfile));
            services.AddAutoMapper(typeof(AddingBookProfile));
            services.AddAutoMapper(typeof(BookProfile));
            services.AddAutoMapper(typeof(UpdateBookProfile));
            services.AddAutoMapper(typeof(AddingReadingSessionProfile));
            services.AddAutoMapper(typeof(ReadingSessionProfile));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
