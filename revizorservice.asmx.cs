using System;
using System.IO;
using System.Web.Services;

namespace revizorservice
{
    [WebService(Namespace = "http://novat-1c.ru/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class revizorservice : System.Web.Services.WebService
    {
        private byte[] _data = null;

        [WebMethod]
        public string hello()
        {
            return "Hi";
        }
        [WebMethod]
        public string getDepts()
        {
            string result = "[\"Радиоусилительный цех\", \"ЦОЭСО\", \"МДЦ\", \"МРЦ\", \"ГПЦ\", \"Костюмерный цех\"]";
            return result;
        }
        [WebMethod]
        public string getItems(int deptNum)
        {
            switch (deptNum)
            {
                case 0: return fromFile();
                default: return "N/A";
            }
        }
        [WebMethod]
        public string setItems(int deptNum, string data)
        {
            string path;
            switch (deptNum)
            {
                case 0:
                    path = Server.MapPath("~/App_Data/data.json");
                    break;
                default: return "nothing done for dept " + deptNum;
            }
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(data);
            }
            return "OK";
        }
        private string fromFile()
        {
            string path = Server.MapPath("~/App_Data/data.json");
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    return sr.ReadToEnd();
                }
            }
            else return "N/A";
        }
        [WebMethod]
        public string uploadBinary(int size, string name, string data)
        {
            byte[] buf = Convert.FromBase64String(data);
            if (size != 0)
            {
                _data = new byte[size];
            }
            return "OK";
        }
    }
}
