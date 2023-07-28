CREATE TABLE "Accounts" (
    "Id" SERIAL NOT NULL,
    "Balance" NUMERIC NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_Accounts" PRIMARY KEY ("Id")
);