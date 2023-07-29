using AccountManagementMicroservice.BusinessLogic;
using AccountManagementMicroservice.CQRS.CommandsHandler;
using AccountManagementMicroservice.Data;
using AccountManagementMicroservice.IServices;
using AccountManagementMicroservice.MessagingService;
using AccountManagementMicroservice.RequestModel;
using AccountManagementMicroservice.SharedModels;
using AccountManagementMicroservice.SharedModels.Requests;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;


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

                    config.ReceiveEndpoint("withdrawal_queue", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<WithdrawalMessageService>(provider);
                        ep.Bind<WithdrawalOperationRequest>(); 
                    });
                }));
            });

            builder.Services.AddValidatorsFromAssemblyContaining<PostWithdrawalRequest>();
            builder.Services.AddAutoMapper(typeof(Program));
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