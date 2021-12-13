using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API_2
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            string connectionString =Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options=> {
                options.UseSqlServer(connectionString);
            });
            services.AddTransient(typeof(ICRUDService<>), typeof(CRUDService<>));
            services.AddTransient(typeof(ICRUDRepository<>), typeof(CRUDRepository<>));
            services.AddTransient<IQuizService, QuizService>();
            services.AddTransient<IQuizRepository, QuizRepository>();

            services.AddTransient<IQuizQuestionsService, QuizQuestionsService>();
            services.AddTransient<IQuizQuestionsRepository, QuizQuestionsRepository>();

            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();

            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
