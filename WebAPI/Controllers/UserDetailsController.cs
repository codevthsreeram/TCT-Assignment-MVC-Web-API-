using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserDetailsController : ApiController
    {
        private TCT_DbContext db = new TCT_DbContext();

        // GET: api/UserDetails
        public IQueryable<UserDetail> GetUserDetails()
        {
            return db.UserDetails;
        }

        // GET: api/UserDetails/5
        [ResponseType(typeof(UserDetail))]
        public async Task<IHttpActionResult> GetUserDetail(int id)
        {
            UserDetail userDetail = await db.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        // PUT: api/UserDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserDetail(int id, UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(userDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserDetails
        [ResponseType(typeof(UserDetail))]
        public async Task<IHttpActionResult> PostUserDetail(UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userDetail.Id = db.TicketDetails.Count() + 1;
            userDetail.Created = System.DateTime.UtcNow;
            db.UserDetails.Add(userDetail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userDetail.Id }, userDetail);
        }

        // DELETE: api/UserDetails/5
        [ResponseType(typeof(UserDetail))]
        public async Task<IHttpActionResult> DeleteUserDetail(int id)
        {
            UserDetail userDetail = await db.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            db.UserDetails.Remove(userDetail);
            await db.SaveChangesAsync();

            return Ok(userDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserDetailExists(int id)
        {
            return db.UserDetails.Count(e => e.Id == id) > 0;
        }
    }
}