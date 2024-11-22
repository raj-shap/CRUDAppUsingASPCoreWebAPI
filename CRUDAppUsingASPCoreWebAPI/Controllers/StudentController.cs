using CRUDAppUsingASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CRUDAppUsingASPCoreWebAPI.Controllers
{
	public class StudentController : Controller
	{
		private string url = "https://localhost:7275/api/StudentAPI/";
		private HttpClient client = new HttpClient();
		[HttpGet]
		public IActionResult Index()
		{
			List<Student> students = new List<Student>();
			HttpResponseMessage response = client.GetAsync(url).Result;
			if (response.IsSuccessStatusCode)
			{
				string result = response.Content.ReadAsStringAsync().Result;
				var data = JsonConvert.DeserializeObject<List<Student>>(result);
				if(data != null)
				{
					students = data;
				}
			}
			return View(students);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Create(Student student)
        {
			string data = JsonConvert.SerializeObject(student);
			StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
			HttpResponseMessage response  = client.PostAsync(url, content).Result;
			if (response.IsSuccessStatusCode)
			{
				TempData["Insert_Message"] = "Student Addedd Succefully...";
				return RedirectToAction("Index");
			}
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
			Student std = new Student();
			HttpResponseMessage response = client.GetAsync(url+id).Result;
			if (response.IsSuccessStatusCode)
			{
				string result = response.Content.ReadAsStringAsync().Result;
				var data = JsonConvert.DeserializeObject<Student>(result);
				if(data != null)
				{
					std = data;
				}
			}
            return View(std);
        }
		[HttpPost]
		public IActionResult Edit(Student std)
		{
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url+std.id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Update_Message"] = "Student Updated Successfully...";
                return RedirectToAction("Index");
            }
            return View();
		}
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmd(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Delete_Message"] = "Student Deleted Successfully...";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
