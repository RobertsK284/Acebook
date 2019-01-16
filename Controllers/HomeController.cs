﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{

    [Route("")]
    [Route("[controller]")]
    //[HomeController]
    public class HomeController : Controller
    {
        readonly TodoContext _context;

        public HomeController(TodoContext context)
        {
             _context = context;
        }
        // GET api/values
        [HttpGet("{username}")]
        public ActionResult<IEnumerable<string>> Index(string username)
        {
            var posts = _context.Posts.ToList();
            long userid = _context.Users.Where(x => x.Username == username).Select(x => x.Id).First();
            Console.WriteLine($"userid is {userid}");
            IEnumerable<Post> UPosts = posts.Where(post => post.UserId == userid);

            Console.WriteLine("In the home controller");
            Profile profile = new Profile(username);

            foreach (var post in UPosts)
            {
                profile.AddPost(new Post { Id = post.Id, UserId = post.UserId, Content = post.Content, CreatedOn = post.CreatedOn});

                Console.WriteLine(post.Content);

            }
            Console.WriteLine(_context.GetType());
            Console.WriteLine("good morning");
            @ViewData["posts"] = posts;

            return View(profile);

        }
        [Route("register")]
        [HttpGet]
        public ActionResult Register()
        {
          Console.WriteLine("I made it into the register route!!!!!!!");
          return View();
        }
        [HttpPost]
        public ActionResult Add(string username, string email, string password)
        {
            Console.WriteLine($"Username is {username}");
            Console.WriteLine($"Email is {email}");
            Console.WriteLine($"Password is {password}");
            Console.WriteLine("The form is triggering the post");
            _context.Users.Add(new User { Username = username, Email = email, Password = password });
            _context.SaveChanges();
            return Redirect($"/{username}");
            // Console.WriteLine("still inside the post controller");
        }
       // [HttpPost]
       // public ActionResult Add(string name)
       // {
       //     Console.WriteLine(name);
       //     Console.WriteLine("The form is triggering the post");
       //     _context.TodoItems.Add(new TodoItem { Name = name, IsComplete = false });
       //     _context.SaveChanges();
       //     return Redirect("/");
       //     // Console.WriteLine("still inside the post controller");
       // }

//        //[HttpDelete("{id}")]
//        //public ActionResult Delete(long id)
//        //{
//        //  Console.WriteLine("Delete has been called");
//        //  _context.TodoItems.Delete(id);
//        //  _context.SaveChanges();
//        //  return Redirect("/");
//        //}





//        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult<string> Get(int id)
//        {
//            Console.WriteLine(id);
//            return id.ToString();
//        }

//        // POST api/values
//        [HttpPost]
//        public void Post()

//        //public void Post([FromBody] string value)
//        {
//            Console.WriteLine("Hello");
//            //return new string[] { "Test string", "value2" };
//        }

//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {

//        }

//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {

//        }
    }
}
