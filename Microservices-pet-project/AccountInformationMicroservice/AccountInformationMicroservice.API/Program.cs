using AccountInformationMicroservice.API.FluentValidation;
using AccountInformationMicroservice.CQRS.Commands;
using AccountInformationMicroservice.Data.DbSettings;
using AccountInformationMicroservice.IServices;
using AccountInformationMicroservice.MessagingConsume;
using AccountInformationMicroservice.Services;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AccountInformationMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IMongoClient>(s =>
                new MongoClient(builder.Configuration.GetValue<String>("AccountDBSettings:ConnectionString")));
            
            builder.Services.Configure<AccountDBSettings>(
                builder.Configuration.GetSection(nameof(AccountDBSettings)));

            builder.Services.AddSingleton<IAccountDBsettings>(sp =>
                sp.GetRequiredService<IOptions<AccountDBSettings>>().Value);

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<GetBalanceMessageService>();
                x.AddConsumer<UpdateBalanceMessageService>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host("rabbitmq://rabbitmq", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    config.ReceiveEndpoint("account_info_queue", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<GetBalanceMessageService>(provider);
                        ep.ConfigureConsumer<UpdateBalanceMessageService>(provider);
                    });

                }));

            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IAccountInformationManagmentService,AccountInformationManagmentService>();
            builder.Services.AddValidatorsFromAssemblyContaining<GetAccountBalanceRequestValidator>();
            builder.Services.AddMediatR(
                cfg =>
                    cfg.RegisterServicesFromAssemblyContaining<PutNewAccountBalanceCommand>());
            
            builder.Services.AddAutoMapper(typeof(Program));
            
            var app = builder.Build();

            
            app.UseSwagger(); 
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}