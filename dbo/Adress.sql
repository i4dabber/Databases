--
-- Create Table    : 'Adress'   
-- Street_Name     :  
-- Street_Nr       :  
-- Apartment_Nr    :  
-- AdressID        :  
-- ZipID           :  (references Zip.ZipID)
--
CREATE TABLE Adress (
    Street_Name    NVARCHAR(50) NOT NULL,
    Street_Nr      BIGINT NOT NULL,
    Apartment_Nr   BIGINT NOT NULL,
    AdressID       BIGINT IDENTITY(1,1) NOT NULL,
    ZipID          BIGINT NOT NULL,
CONSTRAINT pk_Adress PRIMARY KEY CLUSTERED (AdressID),
CONSTRAINT fk_Adress FOREIGN KEY (ZipID)
    REFERENCES Zip (ZipID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)