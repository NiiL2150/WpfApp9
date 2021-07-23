using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp9.Model;

namespace WpfApp9.Service
{
    public interface IFileService
    {
        List<Friend> Open(string fileName);
        void Save(string fileName, List<Friend> friends);
    }
}
