using System.Text.Json;
using System.Text.Json.Serialization;
using TheReplacement.Metaclone.Api.Domain;
using TheReplacement.Metaclone.Api.Services;

namespace TheReplacement.Metaclone.Api
{
    public class Startup
    {
        private const string PolicyName = "MyPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, x =>
                {
                    x.AllowAnyOrigin();
                    x.AllowAnyMethod();
                    x.AllowAnyHeader();
                });
            });
            services.AddProblemDetails();
            services.AddTransient<IRepository, JsonRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(name: PolicyName,
                      builder =>
                      {
                          builder.AllowAnyOrigin();
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                      });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(PolicyName);

            app.UseAuthorization();

            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
