using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Linq;
using System.Linq.Expressions;

namespace HotelNWT.Controllers
{

    public class AccountController : System.Web.Http.ApiController
    {
        bool POSALjIMAIL = false;

        [System.Web.Mvc.HttpPost]
        public System.Net.Http.HttpResponseMessage RegistracijaJson(user korisnik)
        {
            Guid tmpGuid = Guid.NewGuid();
            var parametri = new Dictionary<string, object>{
                {"Username", korisnik.username},
                {"Password", korisnik.password},
                {"Email", korisnik.email},
                {"GUID", tmpGuid.ToString().ToLower()}
            };
            try
            {
                masterEntities dc = new masterEntities();
                dc.user.Add(korisnik);
                user u = dc.user.Where(a=>a.username == korisnik.username).FirstOrDefault();
                int ID = Convert.ToInt32(u.iduser);
                korisnik.iduser = ID;

                SendEmail("Potvrda Registracije", string.Format(@"
                Dobro došli na našu stranicu i čestitamo na uspješnoj registraciji.
                Da biste potvrdili registraciju, kliknite na link ispod:
                   http://www.nwt.somee.com/Account/PotvrdaRegistracijeJson/{0}?guid={1}", korisnik.iduser, tmpGuid), korisnik.email);

                return new HttpResponseMessage()
                {
                    StatusCode= System.Net.HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                Logovi log = new Logovi
                 {
                     Datum = DateTime.Now,
                     Sadrzaj = ex.ToString(),
                     Tip = 3
                 };
                masterEntities dc = new masterEntities();
                dc.Logovi.Add(log);
                
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }
        }
        private bool SendEmail(string subject, string body, string mailTo)
        {
            if (!POSALjIMAIL)
            {
                return false;
            }
            string fromMail = "do.not.answer.nwt@gmail.com";

            MailMessage mail = new MailMessage(fromMail, mailTo, subject, body);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Timeout = 20000,
                Credentials = new NetworkCredential("do.not.answer.nwt", "lozinka123")
            };
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {

                Logovi log = new Logovi
                 {
                     Datum = DateTime.Now,
                     Sadrzaj = ex.ToString(),
                     Tip = 3
                 };
                masterEntities dc = new masterEntities();
                dc.Logovi.Add(log);
                return false;
            }

            return true;
        }

    }
}
