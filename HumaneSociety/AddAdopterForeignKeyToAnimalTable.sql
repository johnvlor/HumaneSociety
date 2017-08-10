ALTER TABLE jl.animal
add Adopter_Id int,
FOREIGN KEY (adopter_id)
    REFERENCES jl.adopter (adopter_id);