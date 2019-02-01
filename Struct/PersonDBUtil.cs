using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Model;



namespace Structure
{
    /// <summary>
    /// The naming concention in this class is both English and Danish. 
    /// The danish terms for entities has been preserved in the code
    /// to be in accordance with RDB.
    /// </summary>
    public class PersonDBUtil
    {
        private Person currentPerson;
        /// <summary>
        /// Constructor may be use to initialize the connection string and likely setup things 
        /// </summary>
        public PersonDBUtil()
        {
            currentPerson = new Person() { PersonID = 0, First_Name = "", Middle_Name = "", Last_Name = "" };
        }

        /// <summary>
        /// This a local utility method providing an opne ADO.NET SQL connection
        /// Examples of connection strings are given like below:
        /// new SqlConnection(@"Data Source=(localdb)\\Projects;Initial Catalog=Opgave1;Integrated Security=True");
        /// new SqlConnection(@"Data Source=(local);Initial Catalog=Northwind;Integrated Security=SSPI");
        /// new SqlConnection(@"Data Source=webhotel10.iha.dk;Initial Catalog=E13I4DABH0Gr1;User ID=E13I4DABH0Gr1; Password=E13I4DABH0Gr1");
        /// new SqlConnection(@"Data Source=(localdb)\ProjectsV13;User ID=Program;Password=Program123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        /// </summary>
        private SqlConnection OpenConnection
        {
            get
            {
                //var con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=CraftManDB;Integrated Security=True");
                var con = new SqlConnection(@"Data Source = st-i4dab.uni.au.dk.; Persist Security Info = True; User ID = E18I4DABau572020; Password = E18I4DABau572020; Pooling = False; MultipleActiveResultSets = False; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = True");
                con.Open();
                return con;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hv">Håndværker som skal tilføjes påvirker ikke currentHåndværker</param>
        public void AddPersonDB(ref Person hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Person] (First_Name, Middle_Name, Last_Name, AdressID)
                                                    OUTPUT INSERTED.PersonID  
                                                    VALUES (@First_Name,@Middle_Name,@Last_Name,@AdressID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@First_Name", hv.First_Name);//.ToString("yyyy-MM-dd HH:mm:ss"); ;
                cmd.Parameters.AddWithValue("@Middle_Name", hv.Middle_Name);
                cmd.Parameters.AddWithValue("@Last_Name", hv.Last_Name);
                cmd.Parameters.AddWithValue("@AdressID", hv.PrimaryAddress.AdressID);
                hv.PersonID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }
        public void UpdatePersonDB(ref Person hv)
        {
            // prepare command string
            string updateString =
               @"UPDATE Person
                        SET First_Name = @First_Name, Middle_Name = @Middle_Name, Last_Name = @Last_Name
                        WHERE PersonID = @PersonID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@PersonID", hv.PersonID);
                cmd.Parameters.AddWithValue("@First_Name", hv.First_Name);
                cmd.Parameters.AddWithValue("@Middle_Name", hv.Middle_Name);
                cmd.Parameters.AddWithValue("@Last_Name", hv.Last_Name);
                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeletePersonDB(ref Person hv)
        {
            string deleteString = @"DELETE FROM Person WHERE (PersonID = @PersonID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", hv.PersonID);

                var id = (int)cmd.ExecuteNonQuery();
                hv = null;
            }
        }

        public void GetPersonByName(ref Person hv)
        {
            string sqlcmd = @"SELECT  TOP 1 * FROM Person WHERE (First_Name = @First_Name) AND (Middle_Name = @Middle_Name) AND (Last_Name=@Last_Name)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@First_Name", hv.First_Name);
                cmd.Parameters.AddWithValue("@Middle_Name", hv.Middle_Name);
                cmd.Parameters.AddWithValue("@Last_Name", hv.Last_Name);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    currentPerson.PersonID = (int)rdr["PersonID"];
                    currentPerson.First_Name = (string)rdr["First_Name"];
                    currentPerson.Middle_Name = (string)rdr["Middle_Name"];
                    currentPerson.Last_Name = (string)rdr["Last_Name"];
                    hv = currentPerson;
                }
            }
        }

        public void GetCurrentPersonById(ref Person hv)
        {
            string sqlcmd = @"SELECT [First_Name],[Middle_Name], [Last_Name] FROM Person WHERE ([PersonID] = @PersonID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", hv.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    currentPerson.PersonID = hv.PersonID;
                    //hv.HID = (int)rdr["HåndværkerId"];//superflous reading from DB and not in projection
                    currentPerson.First_Name = (string)rdr["First_Name"];
                    currentPerson.Middle_Name = (string)rdr["Middle_Name"];
                    currentPerson.Last_Name = (string)rdr["Last_Name"];
                    hv = currentPerson;
                }
            }
        }

        public List<Person> GetPersoner()
        {
            string sqlcmd = @"SELECT * FROM Person";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Person> hver = new List<Person>();
                Person hv = null;
                while (rdr.Read())
                {
                    hv = new Person();
                    hv.PersonID = (int)rdr["PersonID"];
                    hv.First_Name = (string)rdr["First_Name"];
                    hv.Middle_Name = (string)rdr["Middle_Name"];
                    hv.Last_Name = (string)rdr["Last_Name"];
                    hver.Add(hv);
                }
                return hver;
            }

        }

        public List<E_mail> GetPerson_E_Mail(ref E_mail hv)
        {
            string selectToolboxToolString = @"SELECT *
                                                  FROM [E_Mail] 
                                                  WHERE ([PersonID] = @PersonID)";
            using (var cmd = new SqlCommand(selectToolboxToolString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@EmailID", hv.EmailID);
                rdr = cmd.ExecuteReader();
                List<E_mail> boxestohv = new List<E_mail>();
                E_mail ek = null;
                while (rdr.Read())
                {
                    ek = new E_mail(); // Ny instans i hver gennemløb!

                    ek.EmailID = (int)rdr["EmailID"];
                    ek.Email_Adress = (string)rdr["E_mail_Adress"];
                    boxestohv.Add(ek);
                }
                return boxestohv;
            }
        }

        public List<Phone> GetPerson_Phone(ref Phone hv)
        {
            string selectToolboxToolString = @"SELECT *
                                                  FROM [Phone] 
                                                  WHERE ([PersonID] = @PersonID)";
            using (var cmd = new SqlCommand(selectToolboxToolString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@PhoneID", hv.PhoneID);
                rdr = cmd.ExecuteReader();
                List<Phone> boxestohv = new List<Phone>();
                Phone PHk = null;
                while (rdr.Read())
                {
                    PHk = new Phone(); // Ny instans i hver gennemløb!

                    PHk.PhoneID = (long)rdr["PhoneID"];
                    PHk.Phone_Nr = (long)rdr["Phone_Nr"];
                    PHk.Phone_Type = (long)rdr["Phone_Type"];
                    boxestohv.Add(PHk);
                }
                return boxestohv;
            }
        }

        public List<Adress> GetPerson_Address(ref Adress hv)
        {
            string selectToolboxToolString = @"SELECT *
                                                  FROM [Address] 
                                                  WHERE ([PersonID] = @PersonID)";
            using (var cmd = new SqlCommand(selectToolboxToolString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@AddressID", hv.AdressID);
                rdr = cmd.ExecuteReader();
                List<Adress> boxestohv = new List<Adress>();
                Adress ak = null;
                while (rdr.Read())
                {
                    ak = new Adress(); // Ny instans i hver gennemløb!

                    ak.AdressID = (int)rdr["AddressID"];
                    ak.Street_Name = (string)rdr["Street_Name"];
                    ak.Street_Nr = (int)rdr["Street_Nr"];
                    boxestohv.Add(ak);
                }
                return boxestohv;
            }
        }

        public void AddE_mailDB(ref E_mail hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [E_mail] (E_mail_Address, PersonID)
                                                    OUTPUT INSERTED.EmailID  
                                                    VALUES (@E_mail_Address, @PersonID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@E_mail_Address", hv.Email_Adress);//.ToString("yyyy-MM-dd HH:mm:ss")
                cmd.Parameters.AddWithValue("@PersonID", hv.persons.PersonID);
                hv.EmailID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void GetEmailById(ref E_mail ek)
        {
            string sqlcmd = @"SELECT Email_Address, EmailID FROM E_mail WHERE (EmailID =@EmailID )";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailID", ek.EmailID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    //Værktøjskasse curvk = new Værktøjskasse();
                    ek.EmailID = (int)rdr["EmailID"];
                    ek.Email_Adress = (string)rdr["Email_Adress"];
                }
            }
        }

        public void UpdateE_mailDB(ref E_mail hv)
        {
            // prepare command string
            string updateString =
               @"UPDATE E_mail
                        SET Email_Adress = @Email_Adress
                        WHERE EmailID = @EmailID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@EmailID", hv.EmailID);
                cmd.Parameters.AddWithValue("@Email_Adress", hv.Email_Adress);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteE_mailDB(ref E_mail ek)
        {
            string deleteString = @"DELETE FROM E_mail WHERE (EmailID = @EmailID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailID", ek.EmailID);

                var id = (int)cmd.ExecuteNonQuery();
                ek = null;
            }
        }

        public void AddPhoneDB(ref Phone hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Phone] (Phone_Nr, Phone_Type, PersonID)
                                                    OUTPUT INSERTED.PhoneID  
                                                    VALUES (@Phone_Nr, @Phone_Type, @PersonID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready   

                cmd.Parameters.AddWithValue("@Phone_Nr", hv.Phone_Nr);//.ToString("yyyy-MM-dd HH:mm:ss"); 
                cmd.Parameters.AddWithValue("@Phone_Type", hv.Phone_Type);
                cmd.Parameters.AddWithValue("@PersonID", hv.personer.PersonID);
                hv.PhoneID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void GetPhoneById(ref Phone vk)
        {
            string sqlcmd = @"SELECT * FROM Phone WHERE (PhoneID = @PhoneID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PhoneID", vk.PhoneID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    //Værktøjskasse curvk = new Værktøjskasse();
                    //vk.Anskaffet = (int)rdr["Anskaffet"];
                    vk.PhoneID = (long)rdr["PhoneID"];
                    vk.Phone_Nr = (long)rdr["Phone_Nr"];
                    vk.Phone_Type = (long)rdr["Phone_Type"];
                    
                }
            }
        }

        public void UpdatePhone(ref Phone vtk)
        {
            // prepare command string
            string updateString =
               @"UPDATE Phone                 
                        SET Phone_Nr = @Phone_Nr, Phone_Type = @Phone_Type
                            
                        WHERE PhoneID = @PhoneID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@PhoneID", vtk.PhoneID);
                cmd.Parameters.AddWithValue("@Phone_Nr", vtk.Phone_Nr);
                cmd.Parameters.AddWithValue("@Phone_Type", vtk.Phone_Type);
              

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeletePhoneDB(ref Phone vtk)
        {
            string deleteString = @"DELETE FROM Phone WHERE (PhoneID = @PhoneID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PhoneID", vtk.PhoneID);

                var id = (int)cmd.ExecuteNonQuery();
                vtk = null;
            }

        }

        public void AddAdressDB(ref Adress hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Adress] (Street_Name, Street_Nr, Apartment_Nr, ZipID)
                                                    OUTPUT INSERTED.AdressID 
                                                    VALUES (@Street_Name, @Street_Nr, @Apartment_Nr, @ZipID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready   

                cmd.Parameters.AddWithValue("@Street_Name", hv.Street_Name);//.ToString("yyyy-MM-dd HH:mm:ss"); 
                cmd.Parameters.AddWithValue("@Street_Nr", hv.Street_Nr);
                cmd.Parameters.AddWithValue("@Apartment_Nr", hv.Apartment_Nr);
                cmd.Parameters.AddWithValue("@ZipID", hv.ZIPs.ZipID);
                hv.AdressID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void GetAdressId(ref Adress vk)
        {
            string sqlcmd = @"SELECT * FROM Adress WHERE (AdressID = @AdressID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AdressID", vk.AdressID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    //Værktøjskasse curvk = new Værktøjskasse();

                    vk.AdressID = (int)rdr["AdressID"];
                    vk.Street_Name = (string)rdr["Street_Name"];
                    vk.Street_Nr = (int)rdr["Street_Nr"];

                }
            }
        }

        public void UpdateAdress(ref Adress vtk)
        {
            // prepare command string
            string updateString =
               @"UPDATE Adress                 
                        SET Street_Name = @Street_Name, Street_Nr = @Street_Nr, Apartment_Nr = @Apartment_Nr
                            
                        WHERE AdressID = @AdressID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@AdressID", vtk.AdressID);
                cmd.Parameters.AddWithValue("@Street_Name", vtk.Street_Name);
                cmd.Parameters.AddWithValue("@Street_Nr", vtk.Street_Nr);
                cmd.Parameters.AddWithValue("@Apartment_Nr", vtk.Apartment_Nr);


                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAdressDB(ref Adress ek)
        {
            string deleteString = @"DELETE FROM Adress WHERE (AdressID = @AdressID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AdressID", ek.AdressID);

                var id = (int)cmd.ExecuteNonQuery();
                ek = null;
            }
        }

        public void AddZIPDB(ref ZIP hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [ZIP] (City_Name, Country_Name, Postal_Code, ZipListID)
                                                    OUTPUT INSERTED.ZipID 
                                                    VALUES (@City_Name, @Country_Name, @Postal_Code, @ZipListID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready   

                cmd.Parameters.AddWithValue("@City_Name", hv.City_Name);//.ToString("yyyy-MM-dd HH:mm:ss"); 
                cmd.Parameters.AddWithValue("@Country_Name", hv.Country_Name);
                cmd.Parameters.AddWithValue("@Postal_Code", hv.Postal_Code);
                cmd.Parameters.AddWithValue("@ZipListID", hv.ZIPL.ZipListID);
                hv.ZipID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void GetZIPID(ref ZIP vk)
        {
            string sqlcmd = @"SELECT * FROM ZIP WHERE (ZipID = @ZipID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AdressID", vk.ZipID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    //Værktøjskasse curvk = new Værktøjskasse();

                    vk.ZipID = (int)rdr["AdressID"];
                    vk.City_Name = (string)rdr["City_Name"];
                    vk.Country_Name = (string)rdr["Country_Name"];
                    vk.Postal_Code = (int)rdr["Postal_Code"];

                }
            }
        }

        public void UpdateZIP(ref ZIP vtk)
        {
            // prepare command string
            string updateString =
               @"UPDATE ZIP                 
                        SET City_Name = @City_Name, Country_Name = @Country_Name, Postal_Code = @Postal_Code
                            
                        WHERE ZipID = @ZipID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 

                cmd.Parameters.AddWithValue("@City_Name", vtk.City_Name);
                cmd.Parameters.AddWithValue("@Country_Name", vtk.Country_Name);
                cmd.Parameters.AddWithValue("@Postal_Code", vtk.Postal_Code);


                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteZIPDB(ref ZIP vtk)
        {
            string deleteString = @"DELETE FROM ZIP WHERE (ZipID = @ZipID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PhoneID", vtk.ZipID);

                var id = (int)cmd.ExecuteNonQuery();
                vtk = null;
            }

        }

        public void AddZipListeDB(ref ZipListe hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [ZipListe] (ZipCode)
                                                    OUTPUT INSERTED.ZipListID 
                                                    VALUES (@ZipCode)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready   

                cmd.Parameters.AddWithValue("@ZipCode", hv.ZipCode);//.ToString("yyyy-MM-dd HH:mm:ss"); 
                hv.ZipListID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void AddNoterDB(ref Noter hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Noter] (Notes, PersonID)
                                                    OUTPUT INSERTED.NoteID 
                                                    VALUES (@Notes, @PersonID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready   

                cmd.Parameters.AddWithValue("@Notes", hv.Notes);//.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.AddWithValue("@PersonID", hv.perso.PersonID);
                hv.NoteID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void UpdateNoter(ref Noter vtk)
        {
            // prepare command string
            string updateString =
               @"UPDATE Noter                 
                        SET Notes = @Notes
                            
                        WHERE NoteID = @NoteID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 

                cmd.Parameters.AddWithValue("@Notes", vtk.Notes);
                

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteNoterDB(ref Noter ek)
        {
            string deleteString = @"DELETE FROM Noter WHERE (NoteID = @NoteID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@NoteID", ek.NoteID);

                var id = (int)cmd.ExecuteNonQuery();
                ek = null;
            }
        }

        public void AddAlternativDB(ref Alternativ hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Alternativ] (aatype, AdressID, PersonID)
                                                    OUTPUT INSERTED.aaID
                                                    VALUES (@aatype, @AdressID, @PersonID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready  
                cmd.Parameters.AddWithValue("@aatype", hv.aatype);
                cmd.Parameters.AddWithValue("@AdressID", hv.Adresse.AdressID);
                cmd.Parameters.AddWithValue("@PersonID", hv.Persone.PersonID);
                hv.aaID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void UpdateAlternativDB(ref Alternativ vtk)
        {
            // prepare command string
            string updateString =
               @"UPDATE Alternativ                 
                        SET aatype = @aatype
                            
                        WHERE aaID = @aaID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 

                cmd.Parameters.AddWithValue("@aatype", vtk.aatype);


                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAlternativDB(ref Alternativ vtk)
        {
            string deleteString = @"DELETE FROM Alternativ WHERE (aaID = @aaID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@aaID", vtk.aaID);

                var id = (int)cmd.ExecuteNonQuery();
                vtk = null;
            }
        }


            /* public void AddAlternativDB(ref Alternativ hv)
             {
                 // prepare command string using paramters in string and returning the given identity 

                 string insertStringParam = @"INSERT INTO [Person] (aatyper, AdressID, PersonID)
                                                         OUTPUT INSERTED.PersonID  
                                                         VALUES (@aatyper, @AdressID, @PersonID)";

                 using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
                 {
                     // Get your parameters ready                    
                     cmd.Parameters.AddWithValue("@aatyper", hv.aatyper);
                     cmd.Parameters.AddWithValue("@AdressID", hv.AdressID);
                     cmd.Parameters.AddWithValue("@PersonID", hv.PersonID);
                     //hv.PersonID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
                 }
             }*/

            /* public void UpdateAlternativ(ref Alternativ vtk)
             {
                 // prepare command string
                 string updateString =
                    @"UPDATE Noter                 
                             SET aatyper = @aatyper

                             WHERE AlternativID = @AlternativID";

                 using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
                 {
                     // Get your parameters ready 

                     cmd.Parameters.AddWithValue("@aatyper", vtk.aatyper);


                     var id = (int)cmd.ExecuteNonQuery();
                 }
             }*/

            /*public void DeleteAlternativDB(ref Alternativ ek)
            {
                string deleteString = @"DELETE FROM Alternativ WHERE (AlternativID = @AlternativID)";
                using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
                {
                    cmd.Parameters.AddWithValue("@NoteID", ek.AlternativID);

                    var id = (int)cmd.ExecuteNonQuery();
                    ek = null;
                }
            }*/


            /*public void GetFullTreePersonDB(ref Person fhv)
            {
                string getfulltreecraftman = @"SELECT  Person.PersonID, Person.First_Name, Person.Middle_Name, Person.Last_Name, 
                    Adress.AdressID, Adress.Street_Name, Adress.Street_Nr, Adress.Apartment_Nr,
                    ZIP.ZipID, ZIP.City_Name, ZIP.Country_Name,
                    ZipListe.ZipListID, ZipListe.ZipCode

    FROM      Person INNER JOIN
                    Adress ON Person.PersonID = Adress.Person INNER JOIN
                    ZIP ON Adress.AdressID = ZIP.Adress INNER JOIN
                    ZipListe ON ZIP.ZipID = ZipListe.ZIP

    WHERE   (Person.PersonID = @PersonID)";

                using (var cmd = new SqlCommand(getfulltreecraftman, OpenConnection))
                {
                    cmd.Parameters.AddWithValue("@PersonID", fhv.PersonID);
                    SqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();
                    var cmcount = 0;
                    var tbcount = 0;
                    var toolcount = 0;
                    long hvid = 0;
                    int tbid = 0; //VærktøjskasseID
                    Person hv = new Person();
                    Adress ak = null;
                    hv.PrimaryAddress = new Adress();
                    while (rdr.Read())
                    {
                        int hid;
                        hid = (int)rdr["PersonID"];
                        if (hvid != hid) //Hent root håndværker
                        {
                            cmcount++;
                            hv.PersonID = hid;
                            hvid = hv.PersonID;  
                            hv.First_Name = (string)rdr["First_Name"];
                            hv.Middle_Name = (string)rdr["Middle_Name"];
                            hv.Last_Name = (string)rdr["Last_Name"];


                        }
                        if (!rdr.IsDBNull(5)) //Her er en værktøjskasse index 0...->
                        {
                            tbcount++;
                            int vtid;
                            vtid = (int)rdr["AdressID"];//Temp ID
                            if (tbid != vtid) //Vi har en ny vtkasse!
                            {
                                ak = new Adress
                                {
                                    ZIPs = new ZIP(),
                                    AdressID = vtid
                                };
                                hv.Zips.Add(ak);
                            }
                            tbid = ak.AdressID;
                            ak.Anskaffet = (DateTime)rdr["Anskaffet"];
                            vk.Fabrikat = (string)rdr["Fabrikat"];
                            vk.Håndværker = (int)rdr["Håndværker"];
                            vk.Model = (string)rdr["Model"];
                            vk.Serienummer = (string)rdr["Serienummer"];
                            vk.Farve = (string)rdr["Farve"];

                        }
            /*if (!rdr.IsDBNull(12)) //Her er et værktøj 
            {
                toolcount++;
                Phone vt = new Phone();

                vt.PhoneID = (int)rdr["PhoneID"];
                vt.Anskaffet = (int)rdr["Anskaffet"];
                vt.Phone_Nr = (int)rdr["Phone_Nr"];
                vt.Værktøjskasse = (int)rdr["Værktøjskasse"];
                vt.Model = (string)rdr["VTModel"];
                vt.Seriennr = (string)rdr["Serienr"];
                vk.Værktøj_i_kassen.Add(vt);
            }*/
        }
    }




