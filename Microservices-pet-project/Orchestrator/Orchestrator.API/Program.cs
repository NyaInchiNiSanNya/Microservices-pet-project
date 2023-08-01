using MassTransit;
using Orchestrator.IService;
using Orchestrator.MessagingService;
using ShareModel.Requests;
using ShareModel.Response;

namespace Orchestrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IBankManagementMicroserviceMessaging, BankManagementMicroserviceMessaging>();
            builder.Services.AddScoped<IAccountInformationMicroserviceMessaging, AccountInformationMicroserviceMessaging>();
            builder.Services.AddMassTransit(x =>
            {
                x.AddRequestClient<WithdrawalOperationRequest>();
                x.AddRequestClient<ReplenishmentOperationRequest>();
                x.AddRequestClient<UpdateAccountBalanceRequest>();
                x.AddRequestClient<GetAccountBalanceRequest>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host("rabbitmq://rabbitmq", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                }));
            });

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