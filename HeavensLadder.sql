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