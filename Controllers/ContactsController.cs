using ContactList.Data;
using ContactList.Models;
using ContactList.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Controllers{
    public class ContactsController: Controller
    {
        private readonly MyDbContext db;

        public ContactsController(MyDbContext db)
        {
            this.db = db;
        } 
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddContactViewModel cont){
            var contact=new Contact(){
                Id=Guid.NewGuid(),
                Name=cont.Name,
                Email=cont.Email,
                PriMobileNo=cont.PriMobileNo,
                SecMobileNo=cont.SecMobileNo,
                DateOfBirth=cont.DateOfBirth
            };
            await db.Contacts.AddAsync(contact);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index(){
            var contacts=await db.Contacts.ToListAsync();
            return View(contacts);            
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id){
            var contact=await db.Contacts.FirstOrDefaultAsync(x=>x.Id==id);
            if(contact!=null){
                var viewmodel=new UpdateContactViewModel(){
                    Id=contact.Id,
                    Name=contact.Name,
                    Email=contact.Email,
                    PriMobileNo=contact.PriMobileNo,
                    SecMobileNo=contact.SecMobileNo,
                    DateOfBirth=contact.DateOfBirth
                };
                return await Task.Run(()=>View("View",viewmodel));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateContactViewModel model){
            var contact=await db.Contacts.FindAsync(model.Id);
            if(contact!=null){
                contact.Name=model.Name;
                contact.Email=model.Email;
                contact.PriMobileNo=model.PriMobileNo;
                contact.SecMobileNo=model.SecMobileNo;
                contact.DateOfBirth=model.DateOfBirth;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateContactViewModel model){
            var contact=await db.Contacts.FindAsync(model.Id);
            if(contact!=null){
                db.Contacts.Remove(contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}