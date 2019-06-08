CREATE DATABASE HeavensLadder
go

create table [User](
    id int PRIMARY KEY IDENTITY,
    username varchar(30) not null,
    password varchar(30) not null
)

create table [Challenge](
    id int PRIMARY KEY IDENTITY
)

create table [Team](
    id int PRIMARY KEY IDENTITY,
    teamname varchar(30) not null
)
go

create table Sides(
    id int PRIMARY KEY IDENTITY,
    challengeid int not null,
    FOREIGN KEY (challengeid) REFERENCES [Challenge](id),
    teamid int not null,
    FOREIGN KEY (teamid) REFERENCES [Team](id),
    winreport bit not null
)

create table GameModes(
    id int PRIMARY KEY IDENTITY,
    modename varchar(30) not null
)
go

CREATE TABLE [Rank](
    id int PRIMARY KEY IDENTITY,
    teamid int NOT NULL,
    gamemodeid int NOT NULL FOREIGN KEY REFERENCES GameModes(id),
    FOREIGN KEY (teamid) REFERENCES [Team](id),
    rank int NOT NULL,
    wins int NOT NULL,
    losses int NOT NULL
)

go

INSERT INTO GameModes(modename)
VALUES ('Deathmatch'),
		('VIP Deathmatch'),
		('King of the Hill'),
		('Crazy King'),
		('Skyball');


select * from GameModes
select * from Sides