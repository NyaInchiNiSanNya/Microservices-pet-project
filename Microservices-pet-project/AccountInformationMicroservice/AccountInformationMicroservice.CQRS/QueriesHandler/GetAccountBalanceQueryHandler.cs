using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountInformationMicroservice.CQRS.Queries;
using AccountInformationMicroservice.Data.DbSettings;
using AccountInformationMicroservice.DbData;
using MediatR;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace AccountInformationMicroservice.CQRS.QueriesHandler
{
    public class GetAccountBalanceQueryHandler : IRequestHandler< GetAccountBalanceQuery, Decimal>
    {
        private readonly IMongoCollection<Account>? _account;
        
        public GetAccountBalanceQueryHandler(IAccountDBsettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _account = database.GetCollection<Account>(settings.AccountsBankCollectionName);
        }

        public async Task<Decimal> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Account>.Filter.Eq(account => account.AccountName,request.AccountName);

            var account = await _account.Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (account==null)
            {
                throw new NullReferenceException(nameof(account));
            }

            return account.Balance;
        }
    }
}
