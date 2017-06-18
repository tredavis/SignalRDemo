using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace SignalRDemo.Models
{
    public class SpotifyModel
    {
        #region Members

        static AutorizationCodeAuth auth;
        private List<SavedTrack> _usersTopTracks = new List<SavedTrack>();
        private HttpClient _httpClient;
        private static string _tokenType;
        private static string _accessToken;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the token type
        /// </summary>
        public string TokenType
        {
            get { return _tokenType; }
            set { _tokenType = value; }
        }

        /// <summary>
        /// Gets or Sets the spotify access token
        /// </summary>
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        #endregion

        #region Events 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        private static void auth_OnResponseReceivedEvent(AutorizationCodeAuthResponse response)
        {
            //NEVER DO THIS! You would need to provide the ClientSecret.
            //You would need to do it e.g via a PHP-Script.
            Token token = auth.ExchangeAuthCode(response.Code, "714ba0785d9b41d3828a7ad30c782d52");

            //Stop the HTTP Server, done.
            auth.StopHttpServer();

            _tokenType = token.TokenType;
            _accessToken = token.AccessToken;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        internal SpotifyModel()
        {
            //Do authenication
            DoAuth();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoAuth()
        {
            //Create the auth object
            auth = new AutorizationCodeAuth()
            {
                //Your client Id
                ClientId = "ce833ef1272046489c5cb86af277d9c0",
                //Set this to localhost if you want to use the built-in HTTP Server
                RedirectUri = "http://localhost",
                //How many permissions we need?
                Scope = Scope.UserLibraryRead,
            };
            //This will be called, if the user cancled/accept the auth-request
            auth.OnResponseReceivedEvent += auth_OnResponseReceivedEvent;
            //a local HTTP Server will be started (Needed for the response)
            auth.StartHttpServer();
            //This will open the spotify auth-page. The user can decline/accept the request
            auth.DoAuth();

            Thread.Sleep(5000);
            //auth.StopHttpServer();
            Console.WriteLine("Too long, didnt respond, exiting now...");
        }

        /// <summary>
        /// Returns the signed in users saved tracks
        /// </summary>
        /// <param name="nextUrlUsed">Are we using the next url provided by spotify</param>
        /// <returns>the list of the total tracks</returns>
        public List<SavedTrack> TopTracks(string nextUrl = null)
        {
            //lets create our client
            _httpClient = new HttpClient();

            var url = String.IsNullOrWhiteSpace(nextUrl) ? "https://api.spotify.com/v1/me/tracks?&limit=50&access_token=" + AccessToken : nextUrl + "&access_token=" + AccessToken;
            
            var response = _httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                //let's deseriale the data
                var data = JsonConvert.DeserializeObject<Paging<SavedTrack>>(response.Content.ReadAsStringAsync().Result);

                //let's add the data to the return list.
                _usersTopTracks.AddRange(data.Items);

                if (data.HasNextPage())
                {
                    TopTracks(data.Next);
                }
                else
                {
                    
                }
            }

            return _usersTopTracks;
        }
    }
}