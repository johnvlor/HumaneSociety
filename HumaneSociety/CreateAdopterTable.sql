create table jl.Adopter
(
Adopter_Id int Identity(1,1) Primary Key,
First_Name varchar(50) not null,
Last_Name varchar(50) not null,
Address varchar(50),
Phone int,
Previous_Pet_Owner bit,
Personality_Type varchar(50),
Homeowner_Renter varchar(50),
Animal_Preference varchar(50)
);