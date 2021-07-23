using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using WpfApp9.Model;

namespace WpfApp9.Service
{
    class JsonService : IFileService
    {
        public List<Friend> Open(string fileName)
        {
            List<Friend> friends = new List<Friend>();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Friend>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                friends = jsonFormatter.ReadObject(fs) as List<Friend>;
            }
            return friends;
        }

        public void Save(string fileName, List<Friend> friends)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Friend>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, friends);
            }
        }
    }
}
