--
-- Create Table    : 'Phone'   
-- Phone_Nr        :  
-- Phone_Type      :  
-- PhoneID         :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE Phone (
    Phone_Nr       BIGINT NOT NULL,
    Phone_Type     BIGINT NOT NULL,
    PhoneID        BIGINT IDENTITY(1,1) NOT NULL,
    PersonID       BIGINT NOT NULL,
CONSTRAINT pk_Phone PRIMARY KEY CLUSTERED (PhoneID),
CONSTRAINT fk_Phone FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)