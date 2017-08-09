ALTER TABLE jl.animal
ADD CONSTRAINT room_id
    FOREIGN KEY (room_id)
    REFERENCES jl.room (room_id)
    ON DELETE cascade;