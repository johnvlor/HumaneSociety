create table jl.Adopter
(
Adopter_Id int Identity(1,1) Primary Key,
First_Name varchar(50) not null,
Last_Name varchar(50) not null,
Animal_Category varchar(50),
Animal_Gender varchar(50),
Animal_Age varchar(50),
);