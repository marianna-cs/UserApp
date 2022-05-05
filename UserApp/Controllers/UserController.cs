using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserApp.Data;
using UserApp.DTOs;
using UserApp.Repo;
using UserApp.Service;

namespace UserApp.Controllers
{
    public class UserController : Controller
    {
        UserService _userService;

        public UserController(UserAppContext context)
        {           
            _userService = new UserService(context);
        }

        public IActionResult Index()
        {      
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,DateOfBirth,Married,Phone,Salary")] User @user)
        {
            if (id != @user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _userService.Update(user);
                return RedirectToAction(nameof(GetUsers));
            }
            return View(@user);
            
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _userService.GetById(id);
            _userService.Delete(course);
            return RedirectToAction(nameof(GetUsers));
        }

        [HttpPost]
        public IActionResult UploadUsers()
        {
            UpdateUsersFromFiles();
            return RedirectToAction("GetUsers");
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {            
            return View();
        }

        [HttpPost("loadData")]
        public IActionResult LoadData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var userData = (from tempcustomer in GetListUsers() select tempcustomer);
            /*if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
            }*/
            if (!string.IsNullOrEmpty(searchValue))
            {
                userData = userData.Where(m => m.Name.Contains(searchValue));

            }
            recordsTotal = userData.Count();
            var data = userData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }

        // GET: Users/users
        //[HttpGet("users")]
        private List<UserDTO> GetListUsers()
        {
            var users = _userService.GetAllList();
            var usersOutput = new List<UserDTO>();
            foreach (var user in users)
            {
                UserDTO userDTO = new UserDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    DateOfBirth=user.DateOfBirth.ToShortDateString(),
                    Married=user.Married,
                    Phone=user.Phone,
                    Salary=user.Salary
                };
                usersOutput.Add(userDTO);
            }
            return usersOutput;
        }

        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var path = Directory.GetCurrentDirectory();
                path = path + @"\Files\" + uploadedFile.FileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    uploadedFile.CopyToAsync(fileStream);
                }

            }
            return RedirectToAction("Index");


        }
        private void UpdateUsersFromFiles()
        {
            var files = GetFilesFromDirectory();
            foreach(var file in files)
            {
                var fileHandler = new FileHandler.FileHandler(file);
                var contentsFile = fileHandler.ReadFile();
                foreach(var content in contentsFile)
                {
                    var user = new User()
                    {
                        Name = content.Name,
                        DateOfBirth = content.DateOfBirth,
                        Married = content.Married,
                        Phone = content.Phone,
                        Salary = content.Salary
                    };
                    _userService.Create(user);
                    
                }
                System.IO.File.Delete(file);

            }
        }

        private string[] GetFilesFromDirectory()
        {
            var path = Directory.GetCurrentDirectory() + @"\Files\";
            return Directory.GetFiles(path);
        }

    }
}
