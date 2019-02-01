--
-- Create Table    : 'Zip'   
-- ZipID           :  
-- City_Name       :  
-- Country_Name    :  
-- Postal_Code     :  
-- ZipListID       :  (references ZipListe.ZipListID)
--
CREATE TABLE Zip (
    ZipID          BIGINT IDENTITY(1,1) NOT NULL,
    City_Name      NVARCHAR(50) NOT NULL,
    Country_Name   NVARCHAR(50) NOT NULL,
    Postal_Code    BIGINT NOT NULL,
    ZipListID      BIGINT NOT NULL,
CONSTRAINT pk_Zip PRIMARY KEY CLUSTERED (ZipID),
CONSTRAINT fk_Zip FOREIGN KEY (ZipListID)
    REFERENCES ZipListe (ZipListID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)