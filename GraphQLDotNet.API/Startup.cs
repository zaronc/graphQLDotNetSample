using Domain.Context;
using Domain.Repository;
using Domain.Services;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQLDotNet.API.GraphQL;
using GraphQLDotNet.API.GraphQL.Subscriptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static GraphQLDotNet.API.GraphQL.Subscriptions.ShopSubscription;

namespace GraphQLDotNet.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Cors",
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                }));

            services.AddDbContext<ShopDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();

            services.AddSingleton<IReviewService, ReviewService>();

            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddScoped<ShopSchema>();
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = false;  // info sulla richiesta (ms, campi richiesti, ecc)
            })
                .AddWebSockets()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(ShopSchema), ServiceLifetime.Scoped).AddDataLoader();

            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShopDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Cors");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseWebSockets();
            app.UseGraphQLWebSockets<ShopSchema>("/graphql"); // per subscriptions

            app.UseGraphQL<ShopSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions { GraphQLEndPoint ="/graphql", SchemaPollingEnabled = false }); // default a true, fa richieste continue per lo schema

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            dbContext.SeedData();
        }
    }
}
