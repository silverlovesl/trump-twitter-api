using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrumpTwitter.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace TrumpTwitter
{
  public class Startup
  {
    private const string V = "MyPolicy";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Json形式で返却
      services.AddMvc(options => { options.FormatterMappings.SetMediaTypeMappingForFormat("js", "application/json"); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      // SwaggerGenサービスの登録
      services.AddSwaggerGen(option => { option.SwaggerDoc("v1", new Info { Title = "Trump twitter", Version = "v1" }); });
      services.AddCors();
      // services.AddCors(obj => obj.AddPolicy("MyPolicy", builder =>
      //  {
      //    //  builder.WithOrigins(new string[] { "http://localhost:8080" });
      //    builder.AllowAnyOrigin();
      //    builder.AllowAnyMethod();
      //  }));

      // services.Configure<MvcOptions>(options => { options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy")); });
      // 依頼注入登録
      this.DependencyInjection(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseCors(options =>
        options.WithOrigins(new string[] { "http://localhost:8080", "http://localhost:8081" })
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials());
      app.UseMvc();
    }

    private void DependencyInjection(IServiceCollection services)
    {
      //Transient objects are always different; a new instance is provided to every controller and every service.
      //Scoped objects are the same within a request, but different across different requests.
      //Singleton objects are the same for every object and every request.
      services.AddTransient<ITwitterService, TwitterService>();
    }

  }
}
