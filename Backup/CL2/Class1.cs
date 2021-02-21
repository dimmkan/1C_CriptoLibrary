using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime;


namespace CL2
{
    public interface ASignatures
    {
        int s();
        string FName();
    }

    [Guid("AB634001-F13D-11d0-A459-004095E1DAEA")]// стандартный GUID для IInitDone ссылка http://soaron.fromru.com/vkhints.htm
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitDone
    {
        /// <summary>
        /// Инициализация компонента
        /// </summary>
        /// <param name="connection">reference to IDispatch</param>
        void Init(
          [MarshalAs(UnmanagedType.IDispatch)]
    object connection);

        /// <summary>
        /// Вызывается перед уничтожением компонента
        /// </summary>
        void Done();

        /// <summary>
        /// Возвращается инициализационная информация
        /// </summary>
        /// <param name="info">Component information</param>
        void GetInfo(
          [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)]
    ref object[] info);
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class ArsClass : ASignatures, IInitDone
    {

        //private IAsyncEvent asyncEvent = null;
        //private IStatusLine statusLine = null;

        /// <summary>
        /// Инициализация компонента
        /// </summary>
        /// <param name="connection">reference to IDispatch</param>
        public void Init(
          [MarshalAs(UnmanagedType.IDispatch)]
  object connection)
        {
            //asyncEvent = (IAsyncEvent)connection;
            //statusLine = (IStatusLine)connection;
        }

        /// <summary>
        /// Возвращается информация о компоненте
        /// </summary>
        /// <param name="info">Component information</param>
        public void GetInfo(
          [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)]
  ref object[] info)
        {
            info[0] = 2000;
        }

        public void Done()
        {
        }

        public string FName()
        {
            return "Arsen";
        }
        public int s()
        {
            int a = 1;
            int b = 2;
            int c = a + b;
            return c;
        }
    }
}
