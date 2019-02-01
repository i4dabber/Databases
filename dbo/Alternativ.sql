--
-- Create Table    : 'Alternativ'   
-- PersonID        :  (references Person.PersonID)
-- AdressID        :  (references Adress.AdressID)
-- aatype          :  
--
CREATE TABLE Alternativ (
    PersonID       BIGINT NOT NULL,
    AdressID       BIGINT NOT NULL,
    aatype         NVARCHAR(50) NOT NULL,
[aaID] BIGINT NOT NULL IDENTITY, 
    CONSTRAINT pk_Alternativ PRIMARY KEY CLUSTERED (PersonID,AdressID),
CONSTRAINT fk_Alternativ FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
CONSTRAINT fk_Alternativ2 FOREIGN KEY (AdressID)
    REFERENCES Adress (AdressID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
    