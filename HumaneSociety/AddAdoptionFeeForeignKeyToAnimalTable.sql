ALTER TABLE jl.animal
add Adoption_Fee_Id int,
FOREIGN KEY (adoption_Fee_id)
    REFERENCES jl.AdoptionFee (adoption_Fee_id);