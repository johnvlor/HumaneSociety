create table jl.Animal
(
Animal_Id int Identity(1,1) Primary Key,
Name varchar(50) not null,
Category varchar(50) not null,
Gender varchar(50),
Age varchar(50),
Shots varchar(50),
Food varchar(50),
Status varchar (50) default 'Available',
Room_Id int
Foreign Key (Room_Id) References jl.Room(Room_Id)
);