--
-- Create Table    : 'Person'   
-- First_Name      :  
-- Middle_Name     :  
-- Last_Name       :  
-- PersonID        :  
-- AdressID        :  (references Adress.AdressID)
--
CREATE TABLE Person (
    First_Name     NVARCHAR(50) NULL,
    Middle_Name    NVARCHAR(50) NULL,
    Last_Name      NVARCHAR(50) NULL,
    PersonID       BIGINT IDENTITY(1,1) NOT NULL,
    AdressID       BIGINT NOT NULL,
CONSTRAINT pk_Person PRIMARY KEY CLUSTERED (PersonID),
CONSTRAINT fk_Person FOREIGN KEY (AdressID)
    REFERENCES Adress (AdressID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)