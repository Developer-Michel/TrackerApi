using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Tracker_Api.Models;
using Tracker_Api.Services;

namespace Tracker_Api
{
    public class ProgramService
    {
        public static void AddAppBuilder(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //for login

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
                HttpOnly = HttpOnlyPolicy.None,
                Secure = CookieSecurePolicy.None // Change to CookieSecurePolicy.Always for secure (HTTPS) connections
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();
            app.UseAuthorization();


        }
        public static void Add(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITrackerService,TrackerService>();
        }
        public static void AddDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TrackerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TRACKER_API")), ServiceLifetime.Transient);
            //// Configure DbContext
            //builder.Services.AddDbContext<IdentityContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("IDENTITY")));



        }
    }
}
