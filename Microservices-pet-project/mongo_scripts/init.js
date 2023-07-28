db = db.getSiblingDB('Bank');

db.Accounts.insertMany([
  { "_id": "aaaaaaaaaaaa", "balance": 100 },
  { "_id": "aaaaaaaaaaab", "balance": 120 }
]);