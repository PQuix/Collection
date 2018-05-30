using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Collection.Model;
using Newtonsoft.Json;

namespace Collection.DataSource
{
    public class PieceDS : IDisposable
    {
        /// <summary>
        /// The client
        /// </summary>
        HttpClient client;

        /// <summary>
        /// A list of pieces
        /// </summary>
        List<Piece> pieces;

        /// <summary>
        /// Initializes a new instance of the <see cref="PieceDS"/> class.
        /// </summary>
        public PieceDS()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:57994/api/pieces");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            pieces = new List<Piece>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
            this.Dispose();
        }

        /// <summary>
        /// Creates the piece.
        /// </summary>
        /// <param name="piece">The piece.</param>
        /// <returns></returns>
        public async Task<Boolean> CreatePiece(Piece piece)
        {
            StringContent strObject = new StringContent(JsonConvert.SerializeObject(piece), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("Pieces", strObject);

            if(response.IsSuccessStatusCode)
            {
                pieces.Add(piece);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads the piece asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Piece> ReadPieceAsync(string id)
        {
            String reply = await client.GetStringAsync(String.Format("Pieces/{0}", id));
            Piece pieceObj = JsonConvert.DeserializeObject<Piece>(reply);

            return pieceObj;
        }

        /// <summary>
        /// Updates the piece asynchronous.
        /// </summary>
        /// <param name="piece">The piece.</param>
        /// <returns></returns>
        public async Task<Boolean> UpdatePieceAsync(Piece piece)
        {
            var buildString = JsonConvert.SerializeObject(piece, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            });

            StringContent strObj = new StringContent(buildString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(String.Format("Pieces/{0}", piece.PieceId), strObj);

            if(response.IsSuccessStatusCode)
            {
                pieces.Add(piece);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes the piece asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Boolean> DeletePieceAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(String.Format("Pieces/{0}", id));

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
