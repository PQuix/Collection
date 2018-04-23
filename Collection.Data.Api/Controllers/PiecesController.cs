using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Collection.DataAccess;
using Collection.Model;

namespace Collection.Data.Api.Controllers
{
    public class PiecesController : ApiController
    {
        private CollectionContext db = new CollectionContext();

        // GET: api/Pieces
        public IQueryable<Piece> GetPieces()
        {
            return db.Pieces;
        }

        // GET: api/Pieces/5
        [ResponseType(typeof(Piece))]
        public async Task<IHttpActionResult> GetPiece(int id)
        {
            Piece piece = await db.Pieces.FindAsync(id);
            if (piece == null)
            {
                return NotFound();
            }

            return Ok(piece);
        }

        // PUT: api/Pieces/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPiece(int id, Piece piece)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != piece.PieceId)
            {
                return BadRequest();
            }

            db.Entry(piece).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PieceExists(id))
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

        // POST: api/Pieces
        [ResponseType(typeof(Piece))]
        public async Task<IHttpActionResult> PostPiece(Piece piece)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pieces.Add(piece);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = piece.PieceId }, piece);
        }

        // DELETE: api/Pieces/5
        [ResponseType(typeof(Piece))]
        public async Task<IHttpActionResult> DeletePiece(int id)
        {
            Piece piece = await db.Pieces.FindAsync(id);
            if (piece == null)
            {
                return NotFound();
            }

            db.Pieces.Remove(piece);
            await db.SaveChangesAsync();

            return Ok(piece);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PieceExists(int id)
        {
            return db.Pieces.Count(e => e.PieceId == id) > 0;
        }
    }
}