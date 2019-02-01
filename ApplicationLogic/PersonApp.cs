using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure;
using Model;

namespace ApplicationLogic //Jeg har lagt parentes rundt om nogle af operationerne. Specielt Update og Delete er lagt i parantes, dem kan du bare fjerne hvis du vil teste.
                            //Skal siges at jeg ikke kunde få Noter til at virke, grundet til det er jeg glemte at instantiere en primær nøgle. Men princippet er det samme som de andre
{
    public class PersonApp
    {

        public void TheApp()
        {
            ////Allokere Utillen
            PersonDBUtil cmutil = new PersonDBUtil();
            
            ////Opretter ID for Person
            Person hv = new Person() { PersonID = 1 };

            //----------------------------------------Add Ziplister----------------------------------------
            ZipListe nyzl = new ZipListe { ZipListID = 2, ZipCode = "7000" };
            cmutil.AddZipListeDB(ref nyzl);

            //----------------------------------------INSERT ZIP----------------------------------------
            ZIP nyzv = new ZIP() { ZipID = 2, City_Name = "Fredericia", Country_Name = "DK", Postal_Code = 6400, ZIPL = nyzl };
            cmutil.AddZIPDB(ref nyzv);

            //#################################################################################################

            //----------------------------------------INSERT Adresse----------------------------------------
            Adress nyav = new Adress() { AdressID = 4, Street_Name = "Aarhusvej", Street_Nr = 3, Apartment_Nr = 23, ZIPs = nyzv };
            cmutil.AddAdressDB(ref nyav);

            //----------------------------------------UPDATE Adresse----------------------------------------
            //Adress pv = new Adress() { AdressID = 20, Street_Name = "Aarhusvej", Street_Nr = 5, Apartment_Nr = 33};
            //cmutil.UpdateAdress(ref pv);

            //----------------------------------------DELETE Adresse----------------------------------------
            //Adress nyev = new Adress() { AdressID = 10 };
            //cmutil.DeleteAdressDB(ref nyev);

            //#################################################################################################

            //----------------------------------------INSERT Person----------------------------------------
            Person nyhv = new Person() { First_Name = "Lauritz", Middle_Name = "Karl", Last_Name = "Johan", PrimaryAddress = nyav };
            cmutil.AddPersonDB(ref nyhv);

            //----------------------------------------UPDATE Person----------------------------------------
            //Person pv = new Person() { PersonID = 5, First_Name = "Henrik", Middle_Name = "Niels", Last_Name = "Olesen"};
            //cmutil.UpdatePersonDB(ref pv);

            //----------------------------------------DELETE Person----------------------------------------
            //Person nyhv1 = new Person() { PersonID = 19 };
            //cmutil.DeletePersonDB(ref nyhv1);

            //#################################################################################################

            //----------------------------------------INSERT Email----------------------------------------
            //E_mail nyhv2 = new E_mail() { EmailID = 4, Email_Adress = "muho_175@hotmail.com", persons = nyhv};
            //cmutil.AddE_mailDB(ref nyhv2);

            //----------------------------------------DELETE Email----------------------------------------
            //E_mail ev = new E_mail() { EmailID = 10 };
            //cmutil.DeleteE_mailDB(ref ev);

            //----------------------------------------UPDATE Email----------------------------------------
            //E_mail evp = new E_mail() { EmailID = 3, Email_Adress = "Jesper@hotmail.com" };

            //#################################################################################################

            //----------------------------------------INSERT Phone----------------------------------------
            //Phone nyph = new Phone() { PhoneID = 3, Phone_Nr = 8300758, Phone_Type = 5, personer = nyhv };
            //cmutil.AddPhoneDB(ref nyph);

            //----------------------------------------DELETE Phone----------------------------------------
            //Phone nywph = new Phone() { PhoneID = 10 };
            //cmutil.DeletePhoneDB(ref nywph);

            //----------------------------------------UPDATE Phone----------------------------------------
            //Phone eph = new Phone() { PhoneID = 4, Phone_Nr = 93939393, Phone_Type = 2, personer = nyhv };
            //cmutil.UpdatePhone(ref eph);

            //#################################################################################################

            //----------------------------------------INSERT Alternativ----------------------------------------
            //Alternativ avk = new Alternativ() { aatype = "Hjemmeboende", Adresse = nyav, Persone = nyhv, aaID = 1 };
            //cmutil.AddAlternativDB(ref avk);

            //----------------------------------------DELETE Alternativ----------------------------------------
            //Alternativ nyal = new Alternativ() { aaID = 1};
            //cmutil.DeleteAlternativDB(ref nyal);

            //----------------------------------------UPDATE Alternativ----------------------------------------
            /*Alternativ avp = new Alternativ() { AlternativID = 5, aatyper = "Udeboende" };
            cmutil.UpdatePersonDB(ref avp);*/

            //#################################################################################################

            //----------------------------------------INSERT Noter----------------------------------------
            //Noter not = new Noter() { NoteID = 1, Notes = "Studerende", perso = nyhv };
            //cmutil.AddNoterDB(ref not);

            //----------------------------------------UPDATE Noter----------------------------------------
            /*Noter notpv = new Noter() { NoteID = 1, Notes = "Opdateret", perso = nyhv };
            cmutil.UpdatePersonDB(ref nyhv);*/

            //----------------------------------------DELETE Noter----------------------------------------
            //Noter notet = new Noter() { NoteID = 1 };
            //cmutil.DeleteNoterDB(ref notet);

            //#################################################################################################

            //Get ID'er 
            //cmutil.GetCurrentPersonById(ref nyhv);
            //cmutil.GetPersonByName(ref nyhv);



        }


    }
}
