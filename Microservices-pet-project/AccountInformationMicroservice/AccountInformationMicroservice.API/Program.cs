using AccountInformationMicroservice.API.FluentValidation;
using AccountInformationMicroservice.CQRS.Commands;
using AccountInformationMicroservice.Data.DbSettings;
using AccountInformationMicroservice.IServices;
using AccountInformationMicroservice.Services;
using FluentValidation;
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