using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AccountInformationMicroservice.CQRS.Commands;
using AccountInformationMicroservice.Data.DbSettings;
using AccountInformationMicroservice.DbData;
using MongoDB.Driver;

namespace AccountInformationMicroservice.CQRS.CommandsHandler
{
    internal class PutNewAccountBalanceCommandHandler : IRequestHandler<PutNewAccountBalanceCommand>
    {
        private readonly IMongoCollection<Account> _account;

        public PutNewAccountBalanceCommandHandler(IAccountDBsettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _account = database.GetCollection<Account>(settings.AccountsBankCollectionName);
        }
        

        public async Task Handle(PutNewAccountBalanceCommand request,
            CancellationToken cancellationToken)
        {
            var filter = Builders<Account>.Filter.Eq(account => account.AccountName, request.AccountName);

            var account = await _account.Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            if (account == null)
            {
                throw new NullReferenceException(nameof(account));
            }

            await _account.ReplaceOneAsync(student => 
                student.AccountName.Equals(request.AccountName)
                , new Account()
                {
                    AccountName = request.AccountName,
                    Balance = request.NewBalance
                }, cancellationToken: cancellationToken);

        }
    }
}
