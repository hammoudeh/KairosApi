using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiorsApi
{
   public class KairosClient
    {

        public string app_id { set; get; }
        public string app_key { set; get; }
        public string KAIRO_BASE_URL = "https://api.kairos.com/";

        ConnectionManager conn; 

        private KairosClient() {
            
        }

        public KairosClient(string app_id, string app_key)
        {

            this.app_key = app_key;
            this.app_id = app_id; 

            callConnectionMan();
        }

        void callConnectionMan() {
            conn = new ConnectionManager();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("app_id", app_id);
            headers.Add("app_key", app_key);
            conn.Headers = headers;
        }
 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">Publicly accessible URL or Base64 encoded photo.</param>
        /// <param name="gallery_name"> Defined by you. Is used to identify the gallery.</param>
        /// <param name="subject_id">Defined by you. Is used as an identifier for the face.</param>
        /// <param name="minHeadScale"> Defined by you. Is used to set the ratio of the smallest face we should look for in the photo. Accepts a value between .015 (1:64 scale) and .5 (1:2 scale). By default it is set at .125 (1:8 scale) if not specified. </param>
        /// <param name="Selector">selector Is used to adjust the amount of information returned by the face detector. If not specified the default of EYES is used.
        ///Selector Types:
        ///FACE returns the face location
        ///EYES returns the eye locations, face location, and gender * Default option
        ///FULL returns all face features including gender
        ///SETPOSE returns all face features </param>
        /// <param name="multiple_faces">multiple_faces If set to false stops the API from enrolling every face found in your photo under the same subject_id and only consider the first face found.</param>
        /// <returns></returns>
        public string enroll( string image, string gallery_name, string subject_id, 
            string minHeadScale, 
            string Selector, string multiple_faces) {

              minHeadScale = (minHeadScale == null) ? ".125" : minHeadScale;
              Selector = (Selector == null) ? "EYES" : Selector;
              multiple_faces = (multiple_faces == null) ? "false" : multiple_faces;


            string body = string.Format("{{"+
                "\"image\":\"{0}\","+
                "\"gallery_name\":\"{1}\"," +
                "\"subject_id\":\"{2}\"," +
                "\"minHeadScale\":\"{3}\"," +
                "\"selector\":\"{4}\"," +
                "\"multiple_faces\":\"{5}\"" +
                " }}", image, gallery_name, subject_id, minHeadScale, Selector, multiple_faces);

            return conn.doHttpPost(KAIRO_BASE_URL + "enroll", body);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">Publicly accessible URL or Base64 encoded photo.</param>
        /// <param name="gallery_name"> Defined by you. Is used to identify the gallery.</param>
        /// <param name="threshold"> Provided by you. Is used to determine a valid facial match.</param>
        /// <param name="minHeadScale"> Defined by you. Is used to set the ratio of the smallest face we should look for in the photo. Accepts a value between .015 (1:64 scale) and .5 (1:2 scale). By default it is set at .125 (1:8 scale) if not specified. </param>
        /// <param name="Selector">selector Is used to adjust the amount of information returned by the face detector. If not specified the default of EYES is used.
        ///Selector Types:
        ///FACE returns the face location
        ///EYES returns the eye locations, face location, and gender * Default option
        ///FULL returns all face features including gender
        ///SETPOSE returns all face features </param>
        /// <param name="max_num_results"> Provided by you. Is the maximum number of potential matches that are returned. By default it is set to 10 if not specified.</param>
        /// <returns></returns>
        public string recognize(string image, string gallery_name, string threshold,
            string minHeadScale,
            string Selector, string max_num_results)
        {

            minHeadScale = (minHeadScale == null) ? ".125" : minHeadScale;
            Selector = (Selector == null) ? "EYES" : Selector;
            max_num_results = (max_num_results == null) ? "10" : max_num_results;
            threshold = (threshold == null) ? "0.20" : threshold;


            string body = string.Format("{{" +
                "\"image\":\"{0}\"," +
                "\"gallery_name\":\"{1}\"," +
                "\"threshold\":\"{2}\"," +
                "\"minHeadScale\":\"{3}\"," +
                "\"selector\":\"{4}\"," +
                "\"max_num_results\":\"{5}\"" +
                " }}", image, gallery_name, threshold, minHeadScale, Selector, max_num_results);

            return conn.doHttpPost(KAIRO_BASE_URL + "recognize", body);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">Publicly accessible URL or Base64 encoded photo.</param>
        /// <param name="minHeadScale"> Defined by you. Is used to set the ratio of the smallest face we should look for in the photo. Accepts a value between .015 (1:64 scale) and .5 (1:2 scale). By default it is set at .125 (1:8 scale) if not specified. </param>
        /// <param name="Selector">selector Is used to adjust the amount of information returned by the face detector. If not specified the default of EYES is used.
        ///Selector Types:
        ///FACE returns the face location
        ///EYES returns the eye locations, face location, and gender * Default option
        ///FULL returns all face features including gender
        ///SETPOSE returns all face features </param>
        /// <returns></returns>
        public string detect(string image,  string minHeadScale, string Selector)
        {

            minHeadScale = (minHeadScale == null) ? ".125" : minHeadScale;
            Selector = (Selector == null) ? "EYES" : Selector;
           


            string body = string.Format("{{" +
                "\"image\":\"{0}\"," +
                "\"minHeadScale\":\"{1}\"," +
                "\"selector\":\"{2}\"" +
                " }}", image, minHeadScale, Selector);

            return conn.doHttpPost(KAIRO_BASE_URL + "detect", body);
        }



        /// <summary>
        /// Lists out all the faces you have enrolled in a gallery.
       ///  You just need to pass in the gallery_name and will receive back the list of subjects that you have enrolled within that gallery.
        /// </summary>
        /// <returns></returns>
        public string listAllGalleries() {
            return conn.doHttpPost(KAIRO_BASE_URL + "gallery/list_all", "");
        }



        /// <summary>
        /// Removes a gallery and all of its subjects. 
        ///  Pass your gallery_name and it will be removed along with all of its enrolled subjects.
        /// </summary>
        /// <param name="gallery_name"> Is the name of the gallery you wish to remove. </param>
        /// <returns></returns>
        public string viewGallery (string gallery_name )
        { 
            string body = string.Format("{{" +
                "\"gallery_name \":\"{0}\" " +
                " }}", gallery_name );

            return conn.doHttpPost(KAIRO_BASE_URL + "gallery/view", body);
        }


        /// <summary>
        /// Removes a gallery and all of its subjects. 
        ///   Pass your gallery_name and it will be removed along with all of its enrolled subjects.
        /// </summary>
        /// <param name="gallery_name"> Is the name of the gallery you wish to remove. </param>
        /// <returns></returns>
        public string removeGallery(string gallery_name)
        {
            string body = string.Format("{{" +
                "\"gallery_name \":\"{0}\" " +
                " }}", gallery_name);

            return conn.doHttpPost(KAIRO_BASE_URL + "gallery/remove", body);
        }


        /// <summary>
        /// Removes a face you have enrolled within a gallery.
        ///  Pass in a gallery_name and a subject_id and we will remove that subject from the gallery.Once the last subject is removed the gallery is removed automatically.
        /// </summary>
        /// <param name="gallery_name"> Is the name of the gallery you wish to remove. </param>
        /// <param name="subject_id">Defined by you. Is used as an identifier for the face.</param>
        /// <returns></returns>
        public string removeSubjectFromGallery(string gallery_name , string subject_id)
        {
            string body = string.Format("{{" +
                "\"gallery_name \":\"{0}\", " +
                 "\"subject_id \":\"{1}\"  " +
                " }}", gallery_name, subject_id);

            return conn.doHttpPost(KAIRO_BASE_URL + "gallery/remove_subject", body);
        }


    }
}
