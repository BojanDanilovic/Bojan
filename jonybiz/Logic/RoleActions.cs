using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using jonybiz.Models;

namespace jonybiz.Logic
{
    public class RoleActions
    {
        internal void AddUserAndRole()
        {

            /*
             Link objasnjenja:
            https://learn.microsoft.com/en-us/aspnet/web-forms/overview/getting-started/getting-started-with-aspnet-45-web-forms/membership-and-administration
            Zamislimo da smo upload-ovali na server nasu veb aplikaciju za neku firmu(npr. online shop).
            Ta online prodavnica moze da ispisuje podatke i da takodje menja podatke(brise, menja i dodaje nove).
            Svaka web aplikacija mora da ima ove mogucnosti ako radi sa bazom.
            Sada, posto je web aplikacija na serveru, njoj moze pristupi svako, dakle
            ne samo vlasnik ili administrator nego i klijenti(musterije).
            Dobro je to sto:
            -Musterija moze da vidi sve proizvode
            -Administrator ili vlasnik mogu da vide i menjaju proizvode(brisu, menjaju, dodaju nove).
            Lose je to sto:
            -Musterija moze da menja proizvode tj. bazu podataka.
            To ne zelimo, ali veb sajt treba i dalje da ima mogucnost menjanja baze.
            Da bi problem resili, moramo imati neki mehanizam(nacin) koji odredjenim ljudima
            tj. posetiocima sajta daje samo mogucnosti predvidjene za njih.
            Dakle, musterijama treba dati mogucnost npr. da vide i narucuju proizvode,
            ali im ne dozvoliti da menjaju bazu podataka.
            Administratoru ili vlasniku treba dati mogucnost menjanja baze podatka.
            Da bismo znali ko je ko na sajtu, potrebno je napraviti sistem log-ovanja
            sa username i sifrom kojim se prepoznaju korisnici.
            
            ASP.NET Identity je sistem pravljenja korisnickih naloga na nasoj veb
            aplikaciji koji omogucava da razliciti nalozi imaju razlicita ovlascenja
            tj mogucnosti rada na veb aplikaciji.
            NAPOMENA: Pre ASP.NET Identity-ja se koristio stariji princip: ASP.NET Membership.
            ASP.NET Membership: https://learn.microsoft.com/en-us/aspnet/web-forms/overview/older-versions-security/membership/
            ASP.NET Identity: https://learn.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity
            Veb kontrole za rad sa loginom:
            Login Control: https://learn.microsoft.com/en-us/dotnet/api/system.web.ui.webcontrols.login?view=netframework-4.8
            Create User Wizard: https://learn.microsoft.com/en-us/dotnet/api/system.web.ui.webcontrols.createuserwizard?view=netframework-4.8
            Password Recovery: https://learn.microsoft.com/en-us/dotnet/api/system.web.ui.webcontrols.passwordrecovery?view=netframework-4.8
            Kod dole pravi sistem naloga i korisnika sa Role(ulogom) "canEdit".
            Uloga tj. Role je naziv mogucnosti koje taj korisnik ima.
            Ovaj kojeg pravimo je neka vrsta administratora(admin-a).
            U Admin folderu se nalazi stranica kojoj moze pristupiti samo admin.
            U Web.config fajlu stranice Admin smo stavili xml kod koji omogucava
            samo adminu da otvori stranicu.
            U Account folderu se nalaze stranice kojima moze pristupiti samo
            registrovani korisnik, bili koji, bitno je da je ulogovan.
            To je takodje definisano u Web.config fajlu u folderu Account.
            U Site Master-u smo dodali link za stranicu Admin koji je nevidljiv
            onima koji nisu admin.
            U SiteMaster.cs smo u Page Load metodu dodali da se link ka admin
            stranici prikaze ako se admin uloguje.
            U Global.asax.cs fajlu smo instancirali klasu RoleActions
             */

            // Access the application context and create result variables.
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canEdit" role if it doesn't already exist.
            //ako ne postoji uloga(role) canEdit, onda je napravi
            if (!roleMgr.RoleExists("canEdit"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "canEdit" });
            }

            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            //dajemo username i email korisniku, ako nisu isti, mozda nece hteti da se uloguje
            {
                UserName = "admin@university.com",
                Email = "admin@university.com"
            };
            //IdUserResult = userMgr.Create(appUser, ConfigurationManager.AppSettings["AppUserPasswordKey"]);
            /*korisniku dajemo sifru, mora da ima bar jedno malo i bar jedno veliko slovo,
             * jednu cifru i jedan karakter koji nije alfanumericki(nije slovo niti cifra)
            
            */
            IdUserResult = userMgr.Create(appUser, "adminPass1!");

            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            if (!userMgr.IsInRole(userMgr.FindByEmail("admin@university.com").Id, "canEdit"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("admin@university.com").Id, "canEdit");
            }


            /*
             Kada se pokrene aplikacija korisnik(admin) biva napraljen i moze se logovati
            sa njegovim nalogom. Provere gore ako admin ne postoji sluze za kada se aplikacija prvi put pokrece.
            Tada se pravi baza podataka korisnika i pravi se korisnik admin. Obicne korisnike pravimo
            putem registracije preko web forme. Ako aplikaciju ponovo pokrecemo admin-a tada vec imamo
            i kod u if-ovima se nece opet pokretati. Dakle, ovaj kod se pokrece uvek kada se aplikacija pokrene.
            Zasto? Jer je klasa RoleActions instancirana u Global.asax fajlu i tu je pozvan njem metod
            AddUserAndRole().
            Dalje pogledati:
            Account -> Students.aspx i Web.config
            Admin -> InsertStudent.aspx i Web.config
            Site Master
            Siste Master.cs
             */
        }
    }
}