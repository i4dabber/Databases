SELECT Person.First_Name, Person.Middle_Name, Person.Last_Name, Person.PersonID, Noter.NoteID, Noter.Notes, E_mail.EmailID, E_mail.E_mail_address, Phone.Phone_Nr, Phone.Phone_Type, Phone.PhoneID, Adress.Street_Name, 
                  Adress.Street_Nr, Adress.Apartment_Nr, Adress.AdressID, Alternativ.aatype, Zip.ZipID, Zip.City_Name, Zip.Country_Name, Zip.Postal_Code, ZipListe.ZipListID, ZipListe.ZipCode
FROM     Adress INNER JOIN
                  Alternativ ON Adress.AdressID = Alternativ.AdressID INNER JOIN
                  E_mail ON Alternativ.PersonID = E_mail.PersonID INNER JOIN
                  Noter ON Alternativ.PersonID = Noter.PersonID INNER JOIN
                  Person ON Adress.AdressID = Person.AdressID AND Alternativ.PersonID = Person.PersonID AND E_mail.PersonID = Person.PersonID AND Noter.PersonID = Person.PersonID INNER JOIN
                  Phone ON Person.PersonID = Phone.PersonID INNER JOIN
                  Zip ON Adress.ZipID = Zip.ZipID INNER JOIN
                  ZipListe ON Zip.ZipListID = ZipListe.ZipListID

--insert

INSERT INTO [Person] ([First_Name], [Middle_Name], [Last_Name], [AdressID])
VALUES (N'Soren', N'Slot', N'Jens', 36)


--Update

UPDATE Person
SET First_Name = 'John', Middle_Name = 'Steen'
WHERE PersonID = 1;

--Delete
DELETE FROM PERSON
WHERE First_Name = 'David';