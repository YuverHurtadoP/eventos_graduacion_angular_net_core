using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
 
using System.Reflection;
using Event_graduation.Models;
using Event_graduation.Aplication.UseCase.InterfaceUseCase;
using Event_graduation.Aplication.UseCase.Impl;
using Event_graduation.Dominio.Repository;
using Event_graduation.Persistence;
using Event_graduation.Aplication.DTOS.Request;

namespace Event_graduation.IOC
{
    public static class Dependence
    {
        public static void InjectDependency(this IServiceCollection services, IConfiguration Configuration) {


            services.AddDbContext<EventContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("conexionBD")));
            services.AddControllers().AddJsonOptions(
                opt => { opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; }
            );

            services.AddScoped<IEventUseCase, EventUseCaseImpl>();
 
            services.AddScoped<IGenericRepository<Event>, GenericRepositoryImpl<Event>>();
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

        }
    }
}


