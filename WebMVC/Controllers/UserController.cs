using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly string _APIUrl = "https://localhost:44348/api/UserDetails";
        private readonly HttpClient _httpClient = new HttpClient();
        public ActionResult Index()
        {
            IEnumerable<UserDetail> userList = new List<UserDetail>();
            _httpClient.BaseAddress = new Uri(_APIUrl);
            var httpClientTask = _httpClient.GetAsync("UserDetails");
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var userDetailsTask = httpResponse.Content.ReadAsStringAsync();
                userDetailsTask.Wait();
                var userDetails = JsonConvert.DeserializeObject<IList<UserDetail>>(userDetailsTask.Result);
                userList = userDetails;
            }
            return View(userList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserDetail UserDetail)
        {
            _httpClient.BaseAddress = new Uri(_APIUrl);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(UserDetail), Encoding.UTF8, "application/json");
            var httpClientTask = _httpClient.PostAsync("UserDetails", httpContent);
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        public ActionResult Details(int id)
        {
            UserDetail userDetails = new UserDetail();
            _httpClient.BaseAddress = new Uri(_APIUrl);
            var httpClientTask = _httpClient.GetAsync("UserDetails/" + id.ToString());
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var userDetailTask = httpResponse.Content.ReadAsStringAsync();
                userDetailTask.Wait();
                userDetails = JsonConvert.DeserializeObject<UserDetail>(userDetailTask.Result);
            }
            return View(userDetails);
        }
        public ActionResult Edit(int id)
        {
            UserDetail userDetails = new UserDetail();
            _httpClient.BaseAddress = new Uri(_APIUrl);
            var httpClientTask = _httpClient.GetAsync("UserDetails/" + id.ToString());
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var userDetailTask = httpResponse.Content.ReadAsStringAsync();
                userDetailTask.Wait();
                userDetails = JsonConvert.DeserializeObject<UserDetail>(userDetailTask.Result);
            }
            return View(userDetails);
        }
        [HttpPost]
        public ActionResult Edit(UserDetail UserDetail)
        {
            _httpClient.BaseAddress = new Uri(_APIUrl);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(UserDetail), Encoding.UTF8, "application/json");
            var httpClientTask = _httpClient.PutAsync("UserDetails/" + UserDetail.Id.ToString(), httpContent);
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Edit");
        }
        public ActionResult Delete(int id)
        {
            _httpClient.BaseAddress = new Uri(_APIUrl);
            var httpClientTask = _httpClient.DeleteAsync("UserDetails/" + id.ToString());
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}