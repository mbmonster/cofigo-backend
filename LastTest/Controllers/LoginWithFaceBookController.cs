using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Newtonsoft.Json;

namespace LastTest.Controllers
{
    public class LoginWithFaceBookController : ApiController
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Logout()
        {
            //var status = new Dictionary<string,string>();
            //status.Add("status","success");
            //FormsAuthentication.SignOut();
            //var hrm = new HttpResponseMessage();
            //hrm.Content = new StringContent(JsonConvert.SerializeObject(status));
            //return hrm;
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpGet]
        public HttpResponseMessage FaceBookLogin([FromUri] string id, [FromUri] string token)
        {
            
            if (FacebookCheck(id, token).id == id)
            {
                
                if ((db.Users.FirstOrDefault(i=>i.ID==id))!=null)
                {
                    FormsAuthentication.SetAuthCookie(id, true);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    string response = new WebClient().DownloadString("https://graph.facebook.com/me?fields=id,name,email&access_token=" + token);
                    FacebookResponse facebookResponse = JsonConvert.DeserializeObject<FacebookResponse>(response);
                    string avatar = "http://graph.facebook.com/" + facebookResponse.id + "/picture";
                    db.Users.Add(new User { ID = facebookResponse.id, Account = facebookResponse.email, Avatar = avatar, DisplayName = facebookResponse.name, Coin = 5000, Rep = 0, Role = "USER" });
                    try
                    {
                        db.SaveChanges();
                        FormsAuthentication.SetAuthCookie(id, true);
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    catch (DbEntityValidationException ex)
                    {
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    }
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }
        private struct FacebookResponse
        {
            public string id;
            public string name;
            public string email;
        }

        private FacebookResponse FacebookCheck(string id, string token)
        {
            try
            {
                string response = new WebClient().DownloadString("https://graph.facebook.com/me?access_token=" + token);
                Debug.WriteLine(response);
                FacebookResponse facebookResponse = JsonConvert.DeserializeObject<FacebookResponse>(response);
                return facebookResponse;
            }
            catch
            {
                FacebookResponse errorResponse = JsonConvert.DeserializeObject<FacebookResponse>("{id: null}");
                return errorResponse;
            }
        }
    }
}
