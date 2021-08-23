using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebMVC.Controllers
{
    public class TicketController : Controller
    {
        private readonly string _APIUrl = "https://localhost:44348/api/TicketDetails";
        private readonly HttpClient _httpClient = new HttpClient();       
        public ActionResult Index()
        {
            IEnumerable<TicketDetail> ticketList = new List<TicketDetail>();
            _httpClient.BaseAddress = new Uri(_APIUrl);
            var httpClientTask = _httpClient.GetAsync("TicketDetails");
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var ticketDetailsTask = httpResponse.Content.ReadAsStringAsync();
                ticketDetailsTask.Wait();
                var ticketDetails = JsonConvert.DeserializeObject<IList<TicketDetail>>(ticketDetailsTask.Result);
                ticketList = ticketDetails;
            }
            return View(ticketList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TicketDetail ticketDetail)
        {
            _httpClient.BaseAddress = new Uri(_APIUrl);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(ticketDetail), Encoding.UTF8, "application/json");
            var httpClientTask = _httpClient.PostAsync("TicketDetails", httpContent);
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
            TicketDetail ticketDetails = new TicketDetail();
            _httpClient.BaseAddress = new Uri(_APIUrl);
            var httpClientTask = _httpClient.GetAsync("TicketDetails/" + id.ToString());
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var ticketDetailTask = httpResponse.Content.ReadAsStringAsync();
                ticketDetailTask.Wait();
                ticketDetails = JsonConvert.DeserializeObject<TicketDetail>(ticketDetailTask.Result);
            }
            return View(ticketDetails);
        }
        public ActionResult Edit(int id)
        {
            TicketDetail ticketDetails = new TicketDetail();
            _httpClient.BaseAddress = new Uri(_APIUrl);
            var httpClientTask = _httpClient.GetAsync("TicketDetails/" + id.ToString());
            httpClientTask.Wait();
            var httpResponse = httpClientTask.Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var ticketDetailTask = httpResponse.Content.ReadAsStringAsync();
                ticketDetailTask.Wait();
                ticketDetails = JsonConvert.DeserializeObject<TicketDetail>(ticketDetailTask.Result);
            }
            return View(ticketDetails);
        }
        [HttpPost]
        public ActionResult Edit(TicketDetail ticketDetail)
        {
            _httpClient.BaseAddress = new Uri(_APIUrl);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(ticketDetail), Encoding.UTF8, "application/json");
            var httpClientTask = _httpClient.PutAsync("TicketDetails/" + ticketDetail.Id.ToString(), httpContent);
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
            var httpClientTask = _httpClient.DeleteAsync("TicketDetails/" + id.ToString());
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