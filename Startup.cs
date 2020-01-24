using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DI.Controllers;
using DI.Interfaces;
using DI.Models;
using DI.Services;

namespace aspcoremvc {
  public class Startup {
    public Startup(IConfiguration configuration) { Configuration = configuration; }
    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services) {
      services.AddControllersWithViews();
      /*
        Each type of lifestyle is added to the container resolved using Example class. 
            Add*** methods (with different lifestyles) make sure that the objects 
        are created according to the desired behavior expected from them. Once 
        these objects are initialized, they can be injected where ever required.
      */      
      
      /*
        A new object is always created whenever requested from container. 
        In screenshot, you can see the guid values are different in the 
        Controller and the Service class even for a single request.       
      */
      services.AddTransient<IExampleTransient, Example>();
      
      /*
        For a particular request, object is same throughout scope. Notice how
        object's guid value is different in second request. However, values are 
        same for Controller and Service class in both requests. Here scope is 
        web request. When one more request is served, object is recreated.
      */
      services.AddScoped<IExampleScoped, Example>();
      
        /*
          Looking at screenshots, first thing which is clearly seen is Singleton 
          one. No matter how many requests you do after running app, object
          will be same. It does not depend upon Controller or Service class.
        */
      services.AddSingleton<IExampleSingleton, Example>();
      services.AddSingleton<IExampleSingletonInstance, Example>();
      
      /*
        Special case of Singleton, where user creates object and provides that to 
        AddSingleton method. So, we are explicitly creating object of Example class 
        (services.AddSingleton(new Example(Guid.Empty))) and asking DI framework to 
        register it as a Singleton. In this case, we are sending Guid.Empty. 
        Thus, an empty guid is assigned which stays unchanged for all requests.
      */
      services.AddSingleton(new Example(Guid.Empty));

      /*
        Class ExampleService is resolved for itself. That means, whenever we 
        need ExampleService anywhere in the app, to be injected, it will 
        automatically assign an object of that class with all its properties.
      */
      services.AddTransient<ExampleService, ExampleService>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
        else {
          app.UseExceptionHandler("/Home/Error");
          app.UseHsts();
        }
      
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      
      app.UseEndpoints(endpoints => {
        endpoints.MapControllerRoute(
          name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"
        );
      });
    }
  }
}