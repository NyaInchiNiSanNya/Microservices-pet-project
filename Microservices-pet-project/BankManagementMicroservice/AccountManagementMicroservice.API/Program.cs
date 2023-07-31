using AccountManagementMicroservice.BusinessLogic;
using AccountManagementMicroservice.CQRS.CommandsHandler;
using AccountManagementMicroservice.Data;
using AccountManagementMicroservice.FluentValidation;
using AccountManagementMicroservice.IServices;
using AccountManagementMicroservice.MappingProfiles;
using AccountManagementMicroservice.MessagingService;
using AccountManagementMicroservice.RequestModel;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ShareModel.Requests;


namespace BankManagementMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BankContext>(opt =>
            {
                var connString = builder.Configuration
                    .GetConnectionString("DefaultConnection");
                opt.UseNpgsql(connString);

            });
            
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<WithdrawalMessageService>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    config.ReceiveEndpoint("withdrawal_queue1", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<WithdrawalMessageService>(provider);
                    });

                }));

            });

            builder.Services.AddValidatorsFromAssemblyContaining<PostWithdrawalRequest>();
            builder.Services.AddValidatorsFromAssemblyContaining<ReplenishmentOperationRequestValidator>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(TopUpOperationProfile));
            builder.Services.AddScoped<IWithdrawOperationService, WithdrawOperationService>();
            builder.Services.AddScoped<IAccountInformationService,AccountInformationService>();
            builder.Services.AddScoped<ITopUpOperationService, TopUpOperationService>();
            builder.Services.AddMediatR(
                cfg =>
                    cfg.RegisterServicesFromAssemblyContaining<TopUpOperationCommandHandler>());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
                app.UseSwaggerUI();

                app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}