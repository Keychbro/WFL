using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.DataSave
{
    public interface IDataWebRequest
    {
        public string ServerUUID { get; set; }
        public string PlayerUUID { get; set; }
    }
}