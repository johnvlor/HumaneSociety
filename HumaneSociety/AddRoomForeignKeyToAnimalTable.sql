ALTER TABLE jl.animal
add FOREIGN KEY (room_id)
    REFERENCES jl.room (room_id);