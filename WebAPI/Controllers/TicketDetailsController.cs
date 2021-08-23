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
    public class TicketDetailsController : ApiController
    {
        private TCT_DbContext db = new TCT_DbContext();

        // GET: api/TicketDetails
        public IQueryable<TicketDetail> GetTicketDetails()
        {
            return db.TicketDetails;
        }

        // GET: api/TicketDetails/5
        [ResponseType(typeof(TicketDetail))]
        public async Task<IHttpActionResult> GetTicketDetail(int id)
        {
            TicketDetail ticketDetail = await db.TicketDetails.FindAsync(id);
            if (ticketDetail == null)
            {
                return NotFound();
            }

            return Ok(ticketDetail);
        }

        // PUT: api/TicketDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTicketDetail(int id, TicketDetail ticketDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticketDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(ticketDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketDetailExists(id))
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

        // POST: api/TicketDetails
        [ResponseType(typeof(TicketDetail))]
        public async Task<IHttpActionResult> PostTicketDetail(TicketDetail ticketDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ticketDetail.Id = db.TicketDetails.Count() + 1;
            ticketDetail.Created = System.DateTime.UtcNow;
            db.TicketDetails.Add(ticketDetail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ticketDetail.Id }, ticketDetail);
        }

        // DELETE: api/TicketDetails/5
        [ResponseType(typeof(TicketDetail))]
        public async Task<IHttpActionResult> DeleteTicketDetail(int id)
        {
            TicketDetail ticketDetail = await db.TicketDetails.FindAsync(id);
            if (ticketDetail == null)
            {
                return NotFound();
            }

            db.TicketDetails.Remove(ticketDetail);
            await db.SaveChangesAsync();

            return Ok(ticketDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketDetailExists(int id)
        {
            return db.TicketDetails.Count(e => e.Id == id) > 0;
        }
    }
}