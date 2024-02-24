using System;

namespace Kamen.DataSave
{
    public interface IDataException
    {
        #region Methods 

        public void ShowException(Exception e);

        #endregion
    }
}