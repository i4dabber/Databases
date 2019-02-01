--
-- Create Table    : 'E_mail'   
-- E_mail_address  :  
-- EmailID         :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE E_mail (
    E_mail_address NVARCHAR(60) NOT NULL,
    EmailID        BIGINT IDENTITY(1,1) NOT NULL,
    PersonID       BIGINT NULL,
CONSTRAINT pk_E_mail PRIMARY KEY CLUSTERED (EmailID),
CONSTRAINT fk_E_mail FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON UPDATE CASCADE)