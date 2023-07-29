using AccountInformationMicroservice.API.FluentValidation;
using AccountInformationMicroservice.CQRS.Commands;
using AccountInformationMicroservice.Data.DbSettings;
using AccountInformationMicroservice.IServices;
using AccountInformationMicroservice.Services;
using AccountManagementMicroservice.MessagingService;
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
                x.AddConsumer<WithdrawalMessageService>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("Queue7", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<WithdrawalMessageService>(provider);
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