--
-- Create Table    : 'Noter'   
-- NoteID          :  
-- Notes           :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE Noter (
    NoteID         BIGINT NOT NULL IDENTITY,
    Notes          NVARCHAR(1000) NOT NULL,
    PersonID       BIGINT NOT NULL,
CONSTRAINT pk_Noter PRIMARY KEY CLUSTERED (NoteID),
CONSTRAINT fk_Noter FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON UPDATE CASCADE)